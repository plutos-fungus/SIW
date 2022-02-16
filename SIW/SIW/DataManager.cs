using Microsoft.Data.Sqlite;

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
            cmd.CommandText = String.Format(@"CREATE TABLE vars(name TEXT PRIMARY KEY NOT NULL, type TEXT CHECK( type IN ('{0}','{1}','{2}') ) NOT NULL, value VARBINARY)", typeof(int).ToString(), typeof(double).ToString(), typeof(string).ToString());
            cmd.ExecuteNonQuery();            
        }

        ~DataManager()
        {
            connection.Close();
        }

        // Adds a variable with name 'name' and value 'val' to the database
        public void NewVar(string name, dynamic val) // dynamic val
        {
            SqliteCommand cmd = new SqliteCommand("INSERT INTO vars(name, type, value) VALUES(@name, @type, @value)", connection);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@type", val.GetType().ToString());
            cmd.Parameters.AddWithValue("@value", val);
            cmd.Prepare();
            // TODO: Refactor into better/more idiomatic error handling code
            cmd.ExecuteNonQuery();
        }

        // Gets value of variable with name 'name'. If variable doesn't exist, exits with error code
        public dynamic GetVar(string name)
        {
            SqliteCommand cmd = new SqliteCommand("SELECT type,value FROM vars WHERE name=@name", connection);
            cmd.Parameters.AddWithValue("@name", name);
            using (SqliteDataReader r = cmd.ExecuteReader())
            {
                if (!r.Read())
                {
                    Console.Error.WriteLine("Error: variable \"{0}\" doesn't exist", name);
                    Environment.Exit(1);
                }
                else
                {
                    Type t = Type.GetType(r.GetString(0))!;
                    if(t == null)
                    {
                        Console.Error.WriteLine("Error: value of type enum in variable table not recognized");
                        Environment.Exit(1);
                    }
                    if(t == typeof(int))
                    {
                        return r.GetFieldValue<int>(1);
                    } else if(t == typeof(double))
                    {
                        return r.GetFieldValue<double>(1);
                    } else if(t == typeof(string))
                    {
                        return r.GetFieldValue<string>(1);
                    } else
                    {
                        Console.Error.WriteLine("Error: type {0} not implemented", t.ToString());
                        Environment.Exit(1);
                    }
                }
            }
            return null;
        }

        // Update variable of name 'name' with value 'val'
        public void SetVar(string name, dynamic val)
        {
            SqliteCommand updateCMD = connection.CreateCommand();
            string? type = null;

            // Returns value of variable if variable exits
            updateCMD.CommandText = "SELECT type FROM vars WHERE name=@name";
            updateCMD.Parameters.AddWithValue("@name", name);
            // check if variable name exists in database
            using (SqliteDataReader r = updateCMD.ExecuteReader())
            {
                if (!r.Read())
                {
                    Console.Error.WriteLine("Error: variable \"{0}\" doesn't exist and therefore cannot be updated", name);
                    Environment.Exit(1);
                } else
                {
                    type = r.GetString(0);
                }
            }
            // check variable type
            updateCMD.CommandText = "SELECT value FROM vars WHERE name=@name AND type=@type";
            updateCMD.Parameters.AddWithValue("@type", val.GetType().ToString());
            using (SqliteDataReader r = updateCMD.ExecuteReader())
            {
                if (!r.Read())
                {
                    Console.Error.WriteLine("Error: variable \"{0}\" (type: {1}) cannot be assigned values of type \"{2}\"", name, type, val.GetType().ToString());
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
