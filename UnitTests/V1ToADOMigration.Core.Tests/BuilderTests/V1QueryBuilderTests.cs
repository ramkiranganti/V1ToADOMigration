using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using V1ToADOMigration.Core.Builders;
using V1ToADOMigration.Core.Interfaces;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Tests.BuilderTests
{
    [TestClass]
    public class V1QueryBuilderTests
    {
        private IV1QueryBuilder _v1QueryBuilder;
        private IAssetType _mockAssetType;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _mockAssetType = MockRepository.Mock<IAssetType>();
        }

        #region BuildDefectQuery Method Tests

        [TestMethod]
        public void BuildDefectQueryReturnsNullWithNullAssetType()
        {
            // Arrange
            string projectName = "Test";
            IAssetType assetType = null;

            // Act
            _v1QueryBuilder = new V1QueryBuilder(assetType);
            var response = _v1QueryBuilder.BuildDefectQuery(projectName);

            // Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void BuildDefectQueryReturnsNullWithNullProjectName()
        {
            // Arrange
            string projectName = null;
            _mockAssetType.Expect(x => x.GetAttributeDefinition(null)).IgnoreArguments().Return(null).Repeat.Any();

            // Act
            _v1QueryBuilder = new V1QueryBuilder(_mockAssetType);
            var response = _v1QueryBuilder.BuildDefectQuery(projectName);

            // Assert
            Assert.IsNull(response);
            _mockAssetType.VerifyAllExpectations();
        }

        [TestMethod]
        public void BuildDefectQueryReturnsValidResponse()
        {
            // Arrange
            string projectName = "Test";
            _mockAssetType.Expect(x => x.GetAttributeDefinition(null)).IgnoreArguments().Return(null).Repeat.Any();

            // Act
            _v1QueryBuilder = new V1QueryBuilder(_mockAssetType);
            var response = _v1QueryBuilder.BuildDefectQuery(projectName);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Selection.Count, 17);
            _mockAssetType.VerifyAllExpectations();
        }


        #endregion BuildDefectQuery Tests
    }
}
