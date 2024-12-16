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
            while (true)//keeps planet constaltly rotating 
            {
                await galaxyImage.RelRotateTo(360, 20000, Easing.Linear); //rotate the image 360 degrees 
                galaxyImage.Rotation = 0;//restes rotation position to 0

            }
        }

        private void Leaderboard_Clicked(object sender, EventArgs e)
        {

        }
    }

}
