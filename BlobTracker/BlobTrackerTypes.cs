//-----------------------------------------------------------------------
//  This file is part of the Microsoft Robotics Studio Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  $File: BlobTrackerTypes.cs $ $Revision: 1 $
//-----------------------------------------------------------------------
using Microsoft.Ccr.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using System;
using System.Collections.Generic;
using W3C.Soap;
using blobtracker = Microsoft.Robotics.Services.Sample.BlobTracker;
using System.Drawing;
using System.ComponentModel;


namespace Microsoft.Robotics.Services.Sample.BlobTracker
{
    
    /// <summary>
    /// BlobTracker Contract class
    /// </summary>
    public sealed class Contract
    {
        /// <summary>
        /// The Dss Service contract
        /// </summary>
        public const String Identifier = "http://schemas.microsoft.com/robotics/2007/03/blobtracker.html";
    }
    /// <summary>
    /// The BlobTracker State
    /// </summary>
    [DataContract]
    [Description ("The blob tracker's state.")]
    public class BlobTrackerState
    {
        private bool _updateFrame;

        [Browsable(false)]
        public bool UpdateFrame
        {
            get { return _updateFrame; }
            set { _updateFrame = value; }
        }

        private List<ColorBin> _colorBins = new List<ColorBin>();
        [DataMember(IsRequired = true)]
        [Description ("The set of color bins.")]
        public List<ColorBin> ColorBins
        {
            get { return _colorBins; }
            set { _colorBins = value; }
        }

        private DateTime _timeStamp;
        [DataMember]
        [Browsable(false)]
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private List<FoundBlob> _results = new List<FoundBlob>();
        [DataMember(IsRequired = true)]
        [Description ("The list of matching blobs found.")]
        [Browsable(false)]
        public List<FoundBlob> Results
        {
            get { return _results; }
            set { _results = value; }
        }
    }

    [DataContract]
    [Description ("Specifies a color bin (set).")]
    public class ColorBin
    {
        private string _name;
        [DataMember]
        [Description ("Indicates the name of the color bin (set).")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
	
        private int _redMin;
        [DataMember]
        [Description("Indicates minimum red value of the color bin.")]
        public int RedMin
        {
            get { return _redMin; }
            set { _redMin = value; }
        }

        private int _redMax;
        [DataMember]
        [Description("Indicates maximum red value of the color bin.")]
        public int RedMax
        {
            get { return _redMax; }
            set { _redMax = value; }
        }

        private int _greenMin;
        [DataMember]
        [Description("Indicates minimum green value of the color bin.")]
        public int GreenMin
        {
            get { return _greenMin; }
            set { _greenMin = value; }
        }

        private int _greenMax;
        [DataMember]
        [Description("Indicates maximum green value of the color bin.")]
        public int GreenMax
        {
            get { return _greenMax; }
            set { _greenMax = value; }
        }

        private int _blueMin;
        [DataMember]
        [Description("Indicates minimum blue value of the color bin.")]
        public int BlueMin
        {
            get { return _blueMin; }
            set { _blueMin = value; }
        }

        private int _blueMax;
        [DataMember]
        [Description("Indicates maximum blue value of the color bin.")]
        public int BlueMax
        {
            get { return _blueMax; }
            set { _blueMax = value; }
        }

        public bool Test(Color color)
        {
            return Test(color.R, color.G, color.B);
        }

        public bool Test(int red, int green, int blue)
        {
            return (red >= _redMin && red < _redMax &&
                green >= _greenMin && green < _greenMax &&
                blue >= _blueMin && blue < _blueMax);
        }
    }

    [DataContract]
    public class FoundBlob
    {
        private int[] _xProjection;
        [Description ("Indicates the X projection.")]
        public int[] XProjection
        {
            get { return _xProjection; }
            set { _xProjection = value; }
        }

        private int[] _yProjection;
        [Description ("Indicates the Y projection.")]
        public int[] YProjection
        {
            get { return _yProjection; }
            set { _yProjection = value; }
        }

