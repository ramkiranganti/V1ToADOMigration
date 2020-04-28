using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Tests.HelpersTests
{
    [TestClass]
    public class ConnectionHelperGetADOProjectClientTests
    {
        IConnectionHelper _connectionHelper;


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetADOProjectClientNullUriThrowsExceptionTest()
        {
            // Arrange 
            ConfigHelper.ADOUrl = null;
            _connectionHelper = new ConnectionHelper();

            // Act & Assert
            var response = _connectionHelper.GetADOProjectClient();
        }

        [TestMethod, ExpectedException(typeof(UriFormatException))]
        public void GetADOProjectClientEmptyUriThrowsExceptionTest()
        {
            // Arrange 
            ConfigHelper.ADOUrl = string.Empty;
            _connectionHelper = new ConnectionHelper();

            // Act & Assert
            var response = _connectionHelper.GetADOProjectClient();
        }

        [TestMethod]
        public void GetADOProjectClientNullTokenReturnsResponseTest()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = null;
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOProjectClient();

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetADOProjectClientEmptyTokenReturnsResponseTest()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = string.Empty;
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOProjectClient();

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetADOProjectClientReturnsResponseTests()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = ConfigHelper.GetAppSetting("ADOToken");
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOProjectClient();

            // Assert
            Assert.IsNotNull(response);

        }
    }
}
