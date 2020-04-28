using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Interfaces;
using V1ToADOMigration.Core.Services;

namespace V1ToADOMigration.Core.Tests.ServicesTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ADOServiceTests
    {

        private IADOService _aDOService;
        private IConnectionHelper _mockConnectionHelper;
        private IADOPatchDocumentBuilder _mockADOIssuePatchDocumentBuilder;
        private string _projectName;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _projectName = "Test Project";
            _mockConnectionHelper = MockRepository.Mock<IConnectionHelper>();
            _mockADOIssuePatchDocumentBuilder = MockRepository.Mock<IADOPatchDocumentBuilder>();
        }

        #region MigrateAllV1DefectsByProjectName Method Tests
        [TestMethod]
        public void ADOServiceReturnsNoWorkItemsForNullDefectAssetDtoTest()
        {
            // Arrange
            _aDOService = new ADOService(_projectName, _mockConnectionHelper);
            IList<V1DefectAssetDto> v1DefectAssetDtos = null;

            // Act
            IList<WorkItem> response = _aDOService.MigrateAllV1DefectsByProjectName(v1DefectAssetDtos);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 0);
        }


        [TestMethod]
        public void ADOServiceReturnsOneWorkItemsForOneDefectAssetDtoTest()
        {
            // Arrange
            _aDOService = new ADOService(_projectName, _mockConnectionHelper);
            IList<V1DefectAssetDto> v1DefectAssetDtos = new List<V1DefectAssetDto>();
            V1DefectAssetDto v1DefectAssetDto = new V1DefectAssetDto() { ItemName = "Test", ItemNumber = "1" };
            v1DefectAssetDtos.Add(v1DefectAssetDto);

            _mockConnectionHelper.Expect(x => x.GetADOWorkItemTrackingHttpClient()).Return(null);

            // Act            
            IList<WorkItem> response = _aDOService.MigrateAllV1DefectsByProjectName(v1DefectAssetDtos);

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 1);
        }

        #endregion
    }
}
