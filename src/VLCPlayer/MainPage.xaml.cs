namespace VLCPlayer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {
            VideoViewer.Play();
        }

        private void BtnPause_Clicked(object sender, EventArgs e)
        {
            VideoViewer.Pause();
        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            VideoViewer.Stop();
        }
    }

}
