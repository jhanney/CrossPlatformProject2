namespace CrossPlatformProject2;

public partial class Leaderboard : ContentPage
{
    private MainPage mainPage; 
	public Leaderboard(MainPage mainPage)
	{
		InitializeComponent(); 
        this.mainPage = mainPage;//store the reference to mainpage 
	}

    private void homeButton_Clicked(object sender, EventArgs e)//navigate back to home page
    {
        //restart rotation again
        mainPage.startRotation(); 
        Navigation.PopAsync(); //navigate back
    }
}