using System.Collections.Generic;
using V1ToADOMigration.Core.Entities;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IV1Service
    {
        IList<V1DefectAssetDto> GetAllDefectDetailsByProjectName();
    }
}
