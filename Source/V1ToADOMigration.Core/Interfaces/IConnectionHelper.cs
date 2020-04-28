using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using V1ToADOMigration.Core.Entities;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IConnectionHelper
    {
        V1ConnectionDto GetV1Connection();
        ProjectHttpClient GetADOProjectClient();
        WorkItemTrackingHttpClient GetADOWorkItemTrackingHttpClient();
    }
}
