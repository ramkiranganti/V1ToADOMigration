using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V1ToADOMigration.Core.Entities;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IV1AssetToAssetDtoMapper
    {
        V1DefectAssetDto MapDefectAssetToAssetDto();
    }
}
