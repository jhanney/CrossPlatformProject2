namespace CrossPlatformProject2;
using CrossPlatformProject2.ViewModels; 

public partial class Achievements : ContentPage
{
    private MainPage mainPage;//acces methods in mainpage
    public Achievements(MainPage mainPage)
    {
        InitializeComponent(); 
        this.mainPage = mainPage; //assign to mainpage
        BindingContext = new AchievementsViewModel();//add binding context for achievments viewmodel
    }

    private void homeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();//return to home
    }
}