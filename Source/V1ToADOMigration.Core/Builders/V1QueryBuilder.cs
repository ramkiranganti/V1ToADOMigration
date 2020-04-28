using V1ToADOMigration.Core.DataAccess;
using V1ToADOMigration.Core.Interfaces;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Builders
{
    public class V1QueryBuilder : IV1QueryBuilder
    {
        private const string SCOPENAME = "Scope.Name";

        private IAssetType _assetType;
        public V1QueryBuilder(IAssetType assetType)
        {
            _assetType = assetType;
        }
        public Query BuildDefectQuery(string projectName)
        {
            Query query = null;

            if (!string.IsNullOrEmpty(projectName) && _assetType != null)
            {
                query = new Query(_assetType);

                //Since we need to get a specific Story or Stories that meet some criteria, we set up a filter
                FilterTerm term = new FilterTerm(_assetType.GetAttributeDefinition(SCOPENAME));
                term.Equal(projectName);
                query.Filter = term;

                //Add them to the query object since we need to get these specific attributes
                V1ItemTypeFields fieldNames = new V1ItemTypeFields();

                foreach (string fieldName in fieldNames.GetDefectFields())
                {
                    query.Selection.Add(_assetType.GetAttributeDefinition(fieldName));
                }
            }

            return query;
        }
    }
}