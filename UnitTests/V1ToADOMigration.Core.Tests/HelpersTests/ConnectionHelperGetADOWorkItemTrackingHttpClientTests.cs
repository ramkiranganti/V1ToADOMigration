using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Tests.HelpersTests
{
    [TestClass]
    public class ConnectionHelperGetADOWorkItemTrackingHttpClientTests
    {
        IConnectionHelper _connectionHelper;


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetADOWorkItemTrackingHttpClientNullUriThrowsExceptionTest()
        {
            // Arrange 
            ConfigHelper.ADOUrl = null;
            _connectionHelper = new ConnectionHelper();

            // Act & Assert
            var response = _connectionHelper.GetADOWorkItemTrackingHttpClient();
        }

        [TestMethod, ExpectedException(typeof(UriFormatException))]
        public void GetADOWorkItemTrackingHttpClientEmptyUriThrowsExceptionTest()
        {
            // Arrange 
            ConfigHelper.ADOUrl = string.Empty;
            _connectionHelper = new ConnectionHelper();

            // Act & Assert
            var response = _connectionHelper.GetADOWorkItemTrackingHttpClient();
        }

        [TestMethod]
        public void GetADOWorkItemTrackingHttpClientNullTokenReturnsResponseTest()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = null;
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOWorkItemTrackingHttpClient();

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetADOWorkItemTrackingHttpClientEmptyTokenReturnsResponseTest()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = string.Empty;
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOWorkItemTrackingHttpClient();

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void GetADOWorkItemTrackingHttpClientReturnsResponseTests()
        {
            // Arrange
            ConfigHelper.ADOUrl = ConfigHelper.GetAppSetting("ADOUrl");
            ConfigHelper.ADOToken = ConfigHelper.GetAppSetting("ADOToken");
            _connectionHelper = new ConnectionHelper();

            // Act
            var response = _connectionHelper.GetADOWorkItemTrackingHttpClient();

            // Assert
            Assert.IsNotNull(response);

        }
    }
}
