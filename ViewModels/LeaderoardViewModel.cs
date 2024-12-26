

using Newtonsoft.Json;
using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    class LeaderboardViewModel
    {
        public ObservableCollection<ScoreEntry> Scores { get; set; } //observalble collection to store scores

        private static readonly string LeaderboardFilePath = Path.Combine(FileSystem.AppDataDirectory, "LeaderboardData.json");//define file path to save scores

        public LeaderboardViewModel(Dictionary<string, int> playerScores)
        {

            // Load saved scores from file if they exist
            Scores = LoadScoresFromFile() ?? new ObservableCollection<ScoreEntry>();

            // If new scores are passed, update the leaderboard
            if (playerScores != null)
            {
                foreach (var score in playerScores)
                {
                    AddOrUpdateScore(score.Key, score.Value);
                }

                // Sort the scores in descending order after updating
                Scores = new ObservableCollection<ScoreEntry>(
                    Scores.OrderByDescending(s => s.score)
                );

                // Save the updated leaderboard to the file
                SaveScoresToFile();
            }
        }

        public void AddOrUpdateScore(string playerName, int score)
        {
            //check if the player already exists in the leaderboard
            var existingEntry = Scores.FirstOrDefault(s => s.playerName == playerName);
            if (existingEntry != null)
            {
                //if the player exists, update their score only if the new score is higher
                existingEntry.score = Math.Max(existingEntry.score, score);
            }
            else
            {
                //if the player does not exist, add a new entry
                Scores.Add(new ScoreEntry { playerName = playerName, score = score });
            }
            //save the updated leaderboard data to file
            SaveScoresToFile();
        }

        private void SaveScoresToFile()
        {
            try
            {
                //serialize scores to JSON and save to file
                var json = JsonConvert.SerializeObject(Scores);
                File.WriteAllText(LeaderboardFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save leaderboard: {ex.Message}");
            }
        }

        private ObservableCollection<ScoreEntry> LoadScoresFromFile()
        {
            try
            {
                // Check if the leaderboard file exists
                if (File.Exists(LeaderboardFilePath))
                {
                    // Read JSON from the file and deserialize it
                    var json = File.ReadAllText(LeaderboardFilePath);
                    return JsonConvert.DeserializeObject<ObservableCollection<ScoreEntry>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load leaderboard: {ex.Message}");
            }
            return null; // Return null if no file exists or error occurs
        }

        public class ScoreEntry //class represents data entered for a score 
        {
            public string playerName { get; set; }
            public int score { get; set; }
        }
    }
}
