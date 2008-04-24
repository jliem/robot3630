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
using cbir = CoroWare.Robotics.Services.CoroBotIR.Proxy;
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

        private const bool REAL_ROBOT = true;
        String robotIP = "128.61.24.139";

        private const double SIM_DRIVE_POWER = 0.6;
        private const double SIM_ROTATE_POWER = 0.2;

        private const double DRIVE_POWER = 0.6;
        private const double ROTATE_POWER = 0.5;

        /// <summary>
        /// Interval for the encoder timer.
        /// </summary>
        private const int ENCODER_POLLING_INTERVAL = 10;

        private const bool USE_LEFT_ENCODERS = false;

        /// <summary>
        /// Distance of an obstacle in front where we will stop moving.
        /// </summary>
        private const double IR_CLOSE_DISTANCE = 0.3;

        /// <summary>
        /// When the robot is close to an obstacle going forward, how much should
        /// we reverse?
        /// </summary>
        private const double DISTANCE_TO_REVERSE = 0.5;

        private double lastIrDistance;

        // Will be reset in constructor if on sim
        private double drivePower = DRIVE_POWER;
        private double rotatePower = ROTATE_POWER;
        
        private bool followingWaypoints = false;
        private bool pendingDrive = false;
        private bool checkNextWaypointScheduled = false;
        private bool firstWaypoint = false;
        private double turnMultiplier = .9;

        private LinkedList<Vector2> waypoints;
        private Vector2 prevWaypoint;
        private double prevHeading;
        private double amountToTurn;
        private double amountToDrive;

        private BeginWaypoint beginWaypoint = null;

        private System.Timers.Timer motorTimer;
        //private System.Timers.Timer irTimer;
        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/motioncontroller", AllowMultipleInstances=false)]
        private MotionControllerOperations _mainPort = new MotionControllerOperations();

        [Partner("drive", Contract = cbdrive.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        private cbdrive.CoroBotDriveOperations _drivePort = new cbdrive.CoroBotDriveOperations();
        [Partner("encoder", Contract = cbencoder.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        private cbencoder.CoroBotMotorEncodersOperations _encoderPort = new cbencoder.CoroBotMotorEncodersOperations();

        [Partner("corobotir", Contract = cbir.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting)]
        cbir.CoroBotIROperations _irPort = new cbir.CoroBotIROperations();

        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public MotionControllerService(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {

            if (REAL_ROBOT)
            {
                drivePower = DRIVE_POWER;
                rotatePower = ROTATE_POWER;

                Console.WriteLine("Using real robot parameters, IP is " + robotIP);
            }
            else
            {
                drivePower = SIM_DRIVE_POWER;
                rotatePower = SIM_ROTATE_POWER;

                Console.WriteLine("Using simulator");
            }
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

            cbir.CoroBotIROperations irPort = new cbir.CoroBotIROperations();
            _irPort.Subscribe(irPort);
            Activate(Arbiter.Receive<cbir.Replace>(true, irPort, IrHandler));

            SetEncoderInterval(ENCODER_POLLING_INTERVAL);

            System.Timers.Timer encoderTimer = new System.Timers.Timer();
            encoderTimer.Elapsed += new ElapsedEventHandler(EncoderOnTimedEvent);

            encoderTimer.Interval = 500;
            encoderTimer.Enabled = REAL_ROBOT;

            motorTimer = new System.Timers.Timer();
            motorTimer.Elapsed += new ElapsedEventHandler(MotorOnTimedEvent);
            motorTimer.Enabled = false;

            prevWaypoint = new Vector2(0, 0);

            InitializeWaypoints(new string[] { "0,1", "1,2", "0,3", "-1,2", "0,0" });

            WinFormsServicePort.Post(new Microsoft.Ccr.Adapters.WinForms.RunForm(StartForm));
        }

        private System.Windows.Forms.Form StartForm()
        {
            return new MotionForm(_mainPort, _state.Power);
        }

        //private void IrOnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    // Poll the IR sensor
        //    Activate(Arbiter.Choice(_irPort.Get(new GetRequestType()),
        //        delegate(cbir.CoroBotIRState success) { lastIrDistance = success.LastFrontRange; },
        //        delegate(Fault f) { LogError(f); }
        //    ));
        //}

        private void MotorOnTimedEvent(object source, ElapsedEventArgs e)
        {
            SendStopMessage();
            _state.DrivingState = DrivingStates.Stopped;
        }

        private void EncoderOnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.EncoderHandler(null);
        }

        double GetIRDistance()
        {
            return lastIrDistance;
        }

        double GetFakeIRDistance()
        {
            double distance = 0;

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

                    //String startText = @"<th>Front (Meters):</th><td>";
                    //String endText = "</td>";

                    String startText = @"<LastFrontRange>";
                    String endText = "</LastFrontRange>";

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
                                Console.WriteLine(aoore.Message + System.Environment.NewLine +
                                    aoore.StackTrace);
                            }
                        }
                    }

                }
            }

            return distance;
        }

        private int GetFakeEncoderValue()
        {
            int encoderValue = 0;

            WebClient client = new WebClient();
            String url = @"http://" + robotIP + @":50000/corobotmotorencoders";

            //using (StreamReader reader = new StreamReader(file))
            using (StreamReader reader = new StreamReader(client.OpenRead(new Uri(url))))
            {
                String line;
                while ((line = reader.ReadLine()) != null)
                {

                    //<LeftValue>0</LeftValue>

                    String startText = @"<RightValue>";
                    String endText = @"</RightValue>";

                    if (USE_LEFT_ENCODERS)
                    {
                        startText = @"<LeftValue>";
                        endText = @"</LeftValue>";
                    }

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

        private void InitializeWaypoints(string[] points)
        {
            waypoints = new LinkedList<Vector2>();
            foreach (string point in points)
            {
                string[] split = point.Split(',');
                double x = double.Parse(split[0]);
                double y = double.Parse(split[1]);
                waypoints.AddLast(new Vector2(x, y));
            }

        }

        private void BeginNextWaypoint()
        {
            checkNextWaypointScheduled = false;

            if (waypoints.Count == 0)
            {
                // Finished all waypoints
                Console.WriteLine("Finished all waypoints");

                if (beginWaypoint != null)
                {
                    beginWaypoint.ResponsePort.Post(new DefaultUpdateResponseType());
                    beginWaypoint = null;
                }

                SendStopMessage();
                return;
            }

            followingWaypoints = true;

            Vector2 driveVector = waypoints.First.Value.Subtract(prevWaypoint);
            amountToDrive = driveVector.Norm;
            amountToTurn = driveVector.Angle - prevHeading;
            if (amountToTurn < -Math.PI) amountToTurn += (2 * Math.PI);
            if (amountToTurn > Math.PI) amountToTurn -= (2 * Math.PI);
            //amountToTurn = GetWheelTurnDistance(amountToTurn);

            Console.WriteLine("Robot is at " + prevWaypoint.ToString() + ", facing " + ToDegrees(prevHeading));
            Console.WriteLine("Robot needs to turn " + ToDegrees(amountToTurn));

            //if (firstWaypoint)
            //{
            //    // Reduce first one by 10 percent
            //    amountToTurn *= .9;

            //    firstWaypoint = false;
            //}

            amountToTurn *= turnMultiplier;

            Console.WriteLine("Adjusted turn is " + ToDegrees(amountToTurn));

            turnMultiplier += 0.1;

            if (turnMultiplier > 1.1)
            {
                turnMultiplier = 1.15;
            }

            //if (waypoints.Count < 5)
            //{
            //    amountToTurn *= 2;
            //}

            if (amountToTurn != 0)
            {
                //SendTurnLeftMessage();
                _mainPort.Post(new Turn(new TurnRequest(amountToTurn, rotatePower)));

                amountToTurn = 0;
            }
            else
            {
                //SendDriveForwardMessage();
                _mainPort.Post(new Drive(new DriveRequest(amountToDrive, drivePower)));
                amountToDrive = 0;
            }
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
            Console.WriteLine("Sending forward with " + _state.Power);
            cbdrive.CoroBotDriveState newState = new cbdrive.CoroBotDriveState();
            newState.InSequenceNumber = 0;
            newState.DriveEnable = true;
            newState.Rotation = 0;
            newState.Translation = _state.Power;
            Activate(Arbiter.Choice(_drivePort.Replace(newState),
                delegate(DefaultReplaceResponseType success) { Console.WriteLine("forward - Success"); },
                delegate(Fault f) { LogError(f); Console.WriteLine("Failure in SendDriveForwardMessage");  }
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

        private void CheckReachedWaypoint()
        {

            if (followingWaypoints && waypoints.Count > 0)
            {

                if (amountToDrive > 0)
                {
                    pendingDrive = true;
                }
                else
                {
                    pendingDrive = false;
                    // The last waypoint was complete, remove it and see if there are any more
                    prevHeading = waypoints.First.Value.Subtract(prevWaypoint).Angle;
                    prevWaypoint = waypoints.First.Value;

                    Console.WriteLine("Arrived at " + prevWaypoint.X + ", " + prevWaypoint.Y);
                    waypoints.RemoveFirst();

                    checkNextWaypointScheduled = true;
                }
            }
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

        private void IrHandler(cbir.Replace notification)
        {
            lastIrDistance = notification.Body.LastFrontRange;
        }

        private void EncoderHandler(cbencoder.Replace notification)
        {
            int encoderValue = 0;

            if (REAL_ROBOT)
            {
                encoderValue = this.GetFakeEncoderValue();
            }
            else 
            {
                encoderValue = notification.Body.RightValue;

                if (USE_LEFT_ENCODERS)
                {
                    encoderValue = notification.Body.LeftValue;
                }
            }

            Console.WriteLine("Encoder is " + encoderValue);
            Console.WriteLine("State is " + _state.DrivingState);

            int encoderChange = Math.Abs(encoderValue - oldEncoderValue);

            //int encoder = Math.Abs(GetFakeEncoderValue() - oldEncoderValue);

            //Console.WriteLine("Left distance is " + notification.Body.LeftDistance);
            //Console.WriteLine("Left change is " + notification.Body.LeftChange);
            //Console.WriteLine("Right distance is " + notification.Body.RightDistance);
            //Console.WriteLine("Encoder countdown is " + _state.EncoderCountdown);
            //Console.WriteLine("Encoder calibration is " + _state.EncoderCalibration);
            //Console.WriteLine("Encoder: " + encoderValue);

            oldEncoderValue = encoderValue;

            _state.EncoderCountdown -= encoderChange;
            _state.EncoderCalibration += encoderChange;

            switch (_state.DrivingState)
            {
                case DrivingStates.Stopped:
                    SendStopMessage();


                    if (amountToDrive > 0)
                    {
                        if (pendingDrive)
                        {
                            // We finished turning, now need to drive straight to destination
                            _mainPort.Post(new Drive(new DriveRequest(amountToDrive, drivePower)));
                            amountToDrive = 0;
                        }
                        else
                        {
                            pendingDrive = false;
                        }
                    }

                    if (checkNextWaypointScheduled)
                    {
                        BeginNextWaypoint();
                    }

                    break;
                case DrivingStates.MovingForward:

                    //Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished moving forward, encoder calibration is "
                            + _state.EncoderCalibration);
                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();

                        CheckReachedWaypoint();
                    }
                    else
                    {
                        //Console.WriteLine("Moving forward, encoder countdown is " + _state.EncoderCountdown);

                        // Check whether we're close to an obstacle
                        double irDistance = 0;

                        if (REAL_ROBOT)
                        {
                            irDistance = this.GetFakeIRDistance();
                        }
                        else
                        {

                            irDistance = this.GetIRDistance();
                        }

                        Console.WriteLine("Distance is " + irDistance);

                        if (irDistance <= IR_CLOSE_DISTANCE)
                        {
                            // Robot is close to some obstacle, go backwards a bit
                            Console.WriteLine("Robot is too close to an obstacle, backing up...");
                            //_drivePort.Post(new Drive(new DriveRequest(-1 * DISTANCE_TO_REVERSE, drivePower)));

                            _state.DrivingState = DrivingStates.Stopped;
                            SendStopMessage();
                        }
                        else
                        {
                            SendDriveForwardMessage();
                        }
                    }

                    break;
                case DrivingStates.MovingBackward:

                    //Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished moving backward, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();

                        CheckReachedWaypoint();

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
                case DrivingStates.CalibratingLeftTurn:
                    SendTurnLeftMessage();
                    break;
                case DrivingStates.TurningLeft:

                    //Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished turning left, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();

                        CheckReachedWaypoint();

                    }
                    else
                    {
                        //Console.WriteLine("Turning left, encoder countdown is " + _state.EncoderCountdown);

                        SendTurnLeftMessage();
                    }
                    break;
                case DrivingStates.TurningRight:

                    //Console.Write("LeftValue is " + oldEncoderValue + "; ");

                    if (_state.EncoderCountdown <= 0)
                    {
                        Console.WriteLine("Finished turning right, encoder calibration is "
                            + _state.EncoderCalibration);

                        _state.DrivingState = DrivingStates.Stopped;
                        SendStopMessage();

                        CheckReachedWaypoint();

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
            _state.Power = drivePower;

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
            _state.Power = rotatePower;

            _state.EncoderCalibration = 0;

            if (turn.Body.Radians > 0)
            {
                _state.EncoderCountdown = _state.TurningLeftCalibration * Math.Abs(turn.Body.Radians);
                _state.DrivingState = DrivingStates.TurningLeft;
            }
            else
            {
                _state.EncoderCountdown = _state.TurningCalibration * Math.Abs(turn.Body.Radians);
                _state.DrivingState = DrivingStates.TurningRight;
            }

            Console.WriteLine("Encoder countdown set to " + _state.EncoderCountdown);

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
            _state.Power = drivePower;
            _state.EncoderCalibration = 0;
            _state.DrivingState = DrivingStates.CalibratingDrive;
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> BeginCalibrateTurnHandler(BeginCalibrateTurn calibrate)
        {
            _state.Power = rotatePower;
            _state.EncoderCalibration = 0;
            _state.DrivingState = DrivingStates.CalibratingTurn;

            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> BeginCalibrateLeftHandler(BeginCalibrateLeft calibrate)
        {
            _state.Power = rotatePower;
            _state.EncoderCalibration = 0;
            _state.DrivingState = DrivingStates.CalibratingLeftTurn;
            yield break;
        }

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> BeginWaypointHandler(BeginWaypoint calibrate)
        {
            Console.WriteLine("Beginning waypoint navigation");
            
            // Save the waypoint object so we can send a success message later
            beginWaypoint = calibrate;

            prevWaypoint = calibrate.Body.PrevWaypoint;
            prevHeading = calibrate.Body.PrevHeading;

            waypoints = calibrate.Body.Waypoints;

            firstWaypoint = true;

            BeginNextWaypoint();

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
        public IEnumerator<ITask> SetManualCalibrationHandler(SetManualCalibration calibrate)
        {
            // 1 meter
            _state.DistanceCalibration = calibrate.Body.DistanceEncoder;

            // 360 degrees
            _state.TurningCalibration = calibrate.Body.TurnEncoder / (2 * Math.PI);

            _state.TurningLeftCalibration = calibrate.Body.TurnLeftEncoder / (2 * Math.PI);

            Console.WriteLine("Manual calibration: one meter set to " + _state.DistanceCalibration
               + "\n360 degrees Right set to " + _state.TurningCalibration + "\n360 degrees Left set to " + _state.TurningLeftCalibration);

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

        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public IEnumerator<ITask> SetLeftCalibrationHandler(SetLeftCalibration calibrate)
        {
            _state.TurningLeftCalibration = _state.EncoderCalibration / calibrate.Body.Radians;

            Console.WriteLine("Finished turn left calibration: encoderCalib was " + _state.EncoderCalibration +
    " and turn was " + (calibrate.Body.Radians * 180 / 3.14) + ", so left turn set to " + _state.TurningLeftCalibration);
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

        public double ToDegrees(double radians)
        {
            return radians / Math.PI * 180;
        }
    }


}
