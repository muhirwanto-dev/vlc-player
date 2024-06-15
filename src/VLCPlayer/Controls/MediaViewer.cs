namespace VLCPlayer.Controls
{
    public class MediaViewer : View
    {
        public static BindableProperty VideoUrlProperty = BindableProperty.Create(nameof(VideoUrl)
           , typeof(string)
           , typeof(MediaViewer)
           , string.Empty
           , defaultBindingMode: BindingMode.TwoWay);

        public string VideoUrl
        {
            get => (string)GetValue(VideoUrlProperty);
            set => SetValue(VideoUrlProperty, value);
        }

        public MediaViewer()
        {
        }
    }
}