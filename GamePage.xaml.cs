namespace CrossPlatformProject2;
using CrossPlatformProject2.Models; //implement models to gamepage 
using Newtonsoft.Json;//json desrializer
using System.Net;
public partial class GamePage : ContentPage
{
    private int score;

    private string selectedPlayers { get; set; }//selected player
    private string selectedDifficulty { get; set; }//difficulty
    private string selectedCategory { get; set; }//category
     private int categoryID { get; set; }

    private List<string> playerNames { get; set; }//stores player names

    private const string apiURL = "https://opentdb.com/api.php?amount=10"; //api url 

    private int currentPlayerIndex = 0;// keep track of current player

    private Dictionary<string, int> playerScores = new();//track the players scores 

    public static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "SavedGame.json"); //file path


    public GamePage(string selectedPlayers, string selectedDifficulty, string selectedCategory, List<string> playerNames, GameState gameState = null)
    {
        InitializeComponent();

        if (gameState != null)
        {
            //load from game state 
            this.selectedPlayers = gameState.SelectedPlayers; 
            this.selectedDifficulty = gameState.SelectedDifficulty;
            this.selectedCategory = gameState.SelectedCategory;
            this.playerNames = gameState.PlayerNames;
            this.triviaQuestions = gameState.TriviaQuestions;
            this.currentQuestionIndex = gameState.CurrentQuestionIndex;
            this.currentPlayerIndex = gameState.CurrentPlayerIndex;
            this.playerScores = gameState.PlayerScores;

            DisplayQuestion();//diplay next question
        }
        else
        {
            //start new game
            this.selectedPlayers = selectedPlayers;
            this.selectedDifficulty = selectedDifficulty;
            this.selectedCategory = selectedCategory;
            this.playerNames = playerNames;

            foreach (var player in playerNames)
            {
                playerScores[player] = 0;
            }

            //load from the api 
            //LoadQuestionsFromApi(selectedCategory, selectedDifficulty); 
            _ = InitializeGameAsync();//asynchronosouly loads game, as first question after loading appearing blank
        }

    }

    private async Task InitializeGameAsync()
    {
        await LoadQuestionsFromApi(selectedCategory, selectedDifficulty);

        if (triviaQuestions.Any())
        {
            DisplayQuestion();//questions displayed after being loaded
        }
        else
        {
            await DisplayAlert("Error", "Failed to load questions. Please try again.", "OK");
            await Navigation.PopToRootAsync(); //if no options available returns to main page 
        }
    }

    private List<QuestionModel> triviaQuestions = new List<QuestionModel>(); // Stores fetched trivia questions
                                                                             //list to hold question list
    private int currentQuestionIndex = 0; //keep track of question
    private async Task LoadQuestionsFromApi(string selectedCategory, string selectedDifficulty)
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
                    //decode any html
                    Question = WebUtility.HtmlDecode(result.question),
                    CorrectAnswer = WebUtility.HtmlDecode(result.correct_answer),
                    IncorrectAnswers = result.incorrect_answers.Select(WebUtility.HtmlDecode).ToList()
                }).ToList();

               // DisplayQuestion();
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

    private async void DisplayQuestion()
    {
        //ensure index is within range 
        if (currentQuestionIndex < triviaQuestions.Count)
        {
            var question = triviaQuestions[currentQuestionIndex];//retireve question object from the list

            //decode html coded questions
            questionLabel.Text = WebUtility.HtmlDecode(question.Question);

            

            //make a list with correct and incorrect answers
            var answers = question.IncorrectAnswers
                .Concat(new[] { question.CorrectAnswer })//add correct answer
                .OrderBy(_ => Guid.NewGuid())//shuffle answers
                .ToList();//add result to list

            //display shuffled answers on the buttons
            if(answers.Count == 4)
            {
                answerButton1.Text = answers[0];
                answerButton2.Text = answers[1];
                answerButton3.Text = answers[2];
                answerButton4.Text = answers[3];
            }
        }
        else
        {
            await DisplayAlert("Error", "Invalid question data detected. Skipping to the next question.", "OK");
            currentQuestionIndex++;
            if (currentQuestionIndex < triviaQuestions.Count)
                DisplayQuestion();
            else
                endGame();
        }
    }

    private async void endGame()
    {
        //score message for end of game
        string scoresMessage = "Game Over! Here are the final scores:\n";

        foreach (var playerScore in playerScores)
        {
            scoresMessage += $"{playerScore.Key}: {playerScore.Value} points\n";
        }
        //get highest score
        int highestScore = playerScores.Values.Max();

        //determine winner
        var winners = playerScores
            .Where(p => p.Value == highestScore) //where highest score
            .Select(p => p.Key)//get player name
            .ToList();

        if (winners.Count == 1)
        {
            scoresMessage += $"\nWinner: {winners[0]} with {highestScore} points!";
        }
        else
        {
            scoresMessage += $"\nIt's a tie! Winners: {string.Join(", ", winners)} with {highestScore} points!";
        }


        //display final scores
        await DisplayAlert("Final Scores", scoresMessage, "OK");

        // Navigate to the LeaderboardPage with player scores
        await Navigation.PushAsync(new Leaderboard((MainPage)Application.Current.MainPage, playerScores));
    

        //navigate to home page
        await Navigation.PopToRootAsync();
    }

    private void OnAnswerClicked(object sender, EventArgs e)
    {
        //idnetify which button was clickedd
        Button clickedButton = sender as Button;//sender is the button that was clicked
        //cast sender to button, to access its properties

        if (clickedButton == null)
        {
            return;
        }

        var currentQuestion = triviaQuestions[currentQuestionIndex];//retireve current index of question and question
        string currentPlayerName = playerNames[currentPlayerIndex];//retrieves the name of the player whose turn it currently is

        //check if answer is correct
        //compare button text to correct answer, placeholder for now 
        if (clickedButton.Text == currentQuestion.CorrectAnswer)
        {
            //calculate points based on difficulty
            int points = selectedDifficulty switch
            {
                "Easy" => 2,
                "Medium" => 6,
                "Hard" => 10,
                _ => 0 //default
            };

            //updated score for player
            playerScores[currentPlayerName] += points;
            DisplayAlert("Correct!", $"{currentPlayerName} got it right and earned {points} points!", "Next");//display alert now shows points earned
        }
        else
        {
            //display for incorret answer
            DisplayAlert("Wrong!!", "Better luck next time", "Ok");
        }
            currentQuestionIndex++; //increment index of question
            currentPlayerIndex = (currentPlayerIndex + 1) % playerNames.Count;//move to next player in the list

        if (currentQuestionIndex < triviaQuestions.Count) 
            DisplayQuestion();
        else
            endGame();

        //place holder text for next question
        //questionLabel.Text = "Next question goes here.";
    }

    private async Task SaveGameToFile()//save the game state to file 
    {
        try
        {
            var gameState = new GameState
            {
                PlayerScores = playerScores,
                CurrentQuestionIndex = currentQuestionIndex,
                TriviaQuestions = triviaQuestions,//save questions
                CurrentPlayerIndex = currentPlayerIndex,
                SelectedDifficulty = selectedDifficulty,
                SelectedCategory = selectedCategory,
                PlayerNames = playerNames
            };
            //save game state
            var gameStateJson = JsonConvert.SerializeObject(gameState);
            await File.WriteAllTextAsync(FilePath, gameStateJson);

            await DisplayAlert("Save Game", "Game progress saved successfully.", "OK");
        }
        catch (Exception ex) 
        {
            await DisplayAlert("Error", $"Failed to save game: {ex.Message}", "OK");
        }
    }

    

    private async void OnSaveGameClicked(object sender, EventArgs e)
    {
        try
        {
            //save game
            await SaveGameToFile();

            //display game saved message
            await DisplayAlert("Save Game", "Game progress saved successfully.", "OK");

            //take user back to home page
            await Navigation.PopToRootAsync();
        }
        catch (Exception ex)
        {
            //error handling
            await DisplayAlert("Error", $"Failed to save the game: {ex.Message}", "OK");
        }
    }
}