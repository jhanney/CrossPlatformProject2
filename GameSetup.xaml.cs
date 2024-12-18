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

    }
}