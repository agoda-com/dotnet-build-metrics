using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Agoda.Builds.Metrics
{
    public class MeasureBuildTime : Task
    {
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        [Output]
        public string DebugOutput { get; set; }
        public override bool Execute()
        {
            DebugOutput = (DateTime.Parse(EndDateTime) - DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();
            
            return true;
        }
    }
}
