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
                await planetImage.RelRotateTo(360, 12000, Easing.Linear); //rotate the image 360 degrees
                planetImage.Rotation = 0;//restes rotation position to 0
            }
        }

    }

}
