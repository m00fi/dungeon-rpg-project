namespace dungeonRPG.Config;

public class GameConfig
{
    public string PlayerName { get; set; } = "default";
    public string LogDirectory {get; set;} = "logs";

    public void LoadConfig()
    {
        string configPath = Path.Combine("Config", "game_config.json");
        
        //Console.WriteLine($"Loading config file: {configPath}");
        //Console.ReadKey();

        if (File.Exists(configPath))
        {
            try
            {
                string json = File.ReadAllText(configPath);
                var config = System.Text.Json.JsonSerializer.Deserialize<GameConfig>(json);
                if (config != null)
                {
                    PlayerName = config.PlayerName;
                    LogDirectory = config.LogDirectory;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load config file: {ex.Message}");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Config file not found. Using defaults. Press any key to continue...");
            Console.ReadKey();
        }
    }
}