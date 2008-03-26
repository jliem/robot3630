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
    
    /// <summary>
    /// The MotionController State
    /// </summary>
    [DataContract()]
    public class MotionControllerState
    {
    }
    
    /// <summary>
    /// MotionController Main Operations Port
    /// </summary>
    [ServicePort()]
    public class MotionControllerOperations : PortSet<DsspDefaultLookup, DsspDefaultDrop, Get>
    {
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
