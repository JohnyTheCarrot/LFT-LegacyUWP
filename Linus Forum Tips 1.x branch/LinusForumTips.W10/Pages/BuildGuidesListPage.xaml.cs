//---------------------------------------------------------------------------
//
// <copyright file="BuildGuidesListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class BuildGuidesListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public BuildGuidesListPage()
        {
			ViewModel = ViewModelFactory.NewList(new BuildGuidesSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("1a85418c-742c-487e-bd4c-e4809b3f78bb");
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
