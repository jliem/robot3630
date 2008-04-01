//-----------------------------------------------------------------------
//  This file is part of the Microsoft Robotics Studio Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  $File: BlobTracker.cs $ $Revision: 1 $
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using W3C.Soap;

using sm = Microsoft.Dss.Services.SubscriptionManager;
using cam = Microsoft.Robotics.Services.WebCam.Proxy;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;

using System.Threading;

namespace Microsoft.Robotics.Services.Sample.BlobTracker
{
    
    /// <summary>
    /// Implementation class for BlobTracker
    /// </summary>
    [DisplayName("Blob Tracker")]
    [Description("CS 3630 Project 2 - Blob tracker 2")]
    [Contract(Contract.Identifier)]
    public class BlobTrackerService : DsspServiceBase
    {
        /// <summary>
        /// _state
        /// </summary>
        [InitialStatePartner(Optional = true, ServiceUri = "BlobTracker.Config.xml")]
        private BlobTrackerState _state = new BlobTrackerState();
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/blobtracker", AllowMultipleInstances=false)]
        private BlobTrackerOperations _mainPort = new BlobTrackerOperations();


        [Partner("SubMgr", Contract = sm.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.CreateAlways)]
        sm.SubscriptionManagerPort _subMgrPort = new sm.SubscriptionManagerPort();

        //[Partner("WebCam", Contract = cam.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        //cam.WebCamOperations _camPort = new cam.WebCamOperations();
        //cam.WebCamOperations _camNotify = new cam.WebCamOperations();

        // The gui
        private Display disp;

        private System.Timers.Timer _timer;
        public System.Timers.Timer timer
        {
            get
            {
                return _timer;
            }
        }


        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public BlobTrackerService(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {

            // Create a new thread to run the GUI
            // I hate C#, this is the stupidest thing I've ever seen.
            disp = new Display(this);
            Thread thread = new Thread(new ThreadStart(CreateDisplay));
            thread.Start();
        }

        /// <summary>
        /// Helper method to create GUI in new Thread.
        /// </summary>
        private void CreateDisplay()
        {
            Application.Run(disp);
        }

        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
            if (_state == null)
            {
                _state = new BlobTrackerState();
            }


			base.Start();

            // Uncomment this stuff to be notified of new camera
            // images. I just use a Timer, so it's unnecessary.

            //base.MainPortInterleave.CombineWith(
            //    Arbiter.Interleave(
            //        new TeardownReceiverGroup(),
            //        new ExclusiveReceiverGroup(
            //            Arbiter.Receive<cam.UpdateFrame>(true, _camNotify, OnCameraUpdateFrame)
            //        ),
            //        new ConcurrentReceiverGroup()
            //    )
            //);

            //Activate(
            //    Arbiter.Receive(false, TimeoutPort(5000), StartTimer)
            //);

            // Set up color detectors

            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            _timer.Interval = 2000;
            _timer.Enabled = true;

        }

        public void RemoveColorBin(ColorBin bin)
        {
            _state.ColorBins.Remove(bin);
        }

        public void AddColorBin(ColorBin bin)
        {
            _state.ColorBins.Add(bin);
        }

