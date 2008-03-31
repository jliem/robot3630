//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using W3C.Soap;
using project2 = Robotics.Project2;

using cbir = CoroWare.Robotics.Services.CoroBotIR.Proxy;

using blob = Microsoft.Robotics.Services.Sample.BlobTracker.Proxy;

using motioncontroller = Robotics.CoroBot.MotionController;
using System.IO;
using System.Net;

namespace Robotics.Project2
{
    
    
    /// <summary>
    /// Implementation class for Project2
    /// </summary>
    [DisplayName("Project2")]
    [Description("Service for CS 3630 project 2")]
    [Contract(Contract.Identifier)]
    public class Project2Service : DsspServiceBase
    {
        
        /// <summary>
        /// _state
        /// </summary>
        private Project2State _state = new Project2State();
        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/project2", AllowMultipleInstances=false)]
        private Project2Operations _mainPort = new Project2Operations();

        // Partner with blob tracker
        [Partner("BlobTracker", Contract = blob.Contract.Identifier,
                CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        blob.BlobTrackerOperations _blobPort = new blob.BlobTrackerOperations();
        blob.BlobTrackerOperations _blobNotify = new blob.BlobTrackerOperations();

        // Partner with motion controller
        [Partner("MotionController", Contract = motioncontroller.Contract.Identifier,
                CreationPolicy = PartnerCreationPolicy.UseExistingOrCreate)]
        motioncontroller.MotionControllerOperations _motionPort = new motioncontroller.MotionControllerOperations();

        // IR
        //[Partner("corobotir", Contract = cbir.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        //cbir.CoroBotIROperations _irPort = new cbir.CoroBotIROperations();
        
        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public Project2Service(DsspServiceCreationPort creationPort) : 
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

            _blobPort.Subscribe(_blobNotify);

            Activate<ITask>(Arbiter.Receive<blob.ImageProcessed>(true, _blobNotify, OnImageProcessed));

            //Console.WriteLine(_irPort.Get());

            //Activate(Arbiter.Choice(_irPort.Get(),
            //    delegate( success) { },
            //    delegate(Fault f) { LogError(f); }
            //));
        }

        void OnImageProcessed(blob.ImageProcessed imageProcessed)
        {
            Console.WriteLine("Project 2 received ImageProcessed signal");

            if (imageProcessed.Body.Results.Count > 0)
            {

                // Display results for each blob found
                for (int i = 0; i < imageProcessed.Body.Results.Count; i++)
                {
                    blob.FoundBlob foundBlob = imageProcessed.Body.Results[i];

                    if (foundBlob.Area > 100) //object detected
                    {

                        Console.WriteLine("Blob detected at (" + foundBlob.MeanX + "," + foundBlob.MeanY + ")");
                    }
                    else
                    {
                        Console.WriteLine("Blob is too small: area=" + foundBlob.Area);
                    }
                }
            }
        }

        double GetFakeIRDistance()
        {
            double distance = 0;

            //String file = @"C:\Documents and Settings\JL\Desktop\corobotir.htm";

            String robotIP = "128.61.18.18";

            WebClient client = new WebClient();
            String url = @"http://" + robotIP + @":50000/corobotir";

            //using (StreamReader reader = new StreamReader(file))
            using (StreamReader reader = new StreamReader(client.OpenRead(new Uri(url))))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Looking for <LastFrontRange>25.4</LastFrontRange>
                    // or <th>Front (Meters):</th><td>25.4</td>

                    String startText = @"<th>Front (Meters):</th><td>";
                    String endText = "</td>";

                    //String startText = @"<LastFrontRange>";
                    //String endText = "</LastFrontRange>";

                    int start = line.IndexOf(startText);

                    if (start >= 0)
                    {
                        int end = line.IndexOf(endText, start);

                        if (end >= start)
                        {
                            try
                            {
                                String result = line.Substring(start + startText.Length,
                                    end - start - startText.Length);

                                distance = double.Parse(result);
                            }
                            catch (ArgumentOutOfRangeException aoore)
                            {
                                Console.WriteLine(aoore.Message + Environment.NewLine +
                                    aoore.StackTrace);
                            }
                        }
                    }

                }
            }

            return distance;
        }

        void MakeDecision(blob.FoundBlob foundBlob)
        {
            int meanX = (int)(foundBlob.MeanX);

            int irDistance = this.GetFakeIRDistance();

	        if (irDistance <= .6)
            {
                // We are in IR Sensor range
                if (irDistance <= .1)
                { 
			        return; // We win!
		        }
		        else if (irDistance > .3) { 
			        //driveforward(.5 ft);
		        }
		        else {
			        //driveforward(.25 ft); // We are 1 ft away move slowly
		        }
        			
	        }
	        else
            {
                // We need to go off of vision
	            int center = 295;
                int buffer = 5;

	            if ((meanX >= center - buffer)  && (meanX <= center + buffer))
                {
		            //driveforward(.5 ft);
	            }
	            else if (meanX > center)
                {
		            //turn(-.1);
	            }
                else
                {
                    //turn(.1);
                }
            }

        }

        #region UNUSED
        /// <summary>
        /// Get Handler
        /// </summary>
        /// <param name="get"></param>
        /// <returns></returns>
        [ServiceHandler(ServiceHandlerBehavior.Concurrent)]
        public virtual IEnumerator<ITask> GetHandler(Get get)
        {
            get.ResponsePort.Post(_state);
            yield break;
        }

        #endregion
    }
}
