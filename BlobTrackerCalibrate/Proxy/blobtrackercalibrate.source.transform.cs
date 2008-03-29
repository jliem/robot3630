using System;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.Core.Transforms;

#if NET_CF20
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"cf.blobtrackercalibrate.y2007.m04, version=0.0.0.0, culture=neutral, publickeytoken=d159440a8502d2da")]
#else
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"blobtrackercalibrate.y2007.m04, version=0.0.0.0, culture=neutral, publickeytoken=d159440a8502d2da")]
#endif
#if !URT_MINCLR
[assembly: System.Security.SecurityTransparent]
[assembly: System.Security.AllowPartiallyTrustedCallers]
#endif

namespace Dss.Transforms.TransformBlobTrackerCalibrate
{

    public class Transforms: TransformBase
    {

        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_BlobTrackerCalibrateState_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_BlobTrackerCalibrateState(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.BlobTrackerCalibrateState target = new Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.BlobTrackerCalibrateState();
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.BlobTrackerCalibrateState from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.BlobTrackerCalibrateState;
            target.Processing = from.Processing;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_BlobTrackerCalibrateState_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_BlobTrackerCalibrateState(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.BlobTrackerCalibrateState target = new Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.BlobTrackerCalibrateState();
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.BlobTrackerCalibrateState from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.BlobTrackerCalibrateState;
            target.Processing = from.Processing;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_UpdateProcessingRequest_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_UpdateProcessingRequest(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.UpdateProcessingRequest target = new Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.UpdateProcessingRequest();
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.UpdateProcessingRequest from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.UpdateProcessingRequest;
            target.Processing = from.Processing;
            return target;
        }


        public static object Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_UpdateProcessingRequest_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_UpdateProcessingRequest(object transformFrom)
        {
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.UpdateProcessingRequest target = new Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.UpdateProcessingRequest();
            Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.UpdateProcessingRequest from = transformFrom as Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.UpdateProcessingRequest;
            target.Processing = from.Processing;
            return target;
        }

        static Transforms()
        {
            Register();
        }
        public static void Register()
        {
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.BlobTrackerCalibrateState), Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_BlobTrackerCalibrateState_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_BlobTrackerCalibrateState);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.BlobTrackerCalibrateState), Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_BlobTrackerCalibrateState_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_BlobTrackerCalibrateState);
            AddProxyTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.Proxy.UpdateProcessingRequest), Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_UpdateProcessingRequest_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_UpdateProcessingRequest);
            AddSourceTransform(typeof(Microsoft.Robotics.Services.Sample.BlobTrackerCalibrate.UpdateProcessingRequest), Transform_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_UpdateProcessingRequest_TO_Microsoft_Robotics_Services_Sample_BlobTrackerCalibrate_Proxy_UpdateProcessingRequest);
        }
    }
}

