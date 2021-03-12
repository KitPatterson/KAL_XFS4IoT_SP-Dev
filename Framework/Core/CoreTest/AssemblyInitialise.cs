/***********************************************************************************************\
 * (C) Korala Associates Ltd 2019
\***********************************************************************************************/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XFS4IoT.UnitTestCore; 

namespace XFS4IoT
{
    [TestClass]
    public class Global
    {
        [AssemblyInitialize]
        public static void Initialise( TestContext _ )
        {
            UnitTesting.SetupUnitTests();
        }

        public static ILogger TheLogger { get; } = new TestLogger();
    }
}
