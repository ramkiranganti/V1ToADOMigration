using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using V1ToADOMigration.Core.Builders;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Services
{
    public class ADOService : IADOService
    {
        private const string ISSUE = "Issue";

        private string _projectName;
        private IConnectionHelper _connectionHelper;

        public ADOService(string projectName, IConnectionHelper connectionHelper)
        {
            _projectName = projectName;
            _connectionHelper = connectionHelper;
        }
        public IList<WorkItem> MigrateAllV1DefectsByProjectName(IList<V1DefectAssetDto> defectAssetDtos)
        {
            IList<WorkItem> workItems = null;
            try
            {               
                IADOPatchDocumentBuilder aDOIssuePatchDocumentBuilder = new ADOIssuePatchDocumentBuilder(defectAssetDtos);
                IList<JsonPatchDocument> patchDocuments = aDOIssuePatchDocumentBuilder.BuildJsonPatchDocuments();
                IConnectionHelper aDOConnectionHelper = new ConnectionHelper();
                WorkItemTrackingHttpClient client = aDOConnectionHelper.GetADOWorkItemTrackingHttpClient();
                workItems = new List<WorkItem>();

                foreach (JsonPatchDocument patchDocument in patchDocuments)
                {
                    WorkItem result = client.CreateWorkItemAsync(patchDocument, _projectName, ISSUE).Result;
                    workItems.Add(result);
                }                
            }
            catch (Exception)
            {
            }

            return workItems;
        }
    }
}
