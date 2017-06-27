#load "FindPeople.csx"
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Text;
// For more information about this template visit http://aka.ms/azurebots-csharp-luis

[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{

    
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute("52e6b5fb-ff51-4b98-9e5b-81f8ac236d64", "e927faef3c534ff0af810dfea1fc4ccc", domain: "southeastasia.api.cognitive.microsoft.com",staging: true)))
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
            thisschool = "东南大学";
        }
        string result0 = "";
        
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "aaabop.database.windows.net";
            builder.UserID = "tczhong";
            builder.Password = "!Loveyou";
            builder.InitialCatalog = "aaabopsql";                            
        
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT name2 from bop where name = N'");
                sb.Append(thisschool);
                sb.Append("' and relation LIKE N'%");
                sb.Append(thisjob);
                sb.Append("%'");
                String sql = sb.ToString();
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        result0 = reader.GetString(0);

                    }
                }
                if (result0 == "")
                {
                    result0 = "不知道";
                }

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("scores")]
    public async Task fsxIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"你询问的涉及分数线"); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("数量")]
    public async Task slIntent(IDialogContext context, LuisResult result)
    {
        string thisjob, thisschool;
        EntityRecommendation resource,school;
        if (result.TryFindEntity("资源", out resource))
        {
            thisjob = resource.Entity;
            if(thisjob == "学院")
            {
                thisjob = "院（系）";
            }
        }
        else
        {
            thisjob = "院士";
        }
    
        if (result.TryFindEntity("学校", out school))
        {
            thisschool = school.Entity;
        }
        else
        {
            thisschool = "东南大学";
        }
        string result0 = "";
        /*
        if (thisschool[0]!='东')
        {
            thisschool = "东南大学"+thisschool;
        }*/
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "aaabop.database.windows.net";
            builder.UserID = "tczhong";
            builder.Password = "!Loveyou";
            builder.InitialCatalog = "aaabopsql";                            
        
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT name2 from bop where name = N'");
                sb.Append(thisschool);
                sb.Append("' and relation LIKE N'%");
                sb.Append(thisjob);
                sb.Append("%'");
                String sql = sb.ToString();
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        result0 = reader.GetString(0);

                    }
                }
                if (result0 == "")
                {
                    result0 = "不知道";
                }

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("专业")]
    public async Task zyIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"你询问的涉及专业"); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("时间")]
    public async Task sjIntent(IDialogContext context, LuisResult result)
    {
        string thisschool,result0;
        EntityRecommendation school;
        if (result.TryFindEntity("学院", out school))
        {
            thisschool = school.Entity;
            if(thisschool == "信息科学与工程学院")
            {
                result0 = "1923年";
            }
            else
            {
                result0 = "1960年";
            }
        }
        else
        {
            result0 = "1902年";
        }
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("校标")]
    public async Task xbIntent(IDialogContext context, LuisResult result)
    {
        string thissymbol ;
        EntityRecommendation resource;
        if (result.TryFindEntity("标志", out resource))
        {
            thissymbol = resource.Entity;
        }
        else
        {
            thissymbol = "校训";
        }

        
        string result0 = "";
        
        try
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "aaabop.database.windows.net";
            builder.UserID = "tczhong";
            builder.Password = "!Loveyou";
            builder.InitialCatalog = "aaabopsql";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT name2 from bop where relation = N'");
                sb.Append(thissymbol);               
                sb.Append("'");
                String sql = sb.ToString();
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();

                            result0 = reader.GetString(0);

                        }
                    }
                }
                catch {
                    if (result0 == "")
                    {
                        result0 = "不知道";
                    }
                }
                

            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
}