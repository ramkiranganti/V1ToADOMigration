using System.Collections.Generic;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using V1ToADOMigration.Core.Builders;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Tests.BuilderTests
{
    [TestClass]
    public class ADOIssuePatchDocumentBuilderTests
    {
        IADOPatchDocumentBuilder _aDOIssuePatchDocumentBuilder;

        [TestMethod]
        public void BuildJsonPatchDocumentsReturnsEmptyWithNullListDefectAssetDtos()
        {
            // Arrange
            IList<V1DefectAssetDto> v1DefectAssetDtos = null;
            //V1DefectAssetDto v1DefectAssetDto = null;
            _aDOIssuePatchDocumentBuilder = new ADOIssuePatchDocumentBuilder(v1DefectAssetDtos);

            // Act
            var response = _aDOIssuePatchDocumentBuilder.BuildJsonPatchDocuments();

            // Assert
            Assert.AreEqual(response.Count, 0);
        }

        [TestMethod]
        public void BuildJsonPatchDocumentsReturnsEmptyWithEmptyListDefectAssetDtos()
        {
            // Arrange
            IList<V1DefectAssetDto> v1DefectAssetDtos = new List<V1DefectAssetDto>();
            //V1DefectAssetDto v1DefectAssetDto = null;
            _aDOIssuePatchDocumentBuilder = new ADOIssuePatchDocumentBuilder(v1DefectAssetDtos);

            // Act
            var response = _aDOIssuePatchDocumentBuilder.BuildJsonPatchDocuments();

            // Assert
            Assert.AreEqual(response.Count, 0);
        }

        [TestMethod]
        public void BuildJsonPatchDocumentsReturnsEmptyWithOneNullDefectAssetDtoInListDefectAssetDtos()
        {
            // Arrange
            IList<V1DefectAssetDto> v1DefectAssetDtos = new List<V1DefectAssetDto>();
            V1DefectAssetDto v1DefectAssetDto = null;
            v1DefectAssetDtos.Add(v1DefectAssetDto);
            _aDOIssuePatchDocumentBuilder = new ADOIssuePatchDocumentBuilder(v1DefectAssetDtos);

            // Act
            var response = _aDOIssuePatchDocumentBuilder.BuildJsonPatchDocuments();

            // Assert
            Assert.AreEqual(response.Count, 1);
            Assert.IsNull(response[0]);
        }

        [TestMethod]
        public void BuildJsonPatchDocumentsReturnsEmptyWithOneValidDefectAssetDtoInListDefectAssetDtos()
        {
            // Arrange
            IList<V1DefectAssetDto> v1DefectAssetDtos = new List<V1DefectAssetDto>();
            V1DefectAssetDto v1DefectAssetDto = new V1DefectAssetDto() { ItemName = "Test", ItemNumber = "1" };
            v1DefectAssetDtos.Add(v1DefectAssetDto);
            _aDOIssuePatchDocumentBuilder = new ADOIssuePatchDocumentBuilder(v1DefectAssetDtos);

            // Act
            IList<JsonPatchDocument> response = _aDOIssuePatchDocumentBuilder.BuildJsonPatchDocuments();

            // Assert            
            Assert.IsNotNull(response[0]);
            Assert.AreEqual(response.Count, 1);
            Assert.AreEqual(response[0][0].Operation, Operation.Add);
            Assert.AreEqual(response[0][0].Path, "/fields/System.Title");
            Assert.AreEqual(response[0][0].Value, "PROJECT: , ITEM NUMBER: 1, ITEM NAME: Test");
        }
    }
}
