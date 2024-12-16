
namespace CrossPlatformProject2
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
           
            planetRotation(); 
        }

        private async void planetRotation() //method to rorate the planet
        {
            try
            {
                while (true)
                {
                    await galaxyImage.RelRotateTo(360, 20000, Easing.Linear); //try rotation
                    galaxyImage.Rotation = 0; //reset rotation
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
