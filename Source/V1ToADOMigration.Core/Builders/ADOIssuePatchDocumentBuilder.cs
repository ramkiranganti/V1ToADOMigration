using System.Collections.Generic;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.Builders
{
    public class ADOIssuePatchDocumentBuilder : IADOPatchDocumentBuilder
    {
        IList<JsonPatchDocument> _patchDocuments;
        IList<V1DefectAssetDto> _v1DefectAssetDtos;

        public ADOIssuePatchDocumentBuilder(IList<V1DefectAssetDto> v1DefectAssetDtos)
        {
            _v1DefectAssetDtos = v1DefectAssetDtos;
            _patchDocuments = new List<JsonPatchDocument>();
        }
        public IList<JsonPatchDocument> BuildJsonPatchDocuments()
        {
            if (_v1DefectAssetDtos != null)
            {
                foreach (V1DefectAssetDto v1DefectAssetDto in _v1DefectAssetDtos)
                {
                    _patchDocuments.Add(BuildADOIssuePatchDocumentFromV1DefectAssetdto(v1DefectAssetDto));
                }
            }
            return _patchDocuments;
        }

        private JsonPatchDocument BuildADOIssuePatchDocumentFromV1DefectAssetdto(V1DefectAssetDto v1DefectAssetDto)
        {
            JsonPatchDocument patchDocument = null;

            if (v1DefectAssetDto != null)
            {
                patchDocument = new JsonPatchDocument();

                if (!string.IsNullOrEmpty(v1DefectAssetDto.ScopeName)
                    || !string.IsNullOrEmpty(v1DefectAssetDto.ItemNumber)
                    || !string.IsNullOrEmpty(v1DefectAssetDto.ItemName))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/System.Title",
                            Value = string.Format("PROJECT: {0}, ITEM NUMBER: {1}, ITEM NAME: {2}", v1DefectAssetDto.ScopeName, v1DefectAssetDto.ItemNumber, v1DefectAssetDto.ItemName)
                        }
                    );
                }

                if (!string.IsNullOrEmpty(v1DefectAssetDto.EstimatedPoints))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/Microsoft.VSTS.Scheduling.StoryPoints",
                            Value = v1DefectAssetDto.EstimatedPoints
                        }
                    );
                }

                //if (!string.IsNullOrEmpty(v1DefectAssetDto.StatusName)
                //    || !string.IsNullOrEmpty(v1DefectAssetDto.AssetState))
                //{
                //    patchDocument.Add(
                //        new JsonPatchOperation()
                //        {
                //            Operation = Operation.Add,
                //            Path = "/fields/System.State",
                //            Value = string.Format("STATUS NAME: {0}, ASSET STATE: {1}", v1DefectAssetDto.StatusName, v1DefectAssetDto.AssetState)
                //        }
                //    );
                //}

                //if (!string.IsNullOrEmpty(v1DefectAssetDto.PriorityName))
                //{
                //    patchDocument.Add(
                //        new JsonPatchOperation()
                //        {
                //            Operation = Operation.Add,
                //            Path = "/fields/Microsoft.VSTS.Common.Priority",
                //            Value = v1DefectAssetDto.PriorityName
                //        }
                //    );
                //}

                if (!string.IsNullOrEmpty(v1DefectAssetDto.TaggedWith))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/System.Tags",
                            Value = v1DefectAssetDto.TaggedWith
                        }
                    );
                }

                if (!string.IsNullOrEmpty(v1DefectAssetDto.Description))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/System.Description",
                            Value = v1DefectAssetDto.Description
                        }
                    );
                }

                if (!string.IsNullOrEmpty(v1DefectAssetDto.SourceName)
                    || !string.IsNullOrEmpty(v1DefectAssetDto.VersionAffected)
                    || !string.IsNullOrEmpty(v1DefectAssetDto.FoundBy)
                    || !string.IsNullOrEmpty(v1DefectAssetDto.ProductArea))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/Microsoft.VSTS.TCM.SystemInfo",
                            Value = string.Format("SOURCE: {0}, VERSION AFFECTED: {1}, FOUND BY: {2}, PRODUCT AREA: {3}", v1DefectAssetDto.SourceName, v1DefectAssetDto.VersionAffected, v1DefectAssetDto.FoundBy, v1DefectAssetDto.ProductArea)
                        }
                    );
                }

                if (!string.IsNullOrEmpty(v1DefectAssetDto.ReportingSite))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/Microsoft.VSTS.Build.FoundIn",
                            Value = v1DefectAssetDto.ReportingSite
                        }
                    );
                }

                //if (!string.IsNullOrEmpty(v1DefectAssetDto.Severity))
                //{
                //    patchDocument.Add(
                //        new JsonPatchOperation()
                //        {
                //            Operation = Operation.Add,
                //            Path = "/fields/Microsoft.VSTS.Common.Severity",
                //            Value = v1DefectAssetDto.Severity
                //        }
                //    );
                //}

                if (!string.IsNullOrEmpty(v1DefectAssetDto.ResolutionDetails))
                {
                    patchDocument.Add(
                        new JsonPatchOperation()
                        {
                            Operation = Operation.Add,
                            Path = "/fields/Microsoft.VSTS.Common.ResolvedReason",
                            Value = v1DefectAssetDto.ResolutionDetails
                        }
                    );
                }

                //if (!string.IsNullOrEmpty(v1DefectAssetDto.OwnersName))
                //{
                //    patchDocument.Add(
                //        new JsonPatchOperation()
                //        {
                //            Operation = Operation.Add,
                //            Path = "/fields/System.AssignedTo",
                //            Value = v1DefectAssetDto.OwnersName
                //        }
                //    );
                //}
            }
            return patchDocument;
        }
    }
}