        private string _name;
        [DataMember]
        [Description ("Indicates the name of the blob.")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _meanX;
        [DataMember]
        [Description ("Indicates the X mean value.")]
        public double MeanX
        {
            get { return _meanX; }
            set { _meanX = value; }
        }

        private double _meanY;
        [DataMember]
        [Description ("Indicates the Y mean value.")]
        public double MeanY
        {
            get { return _meanY; }
            set { _meanY = value; }
        }

        private double _stdDevX;
        [DataMember]        
        [Description ("Indicates the X standard deviation value.")]
        public double StdDevX
        {
            get { return _stdDevX; }
            set { _stdDevX = value; }
        }

        private double _stdDevY;
        [DataMember]
        [Description ("Indicates the Y standard deviation value.")]
        public double StdDevY
        {
            get { return _stdDevY; }
            set { _stdDevY = value; }
        }

        private double _skewX;
        [DataMember]
        [Description ("Indicates the X skew value.")]
        public double SkewX
        {
            get { return _skewX; }
            set { _skewX = value; }
        }

        private double _skewY;
        [DataMember]
        [Description ("Indicates the Y skew value.")]
        public double SkewY
        {
            get { return _skewY; }
            set { _skewY = value; }
        }

        private double _area;
        [DataMember]
        [Description ("Indicates area. This is the number of pixels that contribute to the blob.")]
        public double Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public void CalculateMoments()
        {
            double square;

            double yOff = -_meanY;
            _stdDevY = 0.0;
            _skewY = 0.0;

            for (int y = 0; y < _yProjection.Length; y++, yOff++)
            {
                if (_yProjection[y] > 0)
                {
                    square = yOff * yOff * _yProjection[y];

                    _stdDevY += square;
                    _skewY += yOff * square;
                }
            }

            _stdDevY = Math.Sqrt(_stdDevY / _area);
            _skewY = _skewY / (_area * _stdDevY * _stdDevY * _stdDevY);

            double xOff = -_meanX;
            _stdDevX = 0.0;
            _skewX = 0.0;

            for (int x = 0; x < _xProjection.Length; x++, xOff++)
            {
                if (_xProjection[x] > 0)
                {
                    square = xOff * xOff * _xProjection[x];

                    _stdDevX += square;
                    _skewX += xOff * square;
                }
            }

            _stdDevX = Math.Sqrt(_stdDevX / _area);
            _skewX = _skewX / (_area * _stdDevX * _stdDevX * _stdDevX);
        }

        public void AddPixel(int x, int y)
        {
            _meanX += x;
            _meanY += y;
            _xProjection[x]++;
            _yProjection[y]++;
            _area++;
        }
    }

    [DataContract]
    public class ImageProcessedRequest
    {
        private DateTime _timeStamp;
        [DataMember]
        [Description("Indicates the time the image was processed.")]
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }

        private List<FoundBlob> _results = new List<FoundBlob>();
        [DataMember(IsRequired = true)]
        [Description("The list of blobs found.")]
        public List<FoundBlob> Results
        {
            get { return _results; }
            set { _results = value; }
        }
    }
	
    /// <summary>
    /// BlobTracker Main Operations Port
    /// </summary>
    [ServicePort]   
    public class BlobTrackerOperations : PortSet
    {
        public BlobTrackerOperations()
            : base( 
                typeof(DsspDefaultLookup), 
                typeof(DsspDefaultDrop), 
                typeof(Get), 
                typeof(ImageProcessed), 
                typeof(Subscribe),
                typeof(InsertBin),
                typeof(DeleteBin),
                typeof(UpdateBin)
            )
        {
        }

        public static implicit operator Port<ImageProcessed>(BlobTrackerOperations portSet)
        {
            if (portSet == null)
            {
                return null;
            }
            return (Port<ImageProcessed>)portSet[typeof(ImageProcessed)];
        }

        public void Post(ImageProcessed msg)
        {
            base.PostUnknownType(msg);
        }

        public static implicit operator Port<DsspDefaultDrop>(BlobTrackerOperations portSet)
        {
            if (portSet == null)
            {
                return null;
            }
            return (Port<DsspDefaultDrop>)portSet[typeof(DsspDefaultDrop)];
        }

        public void Post(DsspDefaultDrop msg)
        {
            base.PostUnknownType(msg);
        }
    }
    /// <summary>
    /// BlobTracker Get Operation
    /// </summary>
    [Description("Gets the current state of the service.")]
    public class Get : Get<GetRequestType, PortSet<BlobTrackerState, Fault>>
    {
    }

    public class Subscribe : Subscribe<SubscribeRequestType, PortSet<SubscribeResponseType, Fault>>
    {
    }
    [Description("Indicates when an image has been processed.")]
    public class ImageProcessed : Update<ImageProcessedRequest, PortSet<DefaultUpdateResponseType, Fault>>
    {
        public ImageProcessed()
        {
        }

        public ImageProcessed(ImageProcessedRequest body)
            : base(body)
        {
        }
    }

    [Description("Inserts a color bin for processing/analysis.")]
    public class InsertBin : Insert<ColorBin, PortSet<DefaultInsertResponseType, Fault>>
    {
    }

    [Description("Deletes a color bin for processing/analysis.")]
    public class DeleteBin : Delete<ColorBin, PortSet<DefaultDeleteResponseType, Fault>>
    {
    }

    [Description("Updates a color bin for processing/analysis.")]
    public class UpdateBin : Update<ColorBin, PortSet<DefaultUpdateResponseType, Fault>>
    {
    }
}
