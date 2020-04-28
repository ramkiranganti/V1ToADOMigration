using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Helpers;
using V1ToADOMigration.Core.Interfaces;
using V1ToADOMigration.Core.Services;

namespace V1ToADOMigration.Controllers
{
    public class HomeController : Controller
    {
        private StringBuilder sb = new StringBuilder(string.Empty);
        private string v1ProjectName = "WP 7.24 BA5 Merge";
        private string adoProjectName = "Test Project";
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult GetAllDefects()
        {
            try
            {
                // Get all defects from VersionOne for a Project
                IV1Service v1Service = new V1Service(v1ProjectName, new ConnectionHelper(new V1UserCredentials()));
                IList<V1DefectAssetDto> defectAssetDtos = v1Service.GetAllDefectDetailsByProjectName();

                // Return all defects in text/html format 
                return base.Content(GetDefectsHtml(defectAssetDtos));
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return base.Content(ex.Message);
            }
        }

        public ActionResult MigrateAllDefects()
        {
            try
            {
                // Get all defects from VersionOne for a Project
                IV1Service v1Service = new V1Service(v1ProjectName, new ConnectionHelper(new V1UserCredentials()));
                IList<V1DefectAssetDto> defectAssetDtos = v1Service.GetAllDefectDetailsByProjectName();

                // Migrate all defects to ADO project
                IADOService aDOService = new ADOService(adoProjectName, new ConnectionHelper());
                IList<WorkItem> workItems = aDOService.MigrateAllV1DefectsByProjectName(defectAssetDtos);
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return base.Content(ex.Message);
            }

            return View("Success");
        }

        #region Private Methods

        private string GetDefectsHtml(IList<V1DefectAssetDto> defectAssetDtos)
        {
            // Generate the html
            sb.Append(GetStyle());
            sb.Append(GetHeader(v1ProjectName, defectAssetDtos.Count));
            sb.Append("<div class='row'><div class='mainColumn'>");

            foreach (V1DefectAssetDto defectAssetDto in defectAssetDtos)
            {
                sb.Append(FormatSingleDefect(defectAssetDto));
            }

            sb.Append("</div></div></body></html>");
            return sb.ToString();
        }
        private string GetStyle()
        {
            return "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\"><style>* {  box-sizing: border-box;}body {  font-family: Arial;font-size :13px; padding: 10px;  background: #f1f1f1;} .header {  padding: 10px;  text-align: center;  background: white;}.header h1 {  font-size: 50px;}  .mainColumn {     float: left;  width: 100%;}  .card {  background-color: white;  padding: 20px;  margin-top: 20px;} .row:after {  display: table;  clear: both;} </style></head><body> ";
        }

        private string GetHeader(string projectName, int count)
        {
            return "<div class='header'><h2>Report</h2>  <p> " + count + " Defects for the project  <strong> " + projectName + "</strong> (" + System.DateTime.Now + " ) </p></div>";
        }

        private string FormatSingleDefect(V1DefectAssetDto assetDto)
        {
            StringBuilder sbHtml = new StringBuilder(string.Empty);
            sbHtml.Append("<div class='card'>");
            sbHtml.Append("<h2>" + assetDto.OidToken + " - " + assetDto.ItemName + "</h2><h5>");
            sbHtml.Append("<br/>PROJECT:  " + assetDto.ScopeName);
            sbHtml.Append("<br/>OWNERS:  " + assetDto.OwnersName);
            sbHtml.Append("<br/>STATUS:  " + assetDto.StatusName);
            sbHtml.Append("<br/>ESTIMATED POINTS:  " + assetDto.EstimatedPoints);
            sbHtml.Append("<br/>PRIORITY:  " + assetDto.PriorityName);
            sbHtml.Append("<br/>TAGS:  " + assetDto.TaggedWith);
            sbHtml.Append("<br/>PRODUCT AREA:  " + assetDto.ProductArea);
            sbHtml.Append("<br/>REPORTING SITE:  " + assetDto.ReportingSite);
            sbHtml.Append("<br/>SEVERITY:  " + assetDto.Severity);
            sbHtml.Append("<br/>SOURCE:  " + assetDto.SourceName);
            sbHtml.Append("<br/>VERSION AFFECTED:  " + assetDto.VersionAffected);
            sbHtml.Append("<br/>FOUND BY:  " + assetDto.FoundBy);
            sbHtml.Append("<br/>RESOLUTIION DETAILS:  " + assetDto.ResolutionDetails);
            sbHtml.Append("<br/>ITEM NUMBER:  " + assetDto.ItemNumber);
            sbHtml.Append("<br/>ASSET STATE:  " + assetDto.AssetState);
            sbHtml.Append("</h5><p>Description: " + assetDto.Description + "</p>");
            sbHtml.Append("</div>");
            return sbHtml.ToString();
        }

        #endregion
    }
}
