
using System.Collections.ObjectModel;


namespace CrossPlatformProject2.ViewModels
{
    public class AchievementsViewModel
    {
        public ObservableCollection<Achievment> Achievements { get; set; }

        public AchievementsViewModel()
        {
            //adding placeholder achievments 
            Achievements = new ObservableCollection<Achievment>
            {
                new Achievment { Title = "First Win", Description = "Win Your First Trivia Galaxy Game!!", IsUnlocked = false },
                new Achievment { Title = "Space Cadet", Description = "Answer 5 questions correctly", IsUnlocked = false },
                new Achievment { Title = "Space Ranger", Description = "Answer 10 questions correctly", IsUnlocked = false }
            };
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
