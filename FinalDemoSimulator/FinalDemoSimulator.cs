//------------------------------------------------------------------------------
//  CS3630 Final Demo Simulation
//------------------------------------------------------------------------------
#define DISPLAY_ALL

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using Microsoft.Robotics.Simulation;
using Microsoft.Robotics.Simulation.Engine;
using engineproxy = Microsoft.Robotics.Simulation.Engine.Proxy;
using Microsoft.Robotics.Simulation.Physics;
using Microsoft.Robotics.PhysicalModel;
using physicsproxy = Microsoft.Robotics.PhysicalModel.Proxy;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

using cbsim = CoroWare.Robotics.Simulation.Services.CoroBotSim.Proxy;
using W3C.Soap;

/*****************************
 *   +y                         -z
 *   ^                           ^
 *   |                           |
 *   |                  ==>      |
 *   |                           |
 *   |                           |
 *   ------------> +x            -------------> +x
 *   
 *   Map coordinate           Simulation coordinate     
 * 
Mark     X       Y     CLR
1	    0.00	0.00	R
2	    2.64    2.00	Y
3       3.42    0.00    G
4       5.68    0.23    R
5       5.42    4.25    G
6       7.49    4.25    R
7      10.54    4.25    Y
8      13.40    4.25    R
9      16.49    4.25    G
10      6.42    2.00    G
11      9.91    2.00    G
12     13.02    2.00    Y
13     15.96    2.00    R
14     18.97    2.00    G
15     22.40    4.30    R
16     20.20    4.40    Y
17     22.40    7.10    Y
18     20.20    6.72    G
19     20.20    9.67    R
20     21.80   10.20    R
21     20.20   12.47    Y
22     22.40   14.65    G
23     20.20   16.63    R
24     22.40   17.40    Y
25     20.20   19.45    G
26     22.40   21.66    Y
27     20.20   21.56    R
**********************************/

namespace Robotics.FinalDemoSimulator
{

    /// <summary>
    /// Implementation class for FinalDemoSimulator
    /// </summary>
    [DisplayName("FinalDemoSimulator")]
    [Description("The FinalDemoSimulator Service")]
    [Contract(Contract.Identifier)]
    public class FinalDemoSimulator : DsspServiceBase
    {
        /// <summary>
        /// _state
        /// </summary>
        private FinalDemoSimulatorState _state = new FinalDemoSimulatorState();

        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/FinalDemoSimulator", AllowMultipleInstances = false)]
        private FinalDemoSimulatorOperations _mainPort = new FinalDemoSimulatorOperations();

        [Partner("CoroBotSim",
            Contract = cbsim.Contract.Identifier,
            CreationPolicy = PartnerCreationPolicy.CreateAlways,
            Optional = false)]
        private cbsim.CoroBotSimOperations _coroBotPort = new cbsim.CoroBotSimOperations();

        [Partner("Engine", Contract = engineproxy.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        private engineproxy.SimulationEnginePort _engineServicePort = new engineproxy.SimulationEnginePort();

        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public FinalDemoSimulator(DsspServiceCreationPort creationPort)
            : base(creationPort)
        {
        }

        int numMarkers = 0;
        int numWalls = 0;
        int numObjects = 0;

        enum ObjectType { Wall, Marker, Misc };

        static Vector4 Red = new Vector4(0.8f, 0.25f, 0.25f, 1.0f);
        static Vector4 Green = new Vector4(0.0f, 0.8f, 0.1f, 1.0f);
        static Vector4 Yellow = new Vector4(0.8f, 0.8f, 0.25f, 1.0f);
        static Vector4 White = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
        static Vector4 Grey = new Vector4(0.25f, 0.25f, 0.25f, 1.0f);
        static Vector4 Black = new Vector4(0, 0, 0, 1);
        static Vector4 WallColor = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);

        static float MarkerWidth = 0.23f;
        static float MarkerHeight = 0.3f;
        static float MarkerTickness = 0.03f;

        static float WallDepth = 0.3f;
        static float WallDepthThin = 0.1f;
        static float WallHeight = 1.5f;

        const int R = 0;
        const int G = 1;
        const int Y = 2;

        static int horizontal = 0;
        static int vertical = 1;

        Vector2 initialRobotPosition = new Vector2(13.0f, 3.0f);

        float[,] map = {
            {0.00f, 0.00f, R, horizontal},
            {2.64f, 2.00f, Y, horizontal},
            {3.42f, 0.00f, G, horizontal},
            {5.68f, 0.23f, R, vertical},
            {5.42f, 4.25f, G, horizontal},
            {7.49f, 4.25f, R, horizontal},
            {10.54f, 4.25f, Y, horizontal},
            {13.40f, 4.25f, R, horizontal},
            {16.49f, 4.25f, G, horizontal},
            {6.42f, 2.00f, G, horizontal},
            {9.91f, 2.00f, G, horizontal},
            {13.02f, 2.00f, Y, horizontal},
            {15.96f, 2.00f, R, horizontal},
            {18.97f, 2.00f, G, horizontal},
            {22.40f, 4.30f, R, vertical},
            {20.20f, 4.40f, Y, vertical},
            {22.40f, 7.10f, Y, vertical},
            {20.20f, 6.72f, G, vertical},
            {20.20f, 9.67f, R, vertical},
            {21.80f, 10.20f, R, vertical},
            {20.20f, 12.47f, Y, vertical},
            {22.40f, 14.65f, G, vertical},
            {20.20f, 16.63f, R, vertical},
            {22.40f, 17.40f, Y, vertical},
            {20.20f, 19.45f, G, vertical},
            {22.40f, 21.66f, Y, vertical},
            {20.20f, 21.56f, R, vertical}
        };
        
        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
            base.Start();

            SetupCamera();
            PopulateWorld();

            //add Corobot
            cbsim.InsertRobotRequest insert = new cbsim.InsertRobotRequest(
                new physicsproxy.Vector3(initialRobotPosition.X, 0.1f, -initialRobotPosition.Y), //position
                "CoroBot");                        //name

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
            CameraView view = new CameraView();
            view.EyePosition = new Vector3(initialRobotPosition.X, 5.0f, 5.0f);
            view.LookAtPoint = new Vector3(initialRobotPosition.X, 3.0f, 3.0f);
            SimulationEngine.GlobalInstancePort.Update(view);
        }

