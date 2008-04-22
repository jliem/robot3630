//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
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
using motioncontroller = Robotics.CoroBot.MotionController;
using cbdrive = CoroWare.Robotics.Services.CoroBotDrive.Proxy;
using cbencoder = CoroWare.Robotics.Services.CoroBotMotorEncoders.Proxy;
using ds = Microsoft.Dss.Services.Directory;
using System.Net;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace Robotics.CoroBot.MotionController
{
    
    
    /// <summary>
    /// Implementation class for MotionController
    /// </summary>
    [DisplayName("MotionController")]
    [Description("CoroBot Simple Motion Controller - Project 3")]
    [Contract(Contract.Identifier)]
    public class MotionControllerService : DsspServiceBase
    {
        /// <summary>
        /// _state
        /// </summary>
        private MotionControllerState _state = new MotionControllerState();
        private int oldEncoderValue;
        private const double DRIVE_POWER = 0.6;
        private const double ROTATE_POWER = 0.2;

        private System.Timers.Timer motorTimer;
        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/motioncontroller", AllowMultipleInstances=false)]
        private MotionControllerOperations _mainPort = new MotionControllerOperations();

        [Partner("drive", Contract = cbdrive.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        private cbdrive.CoroBotDriveOperations _drivePort = new cbdrive.CoroBotDriveOperations();
        [Partner("encoder", Contract = cbencoder.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        private cbencoder.CoroBotMotorEncodersOperations _encoderPort = new cbencoder.CoroBotMotorEncodersOperations();
        
        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public MotionControllerService(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {
        }
        
        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
			base.Start();

            _state.Power = 0;

            // Subscribe to Encoder service
            cbencoder.CoroBotMotorEncodersOperations encoderPort = new cbencoder.CoroBotMotorEncodersOperations();
            _encoderPort.Subscribe(encoderPort);
            Activate(Arbiter.Receive<cbencoder.Replace>(true, encoderPort, EncoderHandler));

            SetEncoderInterval(50);

            System.Timers.Timer encoderTimer = new System.Timers.Timer();
            encoderTimer.Elapsed += new ElapsedEventHandler(EncoderOnTimedEvent);

            encoderTimer.Interval = 1000;
            encoderTimer.Enabled = true;

            motorTimer = new System.Timers.Timer();
            motorTimer.Elapsed += new ElapsedEventHandler(MotorOnTimedEvent);
            motorTimer.Enabled = false;

            WinFormsServicePort.Post(new Microsoft.Ccr.Adapters.WinForms.RunForm(StartForm));
        }

        private System.Windows.Forms.Form StartForm()
        {
            return new MotionForm(_mainPort, _state.Power);
        }

        private void MotorOnTimedEvent(object source, ElapsedEventArgs e)
        {
            SendStopMessage();
            _state.DrivingState = DrivingStates.Stopped;
        }

        private void EncoderOnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.EncoderHandler(null);
        }

        private int GetFakeEncoderValue()
        {
            int encoderValue = 0;

            String robotIP = "128.61.18.148";

            WebClient client = new WebClient();
            String url = @"http://" + robotIP + @":50000/corobotmotorencoders";

            //using (StreamReader reader = new StreamReader(file))
            using (StreamReader reader = new StreamReader(client.OpenRead(new Uri(url))))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {

                    //<LeftValue>0</LeftValue>

                    String startText = @"<LeftValue>";
                    String endText = @"</LeftValue>";

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

                                encoderValue = int.Parse(result);
                            }
                            catch (ArgumentOutOfRangeException aoore)
                            {
                                Console.WriteLine(aoore.Message + System.Environment.NewLine +
                                    aoore.StackTrace);
                            }
                        }
                    }

                }
            }

            return encoderValue;

        }

        private void SetEncoderInterval(int m)
        {
            cbencoder.ResetEncodersMessage reset = new cbencoder.ResetEncodersMessage();
            reset.InSequenceNumber = 0;
            Activate(Arbiter.Choice(_encoderPort.ResetEncoders(reset),
                delegate(DefaultSubmitResponseType success) { LogInfo("Successfully reset encoders."); },
                delegate(Fault f) { LogError(f); }
            ));

            cbencoder.SetIntervalMessage newInterval = new cbencoder.SetIntervalMessage();
            newInterval.NewInterval = m;
            newInterval.InSequenceNumber = 0;
            Activate(Arbiter.Choice(_encoderPort.SetInterval(newInterval),
                delegate(DefaultSubmitResponseType success) { LogInfo("Sucessfully set encoder interval."); },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void SendTurnLeftMessage()
        {
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = true;
            newState.Rotation = _state.Power;
            newState.Translation = 0;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void SendTurnRightMessage()
        {
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = true;
            newState.Rotation = -_state.Power;
            newState.Translation = 0;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void SendDriveForwardMessage()
        {
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = true;
            newState.Rotation = 0;
            newState.Translation = _state.Power;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void SendDriveBackwardMessage()
        {
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = true;
            newState.Rotation = 0;
            newState.Translation = -_state.Power;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void SendStopMessage()
        {
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = false;
            newState.Rotation = 0;
            newState.Translation = 0;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { },
                delegate(Fault f) { LogError(f); }
            ));
        }

        private void EncoderHandler(cbencoder.Replace notification)
        {
            int encoderValue = notification.Body.LeftValue;

            //int encoderValue = this.GetFakeEncoderValue();

            int encoderChange = Math.Abs(encoderValue - oldEncoderValue);

            //int encoder = Math.Abs(GetFakeEncoderValue() - oldEncoderValue);

            //Console.WriteLine("Left distance is " + notification.Body.LeftDistance);
            //Console.WriteLine("Left change is " + notification.Body.LeftChange);
            //Console.WriteLine("Right distance is " + notification.Body.RightDistance);
            //Console.WriteLine("Encoder countdown is " + _state.EncoderCountdown);
            //Console.WriteLine("Encoder calibration is " + _state.EncoderCalibration);

            oldEncoderValue = notification.Body.LeftValue;

            _state.EncoderCountdown -= encoderChange;
            _state.EncoderCalibration += encoderChange;

            switch (_state.DrivingState)
            {
                case DrivingStates.Stopped:
                    SendStopMessage();
                    break;
                case DrivingStates.MovingForward:

                    Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished moving forward, encoder calibration is "
                            + _state.EncoderCalibration);
                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();
                    }
                    else
                    {
                        //Console.WriteLine("Moving forward, encoder countdown is " + _state.EncoderCountdown);
                        SendDriveForwardMessage();
                    }

                    break;
                case DrivingStates.MovingBackward:

                    Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished moving backward, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();
                    }
                    else
                    {
                        //Console.WriteLine("Moving backward, encoder countdown is " + _state.EncoderCountdown);

                        SendDriveBackwardMessage();
                    }
                    break;
                case DrivingStates.CalibratingDrive:
                    SendDriveForwardMessage();

                    break;
                case DrivingStates.CalibratingTurn:
                    SendTurnRightMessage();
                    break;
                case DrivingStates.TurningLeft:

                    Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished turning left, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();
                    }
                    else
                    {
                        //Console.WriteLine("Turning left, encoder countdown is " + _state.EncoderCountdown);

                        SendTurnLeftMessage();
                    }
                    break;
                case DrivingStates.TurningRight:

                    Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished turning right, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();
                    }
                    else
                    {
                        //Console.WriteLine("Turning right, encoder countdown is " + _state.EncoderCountdown);

                        SendTurnRightMessage();
                    }
                    break;
                default:
                    break;
            }
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> DriveHandler(Drive drive)
        {

            if (drive.Body.Power != 0)
            {
                _state.Power = drive.Body.Power;
            }

            // Override argument
            _state.Power = DRIVE_POWER;

            _state.EncoderCalibration = 0;
            _state.EncoderCountdown = _state.DistanceCalibration * Math.Abs(drive.Body.Distance);

            Console.WriteLine("Distance calibration is " + _state.DistanceCalibration);
            Console.WriteLine("Encoder countdown set to " + _state.EncoderCountdown);

            if (drive.Body.Distance > 0)
            {
                _state.DrivingState = DrivingStates.MovingForward;
            }
            else
            {
                _state.DrivingState = DrivingStates.MovingBackward;
            }

            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> TurnHandler(Turn turn)
        {
            
            if (turn.Body.Power != 0)
            {
                _state.Power = turn.Body.Power;
            }

            // Override argument
            _state.Power = ROTATE_POWER;

            _state.EncoderCalibration = 0;
            _state.EncoderCountdown = _state.TurningCalibration * Math.Abs(turn.Body.Radians);

            Console.WriteLine("Encoder countdown set to " + _state.EncoderCountdown);

            if (turn.Body.Radians > 0)
            {
                _state.DrivingState = DrivingStates.TurningLeft;
            }
            else
            {
                _state.DrivingState = DrivingStates.TurningRight;
            }

            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> StopHandler(Stop stop)
        {
            _state.DrivingState = DrivingStates.Stopped;
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> BeginCalibrateDriveHandler(BeginCalibrateDrive calibrate)
        {
            _state.Power = DRIVE_POWER;
            _state.EncoderCalibration = 0;
            _state.DrivingState = DrivingStates.CalibratingDrive;
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> BeginCalibrateTurnHandler(BeginCalibrateTurn calibrate)
        {
            _state.Power = ROTATE_POWER;
            _state.EncoderCalibration = 0;
            _state.DrivingState = DrivingStates.CalibratingTurn;

            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> SetDriveCalibrationHandler(SetDriveCalibration calibrate)
        {
            _state.DistanceCalibration = _state.EncoderCalibration / calibrate.Body.Distance;
            
            TimeSpan duration = calibrate.Body.CalibrateTimespan;

            Console.WriteLine("Finished drive calibration: encoderCalib was " + _state.EncoderCalibration +
                " and distance was " + calibrate.Body.Distance + ", so distanceCalib set to " + _state.DistanceCalibration);

            //MessageBox.Show("Time was " + duration.TotalMilliseconds + " ms");
            Console.WriteLine("Time was " + duration.TotalMilliseconds + " ms");
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> SetTurnCalibrationHandler(SetTurnCalibration calibrate)
        {

            _state.TurningCalibration = _state.EncoderCalibration / calibrate.Body.Radians;

            Console.WriteLine("Finished turn calibration: encoderCalib was " + _state.EncoderCalibration +
    " and turn was " + (calibrate.Body.Radians * 180 / 3.14) + ", so turn set to " + _state.TurningCalibration);


            yield break;
        }
        
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
    }
}
