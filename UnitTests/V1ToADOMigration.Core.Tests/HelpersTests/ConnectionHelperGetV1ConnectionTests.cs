using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Tests.HelpersTests
{
    [TestClass]
    public class ConnectionHelperGetV1ConnectionTests
    {
        IConnectionHelper _connectionHelper;        

        #region GetV1Connection Method Tests
        [TestMethod]
        public void GetV1ConnectionReturnNoConnectionWithNullCredentials()
        {
            // Arrange           
            _connectionHelper = new ConnectionHelper(new V1UserCredentials(null, null));

            // Act
            var response = _connectionHelper.GetV1Connection();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(response.ErrorMessage, "Invalid Username/Password.");
            Assert.IsNull(response.Connection);
        }

        [TestMethod]
        public void GetV1ConnectionReturnNoConnectionWithEmptyCredentials()
        {
            // Arrange           
            _connectionHelper = new ConnectionHelper(new V1UserCredentials(string.Empty, string.Empty));

            // Act
            var response = _connectionHelper.GetV1Connection();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(response.ErrorMessage, "Invalid Username/Password.");
            Assert.IsNull(response.Connection);
        }

        [TestMethod]
        public void GetV1ConnectionReturnNoConnectionWithInvalidCredentials()
        {
            // Arrange            
            _connectionHelper = new ConnectionHelper(new V1UserCredentials("Test", "Test"));

            // Act
            var response = _connectionHelper.GetV1Connection();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsValid);
            Assert.AreEqual(response.ErrorMessage, "Unauthorized");
            Assert.IsNull(response.Connection);
        }

        [TestMethod]
        public void GetV1ConnectionReturnConnectionSuccess()
        {
            // Arrange            
            _connectionHelper = new ConnectionHelper(new V1UserCredentials("yourUsername", "yourPassowrd"));
            IServices _mockServiceConnection = MockRepository.Mock<IServices>();

            IAssetType _mockAssetType = MockRepository.Mock<IAssetType>();
            Oid oid = new Oid(_mockAssetType, 1, null);
            _mockServiceConnection.Expect(x => x.LoggedIn).Return(oid);
            _mockServiceConnection.Expect(x => x.Meta.GetAssetType(null)).IgnoreArguments().Return(_mockAssetType);
            // Act
            var response = _connectionHelper.GetV1Connection();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsValid);
            Assert.AreEqual(response.ErrorMessage, string.Empty);
            Assert.IsNotNull(response.Connection);
            _mockAssetType.VerifyAllExpectations();
            _mockServiceConnection.VerifyAllExpectations();

        }



        #endregion GetV1Connection Tests
    }
}
