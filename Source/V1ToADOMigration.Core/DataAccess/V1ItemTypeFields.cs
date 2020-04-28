using System.Collections.Generic;
using V1ToADOMigration.Core.Interfaces;

namespace V1ToADOMigration.Core.DataAccess
{
    public class V1ItemTypeFields : IV1ItemTypeFields
    {
        public IList<string> GetDefectFields()
        {
            IList<string> defectFields = new List<string>();

            defectFields.Add("Name");
            defectFields.Add("Number");
            defectFields.Add("Estimate");
            defectFields.Add("AssetState");
            defectFields.Add("Scope.Name");
            defectFields.Add("Owners.Name");
            defectFields.Add("Status.Name");
            defectFields.Add("Priority.Name");
            defectFields.Add("TaggedWith");
            defectFields.Add("Description");
            defectFields.Add("Source.Name");
            defectFields.Add("VersionAffected");
            defectFields.Add("FoundBy");
            defectFields.Add("Custom_ProductArea4.Name");
            defectFields.Add("Custom_ReportingSite4.Name");
            defectFields.Add("Custom_Severity3.Name");
            defectFields.Add("ResolutionReason.Name");                      
            //defectFields.Add("Timebox.Name");
            //defectFields.Add("Team.Name");
            //defectFields.Add("Type.Name");
            //defectFields.Add("VerifiedBy.Name");
            //defectFields.Add("CreatedBy.Name");
            //defectFields.Add("CreateDate");
            //defectFields.Add("ChangedBy.Name");
            //defectFields.Add("ChangeDate");
            //defectFields.Add("Custom_DeliveryProject.Name");
            //defectFields.Add("Custom_ReleaseNoteRequired2");
            //defectFields.Add("Custom_ReleaseNotes2");
            //defectFields.Add("Children.Actuals.Value.@Sum");
            //defectFields.Add("Timebox.TargetEstimate");
            //defectFields.Add("Children.Actuals.Date.@MaxDate");            

            return defectFields;
        }
    }
}
