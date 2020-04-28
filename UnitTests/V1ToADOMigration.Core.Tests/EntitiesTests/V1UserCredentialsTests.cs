using Microsoft.VisualStudio.TestTools.UnitTesting;
using V1ToADOMigration.Core.Entities;

namespace V1ToADOMigration.Core.Tests.EntitiesTests
{
    [TestClass]
    public class V1UserCredentialsTests
    {
        [TestMethod]
        public void V1UserCredentialsFromConfigSettingsTest()
        {
            // Arrange & act
            V1UserCredentials v1UserCredentials = new V1UserCredentials();

            // Assert
            Assert.IsTrue(v1UserCredentials.Username.Length > 0);
            Assert.IsTrue(v1UserCredentials.Password.Length > 0);
        }

        public void V1UserCredentialsFromCustomTest()
        {
            // Arrange & act
            V1UserCredentials v1UserCredentials = new V1UserCredentials("Test", "Test");

            // Assert
            Assert.AreEqual(v1UserCredentials.Username, "Test");
            Assert.AreEqual(v1UserCredentials.Password, "Test");
        }
    }
}
