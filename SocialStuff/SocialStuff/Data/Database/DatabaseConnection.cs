using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SocialStuff.Data.Database
{
    public class DatabaseConnection
    {
        string connectionString = @"Data Source=DESKTOP-VRGLOTC\SQLEXPRESS;Initial Catalog=BankingDB;Integrated Security=True;TrustServerCertificate=True";


        private SqlConnection conn;

        public DatabaseConnection()
        {
            conn = new SqlConnection(connectionString);
            Console.WriteLine("Database Connection Created!");
        }

        public SqlConnection getConnection()
        {
            return conn;
        }

        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                Console.WriteLine("Database Connected!");

            }
        }

        public int CheckConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Database Connection is Open!");
                // print something on the screen
                return 1;
            }
            else
            {
                Console.WriteLine("Database Connection is Closed!");
                // print something on the screen
                return 0;
            }

        }
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Database Connection Closed!");
                // print something on the screen

            }
        }

        // Executes a stored procedure and returns a single scalar value (e.g., COUNT(*), SUM(), MAX(), etc.)
        public T? ExecuteScalar<T>(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(storedProcedure, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }

                    var result = command.ExecuteScalar();
                    if (result == DBNull.Value || result == null)
                    {
                        return default;
                    }

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - ExecutingScalar: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }


        // Executes a stored procedure and returns multiple rows and columns as a DataTable
        public DataTable ExecuteReader(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            Console.WriteLine("Haide ma");
            try
            {
                Console.WriteLine("0"); 
                OpenConnection();
                Console.WriteLine("i");
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    Console.WriteLine("1");
                    if (isStoredProcedure)
                    {
                        Console.WriteLine("2");
                        command.CommandType = CommandType.StoredProcedure;
                        Console.WriteLine("3");
                    }
                    else
                    {
                        Console.WriteLine("4");
                        command.CommandType = CommandType.Text;
                        Console.WriteLine("5");
                    }

                    Console.WriteLine("6");
                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - ExecuteReader: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }



        // Executes a stored procedure that modifies data (INSERT, UPDATE, DELETE) and returns the number of affected rows
        //Alexandra- i ve changes such dat it also works with query
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand command = new SqlCommand(storedProcedure, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error - ExecuteNonQuery: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

    }


}