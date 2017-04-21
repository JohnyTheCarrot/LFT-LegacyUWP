using System;
using Windows.Devices.Input;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LinusForumTips.Services;

namespace LinusForumTips.Layouts.Detail
{
    public sealed partial class YouTubeDetailLayout : BaseDetailLayout
    {
        public YouTubeDetailLayout(Uri contentSource)
        {
            InitializeComponent();

            web.Source = contentSource;
        }

        private void WebView_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView webView = sender as WebView;
            if (webView != null) webView.NavigateToString(string.Empty);
        }
    }
}
