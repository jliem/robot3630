//-----------------------------------------------------------------------
//  This file is part of the Microsoft Robotics Studio Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  $File: BlobTrackerCalibrateTypes.cs $ $Revision: 2 $
//-----------------------------------------------------------------------
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using W3C.Soap;
using blobtrackercalibrate = Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate;


namespace Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate
{

    /// <summary>
    /// BlobTrackerCalibrate Contract class
    /// </summary>
    public sealed class Contract
    {
        /// <summary>
        /// The Dss Service contract
        /// </summary>
        public const String Identifier = "http://schemas.microsoft.com/robotics/2007/04/blobtrackercalibrate.html";
    }
    /// <summary>
    /// The BlobTrackerCalibrate State
    /// </summary>
    [DataContract()]
    public class BlobTrackerCalibrateState
    {
        private bool _processing;
        [DataMember]
        public bool Processing
        {
            get { return _processing; }
            set { _processing = value; }
        }
    }

    [DataContract]
    public class UpdateProcessingRequest
    {
        public UpdateProcessingRequest()
        {
        }

        public UpdateProcessingRequest(bool processing)
        {
            _processing = processing;
        }

        private bool _processing;
        [DataMember,DataMemberConstructor]
        public bool Processing
        {
            get { return _processing; }
            set { _processing = value; }
        }

    }
    /// <summary>
    /// BlobTrackerCalibrate Main Operations Port
    /// </summary>
    [ServicePort]
    public class BlobTrackerCalibrateOperations : PortSet<DsspDefaultLookup, DsspDefaultDrop, Get, UpdateProcessing>
    {
    }
    /// <summary>
    /// BlobTrackerCalibrate Get Operation
    /// </summary>
    [Description("Gets the current state of the training service.")]
    public class Get : Get<GetRequestType, PortSet<BlobTrackerCalibrateState, Fault>>
    {
    }

    public class UpdateProcessing : Update<UpdateProcessingRequest, DsspResponsePort<DefaultUpdateResponseType>>
    {
        public UpdateProcessing()
        {
        }

        public UpdateProcessing(bool processing)
            : base(new UpdateProcessingRequest(processing))
        {
        }
    }
}
