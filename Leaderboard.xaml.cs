namespace CrossPlatformProject2;

public partial class Leaderboard : ContentPage
{
	public Leaderboard()
	{
		InitializeComponent(); 
	}

    private void homeButton_Clicked(object sender, EventArgs e)//navigate back to home page
    {
        Navigation.PopAsync(); 
    }
}