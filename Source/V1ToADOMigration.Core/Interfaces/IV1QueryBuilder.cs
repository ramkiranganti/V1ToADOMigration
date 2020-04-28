using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IV1QueryBuilder
    {
        Query BuildDefectQuery(string projectName);
    }
}
