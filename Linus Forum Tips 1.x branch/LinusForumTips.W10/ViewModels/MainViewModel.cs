using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.LocalStorage;
using LinusForumTips.Sections;


namespace LinusForumTips.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel LinusTechTipsVideos { get; private set; }
        public ListViewModel WanShowArchive { get; private set; }
        public ListViewModel TechquickieVideos { get; private set; }
        public ListViewModel ChannelSuperFunVideos { get; private set; }
        public ListViewModel BuildLogs { get; private set; }
        public ListViewModel GuidesAndTutorials { get; private set; }
        public ListViewModel BuildGuides { get; private set; }
        public ListViewModel Menu { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Linus Forum Tips";
            LinusTechTipsVideos = ViewModelFactory.NewList(new LinusTechTipsVideosSection(), visibleItems);
            WanShowArchive = ViewModelFactory.NewList(new WanShowArchiveSection(), visibleItems);
            TechquickieVideos = ViewModelFactory.NewList(new TechquickieVideosSection(), visibleItems);
            ChannelSuperFunVideos = ViewModelFactory.NewList(new ChannelSuperFunVideosSection(), visibleItems);
            BuildLogs = ViewModelFactory.NewList(new BuildLogsSection(), visibleItems);
            GuidesAndTutorials = ViewModelFactory.NewList(new GuidesAndTutorialsSection(), visibleItems);
            BuildGuides = ViewModelFactory.NewList(new BuildGuidesSection(), visibleItems);
            Menu = ViewModelFactory.NewList(new MenuSection());

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return LinusTechTipsVideos;
            yield return WanShowArchive;
            yield return TechquickieVideos;
            yield return ChannelSuperFunVideos;
            yield return BuildLogs;
            yield return GuidesAndTutorials;
            yield return BuildGuides;
            yield return Menu;
        }
    }
}
