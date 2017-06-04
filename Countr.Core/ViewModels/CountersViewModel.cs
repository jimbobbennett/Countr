using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.Navigation;

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel
    {
        readonly ICountersService service;
        readonly MvxSubscriptionToken token;
        readonly IMvxNavigationService navigationService;

        public CountersViewModel(ICountersService service, IMvxMessenger messenger, IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.service = service;
            Counters = new ObservableCollection<CounterViewModel>();
            token = messenger.SubscribeOnMainThread<CountersChangedMessage>(async m => await LoadCounters());
            ShowAddNewCounterCommand = new MvxAsyncCommand(ShowAddNewCounter);
        }

        public ObservableCollection<CounterViewModel> Counters { get; }

        public override async void Start()
        {
            base.Start();
            await LoadCounters();
        }

        public async Task LoadCounters()
        {
            Counters.Clear();

            foreach (var counter in await service.GetAllCounters())
            {
                var viewModel = new CounterViewModel(service, navigationService);
                await viewModel.Initialize(counter);
                Counters.Add(viewModel);
            }
        }

        public IMvxAsyncCommand ShowAddNewCounterCommand { get; }

        async Task ShowAddNewCounter()
        {
            await navigationService.Navigate<CounterViewModel, Counter>(new Counter());
        }
    }
}
