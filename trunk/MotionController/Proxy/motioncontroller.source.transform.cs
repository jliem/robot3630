using System;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.Core.Transforms;

#if NET_CF20
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"cf.motioncontroller.y2008.m02, version=0.0.0.0, culture=neutral, publickeytoken=984c41a8958597ff")]
#else
[assembly: ServiceDeclaration(DssServiceDeclaration.Transform, SourceAssemblyKey = @"motioncontroller.y2008.m02, version=0.0.0.0, culture=neutral, publickeytoken=984c41a8958597ff")]
#endif
#if !URT_MINCLR
[assembly: System.Security.SecurityTransparent]
[assembly: System.Security.AllowPartiallyTrustedCallers]
#endif

namespace Dss.Transforms.TransformMotionController
{

    public class Transforms: TransformBase
    {

        public static object Transform_Robotics_CoroBot_MotionController_Proxy_MotionControllerState_TO_Robotics_CoroBot_MotionController_MotionControllerState(object transformFrom)
        {
            Robotics.CoroBot.MotionController.MotionControllerState target = new Robotics.CoroBot.MotionController.MotionControllerState();
            return target;
        }


        private static Robotics.CoroBot.MotionController.Proxy.MotionControllerState _instance_Robotics_CoroBot_MotionController_Proxy_MotionControllerState = new Robotics.CoroBot.MotionController.Proxy.MotionControllerState();
        public static object Transform_Robotics_CoroBot_MotionController_MotionControllerState_TO_Robotics_CoroBot_MotionController_Proxy_MotionControllerState(object ignore)
        {
            return _instance_Robotics_CoroBot_MotionController_Proxy_MotionControllerState;
        }

        static Transforms()
        {
            Register();
        }
        public static void Register()
        {
            AddProxyTransform(typeof(Robotics.CoroBot.MotionController.Proxy.MotionControllerState), Transform_Robotics_CoroBot_MotionController_Proxy_MotionControllerState_TO_Robotics_CoroBot_MotionController_MotionControllerState);
            AddSourceTransform(typeof(Robotics.CoroBot.MotionController.MotionControllerState), Transform_Robotics_CoroBot_MotionController_MotionControllerState_TO_Robotics_CoroBot_MotionController_Proxy_MotionControllerState);
        }
    }
}

