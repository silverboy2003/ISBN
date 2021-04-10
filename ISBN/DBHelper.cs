using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace ISBN
{
    public class DBHelper
    {
        public const int WRITEDATA_ERROR = -1;
        private static OleDbConnection conn = null;
        private static string connString;
        private static bool connOpen = false;

        public static void SetConnString(string conn)
        {
            connString = conn;
        }//sets connection string
        public static bool OpenConnection()
        {
            if (connOpen)
                return true;

            try
            {
                OleDbConnection connection = new OleDbConnection(connString);
                connection.Open();
                conn = connection;
                connOpen = true;
                return true;
            }
            catch (OleDbException e)
            {
                return false;
            }
        }//opens the connection to the database
        public static OleDbDataReader ReadData(string sql)
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return null;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd;
            }
            catch
            {

                return null;
            }
        }//executes a read data command on the database
        public static OleDbDataReader ReadData(string sql, string text)//sanitized read data
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return null;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Text", text);
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd;
            }
            catch
            {

                return null;
            }
        }
        public static OleDbDataReader ReadData(string sql, List<string> text)//sanitized read data for multiple inputs
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return null;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                for (int i = 1; i <= text.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@Text{i}", text[i - 1]);
                }
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd;
            }
            catch
            {

                return null;
            }
        }
        public static int WriteData(string sql)
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return WRITEDATA_ERROR;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd.RecordsAffected;
            }
            catch (OleDbException e)
            {

                return WRITEDATA_ERROR;
            }
        }//executes a write data command on the database
        public static int WriteData(string sql, string input)//sanitized write data for single string
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return WRITEDATA_ERROR;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Text", input);
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd.RecordsAffected;
            }
            catch (OleDbException e)
            {

                return WRITEDATA_ERROR;
            }
        }
        public static int WriteData<T>(string sql, List<T> inputs)//sanitized write data for multiple inputs
        {
            try
            {
                if (!connOpen)
                {
                    if (!OpenConnection())
                        return WRITEDATA_ERROR;
                }
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                for (int i = 1; i <= inputs.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@Text{i}", inputs[i - 1]);
                }
                OleDbDataReader rd = cmd.ExecuteReader();

                return rd.RecordsAffected;
            }
            catch (OleDbException e)
            {

                return WRITEDATA_ERROR;
            }
        }
        public static int InsertWithAutoNumKey(string sql)
        {
            if (!connOpen)
            {
                if (!OpenConnection())
                    return WRITEDATA_ERROR;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader rd = cmd.ExecuteReader();

                if (rd != null && rd.RecordsAffected == 1)
                {
                    //Create a new command for retrieving the new ID
                    //It MUST use the SAME connection!!!!
                    cmd = new OleDbCommand(@"SELECT @@Identity", conn);
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        //The new ID will be on the first (and only) column
                        return (int)rd[0];
                    }
                }
            }
            catch
            {
                return WRITEDATA_ERROR;
            }
            return WRITEDATA_ERROR;
        }//with insert into database it returns new itentity(id)
        public static int InsertWithAutoNumKey<T>(string sql, List<T> inputs)//insert sanitized for multiple inputs
        {
            if (!connOpen)
            {
                if (!OpenConnection())
                    return WRITEDATA_ERROR;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                for (int i = 1; i <= inputs.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@Text{i}", inputs[i - 1]);
                }
                OleDbDataReader rd = cmd.ExecuteReader();

                if (rd != null && rd.RecordsAffected == 1)
                {
                    //Create a new command for retrieving the new ID
                    //It MUST use the SAME connection!!!!
                    cmd = new OleDbCommand(@"SELECT @@Identity", conn);
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        //The new ID will be on the first (and only) column

                        return (int)rd[0];
                    }
                }
            }

            catch (Exception e)
            {

                return WRITEDATA_ERROR;
            }
            return WRITEDATA_ERROR;
        }
        public static int InsertWithAutoNumKey(string sql, string input)//insety sanitized for single input
        {
            if (!connOpen)
            {
                if (!OpenConnection())
                    return WRITEDATA_ERROR;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Text", input);
                OleDbDataReader rd = cmd.ExecuteReader();

                if (rd != null && rd.RecordsAffected == 1)
                {
                    //Create a new command for retrieving the new ID
                    //It MUST use the SAME connection!!!!
                    cmd = new OleDbCommand(@"SELECT @@Identity", conn);
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        //The new ID will be on the first (and only) column

                        return (int)rd[0];
                    }
                }
            }
            catch
            {

                return WRITEDATA_ERROR;
            }
            return WRITEDATA_ERROR;
        }
        public static void CloseConnection()
        {
            if (connOpen)
            {
                conn.Close();
                connOpen = false;
            }
        }//closes connection to the database
        public static DataTable GetDataTable(string sql)
        {
            try
            {
                string query = sql;
                DataTable dataTable = new DataTable();
                OleDbDataReader reader = ReadData(sql);
                if (reader == null)
                    return null;
                dataTable.Load(reader);

                CloseConnection();
                return dataTable;
            }
            catch (OleDbException e)
            {
                CloseConnection();
                return null;
            }
        }//returns a datetable type collection
        public static DataTable GetDataTable(string sql, string text)//getdatatable sanitized for single input
        {
            try
            {
                string query = sql;
                DataTable dataTable = new DataTable();
                OleDbDataReader reader = ReadData(sql, text);
                if (reader == null || !reader.HasRows)
                    return null;
                dataTable.Load(reader);

                CloseConnection();
                return dataTable;
            }
            catch (OleDbException e)
            {
                CloseConnection();
                return null;
            }
        }
        public static DataTable GetDataTable(string sql, List<string> text)//getdatatable sanitized for multiple inputs
        {
            try
            {
                string query = sql;
                DataTable dataTable = new DataTable();
                OleDbDataReader reader = ReadData(sql, text);
                if (reader == null)
                    return null;
                dataTable.Load(reader);

                CloseConnection();
                return dataTable;
            }
            catch (OleDbException e)
            {
                CloseConnection();
                return null;
            }
        }
        public static DataSet GetDataSet(string[] sql)
        {
            if (!connOpen)
            {
                OpenConnection();
            }

            DataSet ds = new DataSet();
            for (int i = 0; i > sql.Length; i++)
            {
                ds.Tables.Add(GetDataTable(sql[i]));
            }

            if (ds != null)
                return ds;
            else
                return null;
        }//returns dataset of datatables
    }
}