        private void PopulateWorld()
        {
            AddSky();
            AddGround();

            AddMarkers();

#if DISPLAY_ALL
            AddWalls();
            AddObjects();
#endif
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

        void AddWalls()
        {
            float mass = 1f;

            AddBox(new Vector2(1.7f, -0.17f), new Vector3(8f, 1.5f, 0.3f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(13.3f, 1.93f), new Vector3(15f, 0.6f, 0.1f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(5.75f, 0.83f), new Vector3(0.1f, 1.5f, 2.3f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(12.7f, 4.41f), new Vector3(14.96f, 1.5f, 0.3f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(20.03f, 17.06f), new Vector3(0.3f, 1.5f, 25f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(22.58f, 13.08f), new Vector3(0.3f, 1.5f, 18f), mass, WallColor, ObjectType.Wall); 
            AddBox(new Vector2(2.67f, 13.35f), new Vector3(0.3f, 1.5f, 20f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(5.37f, 14.56f), new Vector3(0.3f, 1.5f, 20f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(-2.45f, 2.68f), new Vector3(0.3f, 1.5f, 6f), mass, WallColor, ObjectType.Wall);
            AddBox(new Vector2(0.15f, 5.5f), new Vector3(4.85f, 1.5f, 0.3f), mass, WallColor, ObjectType.Wall);
        }

        void AddMarkers()
        {
            Vector3 sizeHorizontal  = new Vector3(MarkerWidth, MarkerHeight, MarkerTickness);
            Vector3 sizeVertical    = new Vector3(MarkerTickness, MarkerHeight, MarkerWidth);

            Vector3 size;
            Vector4 color;
           
            float mass = 0.05f;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                if (horizontal == (int)map[i, 3])
                    size = sizeHorizontal;
                else
                    size = sizeVertical;

                switch ((int)map[i, 2])
                {
                    case R: color = Red; break;
                    case G: color = Green; break;
                    case Y: color = Yellow; break;
                    default: color = WallColor;  break;
                }

                AddBox(new Vector2(map[i, 0], map[i, 1]), size, mass, color, ObjectType.Marker);
            }
        }

        void AddObjects()
        {
            float mass = 10f;

            AddBox(new Vector2(2.63f, 2.68f), new Vector3(0.46f, 1, 1.32f), mass, Black, ObjectType.Misc);
            AddBox(new Vector2(3.10f, 2.68f), new Vector3(0.46f, 1, 1.32f), mass, Red, ObjectType.Misc);
            AddBox(new Vector2(22.12f, 10.2f), new Vector3(0.6f, 1f, 2), mass, Grey, ObjectType.Misc);
            AddBox(new Vector2(22.12f, 1.1f), new Vector3(2f, 1.4f, 0.6f), mass, Black, ObjectType.Misc);
        }

        void AddBox(Vector2 position, Vector3 size, float mass, Vector4 color, ObjectType type)
        {
            BoxShapeProperties boxShapeProp = null;
            SingleShapeEntity boxEntity = null;

            boxShapeProp = new BoxShapeProperties(mass, new Pose(), size);
            boxShapeProp.DiffuseColor = color;
            boxShapeProp.Material = new MaterialProperties("gbox", 0.5f, 0.4f, 0.5f);

            boxEntity = new SingleShapeEntity(
                new BoxShape(boxShapeProp),
                new Vector3(position.X, size.Y / 2, -position.Y));
            switch (type)
            {
                case ObjectType.Wall: boxEntity.State.Name = "Wall" + (++numWalls); break;
                case ObjectType.Marker: boxEntity.State.Name = "Marker" + (++numMarkers); break;
                default: boxEntity.State.Name = "Misc" + (++numObjects); break;
            }

            SimulationEngine.GlobalInstancePort.Insert(boxEntity);
        }
    }
}
