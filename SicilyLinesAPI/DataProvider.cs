using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Security.Cryptography;

namespace SicilyLinesAPI
{
    public class DataProvider
    {
        static SqliteConnection sql;

        static List<Client> _clients = new List<Client>();
        static List<Booking> _bookings = new List<Booking>();
        static List<FerryCrossing> _crossings = new List<FerryCrossing>();



        public static List<Client> Clients
        {
            get {
                if (sql == null)
                    sql = new SqliteConnection("DataSource=./db_sicilylines.db");
               
                var db = sql.OpenAsync();
                _clients.Clear();

                var command = sql.CreateCommand();
                command.CommandText = "SELECT * FROM client";
               
                var result = command.ExecuteReaderAsync().Result;

                foreach (DbDataRecord item in result)
                {
                    string date = item.GetString(3);
                    Client c = new Client(item.GetInt64(0), item.GetString(1), item.GetString(2), DateOnly.Parse(date), (Client.Gender) item.GetInt64(4), item.GetString(5), item.GetString(6));

                    _clients.Add(c);
                }

                return _clients;
            }
        }

        public static List<FerryCrossing> Crossings
        {
            get
            {
                if (sql == null)
                    sql = new SqliteConnection("DataSource=./db_sicilylines.db");

                var db = sql.OpenAsync();
                _crossings.Clear();

                var command = sql.CreateCommand();
                command.CommandText = "SELECT * FROM crossing";

                var result = command.ExecuteReaderAsync().Result;

                foreach (DbDataRecord item in result)
                {
                    FerryCrossing c = new FerryCrossing(item.GetInt64(0), item.GetString(1), item.GetString(2));
                    _crossings.Add(c);
                }

                return _crossings;
            }
        }
        public static List<Booking> Bookings
        {
            get
            {
                if (sql == null)
                    sql = new SqliteConnection("DataSource=./db_sicilylines.db");

                var db = sql.OpenAsync();
                _bookings.Clear();

                var command = sql.CreateCommand();
                command.CommandText = "SELECT * FROM booking";

                var result = command.ExecuteReaderAsync().Result;

                foreach (DbDataRecord item in result)
                {
                    Booking b = new Booking(item.GetInt64(0), Clients[(int) item.GetInt64(1) - 1], Crossings[(int) item.GetInt64(2)], DateTime.Parse(item.GetString(3)));

                    _bookings.Add(b);
                }


                return _bookings;
            }
        }

        public static async void UpdateClient(Client newClient)
        {
            var command = sql.CreateCommand();
            command.CommandText = "UPDATE client SET birth_date=@birthDate, password=@password WHERE id=@id;";
            command.Parameters.AddWithValue("@birthDate", newClient.BirthDate);
            command.Parameters.AddWithValue("@password", newClient.Password);
            command.Parameters.AddWithValue("@id", newClient.Id);
            
            await command.PrepareAsync();
            await command.ExecuteNonQueryAsync();
        }

    }
}
