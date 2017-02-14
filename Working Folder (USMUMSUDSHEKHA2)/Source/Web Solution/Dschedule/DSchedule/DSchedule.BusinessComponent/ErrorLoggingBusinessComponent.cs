using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DSchedule.BusinessComponent
{
    public static class ErrorLoggingBusinessComponent
    {
        private static String exepurl;
        static SqlConnection con;
        private static void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Logging"].ConnectionString.ToString();
            con = new SqlConnection(constr);
            con.Open();
        }
        public static int SendExcepToDB(Exception exdb)
        {

            connection();
            exepurl = HttpContext.Current.Request.Url.ToString();
            SqlCommand com = new SqlCommand("uspSaveErrorLog", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ExceptionMsg", exdb.Message.ToString());
            com.Parameters.AddWithValue("@ExceptionType", exdb.GetType().Name.ToString());
            com.Parameters.AddWithValue("@ExceptionURL", exepurl);
            com.Parameters.AddWithValue("@ExceptionSource", exdb.StackTrace.ToString());
            com.Parameters.Add("@ErrorId", SqlDbType.Int).Direction = ParameterDirection.Output;
            com.ExecuteNonQuery();
            int ErrorId = int.Parse(com.Parameters["@ErrorId"].Value.ToString());
            return ErrorId;


        }
    }
}
