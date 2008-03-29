//------------------------------------------------------------------------------
// CoroWare
//
// Sample CoroBot simulation environment
//------------------------------------------------------------------------------

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

//MSRS simulation namespaces
using Microsoft.Robotics.Simulation;
using Microsoft.Robotics.Simulation.Engine;
using engineproxy = Microsoft.Robotics.Simulation.Engine.Proxy;
using Microsoft.Robotics.Simulation.Physics;
using Microsoft.Robotics.PhysicalModel;
using physicsproxy = Microsoft.Robotics.PhysicalModel.Proxy;

using cbsim = CoroWare.Robotics.Simulation.Services.CoroBotSim.Proxy;
using W3C.Soap;

namespace Robotics.SampleSimulation
{
    
    /// <summary>
    /// Implementation class for SampleSimulation
    /// </summary>
    [DisplayName("SampleSimulation")]
    [Description("The SampleSimulation Service")]
    [Contract(Contract.Identifier)]
    public class SampleSimulationService : DsspServiceBase
    {
        /// <summary>
        /// _state
        /// </summary>
        private SampleSimulationState _state = new SampleSimulationState();

        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/samplesimulation", AllowMultipleInstances=false)]
        private SampleSimulationOperations _mainPort = new SampleSimulationOperations();

        [Partner("CoroBotSim",
            Contract = cbsim.Contract.Identifier,
            CreationPolicy = PartnerCreationPolicy.CreateAlways,
            Optional = false)]
        private cbsim.CoroBotSimOperations _coroBotPort = new cbsim.CoroBotSimOperations();

        // partner attribute will cause simulation engine service to start
        [Partner("Engine", Contract = engineproxy.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        private engineproxy.SimulationEnginePort _engineServicePort = new engineproxy.SimulationEnginePort();

        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public SampleSimulationService(DsspServiceCreationPort creationPort) : base(creationPort)
        {
        }

        int Nboxes = 0;

        //color definitions
        static Vector4 RedColor = new Vector4(0.8f, 0.25f, 0.25f, 1.0f);
        static Vector4 BlueColor = new Vector4(0.25f, 0.8f, 0.25f, 1.0f);
        static Vector4 GreenColor = new Vector4(0.25f, 0.25f, 0.8f, 1.0f);
        static Vector4 GreyColor = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);
        static Vector4 YellowColor = new Vector4(0.8f, 0.8f, 0.25f, 1.0f);
        static Vector4 CyanColor = new Vector4(0.25f, 0.8f, 0.8f, 1.0f);
        static Vector4 MagentaColor = new Vector4(0.8f, 0.25f, 0.8f, 1.0f);
        static Vector4 WhiteColor = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);

        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
			base.Start();
			
            // Orient camera view point
            SetupCamera();

            // Add objects in our simulated world
            PopulateWorld();

            //add Corobot
            //the robot will be facing the negative z axis
            cbsim.InsertRobotRequest insert = new cbsim.InsertRobotRequest(
                new physicsproxy.Vector3(0f, 0f, 0f), //position
                "My CoroBot");                        //name

            Activate(Arbiter.Choice(_coroBotPort.InsertRobot(insert),
                delegate(DefaultUpdateResponseType success)
                {
                    LogInfo("My CoroBot insterted successfully");
                },
                delegate(Fault f)
                {
                    LogError(f);
                }
            ));

        }

        #region Standard Operation Handlers

        /// <summary>
        /// Get Handler
        /// </summary>
        [ServiceHandler(ServiceHandlerBehavior.Concurrent)]
        public virtual IEnumerator<ITask> GetHandler(Get get)
        {
            get.ResponsePort.Post(_state);
            yield break;
        } 

        #endregion

        private void SetupCamera()
        {
            // Set up initial view
            CameraView view = new CameraView();
            view.EyePosition = new Vector3(-1.17f, 1.05f, 1.07f);
            view.LookAtPoint = new Vector3(-0.41f, 0.7f, 0.32f);
            SimulationEngine.GlobalInstancePort.Update(view);
        }

        private void PopulateWorld()
        {
            AddSky();
            AddGround();
            
            AddTable(new Vector3(1, 0.5f, -2));

            //small boxes to pick up
            AddBox(new Vector3(-0.00f, 0.04f, -1f), new Vector3(0.02f, 0.08f, 0.08f), 0.05f, RedColor);
            AddBox(new Vector3(-0.25f, 0.04f, -1f), new Vector3(0.02f, 0.08f, 0.08f), 0.05f, BlueColor);
            AddBox(new Vector3(-0.50f, 0.04f, -1f), new Vector3(0.02f, 0.08f, 0.08f), 0.05f, GreenColor);

            //medium boxes
            AddBox(new Vector3(2f, 0.1f, 0.0f), new Vector3(0.2f, 0.2f, 0.2f), 1f, GreyColor);
            AddBox(new Vector3(2f, 0.1f, 0.5f), new Vector3(0.2f, 0.2f, 0.2f), 1f, YellowColor);
            AddBox(new Vector3(2f, 0.1f, 1.0f), new Vector3(0.2f, 0.2f, 0.2f), 1f, CyanColor);
        }

