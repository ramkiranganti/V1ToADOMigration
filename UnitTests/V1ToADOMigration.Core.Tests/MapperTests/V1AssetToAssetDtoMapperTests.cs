using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using V1ToADOMigration.Core.Interfaces;
using V1ToADOMigration.Core.Mappers;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Tests.MapperTests
{
    [TestClass]
    public class V1AssetToAssetDtoMapperTests
    {
        private IV1AssetToAssetDtoMapper _v1AssetToAssetDtoMapper;
        private IAssetType _mockAssetType;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _mockAssetType = MockRepository.Mock<IAssetType>();
        }

        #region MapDefectAssetToAssetDto Method Tests

        [TestMethod]
        public void MapDefectAssetToAssetDtoReturnsNullWithNullAssetType()
        {
            // Arrange
            IAssetType assetType = null;
            Oid oid = new Oid(_mockAssetType, 1, null);
            Asset asset = new Asset(oid);

            // Act
            _v1AssetToAssetDtoMapper = new V1AssetToAssetDtoMapper(assetType, asset);
            var response = _v1AssetToAssetDtoMapper.MapDefectAssetToAssetDto();

            // Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void MapDefectAssetToAssetDtoReturnsNullWithNullAsset()
        {
            // Arrange            
            Asset asset = null;

            // Act
            _v1AssetToAssetDtoMapper = new V1AssetToAssetDtoMapper(_mockAssetType, asset);
            var response = _v1AssetToAssetDtoMapper.MapDefectAssetToAssetDto();

            // Assert
            Assert.IsNull(response);
        }

        [TestMethod]
        public void MapDefectAssetToAssetDtoReturnsValidResponse()
        {
            // Arrange            
            //Oid oid = new Oid(_mockAssetType, 1, null);
            Asset asset = new Asset(_mockAssetType);

            _mockAssetType.Expect(x => x.GetAttributeDefinition(null)).IgnoreArguments().Return(null).Repeat.Any();
            // Act
            _v1AssetToAssetDtoMapper = new V1AssetToAssetDtoMapper(_mockAssetType, asset);
            var response = _v1AssetToAssetDtoMapper.MapDefectAssetToAssetDto();

            // Assert
            Assert.IsNotNull(response);
            _mockAssetType.VerifyAllExpectations();
        }

        #endregion MapDefectAssetToAssetDto Tests
    }
}
