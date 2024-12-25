
using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    class LeaderboardViewModel
    {
        public ObservableCollection<ScoreEntry> Scores { get; set; } //observalble collection to store scores

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

        public class ScoreEntry //class represents data entered for a score 
        {
            public string playerName { get; set; }
            public int score { get; set; }
        }
    }
}
