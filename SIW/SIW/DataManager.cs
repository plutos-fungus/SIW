﻿using Microsoft.Data.Sqlite;

namespace SIW
{
    // Opens and manages a connection to an SQLITE database in RAM
    public class DataManager
    {
        private const string sourceString = @"Data Source=:memory:";
        private SqliteConnection connection; // stores database connection

        // Opens a connection to a new database of variables (TODO: Change to generic database) in memory
        public DataManager()
        {
            connection = new SqliteConnection(sourceString);
            connection.Open();
            SqliteCommand cmd = new SqliteCommand("DROP TABLE IF EXISTS vars", connection);
            cmd.ExecuteNonQuery();            
            cmd.CommandText = @"CREATE TABLE vars(name TEXT PRIMARY KEY, value INT)";
            cmd.ExecuteNonQuery();            
        }

        ~DataManager()
        {
            connection.Close();
        }

        // Adds a variable with name 'name' and value 'val' to the database
        public void NewVar(string name, int val)
        {
            SqliteCommand cmd = new SqliteCommand("INSERT INTO vars(name, value) VALUES(@name, @value)", connection);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@value", val);
            cmd.Prepare();
            // TODO: Refactor into better/more idiomatic error handling code
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqliteException)
            {
                Console.Error.WriteLine("Error: variable name \"{0}\" already used", name);
                Environment.Exit(1);
            }
        }

        // Gets value of variable with name 'name'. If variable doesn't exist, exits with error code
        public int GetVar(string name)
        {
            SqliteCommand cmd = new SqliteCommand("SELECT value FROM vars WHERE name=@name", connection);
            cmd.Parameters.AddWithValue("@name", name);
            using (SqliteDataReader r = cmd.ExecuteReader())
            {
                if (!r.Read())
                {
                    Console.Error.WriteLine("Error: variable \"{0}\" doesn't exist", name);
                    Environment.Exit(1);
                    return -1;
                }
                else
                {
                    return r.GetInt32(0);
                }
            }
        }

        // Update variable of name 'name' with value 'val'
        public void SetVar(string name, int val)
        {
            SqliteCommand updateCMD = connection.CreateCommand();

            // Returns value of variable if variable exits
            updateCMD.CommandText = "SELECT value FROM vars WHERE name=@name";
            updateCMD.Parameters.AddWithValue("@name", name);
            // check if variable name exists in database
            using (SqliteDataReader r = updateCMD.ExecuteReader())
            {
                if (!r.Read())
                {
                    Console.Error.WriteLine("Error: variable \"{0}\" doesn't exist and thus cannot be updated", name);
                    Environment.Exit(1);
                }
            }
            // update the value
            updateCMD.CommandText = "UPDATE vars SET value=@value WHERE name=@name";
            updateCMD.Parameters.AddWithValue("@value", val);
            updateCMD.ExecuteNonQuery();
        }

        public void DestroyVar(string name)
        {
            SqliteCommand destroyCMD = connection.CreateCommand();
            destroyCMD.CommandText = "DELETE FROM vars WHERE name=@name";
            destroyCMD.Parameters.AddWithValue("@name", name);
            destroyCMD.ExecuteNonQuery();
        }
        
        //Executes any non-query on the database
        private void ExecuteNonQuery(string command)
        {
            SqliteCommand cmd = new SqliteCommand(command, connection);
            cmd.ExecuteNonQuery();
            return;
        }

        // Test the DataManager by trying to print the sqlite version
        public void test()
        {
            SqliteCommand cmd = new SqliteCommand("SELECT SQLITE_VERSION()", connection);
            Console.WriteLine(cmd
                .ExecuteScalar()!
                .ToString());
        }
    }
}
