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
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using System;
using System.Collections.Generic;
using W3C.Soap;
using coord = Robotics.CoroBot.Coordinator;


namespace Robotics.CoroBot.Coordinator
{
    
    
    /// <summary>
    /// Coordinator Contract class
    /// </summary>
    public sealed class Contract
    {
        
        /// <summary>
        /// The Dss Service contract
        /// </summary>
        public const String Identifier = "http://schemas.tempuri.org/2008/04/coordinator.html";
    }
    
    /// <summary>
    /// The Coordinator State
    /// </summary>
    [DataContract()]
    public class CoordinatorState
    {
    }
    
    /// <summary>
    /// Coordinator Main Operations Port
    /// </summary>
    [ServicePort()]
    public class CoordinatorOperations : PortSet<DsspDefaultLookup, DsspDefaultDrop, Get>
    {
    }
    
    /// <summary>
    /// Coordinator Get Operation
    /// </summary>
    public class Get : Get<GetRequestType, PortSet<CoordinatorState, Fault>>
    {
        
        /// <summary>
        /// Coordinator Get Operation
        /// </summary>
        public Get()
        {
        }
        
        /// <summary>
        /// Coordinator Get Operation
        /// </summary>
        public Get(Microsoft.Dss.ServiceModel.Dssp.GetRequestType body) : 
                base(body)
        {
        }
        
        /// <summary>
        /// Coordinator Get Operation
        /// </summary>
        public Get(Microsoft.Dss.ServiceModel.Dssp.GetRequestType body, Microsoft.Ccr.Core.PortSet<CoordinatorState,W3C.Soap.Fault> responsePort) : 
                base(body, responsePort)
        {
        }
    }
}