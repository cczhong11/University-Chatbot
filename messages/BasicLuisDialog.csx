#load "FindPeople.csx"
using System;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

// For more information about this template visit http://aka.ms/azurebots-csharp-luis

[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
        
    }

    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {        
        Console.WriteLine(result.Query);
        await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
        context.Wait(MessageReceived);
    }

    // Go to https://luis.ai and create a new intent, then train/publish your luis app.
    // Finally replace "MyIntent" with the name of your newly created intent in the following handler
    [LuisIntent("people.information")]
    public async Task MyIntent(IDialogContext context, LuisResult result)
    {
        string thisjob, thisschool;
        EntityRecommendation job,school;
        if (result.TryFindEntity("职位", out job))
        {
            thisjob = job.Entity;
        }
        else
        {
            thisjob = "院长";
        }
    
        if (result.TryFindEntity("学校", out school))
        {
            thisschool = school.Entity;
        }
        else
        {
            thisschool = "信息科学与工程学院";
        }
        await context.PostAsync(FindPeople.getPeople(thisschool,thisjob)); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("scores")]
    public async Task fsxIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"你询问的涉及分数线"); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("专业")]
    public async Task zyIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"你询问的涉及专业"); //
        context.Wait(MessageReceived);
    }
    
}