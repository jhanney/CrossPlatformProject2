
namespace CrossPlatformProject2
{
    public partial class MainPage : ContentPage

    {
        public bool isRotoating = true; //added boolean so galaxy only rotates if true 
    public MainPage()
        {
            InitializeComponent();

            //planetRotation();
        }

        private void OnGalaxyImageLoaded(object sender, EventArgs e)//only beigin rotation after image is loaded, found thread on error i was getting more info in ReadMe
        {
            planetRotation(); 
           
        }


        private async void planetRotation() //method to rorate the planet
        {
            try
            {
                while (isRotoating)
                {
                    
                    {
                         await galaxyImage.RelRotateTo(360, 20000, Easing.Linear);
                         galaxyImage.Rotation = 0; //reset rotation 
                    };
                }
            }
            catch (Exception ex)
            {
                //log the exception and display a message
                Console.WriteLine($"Rotation failed: {ex.Message}");
                await DisplayAlert("Error", "Planet rotation failed. Please restart the app.", "OK");//display image to show rotation not working
            }
        }

        public void startRotation() //method to start rotation when returning to main page 
        {
            if (isRotoating)
            {
                isRotoating= true; 
                planetRotation(); 
            }
        }

        private async void Leaderboard_Clicked(object sender, EventArgs e) 
        {
            //navigate to leaderboard
            isRotoating = false;
            await Navigation.PushAsync(new Leaderboard(this, playerScores));  //pass references so leaderboard can access methods while navigating to leaderboard
        }

        private async void Achievments_Clicked(object sender, EventArgs e)
        {
            isRotoating =false;
            await Navigation.PushAsync(new Achievements(this));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            isRotoating = false;
            await Navigation.PushAsync(new GameSetup());
        }
    }

}