        void AddSky()
        {
            // Add a sky using a static texture. We will use the sky texture
            // to do per pixel lighting on each simulation visual entity
            SkyEntity sky = new SkyEntity("sky.dds", "sky_diff.dds");
            SimulationEngine.GlobalInstancePort.Insert(sky);

            // Add a directional light to simulate the sun.
            LightSourceEntity sun = new LightSourceEntity();
            sun.State.Name = "Sun";
            sun.Type = LightSourceEntityType.Directional;
            sun.Color = new Vector4(0.8f, 0.8f, 0.8f, 1);
            sun.Direction = new Vector3(-1.0f, -1.0f, 0.5f);
            SimulationEngine.GlobalInstancePort.Insert(sun);
        }

        void AddGround()
        {
            // create a large horizontal plane, at zero elevation.
            HeightFieldEntity ground = new HeightFieldEntity(
                "simple ground", // name
                "03RamieSc.dds", // texture image
                new MaterialProperties("ground",
                    0.2f, // restitution
                    0.5f, // dynamic friction
                    0.5f) // static friction
                );
            SimulationEngine.GlobalInstancePort.Insert(ground);
        }

        void AddTable(Vector3 position)
        {
            // create an instance of our custom entity
            TableEntity entity = new TableEntity(position);

            // Name the entity
            entity.State.Name = "table:" + Guid.NewGuid().ToString();

            // Insert entity in simulation. 
            SimulationEngine.GlobalInstancePort.Insert(entity);
        }

        /// <summary>
        /// Add a simple single shape box entity into the simulation environment
        /// </summary>
        /// <param name="position">the initial position of the box in meters</param>
        /// <param name="size">the dimensions of the box in meters</param>
        /// <param name="mass">the mass of the box in kg</param>
        /// <param name="color">the color of the box ()</param>
        void AddBox(Vector3 position, Vector3 size, float mass, Vector4 color)
        {
            // create simple movable entity, with a single shape
            SingleShapeEntity box = new SingleShapeEntity(
                new BoxShape(
                    new BoxShapeProperties(
                    mass,       // mass in kilograms.
                    new Pose(), // relative pose
                    size)),     // dimensions
                position);

            Nboxes++;
            // Name the entity. All entities must have unique names
            box.State.Name = "box " + Nboxes;

            //optional properties
            box.BoxShape.State.DiffuseColor = color;
            box.BoxShape.State.Material = new MaterialProperties("high friction", 0.1f, 0.9f, 0.9f);

            // Insert entity in simulation.  
            SimulationEngine.GlobalInstancePort.Insert(box);
        }



    }

    /// <summary>
    /// An entity for approximating a table. 
    /// </summary>
    [DataContract]
    public class TableEntity : MultiShapeEntity
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TableEntity() { }

        /// <summary>
        /// Custom constructor, programmatically builds physics primitive shapes to describe
        /// a particular table. 
        /// </summary>
        /// <param name="position"></param>
        public TableEntity(Vector3 position)
        {
            State.Pose.Position = position;
            State.Assets.Mesh = "table_01.obj";
            float tableHeight = 0.65f;
            float tableWidth = 1.05f;
            float tableDepth = 0.7f;
            float tableThinkness = 0.03f;
            float legThickness = 0.03f;
            float legOffset = 0.05f;

            // add a shape for the table surface
            BoxShape tableTop = new BoxShape(
                new BoxShapeProperties(30,
                new Pose(new Vector3(0, tableHeight, 0)),
                new Vector3(tableWidth, tableThinkness, tableDepth))
            );

            // add a shape for the left leg
            BoxShape tableLeftLeg = new BoxShape(
                new BoxShapeProperties(10, // mass in kg
                new Pose(
                    new Vector3(-tableWidth / 2 + legOffset, tableHeight / 2, 0)),
                new Vector3(legThickness, tableHeight + tableThinkness, tableDepth))
            );

            BoxShape tableRightLeg = new BoxShape(
                new BoxShapeProperties(10, // mass in kg
                new Pose(
                    new Vector3(tableWidth / 2 - legOffset, tableHeight / 2, 0)),
                new Vector3(legThickness, tableHeight + tableThinkness, tableDepth))
            );

            BoxShapes = new List<BoxShape>();
            BoxShapes.Add(tableTop);
            BoxShapes.Add(tableLeftLeg);
            BoxShapes.Add(tableRightLeg);
        }

        public override void Update(FrameUpdate update)
        {
            base.Update(update);
        }
    }

}
