namespace VLCPlayer
{
    public partial class MainPage : ContentPage
    {
        private string? _url;
        public string? Url
        {
            get => _url;
            set
            {
                _url = value;

                OnPropertyChanged();
            }
        }

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

        private void BtnPlayUrl_Clicked(object sender, EventArgs e)
        {
            VideoViewer.Play(Url);
        }
    }

}
