namespace CrossPlatformProject2;

public partial class GamePage : ContentPage
{

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
