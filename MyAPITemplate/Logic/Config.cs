using System.Text.Json;

namespace MyAPITemplate.Logic
{
    public class Config
    {
        public string PageURL { get; set; }
        public string DB_host { get; set; }
        public string DB_username { get; set; }
        public string DB_password { get; set; }
        public string DB_database { get; set; }
        public string SecretKey { get; set; }

        public Config() {
            PageURL = "localhost";
            DB_host = "localhost";
            DB_username = "root";
            DB_password = "";
            DB_database = "Template_DB";
            SecretKey = "<key>";
        }

        public static Config? current;

        public static void LoadConfig(string _filepath) {
            try {
                string _cfg = File.ReadAllText(_filepath);
                current = JsonSerializer.Deserialize<Config>(_cfg);
                if (current == null) throw new Exception("Config not created");
                Console.WriteLine("Loaded Config: " + _filepath);
            }
            catch {
                current = new Config();
                SaveConfig(_filepath);
                Console.WriteLine("Created Config: " + _filepath);
            }
        }

        public static void SaveConfig(string _filepath) {
            string _cfg = JsonSerializer.Serialize(current);
            File.WriteAllText(_filepath, _cfg);
        }

    }
}
