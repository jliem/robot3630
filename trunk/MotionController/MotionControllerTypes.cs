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
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using System;
using System.Collections.Generic;
using W3C.Soap;
using motioncontroller = Robotics.CoroBot.MotionController;


namespace Robotics.CoroBot.MotionController
{
    
    
    /// <summary>
    /// MotionController Contract class
    /// </summary>
    public sealed class Contract
    {
        
        /// <summary>
        /// The Dss Service contract
        /// </summary>
        public const String Identifier = "http://schemas.tempuri.org/2008/02/motioncontroller.html";
    }

    [DataContract]
    public enum DrivingStates { Stopped, MovingForward, MovingBackward, CalibratingDrive, CalibratingTurn, TurningLeft, TurningRight }
    
    /// <summary>
    /// The MotionController State
    /// </summary>
    [DataContract]
    public class MotionControllerState
    {
        private double _distanceCalibration;
        private double _turningCalibration;
        private DrivingStates _drivingState;
        private double _encoderCountdown;
        private double _encoderCalibration;
        private double _power;

        [DataMember]
        public double DistanceCalibration { get { return _distanceCalibration; } set { _distanceCalibration = value; } }

        [DataMember]
        public double TurningCalibration { get { return _turningCalibration; } set { _turningCalibration = value; } }

        [DataMember]
        public DrivingStates DrivingState { get { return _drivingState; } set { _drivingState = value; } }

        [DataMember]
        public double EncoderCountdown { get { return _encoderCountdown; } set { _encoderCountdown = value; } }

        [DataMember]
        public double EncoderCalibration { get { return _encoderCalibration; } set { _encoderCalibration = value; } }

        [DataMember]
        public double Power { get { return _power; } set { _power = value; } }
    }
    
    /// <summary>
    /// MotionController Main Operations Port
    /// </summary>
    [ServicePort()]
    public class MotionControllerOperations : PortSet<
        DsspDefaultLookup,
        DsspDefaultDrop,
        Get,
        Drive,
        Turn,
        BeginCalibrateDrive,
        BeginCalibrateTurn,
        SetDriveCalibration,
        SetTurnCalibration,
        Stop>
    {
    }

    public class Drive : Update<DriveRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public Drive() { }

        public Drive(DriveRequest body)
            : base(body)
        {

        }
    }

    public class Turn : Update<TurnRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public Turn() { }

        public Turn(TurnRequest body)
            : base(body)
        {

        }
    }

    public class BeginCalibrateDrive : Update<BeginCalibrateDriveRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public BeginCalibrateDrive() { }

        public BeginCalibrateDrive(BeginCalibrateDriveRequest body)
            : base(body)
        {

        }
    }

    public class BeginCalibrateTurn : Update<BeginCalibrateTurnRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public BeginCalibrateTurn() { }

        public BeginCalibrateTurn(BeginCalibrateTurnRequest body)
            : base(body)
        {

        }
    }

    public class SetDriveCalibration : Update<SetDriveCalibrationRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public SetDriveCalibration() { }

        public SetDriveCalibration(SetDriveCalibrationRequest body)
            : base(body)
        {

        }
    }

    public class SetTurnCalibration : Update<SetTurnCalibrationRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public SetTurnCalibration() { }

        public SetTurnCalibration(SetTurnCalibrationRequest body)
            : base(body)
        {

        }
    }

    public class Stop : Update<StopRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public Stop() : base(new StopRequest())
        {

        }
    }

    [DataContract]
    public class DriveRequest
    {
        private double _distance;
        public double Distance { get { return _distance; } set { _distance = value; } }

        private double _power;
        public double Power { get { return _power; } set { _power = value; } }

        public DriveRequest() { }

        public DriveRequest(double distance)
        {
            Distance = distance;
        }

        public DriveRequest(double distance, double power)
        {
            Distance = distance;
            Power = power;
        }
    }

    [DataContract]
    public class TurnRequest
    {
        private double _radians;
        public double Radians { get { return _radians; } set { _radians = value; } }

        private double _power;
        public double Power { get { return _power; } set { _power = value; } }

        public TurnRequest() { }

        public TurnRequest(double radians)
        {
            Radians = radians;
        }

        public TurnRequest(double radians, double power)
        {
            Radians = radians;
            Power = power;
        }
    }

    [DataContract]
    public class BeginCalibrateDriveRequest
    {
        public BeginCalibrateDriveRequest() { }
    }

    [DataContract]
    public class BeginCalibrateTurnRequest
    {
        public BeginCalibrateTurnRequest() { }
    }

    [DataContract]
    public class SetDriveCalibrationRequest
    {
        private double _distance;
        public double Distance { get { return _distance; } set { _distance = value; } }

        public SetDriveCalibrationRequest() { }

        public SetDriveCalibrationRequest(double distance)
        {
            Distance = distance;
        }
    }

    [DataContract]
    public class SetTurnCalibrationRequest
    {
        private double _radians;
        public double Radians { get { return _radians; } set { _radians = value; } }

        public SetTurnCalibrationRequest() { }

        public SetTurnCalibrationRequest(double radians)
        {
            Radians = radians;
        }
    }

    [DataContract]
    public class StopRequest
    {
        public StopRequest() { }
    }

    /// <summary>
    /// MotionController Get Operation
    /// </summary>
    public class Get : Get<GetRequestType, PortSet<MotionControllerState, Fault>>
    {
        
        /// <summary>
        /// MotionController Get Operation
        /// </summary>
        public Get()
        {
        }
        
        /// <summary>
        /// MotionController Get Operation
        /// </summary>
        public Get(Microsoft.Dss.ServiceModel.Dssp.GetRequestType body) : 
                base(body)
        {
        }
        
        /// <summary>
        /// MotionController Get Operation
        /// </summary>
        public Get(Microsoft.Dss.ServiceModel.Dssp.GetRequestType body, Microsoft.Ccr.Core.PortSet<MotionControllerState,W3C.Soap.Fault> responsePort) : 
                base(body, responsePort)
        {
        }
    }
}