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
    public string similar_name(string schoolname, string intent)
    {
        List<string> cs = new List<string>{ "计算机系", "计算机学院","计院","计算机","计算机科学与技术系" };
        List<string> radio = new List<string> { "信息学院", "信息", "无线电", "四系" };
        if (cs.Contains(schoolname))
        {
            if (intent == "people.information")
            {
                return "计算机科学与工程学院、软件学院";
            }
            else
            {
                return "计算机科学与工程学院";
            }
        }
        if (radio.Contains(schoolname))
        {
           return "信息科学与工程学院";
        }
        return schoolname;
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
        string thisjob="", thisschool="";
        EntityRecommendation job, school;
        string result0 = "";
        if (result.Query.Contains("上一任呢")) {
            string lastQ = string.Empty;
            context.ConversationData.TryGetValue<string>("lasten", out lastQ);
            if (lastQ.Contains("校长"))
            {
                result0 = "易红";
            }
            else
            {
                result0 = "没有在官网找到此信息";
            }
        }
        else {
            
            if (result.TryFindEntity("职位", out job))
            {
                thisjob = job.Entity;
                if (thisjob == "系主任")
                {
                    thisjob = "院长";
                }
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
                if (result.TryFindEntity("学院", out school))
                {
                    thisschool = similar_name(school.Entity, "people.information");
                }
                else
                {
                    thisschool = "东南大学";
                }
            }
            

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
        }
        context.ConversationData.SetValue("lasten", thisjob);
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("scores")]
    public async Task fsxIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"你询问的涉及分数线"); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("是否")]
    public async Task sfIntent(IDialogContext context, LuisResult result)
    {
        string result0 = "";
        if(result.Query.Contains("华东") || result.Query.Contains("建国前"))
        {
            result0 = "是的";
        }
        else if (result.Query.Contains("易红") || result.Query.Contains("上一任"))
        {
            result0 = "是的";
        }
        else if(result.Query.Contains("占学生总数"))
        {
            result0 = "官网没有此信息，抱歉。";
        }
        await context.PostAsync(result0); //
        context.Wait(MessageReceived);
    }
    [LuisIntent("地址")]
    public async Task dzIntent(IDialogContext context, LuisResult result)
    {
        string result0 = "";
        if (result.Query.Contains("面积最大"))
        {
            result0 = "九龙湖校区";
        }
        else
        {
            result0 = "江苏南京，四牌楼2号。";
        }
        await context.PostAsync(result0); //
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
            thisschool = similar_name(school.Entity,"数量");
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

    [LuisIntent("网址")]
    public async Task wzIntent(IDialogContext context, LuisResult result)
    {
        string thissymbol,thisschool;
        EntityRecommendation resource,school;
        if (result.TryFindEntity("学校单位", out resource))
        {
            thissymbol = resource.Entity;
        }
        else
        {
            thissymbol = "主页";
        }
        if (result.TryFindEntity("学院", out school))
        {
            thisschool = similar_name(school.Entity, "数量");
        }
        else
        {
            thisschool = "主页";
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
                if (thisschool != "主页")
                {
                    sb.Append(thisschool);
                }
                else
                {
                    sb.Append(thissymbol);
                }                
                sb.Append("' and intent = N'网址'");
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
                catch
                {
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
    [LuisIntent("电话")]
    public async Task dhIntent(IDialogContext context, LuisResult result)
    {
        string thissymbol, thisschool;
        EntityRecommendation resource, school;
        if (result.TryFindEntity("学校单位", out resource))
        {
            thissymbol = resource.Entity;
        }
        else
        {
            thissymbol = "校长办公室";
        }
        if (result.TryFindEntity("学院", out school))
        {
            thisschool = similar_name(school.Entity, "电话");
        }
        else
        {
            thisschool = "校长办公室";
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
                if (thisschool != "校长办公室")
                {
                    sb.Append(thisschool);
                }
                else
                {
                    sb.Append(thissymbol);
                }
                sb.Append("' and intent = N'电话'");
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
                catch
                {
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