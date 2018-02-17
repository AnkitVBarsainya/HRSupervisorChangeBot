using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HRSupervisorChangeBot.AzureModels;

namespace HRSupervisorChangeBot
{
	[Serializable]
	public class MethodHolder
	{
		public async Task<String> SpellCheck(String getString)
		{
			var modeInfo = "&mode=spell";
			var httpClient = new HttpClient();
			var stringArr = getString.Split(' ');
			string[] resultArr = new string[stringArr.Length];
			httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "35833b8ad7d54eaa8c347bc18e1c0a44");
			for (int i = 0; i < stringArr.Length; i++)
			{
				var uri = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?text=" + stringArr[i] + modeInfo;
				var response = await httpClient.GetAsync(uri);
				response.EnsureSuccessStatusCode();
				var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
				// var firstSuggestion = "";
				if (((JArray)(result.flaggedTokens)).Count > 0)
				{
					resultArr[i] = result.flaggedTokens[0].suggestions[0].suggestion;
					// firstSuggestion = result.flaggedTokens[0].suggestions[0].suggestion;
				}
				else
				{
					resultArr[i] = stringArr[i];
				}
			}
			return string.Join(" ", resultArr);
		}

		public async Task<String> EmpValidityCheck(int empId, int spvsrId)
		{
			AzureModels.HRHCMDataEntities databaseEntity = new AzureModels.HRHCMDataEntities();
			AzureModels.Table table = new AzureModels.Table();
			var employeeName = (from Table in databaseEntity.Tables
								where Table.EmployeeID == empId
								select Table.EmployeeName);

			var currentSupervisorName = (from Table in databaseEntity.Tables
										 where Table.EmployeeID == empId
										 select Table.SupervisorName);
			var newSupervisorName = (from Table in databaseEntity.Tables
									 where Table.EmployeeID == spvsrId
									 select Table.EmployeeName);
			var output = currentSupervisorName.Single() + "||" + newSupervisorName.Single();

			return output;
		}
		public async Task<String> ChangeSupervisor(int empId, int spvsrId)
		{
			AzureModels.HRHCMDataEntities databaseEntity = new AzureModels.HRHCMDataEntities();
			AzureModels.Table table = new AzureModels.Table();
			var newSupervisorName = (from Table in databaseEntity.Tables
									 where Table.EmployeeID == spvsrId
									 select Table.EmployeeName);
			var employeeDetails = (from Table in databaseEntity.Tables
								   where Table.EmployeeID == empId
								   select Table);
			foreach (Table Table in employeeDetails)
			{
				Table.SupervisorID = spvsrId;
				Table.SupervisorName = newSupervisorName.Single();
			}
			try
			{
				databaseEntity.SaveChanges();
				return newSupervisorName.Single();

			}
			catch (Exception e)
			{
				return null;
			}
		}
	}	
}