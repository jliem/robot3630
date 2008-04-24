//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using W3C.Soap;
using coord = Robotics.CoroBot.Coordinator;
using image = Robotics.CoroBot.ImageProcessor.Proxy;
using motion = Robotics.CoroBot.MotionController.Proxy;
using System.Drawing;
using Microsoft.Ccr.Adapters.WinForms;


namespace Robotics.CoroBot.Coordinator
{


    /// <summary>
    /// Implementation class for Coordinator
    /// </summary>
    [DisplayName("Coordinator")]
    [Description("The Coordinator Service")]
    [Contract(Contract.Identifier)]
    public class CoordinatorService : DsspServiceBase
    {
        public enum States { Idle, Lost };

        public States state = States.Idle;
        public bool ready = false;
        public image.ImageProcessorResult imageResult = null;
        CoordinatorForm form = null;
        List<FoundFolder> visitedFolders = new List<FoundFolder>();

        /// <summary>
        /// _state
        /// </summary>
        private CoordinatorState _state = new CoordinatorState();

        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/coordinator", AllowMultipleInstances = false)]
        private CoordinatorOperations _mainPort = new CoordinatorOperations();

        [Partner("Image", Contract = image.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting, Optional = false)]
        private image.ImageProcessorOperations _imagePort = new image.ImageProcessorOperations();

        [Partner("Motion", Contract = motion.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting, Optional = false)]
        private motion.MotionControllerOperations _drivePort = new motion.MotionControllerOperations();

        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public CoordinatorService(DsspServiceCreationPort creationPort)
            :
                base(creationPort)
        {
        }

        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
            base.Start();
            // Add service specific initialization here.
            WinFormsServicePort.Post(new RunForm(CreateForm));
        }

        private System.Windows.Forms.Form CreateForm()
        {
            form = new CoordinatorForm(_mainPort);
            return form;
        }

        private void Begin()
        {
            state = States.Lost;
            TurnToLargestFolder();
            CenterToFolder();
            DriveToFolder()
        }

        public void GetImage()
        {
            ready = false;
            Activate(Arbiter.Choice(_imagePort.Get(new GetRequestType()),
                delegate(image.ImageProcessorResult result)
                {
                    ready = true;
                    imageResult = result;
                },
                delegate(Fault f) { LogError(f); }
            ));
            while (!ready)
            {
                Thread.Sleep(1000);
            }
        }

        public void TurnRight(int degrees)
        {
            ready = false;
            motion.TurnRequest req = new motion.TurnRequest();
            req.Radians = -degrees * Math.PI / 180;
            Activate(Arbiter.Choice(_drivePort.Turn(req),
                delegate(DefaultUpdateResponseType result) { ready = true; },
                delegate(Fault f) { LogError(f); }
            ));
            while (!ready)
            {
                Thread.Sleep(1000);
            }
        }

        public void TurnLeft(int degrees)
        {
            ready = false;
            motion.TurnRequest req = new motion.TurnRequest();
            req.Radians = degrees * Math.PI / 180;
            Activate(Arbiter.Choice(_drivePort.Turn(req),
                delegate(DefaultUpdateResponseType result) { ready = true; },
                delegate(Fault f) { LogError(f); }
            ));
            while (!ready)
            {
                Thread.Sleep(1000);
            }
        }

        public bool DriveForward(float meters)
        {
            ready = false;
            bool completed = true;
            motion.DriveRequest req = new motion.DriveRequest();
            req.Distance = meters;
            Activate(Arbiter.Choice(_drivePort.Drive(req),
                delegate(DefaultUpdateResponseType result) { ready = true; },
                delegate(Fault f) { completed = false; }
            ));
            while (!ready)
            {
                Thread.Sleep(1000);
            }
            return completed;
        }

        public bool CenterToFolder()
        {
            //Return true if  robot is +- OffsetThreshold degrees 
            //from the center of the folder
            int OffsetThreshold = 3;
            GetImage();
            if (imageResult.Folders.Count > 0)
            {
                image.Folder bestFolder = imageResult.Folders[0];
                int bestOffset = GetHeadingOffset((int)imageResult.Folders[0].X);
                foreach (image.Folder f in imageResult.Folders)
                {
                    if (Math.Abs(GetHeadingOffset((int)f.X)) < Math.Abs(bestOffset))
                    {
                        bestFolder = f;
                        bestOffset = GetHeadingOffset((int)f.X);
                    }
                }
                //If Done
                if (Math.Abs(bestOffset) <= OffsetThreshold)
                {
                    return true;
                }
                //Otherwise Turn and try again
                if (bestOffset < 0)
                {
                    TurnRight(Math.Abs(bestOffset));
                    return CenterToFolder();
                }
                else
                {
                    TurnLeft(Math.Abs(bestOffset));
                    return CenterToFolder();
                }
            }
            //I didn't find any folders
            else
            {
                return false;
            }
        }

        public void DriveToPoint(Point start, Point end)
        {



        }

        private void TurnToLargestFolder()
        {
            List<FoundFolder> folders = new List<FoundFolder>();
            int numTurns = 9;
            int degPerTurn = 360 / numTurns;
            int lastFolderHeading = -1000;
            for (int i = 0; i < numTurns; i++)
            {
                LogInfo("Sending Image request.");
                GetImage();
                LogInfo("Received Image.");
                foreach (image.Folder f in imageResult.Folders)
                {
                    FoundFolder ff = new FoundFolder();
                    ff.Color = f.Color;
                    ff.Heading = i * degPerTurn + GetHeadingOffset((int)f.X);
                    ff.Size = f.Area;
                    if (Math.Abs(ff.Heading - lastFolderHeading) < 15)
                    {
                        folders.Add(ff);
                    }
                }
                LogInfo("Send Turn RIght.");
                TurnRight(degPerTurn);
                LogInfo("Received TurnRight");
            }
            if (folders.Count > 0)
            {
                FoundFolder largestFolder = folders[0];
                foreach (FoundFolder f in folders)
                {
                    if (f.Size > largestFolder.Size)
                    {
                        largestFolder = f;
                    }
                }
                TurnLeft(360 - largestFolder.Heading);
            }
        }

        public int GetHeadingOffset(int X)
        {
            return -(((X - 320) * 20) / 320);
        }

        public class FoundFolder
        {
            public string Color;
            public int Heading;
            public int Size;
        }

        /// <summary>
        /// Get Handler
        /// </summary>
        /// <param name="get"></param>
        /// <returns></returns>
        [ServiceHandler(ServiceHandlerBehavior.Concurrent)]
        public virtual IEnumerator<ITask> GetHandler(Get get)
        {
            Begin();
            get.ResponsePort.Post(_state);
            yield break;
        }
    }
}