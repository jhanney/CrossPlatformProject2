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
                await planetImage.RelRotateTo(360, 10000, Easing.Linear); 
                planetImage.Rotation = 0; 
            }
        }

    }

}
