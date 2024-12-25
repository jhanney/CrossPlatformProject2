

using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    class LeaderboardViewModel
    {
        public ObservableCollection<ScoreEntry> Scores { get; set; } //observalble collection to store scores

        private static readonly string LeaderboardFilePath = Path.Combine(FileSystem.AppDataDirectory, "LeaderboardData.json");//define file path to save scores

        public LeaderboardViewModel(Dictionary<string, int> playerScores)
        {


            Scores = new ObservableCollection<ScoreEntry>(
               playerScores
                   .OrderByDescending(score => score.Value)//sort score in descending order
                   .Select(score => new ScoreEntry
                   {
                       playerName = score.Key,
                       score = score.Value
                   }));
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
            throw new NotImplementedException();
        }

        public class ScoreEntry //class represents data entered for a score 
        {
            public string playerName { get; set; }
            public int score { get; set; }
        }
    }
}
