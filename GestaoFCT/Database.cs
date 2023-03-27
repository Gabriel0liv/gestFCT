using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestaoFCT
{
    public class Database
    {

        private static string GetConnString(string type)
        {
            string connString = @"Data Source=DESKTOP-5OQ8K2C\SQLEXPRESS;Initial Catalog=FCTGest;User ID=sa;Password=gbro2004";
            return connString;
        }

        public static DataTable GetFromDBSqlSrv(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, GetConnString("SqlSrv"));
                da.Fill(dt);
            }
            catch (Exception e)
            {
            }

            return dt;
        }

        public static Boolean NonQuerySqlSrv(string com)
        {
            try
            {
                SqlConnection conn = new SqlConnection(GetConnString("SqlSrv"));
                SqlCommand command = new SqlCommand(com, conn);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }
}