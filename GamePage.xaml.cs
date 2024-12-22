namespace CrossPlatformProject2;
using CrossPlatformProject2.Models; //implement models to gamepage 
using Newtonsoft.Json;//json desrializer

public partial class GamePage : ContentPage
{
    private int score;

    private string selectedPlayers { get; set; }//selected player
    private string selectedDifficulty { get; set; }//difficulty
    private string selectedCategory { get; set; }//category

    private List<string> playerNames { get; set; }//stores player names

    private const string apiURL = "https://opentdb.com/api.php?amount=10&category={category}&difficulty={difficulty}"; //api url 

    private int currentPlayerIndex = 0;// keep track of current player

    private Dictionary<string, int> playerScores = new();//track the players scores 


    public GamePage(string selectedPlayers, string selectedDifficulty, string selectedCategory, List<string> playerNames)
    {
        InitializeComponent();

        this.selectedPlayers = selectedPlayers;
        this.selectedDifficulty = selectedDifficulty;
        this.playerNames = playerNames;
        this.selectedCategory = selectedCategory;

        foreach (var player in playerNames)//start player scores at 0
        {
            playerScores[player] = 0;
        }

        LoadQuestionsFromApi(selectedCategory, selectedDifficulty);//load the questions dynamically from API

    }
    private List<QuestionModel> questions = new List<QuestionModel>(); //list to hold question list
    private int currentQuestionIndex = 0; //keep track of question
    private async void LoadQuestionsFromApi(string selectedCategory, string selectedDifficulty)
    {
       

        using HttpClient client = new HttpClient();
        var response = await client.GetStringAsync(apiURL);

        //async request to get from api
        var root = JsonConvert.DeserializeObject<Root>(response); 
    }

    private void OnAnswerClicked(object sender, EventArgs e)
    {
        //idnetify which button was clickedd
        Button clickedButton = sender as Button;//sender is the button that was clicked
        //cast sender to button, to access its properties

        //check if answer is correct
        //compare button text to correct answer, placeholder for now 
        if (clickedButton.Text == "Correct Answer") 
        {
            //increment score
            score++;
            //update the score UI
            
        }
        else
        {
            //display for incorret answer
            DisplayAlert("Wrong!!", "Better luck next time", "Ok");
        }
        //place holder text for next question
        questionLabel.Text = "Next question goes here.";
    }

    private void OnSaveGameClicked(object sender, EventArgs e)
    {
        //placeholder for save game
        //implementation will include saving game and scores
        DisplayAlert("Save Game", "Game progress saved successfuly.", "OK"); 
    }
}