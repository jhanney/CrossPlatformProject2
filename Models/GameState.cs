

namespace CrossPlatformProject2.Models
{
    public class GameState //game state page to save
    {
        public Dictionary<string, int> PlayerScores { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public List<QuestionModel> RemainingQuestions { get; set; }
        public int CurrentPlayerIndex { get; set; }
        public string SelectedDifficulty { get; set; }
        public string SelectedCategory { get; set; }
        public List<string> PlayerNames { get; set; }
    }
}
