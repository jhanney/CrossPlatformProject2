using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformProject2.ViewModels
{
    class LeaderboardViewModel
    {
        public ObservableCollection<ScoreEntry> Scores { get; set; }

        public class ScoreEntry
        {
            public string playerName { get; set; }
            public int score { get; set; }
        }
    }
}
