//------------------------------------------------------------------------------
// CoroWare
//
// Sample CoroBot simulation environment
//------------------------------------------------------------------------------

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using System;
using System.Collections.Generic;
using W3C.Soap;


namespace Robotics.SampleSimulation
{
    
    /// <summary>
    /// SampleSimulation Contract class
    /// </summary>
    public sealed class Contract
    {
        /// <summary>
        /// The Dss Service contract
        /// </summary>
        public const String Identifier = "http://schemas.tempuri.org/2008/01/samplesimulation.html";
    }

    /// <summary>
    /// The SampleSimulation State
    /// </summary>
    [DataContract()]
    public class SampleSimulationState
    {
    }

    /// <summary>
    /// SampleSimulation Main Operations Port
    /// </summary>
    [ServicePort()]
    public class SampleSimulationOperations : PortSet<
        DsspDefaultLookup, 
        DsspDefaultDrop, 
        Get>
    {
    }

    /// <summary>
    /// SampleSimulation Get Operation
    /// </summary>
    public class Get : Get<GetRequestType, PortSet<SampleSimulationState, Fault>>
    {
    }
}
