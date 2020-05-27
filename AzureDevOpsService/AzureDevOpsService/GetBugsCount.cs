using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetBugsCount" in both code and config file together.
    public class GetBugsCount : IGetBugsCount
    {
       
        public string GetData(string value)
        {
            var bugs = QueryOpenBugs(value);
            return string.Format("Bugs Workitem Count: {0}", bugs.Result.WorkItems.Count());
        }

        public async Task<WorkItemQueryResult> QueryOpenBugs(string areaPath)
        {

            Uri orgUrl = new Uri(AzureDevOpsService.Properties.Settings.Default.TFSServerUrl);
            VssConnection connection = new VssConnection(orgUrl, new VssBasicCredential(string.Empty, AzureDevOpsService.Properties.Settings.Default.PAT));
           
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            Wiql wiql = new Wiql();

            wiql.Query = "SELECT * "
            + " FROM WorkItems WHERE [System.WorkItemType] in ('Bug')";
          //  + " AND [System.AreaPath] Under 'TEST254'";

            WorkItemQueryResult bugs = await witClient.QueryByWiqlAsync(wiql);
            return bugs;
            
        }

    }
    }
