
using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    public class AchievementsViewModel
    {
        public ObservableCollection<Achievment> Achievements { get; set; }

        private static readonly string AchievementsFilePath = Path.Combine(FileSystem.AppDataDirectory, "AchievementsData.json"); //filepath for achievements

        public AchievementsViewModel()
        {
            //load achievements from file or initialize with default achievements if the file doesn't exist
            Achievements = LoadAchievementsFromFile() ?? new ObservableCollection<Achievment>
            {
                new Achievment { Title = "First Win", Description = "Win Your First Trivia Galaxy Game!!", IsUnlocked = false },
                new Achievment { Title = "Space Cadet", Description = "Answer 5 questions correctly", IsUnlocked = false },
                new Achievment { Title = "Space Ranger", Description = "Answer 10 questions correctly", IsUnlocked = false }
            };
        }

        private ObservableCollection<Achievment>? LoadAchievementsFromFile()
        {
            throw new NotImplementedException();
        }
    }
    public class Achievment
    {
        public string Title { get; set; } //title of the achievement
        public string Description { get; set; }//achievment description
        public bool IsUnlocked { get; set; }//whther its been completed

        public int PointThreshold { get; set; } //points required to unlock this achievement

        //determins which trophy image to show
        public string TrophyImage => IsUnlocked ? "trophy.png" : "lock.png"; 
    }
}
