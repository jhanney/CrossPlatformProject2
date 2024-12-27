using Newtonsoft.Json;
using CrossPlatformProject2.Models; //implement models to gamepage  

namespace CrossPlatformProject2;

public partial class GameSetup : ContentPage
{
    private List<Entry> playerNameEntriesList = new List<Entry>();//list to store player names

    private static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "SavedGame.json");//file path

    //dictionary to map category names to IDs
    private Dictionary<string, int> Categories { get; set; }
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

        LoadCategories();//method to load categories from API
    }

    private async void LoadCategories()
    {
        string apiUrl = "https://opentdb.com/api.php?amount=10";

        using HttpClient client = new HttpClient();
        var response = await client.GetStringAsync(apiUrl);

        // Deserialize JSON into CategoryResponse
        var root = JsonConvert.DeserializeObject<Root>(response);


        //extract categories
        if (root?.results != null)
        {
            var uniqueCategories = root.results
                .Select(result => result.category)
                .Distinct() // Remove duplicates
                .ToList();


            foreach (  var category in uniqueCategories)
            {
                categoryPicker.Items.Add(category); // Populate the picker with category names
            }
        }
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
        string selectedCategory = categoryPicker.SelectedItem.ToString();


        //push these values to the game page for use
        await Navigation.PushAsync(new GamePage(selectedPlayers, selectedDifficulty, selectedCategory, playerNames));

    }

    private async Task LoadGameFromFile()
    {
        if (File.Exists(GamePage.FilePath))
        {
            string json = await File.ReadAllTextAsync(GamePage.FilePath);

            var gameState = JsonConvert.DeserializeObject<GameState>(json);

            if (gameState != null)
            {
                //navigate to the GamePage with the loaded data
                await Navigation.PushAsync(new GamePage(
                    gameState.PlayerNames.Count.ToString() + " Players", // Use number of players
                    gameState.SelectedDifficulty,
                    gameState.SelectedCategory,
                    gameState.PlayerNames,
                    gameState
                ));
            }
            else
            {
                await DisplayAlert("Error", "Failed to load the saved game.", "OK");
            }
        }
        else
        {
            await DisplayAlert("No Saved Game", "No saved game data found.", "OK");
        }
    }

    private async void OnLoadGameClicked(object sender, EventArgs e)
    {
        await LoadGameFromFile();
    }



    private async void homeButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}