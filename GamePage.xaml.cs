namespace CrossPlatformProject2;

public partial class GamePage : ContentPage
{
    private int score;

    private string selectedPlayers { get; set; }//selected player
    private string selectedDifficulty { get; set; }//difficulty

    private List<string> playerNames { get; set; }//stores player names

    public GamePage(string selectedPlayers, string selectedDifficulty, List<string> playerNames)
    {
        InitializeComponent();

        this.selectedPlayers = selectedPlayers;
        this.selectedDifficulty = selectedDifficulty;
        this.playerNames = playerNames;

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
}