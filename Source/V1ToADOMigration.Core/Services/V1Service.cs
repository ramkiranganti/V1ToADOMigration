using System;
using System.Collections.Generic;
using V1ToADOMigration.Core.Builders;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;
using V1ToADOMigration.Core.Mappers;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Services
{
    public class V1Service : IV1Service
    {
        private const string DEFECT = "Defect";

        private string _projectName;        
        private IConnectionHelper _connectionHelper;

        public V1Service(string projectName, IConnectionHelper connectionHelper)
        {            
            _projectName = projectName;
            _connectionHelper = connectionHelper;            
        }
        public IList<V1DefectAssetDto> GetAllDefectDetailsByProjectName()
        {
            V1ConnectionDto V1ConnectionDto = null;
            try
            {
                V1ConnectionDto = _connectionHelper.GetV1Connection();
                if (V1ConnectionDto.Connection != null && V1ConnectionDto.IsValid && string.IsNullOrEmpty(V1ConnectionDto.ErrorMessage))
                {
                    IList<V1DefectAssetDto> defectAssetDtos = new List<V1DefectAssetDto>();
                    // Create AssetType object
                    IAssetType assetType = V1ConnectionDto.Connection.Meta.GetAssetType(DEFECT);

                    //Build a query to fetch data from V1 database
                    V1QueryBuilder queryBuilder = new V1QueryBuilder(assetType);
                    Query query = queryBuilder.BuildDefectQuery(_projectName);

                    // We do the actually query on the server here and get our results to be processed soon after
                    QueryResult result = V1ConnectionDto.Connection.Retrieve(query);

                    //Map result assets to DefectAssetDto objects and add to list
                    foreach (Asset asset in result.Assets)
                    {
                        V1AssetToAssetDtoMapper defectMapper = new V1AssetToAssetDtoMapper(assetType, asset);
                        V1DefectAssetDto defectAssetDto = defectMapper.MapDefectAssetToAssetDto();
                        defectAssetDtos.Add(defectAssetDto);
                    }

                    return defectAssetDtos;
                }
                else
                {
                    throw new Exception(V1ConnectionDto.ErrorMessage);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
