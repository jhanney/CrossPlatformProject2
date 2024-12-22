namespace CrossPlatformProject2;

public partial class GameSetup : ContentPage
{
    private List<Entry> playerNameEntriesList = new List<Entry>();//list to store player names
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

        //placeholder category options
        categoryPicker.Items.Add("General Knowledge");
        categoryPicker.Items.Add("Science");
        categoryPicker.Items.Add("History");
        categoryPicker.Items.Add("Sports");
        categoryPicker.Items.Add("Movies");
    }


    private void OnPlayerCountChanged(object sender, EventArgs e)
    {
        //clear prvious player names entered
        playerNameEntries.Clear();
        playerNameEntriesList.Clear();

        if (playerPicker.SelectedIndex < 0)
        {
            return;
        }
        //calculate number of players
        int numberOfPlayers = playerPicker.SelectedIndex + 1;

        //for loop to add player names entries based on selection
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            var entry = new Entry
            {
                Placeholder = $"Enter player {i} Name",
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                FontSize = 16,
            };
            //add player entry
            playerNameEntriesList.Add(entry);
            playerNameEntries.Children.Add(entry);
        }
    }

    private async void OnStartButtonClicked_Clicked(object sender, EventArgs e)
    {
        if (playerPicker.SelectedItem == null)//method to validate player picker selection
        {
            //display alert
            await DisplayAlert("Error", "Please select the number of players.", "OK");
            return; 
        }

        if (difficultyPicker.SelectedItem == null)//method to check if difficulty selected
        {

            //display alert
            await DisplayAlert("Error", "Please select the difficulty.", "OK");
            return;
        }

        //get the player names from the entry fiels
        var playerNames = playerNameEntriesList
                //ensures only non empty fields
                .Where(entry => !string.IsNullOrWhiteSpace(entry.Text))//takes the player names
                .Select(entry => entry.Text)
                .ToList();//adds to list

        //checks if number of players matches names filled
        if (playerNames.Count != playerPicker.SelectedIndex + 1)
        {
            await DisplayAlert("Error", "Please fill in all player names.", "OK");//displays if not
            return;
        }

        //conver items to strings, will be used in game session
        string selectedPlayers = playerPicker.SelectedItem.ToString(); 
        string selectedDifficulty = difficultyPicker.SelectedItem.ToString();


        //push these values to the game page for use
        await Navigation.PushAsync(new GamePage(selectedPlayers, selectedDifficulty, playerNames));

    }
  

    private async void homeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}