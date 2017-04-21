using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using LinusForumTips.Navigation;

namespace LinusForumTips.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"d6dd521d-7ebf-474f-9578-547618d29d8b",
                "Linus Tech Tips Videos",                
                AppNavigation.ActionFromPage("LinusTechTipsVideosListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"2f9ed5d6-bae9-4833-9ce0-54d827698b7a",
                "Wan Show Archive",                
                AppNavigation.ActionFromPage("WanShowArchiveListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1cf79357-f873-4210-b657-47ff7cd65396",
                "Techquickie Videos",                
                AppNavigation.ActionFromPage("TechquickieVideosListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"5c707716-fb7e-4986-8c3b-6dc9d09ce700",
                "Channel Super Fun Videos",                
                AppNavigation.ActionFromPage("ChannelSuperFunVideosListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"c4cea27e-34d3-4a23-b659-d0fa31bec5f2",
                "Build Logs",                
                AppNavigation.ActionFromPage("BuildLogsListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"58b49a14-f3d0-4696-8cd4-31455f130e63",
                "Guides and Tutorials",                
                AppNavigation.ActionFromPage("GuidesAndTutorialsListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1a85418c-742c-487e-bd4c-e4809b3f78bb",
                "Build Guides",                
                AppNavigation.ActionFromPage("BuildGuidesListPage"),
				AppNavigation.IconFromGlyph("\ue173")));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
