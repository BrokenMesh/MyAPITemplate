using MySql.Data.MySqlClient;
using MyAPITemplate.Logic;

namespace MyAPITemplate.Database
{
    public partial class DBManager
    {
        public static MySqlConnection DB;
        private static bool isStreamOpen = true;

        public static void StartDataBaseConnection() {
            string _constr =
                $"server={Config.current.DB_host};" +
                $"userid={Config.current.DB_username};" +
                $"password={Config.current.DB_password};" +
                $"database={Config.current.DB_database};";

            DB = new MySqlConnection(_constr);
            DB.Open();
            Console.WriteLine($"MySql version: {DB.ServerVersion}");

        }

        public static void SetForeignKeyChecks(bool _state) {
            OpenStream();

            string _sql = "SET FOREIGN_KEY_CHECKS = @state;";
            using (MySqlCommand _command = new MySqlCommand(_sql, DB)) {
                _command.Parameters.AddWithValue("@state", _state);

                _command.ExecuteNonQuery();
            }

            CloseStream();
        }

        public static List<T> ExecuteQuery<T>(string sql, Dictionary<string, object> parameters, Func<MySqlDataReader, T> mapFunction) {
            List<T> results = new List<T>();

            OpenStream();

            using (MySqlCommand command = new MySqlCommand(sql, DB)) {
                foreach (var param in parameters) {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        results.Add(mapFunction(reader));
                    }
                }
            }

            CloseStream();

            return results;
        }

        public static void ExecuteNonQuery(string sql, Dictionary<string, object> parameters) {
            OpenStream();

            using (MySqlCommand command = new MySqlCommand(sql, DB)) {
                foreach (var param in parameters) {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                command.ExecuteNonQuery();
            }

            CloseStream();
        }

        public static long ExecuteInsert(string sql, Dictionary<string, object> parameters) {
            OpenStream();

            long lastInsertedId = 0;

            using (MySqlCommand command = new MySqlCommand(sql, DB)) {
                foreach (var param in parameters) {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                command.ExecuteNonQuery();
                lastInsertedId = command.LastInsertedId;
            }

            CloseStream();
            return lastInsertedId;
        }


        private static void CloseStream() {
            isStreamOpen = true;
        }

        private static void OpenStream() {
            while (!isStreamOpen) {
                Thread.Sleep(30);
            }
            isStreamOpen = false;
        }
    }
}
