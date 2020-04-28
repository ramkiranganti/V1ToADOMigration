using System;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using V1ToADOMigration.Core.Entities;
using V1ToADOMigration.Core.Interfaces;
using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Helpers
{
    public class ConnectionHelper : IConnectionHelper
    {
        private V1UserCredentials _credentials;

        public ConnectionHelper()
        {

        }
        public ConnectionHelper(V1UserCredentials credentials)
        {
            _credentials = credentials;
        }

        public V1ConnectionDto GetV1Connection()
        {
            V1ConnectionDto v1ConnectionDto = new V1ConnectionDto();
            try
            {
                if (!string.IsNullOrEmpty(_credentials.Username) && !string.IsNullOrEmpty(_credentials.Password))
                {
                    V1Connector connector = V1Connector
                           .WithInstanceUrl(ConfigHelper.V1Url)
                           .WithUserAgentHeader(ConfigHelper.V1Name, ConfigHelper.V1Version)
                           .WithUsernameAndPassword(_credentials.Username, _credentials.Password)
                           .Build();

                    IServices connection = new VersionOne.SDK.APIClient.Services(connector);

                    if (connection.LoggedIn != null)
                    {
                        v1ConnectionDto.IsValid = true;
                        v1ConnectionDto.ErrorMessage = string.Empty;
                        v1ConnectionDto.Connection = connection;
                    }
                }
                else
                {
                    v1ConnectionDto.IsValid = false;
                    v1ConnectionDto.ErrorMessage = "Invalid Username/Password.";
                }
            }
            catch (Exception ex)
            {
                v1ConnectionDto.IsValid = false;
                if ((ex.InnerException != null) && (ex.InnerException.Message.Equals("Unauthorized")))
                    v1ConnectionDto.ErrorMessage = "Unauthorized";
                else
                    v1ConnectionDto.ErrorMessage = "Exception Occured";
            }

            return v1ConnectionDto;
        }

        public WorkItemTrackingHttpClient GetADOWorkItemTrackingHttpClient()
        {
            try
            {
                var connection = new VssConnection(new Uri(ConfigHelper.ADOUrl), new VssBasicCredential(string.Empty, ConfigHelper.ADOToken));
                return connection.GetClient<WorkItemTrackingHttpClient>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProjectHttpClient GetADOProjectClient()
        {
            try
            {
                var connection = new VssConnection(new Uri(ConfigHelper.ADOUrl), new VssBasicCredential(string.Empty, ConfigHelper.ADOToken));
                return connection.GetClient<ProjectHttpClient>();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
