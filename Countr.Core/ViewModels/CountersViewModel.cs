using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel
    {
        readonly ICountersService service;
        readonly MvxSubscriptionToken token;

        public CountersViewModel(ICountersService service, IMvxMessenger messenger)
        {
            this.service = service;
            Counters = new ObservableCollection<CounterViewModel>();
            token = messenger.SubscribeOnMainThread<CountersChangedMessage>(async m => await LoadCounters());
            ShowAddNewCounterCommand = new MvxCommand(ShowAddNewCounter);
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
                var viewModel = new CounterViewModel(service);
                viewModel.Init(counter);
                Counters.Add(viewModel);
            }
        }

        public ICommand ShowAddNewCounterCommand { get; }

        void ShowAddNewCounter()
        {
            ShowViewModel<CounterViewModel>(new Counter());
        }
    }
}
