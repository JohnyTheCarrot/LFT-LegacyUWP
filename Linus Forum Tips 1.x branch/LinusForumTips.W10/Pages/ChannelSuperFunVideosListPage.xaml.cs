//---------------------------------------------------------------------------
//
// <copyright file="ChannelSuperFunVideosListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>2/17/2017 11:01:17 AM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using LinusForumTips.Sections;
using LinusForumTips.ViewModels;
using AppStudio.Uwp;

namespace LinusForumTips.Pages
{
    public sealed partial class ChannelSuperFunVideosListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public ChannelSuperFunVideosListPage()
        {
			ViewModel = ViewModelFactory.NewList(new ChannelSuperFunVideosSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("5c707716-fb7e-4986-8c3b-6dc9d09ce700");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }

    }
}
