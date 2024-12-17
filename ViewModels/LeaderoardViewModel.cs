
using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    class LeaderboardViewModel
    {
        public ObservableCollection<ScoreEntry> Scores { get; set; } //observalble collection to store scores

        public LeaderboardViewModel()
        {
            Scores = new ObservableCollection<ScoreEntry>
            {
             new ScoreEntry { playerName = "Josh", score = 100 },
             new ScoreEntry { playerName = "Trish", score = 90 },
             new ScoreEntry { playerName = "Joe", score = 80 }
            };
        }

        public class ScoreEntry //class represents data entered for a score 
        {
            public string playerName { get; set; }
            public int score { get; set; }
        }
    }
}