        public void SetTimerEnabled(Boolean value)
        {
            _timer.Enabled = value;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            SpawnIterator(new List<ColorBin>(_state.ColorBins), ProcessImage);
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> ImageProcessedHandler(ImageProcessed imageProcessed)
        {
            _state.UpdateFrame = false;

            if (imageProcessed.Body.Results.Count > 0)
            {

                _state.TimeStamp = imageProcessed.Body.TimeStamp;
                _state.Results = imageProcessed.Body.Results;
                SendNotification(_subMgrPort, imageProcessed);
                disp.Write("Notification has been sent from handler");
            }
            else
            {
                disp.Write("Handler was called, but no results so no notification sent");
            }
            imageProcessed.ResponsePort.Post(DefaultUpdateResponseType.Instance);

            yield break;
        }

        /// <summary>
        /// Method to get bitmap directly from camera because
        /// converting from byte[] to image isn't working for some reason
        /// </summary>
        /// <returns></returns>
        private Bitmap getSnapshot()
        {
            String robotIP = "128.61.22.166";

            //String webCam = @"http://localhost:50000/simulatedwebcam/c773b79a-8de4-422e-9d8c-0a21878ab2ef/jpeg";
            String url = @"http://" + robotIP + @":50000/corobotcamera";

            Bitmap bitmap = null;

            try
            {
                System.Net.WebClient client = new WebClient();

                using (Stream input = client.OpenRead(new Uri(url)))
                {
                    bitmap = new Bitmap(input);
                }
                
            }
            catch (WebException we)
            {
                MessageBox.Show("Unable to connect: " + we.StackTrace);
            }

            return bitmap;
        }

        /// <summary>
        /// Reads an image, then packages it into a QueryFrameResponse
        /// as though we were reading from the webcam
        /// </summary>
        /// <returns></returns>
        private cam.QueryFrameResponse getFakeQueryFrameResponse()
        {
            cam.QueryFrameResponse response = new cam.QueryFrameResponse();

            Bitmap bitmap = this.getSnapshot();

            response.Size = new Size(bitmap.Width, bitmap.Height);
            
            // Convert to byte array
            using (MemoryStream ms = new MemoryStream()) {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                response.Frame = ms.ToArray();

            }
            
            disp.SetImage(bitmap);

            //disp.Write("Retrieved image from " + filePath);

            return response;
        }

        IEnumerator<ITask> ProcessImage(List<ColorBin> bins)
        {
            Fault fault = null;

            cam.QueryFrameRequest request = new cam.QueryFrameRequest();
            request.Format = Guid.Empty;// new Guid("b96b3cae-0728-11d3-9d7b-0000f81ef32e");

            byte[] frame = null;
            DateTime timestamp = DateTime.MinValue;
            int height = 0;
            int width = 0;

            // Uncomment stuff below to pull images from the camera (works for webcam,
            // not sure about real one

            //yield return Arbiter.Choice(
            //    _camPort.QueryFrame(request),
            //delegate(cam.QueryFrameResponse response)
            //{
            //    timestamp = response.TimeStamp;
            //    frame = response.Frame;
            //    width = response.Size.Width;
            //    height = response.Size.Height;
            //    disp.Write("Got image from camera");

            //},
            //    delegate(Fault f)
            //    {
            //        fault = f;
            //    }
            //);

            cam.QueryFrameResponse response = this.getFakeQueryFrameResponse();

            frame = response.Frame;

            width = response.Size.Width;
            height = response.Size.Height;

            ImageProcessedRequest processed = new ImageProcessedRequest();

            if (fault != null)
            {
                _mainPort.Post(new ImageProcessed(processed));
                yield break;
            }


            // ********** Converting from byte[] to image is not working ********** 

            //Bitmap bitmap = null;
            //using (MemoryStream stream = new MemoryStream(frame))
            //{
            //    bitmap = new Bitmap(stream);
            //}

            ////Bitmap bitmap = this.getImage();
            //disp.SetImage(bitmap);

            int size = width * height * 3;

            processed.TimeStamp = timestamp;
            List<FoundBlob> results = processed.Results;

            foreach (ColorBin bin in bins)
            {
                FoundBlob blob = new FoundBlob();

                blob.Name = bin.Name;
                blob.XProjection = new int[width];
                blob.YProjection = new int[height];

                results.Add(blob);
            }

            int offset;

            Bitmap bitmap = new Bitmap(width, height);

            if (bins.Count <= 0)
            {
                disp.Write("No bins found");
                yield return null;
            }

            
            for (int i=0; i<bins.Count; i++) {
                ColorBin currentBin = bins[i];

                disp.Write("Checking bin: " + currentBin.RedMin + "-" + currentBin.RedMax + ", " +
                    currentBin.GreenMin + "-" + currentBin.GreenMax + ", " +
                    currentBin.BlueMin + "-" + currentBin.BlueMax);

                for (int y = 0; y < height; y++)
                {
                    offset = y * width * 3;

                    for (int x = 0; x < width; x++, offset += 3)
                    {
                        int r, g, b;

                        b = frame[offset];
                        g = frame[offset + 1];
                        r = frame[offset + 2];

                        if (currentBin.Test(r, g, b))
                        {
                            results[i].AddPixel(x, y);
                            //Console.WriteLine(x + "," + y);

                            bitmap.SetPixel(x, height - y - 1, Color.FromArgb(r, g, b));

                        }
                        else
                        {
                            bitmap.SetPixel(x, y, Color.White);
                        }
                    }
                }
            }

            foreach (FoundBlob blob in results)
            {
                if (blob.Area > 0)
                {
                    blob.MeanX = blob.MeanX / blob.Area;
                    blob.MeanY = blob.MeanY / blob.Area;

                    blob.CalculateMoments();

                    disp.Write("Blob found. Area: " + blob.Area + ", Mean x: " + blob.MeanX +
                        ", mean y: " + blob.MeanY + ", skewX: " + blob.SkewX + 
                        ", skewY: " + blob.SkewY);
                    
                }
            }

//            disp.Write("BlobTracker is posting results");
            ImageProcessed imgP = new ImageProcessed(processed);
            
            disp.SetImage(bitmap);

            _mainPort.Post(imgP);

        }

        #region UNUSED
        //void StartTimer(DateTime signal)
        //{
        //    //_camPort = ServiceForwarder<cam.WebCamOperations>(FindPartner("WebCam").Service);
        //    LogInfo(LogGroups.Console, "Subscribe to webcam");
        //    Activate(
        //        Arbiter.Choice(
        //            _camPort.Subscribe(_camNotify),
        //            OnSubscribed,
        //            OnSubscribeFailed
        //        )
        //    );
        //}

        //void OnSubscribed(SubscribeResponseType success)
        //{
        //    LogInfo(LogGroups.Console, "Subscribed to camera");
        //}

        //void OnSubscribeFailed(Fault fault)
        //{
        //    LogError(LogGroups.Console, "Failed to subscribe to camera");
        //    _mainPort.Post(new DsspDefaultDrop());
        //}

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

        void OnCameraUpdateFrame(cam.UpdateFrame updateFrame)
        {
            if (!_state.UpdateFrame)
            {
                _state.UpdateFrame = true;
                SpawnIterator(new List<ColorBin>(_state.ColorBins), ProcessImage);
            }
        }

        [ServiceHandler(ServiceHandlerBehavior.Concurrent)]
        public IEnumerator<ITask> SubscribeHandler(Subscribe subscribe)
        {
            SubscribeHelper(_subMgrPort, subscribe.Body, subscribe.ResponsePort);
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> InsertBin(InsertBin insert)
        {
            ColorBin existing = _state.ColorBins.Find(
                delegate(ColorBin test)
                {
                    return test.Name == insert.Body.Name;
                }
            );

            if (existing == null)
            {
                _state.ColorBins.Add(insert.Body);
                insert.ResponsePort.Post(DefaultInsertResponseType.Instance);
                SendNotification(_subMgrPort, insert);
            }
            else
            {
                insert.ResponsePort.Post(
                    Fault.FromCodeSubcodeReason(
                        FaultCodes.Receiver,
                        DsspFaultCodes.DuplicateEntry,
                        "A Color Bin named " + insert.Body.Name + " already exists."
                    )
                );
            }
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> DeleteBin(DeleteBin delete)
        {
            ColorBin existing = _state.ColorBins.Find(
                delegate(ColorBin test)
                {
                    return test.Name == delete.Body.Name;
                }
            );

            if (existing != null)
            {
                _state.ColorBins.Remove(existing);
                delete.ResponsePort.Post(DefaultDeleteResponseType.Instance);
                SendNotification<DeleteBin>(_subMgrPort, existing);
            }
            else
            {
                delete.ResponsePort.Post(
                    Fault.FromCodeSubcodeReason(
                        FaultCodes.Receiver,
                        DsspFaultCodes.UnknownEntry,
                        "A Color Bin named " + delete.Body.Name + " could not be found."
                    )
                );
            }
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> UpdateBin(UpdateBin update)
        {
            ColorBin existing = _state.ColorBins.Find(
                delegate(ColorBin test)
                {
                    return test.Name == update.Body.Name;
                }
            );

            if (existing != null)
            {
                int index = _state.ColorBins.IndexOf(existing);
                _state.ColorBins[index] = update.Body;

                update.ResponsePort.Post(DefaultUpdateResponseType.Instance);
                SendNotification(_subMgrPort, update);
            }
            else
            {
                update.ResponsePort.Post(
                    Fault.FromCodeSubcodeReason(
                        FaultCodes.Receiver,
                        DsspFaultCodes.UnknownEntry,
                        "A Color Bin named " + update.Body.Name + " could not be found."
                    )
                );
            }
            yield break;
        }
        #endregion

    }
}
