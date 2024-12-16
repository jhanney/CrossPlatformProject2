
namespace CrossPlatformProject2
{
    public partial class MainPage : ContentPage
    {
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
                while (true)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                         galaxyImage.RelRotateTo(360, 5000, Easing.Linear);
                         galaxyImage.Rotation = 0; //reset rotation 
                    });
                }
            }
            catch (Exception ex)
            {
                //log the exception and display a message
                Console.WriteLine($"Rotation failed: {ex.Message}");
                await DisplayAlert("Error", "Planet rotation failed. Please restart the app.", "OK");//display image to show rotation not working
            }
        }

        

        private async void Leaderboard_Clicked(object sender, EventArgs e) 
        {
            //navigate to leaderboard
            await Navigation.PushAsync(new Leaderboard());  
        }
    }

}
