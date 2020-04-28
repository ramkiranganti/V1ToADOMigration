using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Interfaces;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Mappers
{
    public class V1AssetToAssetDtoMapper : IV1AssetToAssetDtoMapper
    {
        private IAssetType _assetType;
        private Asset _asset;
        public V1AssetToAssetDtoMapper(IAssetType assetType, Asset asset)
        {
            _assetType = assetType;
            _asset = asset;
        }
        public V1DefectAssetDto MapDefectAssetToAssetDto()
        {
            V1DefectAssetDto defectAssetDto = null;

            if (_asset != null && _assetType != null)
            {
                defectAssetDto = new V1DefectAssetDto();
                defectAssetDto.OidToken = _asset.Oid.Token;
                if (_asset.Attributes.Count > 0)
                {
                    defectAssetDto.ItemName = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Name")).Value.ToString() : string.Empty;

                    defectAssetDto.ItemNumber = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Number")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Number")).Value.ToString() : string.Empty;

                    defectAssetDto.EstimatedPoints = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Estimate")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Estimate")).Value.ToString() : string.Empty;

                    defectAssetDto.AssetState = (_asset.GetAttribute(_assetType.GetAttributeDefinition("AssetState")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("AssetState")).Value.ToString() : string.Empty;

                    defectAssetDto.ScopeName = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Scope.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Scope.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.StatusName = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Status.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Status.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.PriorityName = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Priority.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Priority.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.TaggedWith = (_asset.GetAttribute(_assetType.GetAttributeDefinition("TaggedWith")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("TaggedWith")).Value.ToString() : string.Empty;

                    defectAssetDto.Description = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Description")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Description")).Value.ToString() : string.Empty;

                    defectAssetDto.SourceName = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Source.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Source.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.VersionAffected = (_asset.GetAttribute(_assetType.GetAttributeDefinition("VersionAffected")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("VersionAffected")).Value.ToString() : string.Empty;

                    defectAssetDto.FoundBy = (_asset.GetAttribute(_assetType.GetAttributeDefinition("FoundBy")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("FoundBy")).Value.ToString() : string.Empty;

                    defectAssetDto.ProductArea = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_ProductArea4.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_ProductArea4.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.ReportingSite = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_ReportingSite4.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_ReportingSite4.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.Severity = (_asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_Severity3.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("Custom_Severity3.Name")).Value.ToString() : string.Empty;

                    defectAssetDto.ResolutionDetails = (_asset.GetAttribute(_assetType.GetAttributeDefinition("ResolutionReason.Name")).Value != null) ? _asset.GetAttribute(_assetType.GetAttributeDefinition("ResolutionReason.Name")).Value.ToString() : string.Empty;

                    if (_asset.GetAttribute(_assetType.GetAttributeDefinition("Owners.Name")).ValuesList != null
                         && _asset.GetAttribute(_assetType.GetAttributeDefinition("Owners.Name")).ValuesList.Count > 0)
                    {
                        foreach (var owner in _asset.GetAttribute(_assetType.GetAttributeDefinition("Owners.Name")).ValuesList)
                        {
                            defectAssetDto.OwnersName += owner.ToString() + ", ";
                        }
                        if (defectAssetDto.OwnersName.Length > 0)
                        {
                            defectAssetDto.OwnersName = defectAssetDto.OwnersName.Substring(0, defectAssetDto.OwnersName.Length - 2);
                        }
                    }
                }
            }

            return defectAssetDto;
        }
    }
}