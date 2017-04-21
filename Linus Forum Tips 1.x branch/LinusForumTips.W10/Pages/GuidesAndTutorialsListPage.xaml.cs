//---------------------------------------------------------------------------
//
// <copyright file="GuidesAndTutorialsListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class GuidesAndTutorialsListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public GuidesAndTutorialsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new GuidesAndTutorialsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("58b49a14-f3d0-4696-8cd4-31455f130e63");
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
