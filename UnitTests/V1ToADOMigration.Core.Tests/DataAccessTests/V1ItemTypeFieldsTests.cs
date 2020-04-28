using Microsoft.VisualStudio.TestTools.UnitTesting;
using V1ToADOMigration.Core.DataAccess;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Tests.DataAccessTests
{
    [TestClass]
    public class V1ItemTypeFieldsTests
    {

        #region GetDefectFields Method Tests
        [TestMethod]
        public void GetDefectFieldsReturnsValidResponse()
        {
            // Arrange
            IV1ItemTypeFields v1ItemTypeFields = new V1ItemTypeFields();

            // Act
            var response = v1ItemTypeFields.GetDefectFields();

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, 17);
        }

        #endregion
    }
}
