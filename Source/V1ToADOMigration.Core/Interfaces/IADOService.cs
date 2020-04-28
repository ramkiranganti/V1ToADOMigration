using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using V1ToADOMigration.Core.Entities;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IADOService
    {
        IList<WorkItem> MigrateAllV1DefectsByProjectName(IList<V1DefectAssetDto> defectAssetDtos);
    }
}
