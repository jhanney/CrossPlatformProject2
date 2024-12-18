namespace CrossPlatformProject2;

public partial class GameSetup : ContentPage
{
	public GameSetup()
	{
		InitializeComponent();
        //add options for player selection
		playerPicker.Items.Add("1 Player");
        playerPicker.Items.Add("2 Player");
        playerPicker.Items.Add("3 Player");
        playerPicker.Items.Add("4 Player");

        //add difficulty options
        difficultyPicker.Items.Add("Easy");
        difficultyPicker.Items.Add("Medium");
        difficultyPicker.Items.Add("Hard");
    }

    private async void OnStartButtonClicked_Clicked(object sender, EventArgs e)
    {
        if (playerPicker.SelectedItem != null)//method to validate player picker selection
        {
            //display alert
            await DisplayAlert("Error", "Please select the number of players.", "OK");
            return; 
        }

        if (difficultyPicker.SelectedItem != null)//method to check if difficulty selected
        {

            //display alert
            await DisplayAlert("Error", "Please select the difficulty.", "OK");
            return;
        }
        //conver items to strings, will be used in game session
        string selectedPlayers = playerPicker.SelectedItem.ToString(); 
        string selectedDifficulty = difficultyPicker.SelectedItem.ToString();


        //push these values to the game page for use
        await Navigation.PushAsync(new GamePage(selectedPlayers, selectedDifficulty));

    }

    private async void homeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}