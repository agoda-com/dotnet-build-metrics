using System;

namespace Agoda.Tests.Metrics
{
    /// <summary>
    /// Provide information about the host we are running on
    /// </summary>
    public class HostContext
    {
        /// <summary>
        /// Name of the host
        /// </summary>
        public string Hostname { get; private set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Platform name
        /// </summary>
        public string Platform { get; private set; }

        /// <summary>
        /// What OS are we running
        /// </summary>
        public string Os { get; private set; }

        /// <summary>
        /// Number of processors
        /// </summary>
        public int CpuCount { get; private set; }

        /// <summary>
        /// Create and populate
        /// </summary>
        public HostContext()
        {
            Hostname = Environment.MachineName;
            Username = Environment.UserName;
            Platform = Environment.OSVersion.Platform.ToString();
            Os = Environment.OSVersion.VersionString;
            CpuCount = Environment.ProcessorCount;
        }

    }
}
