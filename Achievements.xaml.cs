namespace CrossPlatformProject2;

public partial class Achievements : ContentPage
{
    private MainPage mainPage;//acces methods in mainpage
    public Achievements(MainPage mainPage)
    {
        InitializeComponent();
        this.mainPage = mainPage; //assign to mainpage
    }

    private void homeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();//return to home
    }
}