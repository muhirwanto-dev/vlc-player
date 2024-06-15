namespace VLCPlayer.Controls
{
    public class MediaViewer : View
    {
        public event EventHandler? OnPlayEvent;
        public event EventHandler? OnPauseEvent;
        public event EventHandler? OnStopEvent;

        public static BindableProperty VideoUrlProperty = BindableProperty.Create(nameof(VideoUrl)
           , typeof(string)
           , typeof(MediaViewer)
           , string.Empty
           , defaultBindingMode: BindingMode.OneWay);

        public string? VideoUrl
        {
            get => (string)GetValue(VideoUrlProperty);
            set => SetValue(VideoUrlProperty, value);
        }

        public MediaViewer()
        {
        }

        public void Play()
        {
            if (VideoUrl != null)
            {
                OnPlayEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Play(string? url)
        {
            VideoUrl = url;

            Play();
        }

        public void Pause()
        {
            OnPauseEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            OnStopEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}