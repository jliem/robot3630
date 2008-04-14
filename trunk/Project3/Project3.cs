//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
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
using project3 = Robotics.Project3;


namespace Robotics.Project3
{
    
    
    /// <summary>
    /// Implementation class for Project3
    /// </summary>
    [DisplayName("Project3")]
    [Description("The Project3 Service")]
    [Contract(Contract.Identifier)]
    public class Project3Service : DsspServiceBase
    {
        
        /// <summary>
        /// _state
        /// </summary>
        private Project3State _state = new Project3State();
        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/project3", AllowMultipleInstances=false)]
        private Project3Operations _mainPort = new Project3Operations();
        
        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public Project3Service(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {
        }
        
        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
			base.Start();
			// Add service specific initialization here.
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
    }
}
