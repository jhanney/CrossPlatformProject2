namespace CrossPlatformProject2;
using CrossPlatformProject2.ViewModels; //initialise the view models folder

public partial class Leaderboard : ContentPage
{
    private MainPage mainPage; 
	public Leaderboard(MainPage mainPage)
	{
		InitializeComponent(); 
        this.mainPage = mainPage;//store the reference to mainpage 
        BindingContext = new LeaderboardViewModel(); //set the binding context to leaderboardView model
	}

    private void homeButton_Clicked(object sender, EventArgs e)//navigate back to home page
    {
        //restart rotation again
        mainPage.startRotation(); 
        Navigation.PopAsync(); //navigate back
    }
}