using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Text;

namespace HRSupervisorChangeBot
{
	[LuisModel("2e517610-2a23-4ab4-8401-bba9c9dde08d", "85cd36007998464da3240d6e5f8c00c6")]
	[Serializable]
	public class LuisDialog : LuisDialog<object>
	{
		MethodHolder methodObj = new MethodHolder();

		int newHCM = 0;
		int empId = 0;
		[LuisIntent("Greetings")]
		public async Task NeedService(IDialogContext context, LuisResult result)
		{
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("Greetings_intent", result.Intents[0].Intent);
			if (result.Intents[0].Score > 0.20)
			{
				String MyHour = DateTime.Now.ToString(("hh"));
				int MyHourInt = Int32.Parse(MyHour);
				String Mytime = DateTime.Now.ToString(("tt"));
				/*string ReplyTime;
				if (MyHourInt + 4.5 >= 1 && MyHourInt + 4.5 <= 12 && Mytime == "AM")
					ReplyTime = "Good Morning";
				else if (MyHourInt + 4.5 >= 1 && MyHourInt + 4.5 <= 4 && Mytime == "PM")
					ReplyTime = "Good Afternoon";
				else if (MyHourInt + 4.5 >= 5 && MyHourInt + 4.5 <= 11 && Mytime == "PM")
					ReplyTime = "Good Evening";
				else
					ReplyTime = "nice to see you";*/
				await context.PostAsync("Hi " + context.Activity.From.Name + $". How can I help you?");

			}
			else
			{
				await context.PostAsync($"sorry, I am not smart enough to understand that just yet :-( please ask me something  else");
			}
			context.Wait(MessageReceived);
		}

		[LuisIntent("Complement")]
		public async Task NeedService1(IDialogContext context, LuisResult result)
		{
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("Complement_intent", result.Intents[0].Intent);
			
			if (result.Intents[0].Score > 0.20)
			{
				Random rnd = new Random();
				int MyNum = rnd.Next(1, 6);
				if (MyNum == 1)
				{
					await context.PostAsync($"You are welcome:)");
				}
				else if (MyNum == 2)
				{
					await context.PostAsync($"No worries, that's my job :-)");
				}
				else if (MyNum == 3)
				{
					await context.PostAsync($"My Pleasure :-)  I don't get tired helping people");
				}
				else if (MyNum == 4)
				{
					await context.PostAsync($"That's fine,always a pleasure to help you anyway:)");
				}
				else if (MyNum == 5)
				{
					await context.PostAsync($"No probs :-), Please let me know if you need any more help");
				}
				else
				{
					await context.PostAsync($"anytime, I don't get tired helping people");
				}
			}
			else
			{
				await context.PostAsync($"sorry, I am not smart enough to understand that just yet :-( please ask me something  else");
			}
			context.Wait(MessageReceived);
		}

		[LuisIntent("Bye")]
		public async Task NeedService2(IDialogContext context, LuisResult result)
		{
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("Bye_intent", result.Intents[0].Intent);
			
			if (result.Intents[0].Score > 0.20)
			{
				Random rnd2 = new Random();
				int MyNum2 = rnd2.Next(1, 3);
				if (MyNum2 == 1)
				{
					await context.PostAsync($"Okay, see you later,TC :-)");
				}
				else if (MyNum2 == 2)

				{
					await context.PostAsync($"Good Bye! have a good time ahead :-)");
				}
				else
				{
					await context.PostAsync($"Bye, Take care");
				}
			}
			else
			{
				await context.PostAsync($"sorry, I am not smart enough to understand that just yet :-( please ask me something  else");
			}
			newHCM = 0;
			empId = 0;
			context.Wait(MessageReceived);
		}

		[LuisIntent("FormalQuestions")]
		public async Task NeedService3(IDialogContext context, LuisResult result)
		{
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("FormalQuestions_intent", result.Intents[0].Intent);
			
			if (result.Intents[0].Score > 0.20)
			{
				Random rnd1 = new Random();
				int MyNum1 = rnd1.Next(1, 6);
				if (MyNum1 == 1)
				{
					await context.PostAsync($"just the usual stuff, i think it will be better if i could help you :-)");
				}
				else if (MyNum1 == 2)
				{
					await context.PostAsync($"Hey! I am doing pretty well :-),happy to help you?");
				}
				else if (MyNum1 == 3)
				{
					await context.PostAsync($"Not too bad! having a rough day but anyway glad to help!!");
				}
				else if (MyNum1 == 4)
				{
					await context.PostAsync($"oh gosh! All kind of stuff, Thankfuly all is well in the end, but anyway your virtual assistant is not tired so go ahead :-)  ");
				}
				else if (MyNum1 == 5)
				{
					await context.PostAsync($"Pretty awesome! can be better if i can help you in some way?");
				}
				else
				{
					await context.PostAsync($"Hey!:) I am doing well,how can i help you?");
				}
			}
			else
			{
				await context.PostAsync($"sorry, I am not smart enough to understand that just yet :-( please ask me something  else");
			}
			context.Wait(MessageReceived);
		}

