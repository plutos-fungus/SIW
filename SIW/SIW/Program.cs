namespace SIW // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
           
            //Parameters.AddWithValue("@nameValue", nameValue);

            DataManager d = new DataManager(@"CREATE TABLE vars(id INTEGER PRIMARY KEY,
            name TEXT, value INT)");
            string nameValue = "Tesla";
            string priceValue = "3";
            string RandomString = String.Format("INSERT INTO cars(name, price) VALUES('{0}', {1})", nameValue, priceValue);
            d.ExecuteNonQuery(RandomString);
            d.test();
        }
    }
}