using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using LibVLCSharp.Platforms.Android;
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

        protected override VideoView CreatePlatformView() => new VideoView(Context);

        protected override void ConnectHandler(VideoView nativeView)
        {
            base.ConnectHandler(nativeView);

            _libVLC = new LibVLC(enableDebugLogs: true);
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = nativeView ?? new VideoView(Context);
            _videoView.MediaPlayer = _mediaPlayer;

            HandleUrl(VirtualView.VideoUrl);

            if (nativeView != null)
            {
                base.ConnectHandler(nativeView);
            }
        }

        protected override void DisconnectHandler(VideoView nativeView)
        {
            nativeView.Dispose();
            base.DisconnectHandler(nativeView);
        }

        private void HandleUrl(string url)
        {
            try
            {
                if (_libVLC == null || _mediaPlayer == null || _videoView == null)
                {
                    return;
                }

                if (url.EndsWith("/"))
                {
                    url = url.TrimEnd('/');
                }

                //url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

                if (!string.IsNullOrEmpty(url))
                {
                    var media = new Media(_libVLC, url, FromType.FromLocation);

                    _mediaPlayer.NetworkCaching = 1500;

                    if (_mediaPlayer.Media != null)
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Media.Dispose();
                    }

                    _mediaPlayer.Media = media;
                    _mediaPlayer.Mute = true;

                    _videoView.MediaPlayer?.Play();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
