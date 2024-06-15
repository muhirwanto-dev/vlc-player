using LibVLCSharp.Platforms.iOS;
using LibVLCSharp.Shared;
using Microsoft.Maui.Handlers;
using VLCPlayer.Controls;

namespace VLCPlayer.Handlers
{
    public partial class MediaViewerHandler : ViewHandler<MediaViewer, VideoView>
    {
        VideoView? _videoView;
        LibVLC? _libVLC;
        LibVLCSharp.Shared.MediaPlayer? _mediaPlayer;

        protected override VideoView CreatePlatformView() => new VideoView();

        protected override void ConnectHandler(VideoView nativeView)
        {
            Core.Initialize();

            _libVLC = new LibVLC(enableDebugLogs: true);
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = nativeView ?? new VideoView();
            _videoView.MediaPlayer = _mediaPlayer;

            VirtualView.OnPlayEvent += OnPlay;
            VirtualView.OnPauseEvent += OnPause;
            VirtualView.OnStopEvent += OnStop;

            base.ConnectHandler(_videoView);
        }

        protected override void DisconnectHandler(VideoView nativeView)
        {
            nativeView.Dispose();

            VirtualView.OnPlayEvent -= OnPlay;
            VirtualView.OnPauseEvent -= OnPause;
            VirtualView.OnStopEvent -= OnStop;

            base.DisconnectHandler(nativeView);
        }

        private void Play(string? url)
        {
            if (_libVLC == null || _mediaPlayer == null || _videoView == null)
            {
                return;
            }

            if (_mediaPlayer.IsPlaying)
            {
                return;
            }

            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            if (url.EndsWith("/"))
            {
                url = url.TrimEnd('/');
            }

            if (!string.IsNullOrEmpty(url))
            {
                var media = new Media(_libVLC, url, FromType.FromLocation);

                _mediaPlayer.NetworkCaching = 1500;

                Stop();

                _mediaPlayer.Media = media;
                _mediaPlayer.Mute = false;
                _mediaPlayer.Play();
            }
        }

        private void Pause()
        {
            _mediaPlayer?.Pause();
        }

        private void Stop()
        {
            if (_mediaPlayer?.Media != null)
            {
                _mediaPlayer.Stop();
                _mediaPlayer.Media.Dispose();
            }
        }

        private void OnPlay(object? sender, EventArgs arg)
        {
            try
            {
                Play(VirtualView.VideoUrl);
            }
            catch (Exception ex)
            {
            }
        }

        private void OnPause(object? sender, EventArgs arg)
        {
            try
            {
                Pause();
            }
            catch (Exception ex)
            {
            }
        }

        private void OnStop(object? sender, EventArgs arg)
        {
            try
            {
                Stop();
            }
            catch (Exception ex)
            {
            }
        }

    }
}