		[LuisIntent("supervisorchange")]
		public async Task NeedService4(IDialogContext context, LuisResult result) {
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("supervisorchange", result.Intents[0].Intent);
			newHCM = 0;
			empId = 0;
			await context.PostAsync("Please provide your employee ID and New Supervisor's ID");
			context.Wait(MessageReceived);
		}
		[LuisIntent("affirmative")]
		public async Task NeedService5(IDialogContext context, LuisResult result)
		{
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.RemoveValue("id");
			context.ConversationData.SetValue("Affirmative", result.Intents[0].Intent);
			// Call method to alter the Supervisor in DB
			var newSupervisorName = await methodObj.ChangeSupervisor(empId, newHCM);
			// Needs to be revisited
			if (!string.IsNullOrEmpty(newSupervisorName)) {
				await context.PostAsync(newSupervisorName + " is your new supervisor, changes will reflect in the system in 8 hours");
				newHCM = 0;
				empId = 0;
				context.Wait(MessageReceived);
			}
			else
			{
				await context.PostAsync("There seems to be some problem with savings the changes, please try again in a few mins");
			}
		}
		[LuisIntent("id")]
		public async Task NeedService6(IDialogContext context, LuisResult result) {
			context.ConversationData.RemoveValue("FormalQuestions_intent");
			context.ConversationData.RemoveValue("Greetings_intent");
			context.ConversationData.RemoveValue("Complement_intent");
			context.ConversationData.RemoveValue("Bye_intent");
			context.ConversationData.RemoveValue("Affirmative");
			context.ConversationData.RemoveValue("supervisorchange");
			context.ConversationData.SetValue("id", result.Intents[0].Intent);
			Activity activity = (Activity)context.Activity;
			
			// EntityRecommendation empEntity;
			//await context.PostAsync("before first if");
			if (result.Entities[0].Entity.Contains("supervisor")|| result.Entities[0].Entity.Contains("manager"))
			{
				bool success = int.TryParse(new string(activity.Text
					 .SkipWhile(x => !char.IsDigit(x))
					 .TakeWhile(x => char.IsDigit(x))
					 .ToArray()), out newHCM);
				//await context.PostAsync("new HCM will be :"+newHCM.ToString());
			}
			//await context.PostAsync("before second if");
			if (result.Entities[0].Entity.Contains("emp") || result.Entities[0].Entity.Contains("employee") || result.Entities[0].Entity.Contains("empid"))
			{
				bool success = int.TryParse(new string(activity.Text
					 .SkipWhile(x => !char.IsDigit(x))
					 .TakeWhile(x => char.IsDigit(x))
					 .ToArray()), out empId);
				//await context.PostAsync("emp id :" + empId.ToString());
			}
			// Call Method to check with DB is the details entered are correct and then get it validated by user once

			// Needs to be revisited
			if (newHCM != 0 && empId != 0)
			{
				if (newHCM == empId)
				{
					await context.PostAsync("You can't be your own supervisor. " +
						"Please ensure it is a different and valid employee");
				}
				else
				{
					var supervisorDetails = await methodObj.EmpValidityCheck(empId, newHCM);
					var currentSup = supervisorDetails.Split(new[] { "||" }, StringSplitOptions.None)[0];
					var newSup = supervisorDetails.Split(new[] { "||" }, StringSplitOptions.None)[1];
					await context.PostAsync(supervisorDetails);
					await context.PostAsync(currentSup);
					await context.PostAsync(newSup);
					if (currentSup.Equals(newSup, StringComparison.OrdinalIgnoreCase) && !currentSup.Equals("null", StringComparison.OrdinalIgnoreCase) 
						&& !String.IsNullOrEmpty(currentSup) )
					{
						await context.PostAsync("Your current Supervisor and New supervisor are same. " +
							"Please try again with correct details.");
						context.Wait(MessageReceived);
					}
					else if (newSup.Equals("null", StringComparison.OrdinalIgnoreCase) || String.IsNullOrEmpty(newSup))
					{
						await context.PostAsync("The new supervisor is not a valid Employee in the system currently. " +
							"Please try again with correct details.");
						context.Wait(MessageReceived);
					}
					else
					{
						await context.PostAsync("Your Supervisor is being changed with the following details :"+ Environment.NewLine +
							"Current Supervisor :" + currentSup + 
							"New Supervisor : " + newSup + 
							"Please confirm if this is correct.");
						context.Wait(MessageReceived);
					}
				}
			}
			else
			{

				context.Wait(MessageReceived);
			}
		}
		[LuisIntent("")]
		[LuisIntent("None")]
		public async Task NoneIntent(IDialogContext context, LuisResult result)
		{
			await context.PostAsync("I could not understand you. Please ask a different question.");
			context.Wait(MessageReceived);
		}
		
	}
}