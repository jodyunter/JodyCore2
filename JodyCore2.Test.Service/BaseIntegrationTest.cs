using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyCore2.Test.Service
{
    public class BaseIntegrationTest
    {
        [SetUp]
        public void SetupEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");
            Environment.SetEnvironmentVariable("CONNECTION_STRING", "DefaultConnectionString");

        }
    }
}
