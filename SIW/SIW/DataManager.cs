using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SIW
{
    /*
     * class DataManager:
     * private database
     * methods for handling database
     */
    public class DataManager
    {
        private const string sourceString = @"Data Source=:memory:";
        private SqliteConnection connection; // stores database connection

        public DataManager(string createTableString)
        {
            connection = new SqliteConnection();
            connection.Open();
            SqliteCommand createCMD = new SqliteCommand(@"CREATE TABLE vars(id INTEGER PRIMARY KEY,
            name TEXT, value INT)", connection);
            createCMD.ExecuteNonQuery();            
        }

        //public Elem<T> GetElementByName<T>(T name)
        //{
        //    return new Elem<T>(name);
        //}

        //public void AddElement<T>(Elem<T> e)
        //{
        //    // Add element elem to 
        //}
        //public void DestroyElement(string name)
        //{

        //}
        public void AddVar(string name, int val)
        {
            ExecuteNonQuery(String.Format("INSERT INTO vars(name, value) VALUES('{0}', {1})", name, val));
        }

        public void ExecuteNonQuery(string command)
        {
            SqliteCommand cmd = new SqliteCommand(command, connection);
            cmd.ExecuteNonQuery();
            return;
        }

        public void test()
        {
            SqliteCommand cmd = new SqliteCommand("SELECT SQLITE_VERSION()", connection);
            Console.WriteLine(cmd
                .ExecuteScalar()!
                .ToString());
        }
    }
}
