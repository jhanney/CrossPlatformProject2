namespace CrossPlatformProject2;

public partial class GamePage : ContentPage
{

    private string selectedPlayers { get; set; }
    private string selectedDifficulty { get; set; }
    public GamePage(string selectedPlayers, string selectedDifficulty)
	{
		InitializeComponent();

        this.selectedPlayers = selectedPlayers;
        this.selectedDifficulty = selectedDifficulty;
	}
}