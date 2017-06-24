using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
public class FindPeople
    {
        public FindPeople()
        {
            

        }
        static public string getPeople(SqlConnection connection, string school, string job)
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT name2 from bop where name = N'");
            sb.Append(school);
            sb.Append("' and relation = N'");
            sb.Append(job);
            sb.Append("'");
            String sql = sb.ToString();
             using (SqlCommand command = new SqlCommand(sql, connection))
             {
                 using (SqlDataReader reader = command.ExecuteReader())
                 {
                     reader.Read();

                    return reader.GetString(0);

                 }
             }
            return "不知道";
        }
    }
