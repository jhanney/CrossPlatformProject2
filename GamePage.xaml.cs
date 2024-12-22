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
    private List<QuestionModel> triviaQuestions = new List<QuestionModel>(); // Stores fetched trivia questions
                                                                             //list to hold question list
    private int currentQuestionIndex = 0; //keep track of question
    private async void LoadQuestionsFromApi(string selectedCategory, string selectedDifficulty)
    {
        try
        {

            using HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(apiURL);

            //async request to get from api
            var root = JsonConvert.DeserializeObject<Root>(response);

            if (root?.response_code == 0 && root.results?.Count > 0)//ensures root and properties not null
            {
                triviaQuestions = root.results.Select(result => new QuestionModel //populate trivia questions list with data from API
                {
                    Question = result.question,
                    CorrectAnswer = result.correct_answer,
                    IncorrectAnswers = result.incorrect_answers
                }).ToList();

                DisplayQuestion();
            }
            else //display in case of error
            {
                await DisplayAlert("Error", "No questions available for the selected options.", "OK");
            }
        }
        catch (Exception ex) //display alert if questions not loaded
        {
            await DisplayAlert("Error", $"Failed to load questions: {ex.Message}", "OK");
        }

    }

    private void DisplayQuestion()
    {
        //ensure index is within range 
        if (currentQuestionIndex < triviaQuestions.Count)
        {
            var question = triviaQuestions[currentQuestionIndex];//retireve question object from the list

            questionLabel.Text = question.Question;//update UI

            //make a list with correct and incorrect answers
            var answers = question.IncorrectAnswers
                .Concat(new[] { question.CorrectAnswer })//add correct answer
                .OrderBy(_ => Guid.NewGuid())//shuffle answers
                .ToList();//add result to list

            //display shuffled answers on the buttons
            answerButton1.Text = answers[0]; 
            answerButton2.Text = answers[1]; 
            answerButton3.Text = answers[2];
            answerButton4.Text = answers[3];
        }
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