using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;
using V1ToADOMigration.Core.Services;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Tests.ServicesTests
{
    [TestClass]
    public class V1ServiceTests
    {
        private IV1Service _v1Service;
        private IServices _mockServiceConnection;
        private IConnectionHelper _mockConnectionHelper;
        private IAssetType _mockAssetType;
        
        private string _projectName;
        private V1UserCredentials _credentials;        

        [TestInitialize]
        public void MyTestInitialize()
        {
            _projectName = "Test";
            _credentials = new V1UserCredentials("Test", "Test");

            _mockServiceConnection = MockRepository.Mock<IServices>();
            _mockConnectionHelper = MockRepository.Mock<IConnectionHelper>();
            _mockAssetType = MockRepository.Mock<IAssetType>();
        }

        #region GetAllDefectDetailsByProjectName Method Tests
        [TestMethod, ExpectedException(typeof(Exception))]
        public void GetAllDefectDetailsByProjectNameConnectionFailTest()
        {
            // Arrange
            IConnectionHelper _connectionHelper = new ConnectionHelper(_credentials);
            _v1Service = new V1Service(_projectName, _connectionHelper);

            // Act & Assert
            var response = _v1Service.GetAllDefectDetailsByProjectName();
        }

        [TestMethod]
        public void GetAllDefectDetailsByProjectNameReturnsValidResponseTest()
        {
            // Arrange
            V1ConnectionDto v1ConnectionDto = new V1ConnectionDto() { IsValid = true, ErrorMessage = string.Empty, Connection = _mockServiceConnection };
            _v1Service = new V1Service(_projectName, _mockConnectionHelper);
            Asset asset = new Asset(_mockAssetType);
            AssetList assets = new AssetList { asset };
            QueryResult result = new QueryResult(assets, 1, null);

            _mockConnectionHelper.Expect(x => x.GetV1Connection()).Return(v1ConnectionDto);
            _mockServiceConnection.Expect(x => x.Meta.GetAssetType("Defect")).IgnoreArguments().Return(_mockAssetType);
            _mockServiceConnection.Expect(x => x.Retrieve(null)).IgnoreArguments().Return(result);

            // Act
            var response = _v1Service.GetAllDefectDetailsByProjectName();

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
            _mockConnectionHelper.VerifyAllExpectations();
            _mockAssetType.VerifyAllExpectations();
            _mockServiceConnection.VerifyAllExpectations();
        }

        #endregion
    }
}
