using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel
    {
        readonly ICountersService service;
        readonly MvxSubscriptionToken token;
        readonly IMvxNavigationService navigationService;

        public CountersViewModel(ICountersService service, IMvxMessenger messenger, IMvxNavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            token = messenger.SubscribeOnMainThread<CountersChangedMessage>(async m => await LoadCounters());
            Counters = new ObservableCollection<CounterViewModel>();
            ShowAddNewCounterCommand = new MvxAsyncCommand(ShowAddNewCounter);
        }

        public ObservableCollection<CounterViewModel> Counters { get; }

        public override async Task Initialize()
        {
            await LoadCounters();
        }

        public async Task LoadCounters()
        {
            Counters.Clear();
            var counters = await service.GetAllCounters();
            foreach (var counter in counters)
            {
                var viewModel = new CounterViewModel(service, navigationService);
                viewModel.Prepare(counter);
                Counters.Add(viewModel);
            }
        }
        public IMvxAsyncCommand ShowAddNewCounterCommand { get; }

        async Task ShowAddNewCounter()
        {
            throw new System.ArgumentException("Argument");
            Analytics.TrackEvent("Show add new counter");
            await navigationService.Navigate(typeof(CounterViewModel), new Counter());
        }
    }
}