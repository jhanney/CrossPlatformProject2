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

    private void OnStartButtonClicked_Clicked(object sender, EventArgs e)
    {

    }
}