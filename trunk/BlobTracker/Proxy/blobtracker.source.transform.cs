using System;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.Core.Transforms;

#if NET_CF20
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"cf.blobtracker.y2007.m03, version=0.0.0.0, culture=neutral, publickeytoken=d159440a8502d2da")]
#else
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"blobtracker.y2007.m03, version=0.0.0.0, culture=neutral, publickeytoken=d159440a8502d2da")]
#endif
#if !URT_MINCLR
[assembly: System.Security.SecurityTransparent]
[assembly: System.Security.AllowPartiallyTrustedCallers]
#endif

namespace Dss.Transforms.TransformBlobTracker
{

    public class Transforms: TransformBase
    {

        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_BlobTrackerState_TO_Microsoft_Robotics_Services_Sample_BlobTracker_BlobTrackerState(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.BlobTrackerState target = new Microsoft.Robotics.Services.Sample.BlobTracker.BlobTrackerState();
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.BlobTrackerState from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.BlobTrackerState;

            // copy IEnumerable ColorBins
            if (from.ColorBins != null)
            {
                target.ColorBins = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin elem in from.ColorBins)
                {
                    target.ColorBins.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin(elem));
                }
            }
            target.TimeStamp = from.TimeStamp;

            // copy IEnumerable Results
            if (from.Results != null)
            {
                target.Results = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob elem in from.Results)
                {
                    target.Results.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob(elem));
                }
            }
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_BlobTrackerState_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_BlobTrackerState(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.BlobTrackerState target = new Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.BlobTrackerState();
            Microsoft.Robotics.Services.Sample.BlobTracker.BlobTrackerState from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.BlobTrackerState;

            // copy IEnumerable ColorBins
            if (from.ColorBins != null)
            {
                target.ColorBins = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin elem in from.ColorBins)
                {
                    target.ColorBins.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin(elem));
                }
            }
            target.TimeStamp = from.TimeStamp;

            // copy IEnumerable Results
            if (from.Results != null)
            {
                target.Results = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob elem in from.Results)
                {
                    target.Results.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob(elem));
                }
            }
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ImageProcessedRequest_TO_Microsoft_Robotics_Services_Sample_BlobTracker_ImageProcessedRequest(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.ImageProcessedRequest target = new Microsoft.Robotics.Services.Sample.BlobTracker.ImageProcessedRequest();
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ImageProcessedRequest from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ImageProcessedRequest;
            target.TimeStamp = from.TimeStamp;

            // copy IEnumerable Results
            if (from.Results != null)
            {
                target.Results = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob elem in from.Results)
                {
                    target.Results.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob(elem));
                }
            }
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_ImageProcessedRequest_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ImageProcessedRequest(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ImageProcessedRequest target = new Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ImageProcessedRequest();
            Microsoft.Robotics.Services.Sample.BlobTracker.ImageProcessedRequest from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.ImageProcessedRequest;
            target.TimeStamp = from.TimeStamp;

            // copy IEnumerable Results
            if (from.Results != null)
            {
                target.Results = new System.Collections.Generic.List<Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob>();
                foreach(Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob elem in from.Results)
                {
                    target.Results.Add((elem == null) ? null : (Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob)Transform_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob(elem));
                }
            }
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin target = new Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin();
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin;
            target.Name = from.Name;
            target.RedMin = from.RedMin;
            target.RedMax = from.RedMax;
            target.GreenMin = from.GreenMin;
            target.GreenMax = from.GreenMax;
            target.BlueMin = from.BlueMin;
            target.BlueMax = from.BlueMax;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin target = new Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin();
            Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin;
            target.Name = from.Name;
            target.RedMin = from.RedMin;
            target.RedMax = from.RedMax;
            target.GreenMin = from.GreenMin;
            target.GreenMax = from.GreenMax;
            target.BlueMin = from.BlueMin;
            target.BlueMax = from.BlueMax;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob target = new Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob();
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob;
            target.Name = from.Name;
            target.MeanX = from.MeanX;
            target.MeanY = from.MeanY;
            target.StdDevX = from.StdDevX;
            target.StdDevY = from.StdDevY;
            target.SkewX = from.SkewX;
            target.SkewY = from.SkewY;
            target.Area = from.Area;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob target = new Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob();
            Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob;
            target.Name = from.Name;
            target.MeanX = from.MeanX;
            target.MeanY = from.MeanY;
            target.StdDevX = from.StdDevX;
            target.StdDevY = from.StdDevY;
            target.SkewX = from.SkewX;
            target.SkewY = from.SkewY;
            target.Area = from.Area;
            return target;
        }

        static Transforms()
        {
            Register();
        }
        public static void Register()
        {
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.BlobTrackerState), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_BlobTrackerState_TO_Microsoft_Robotics_Services_Sample_BlobTracker_BlobTrackerState);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.BlobTrackerState), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_BlobTrackerState_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_BlobTrackerState);
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ImageProcessedRequest), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ImageProcessedRequest_TO_Microsoft_Robotics_Services_Sample_BlobTracker_ImageProcessedRequest);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.ImageProcessedRequest), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_ImageProcessedRequest_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ImageProcessedRequest);
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.ColorBin), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.ColorBin), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_ColorBin_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_ColorBin);
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.Proxy.FoundBlob), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTracker.FoundBlob), Transform_Microsoft_Robotics_Services_Sample_BlobTracker_FoundBlob_TO_Microsoft_Robotics_Services_Sample_BlobTracker_Proxy_FoundBlob);
        }
    }
}

