
using Newtonsoft.Json;
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
                new Achievment { Title = "First Points", Description = "Score your first points", IsUnlocked = false, PointThreshold = 1 },
                new Achievment { Title = "Space Cadet", Description = "Score 6 points", IsUnlocked = false, PointThreshold = 6 },
                new Achievment { Title = "Space Ranger", Description = "Score 10 points", IsUnlocked = false, PointThreshold = 10 },
                new Achievment { Title = "Galaxy Explorer", Description = "Score 20 points", IsUnlocked = false, PointThreshold = 20 },
                new Achievment { Title = "Trivia Master", Description = "Score 100 points", IsUnlocked = false, PointThreshold = 100 }
            };
        }

        //update achievements dynamically based on the current score
        public void UpdateAchievements(int currentScore)
        {
            //check and loop through each achievement
            foreach (var achievement in Achievements)
            {
                //ff the achievement is not unlocked and the player's score meets or exceeds the threshold, unlock it
                if (!achievement.IsUnlocked && currentScore >= achievement.PointThreshold)
                {
                    achievement.IsUnlocked = true; //unlock completed achievment
                }
            }

          
            SaveAchievementsToFile();//save updated achievments 
        }

        private void SaveAchievementsToFile()
        {
            try
            {
                //convert the achievements collection to JSON format
                var json = JsonConvert.SerializeObject(Achievements);


                File.WriteAllText(AchievementsFilePath, json);//write to file
            }
            catch(Exception ex)
            {
                //display an alert to the user
                Application.Current.MainPage?.DisplayAlert("Error", $"Failed to save achievements: {ex.Message}", "OK");
            }
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
