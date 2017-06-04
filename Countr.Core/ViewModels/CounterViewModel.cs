using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;

namespace Countr.Core.ViewModels
{
    public class CounterViewModel : MvxViewModel<Counter>
    {
        Counter counter;
        readonly ICountersService service;
        readonly IMvxNavigationService navigationService;

        public CounterViewModel(ICountersService service, IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            this.service = service;
            IncrementCommand = new MvxAsyncCommand(IncrementCounter);
            DeleteCommand = new MvxAsyncCommand(DeleteCounter);
            CancelCommand = new MvxAsyncCommand(Cancel);
            SaveCommand = new MvxAsyncCommand(Save);
        }

        public IMvxAsyncCommand IncrementCommand { get; }
        public IMvxAsyncCommand DeleteCommand { get; }
        public IMvxAsyncCommand CancelCommand { get; }
        public IMvxAsyncCommand SaveCommand { get; }

        async Task IncrementCounter()
        {
            await service.IncrementCounter(counter);
            RaisePropertyChanged(() => Count);
        }

        async Task DeleteCounter()
        {
            await service.DeleteCounter(counter);
        }

        async Task Cancel()
        {
            await navigationService.Close(this);
        }

        async Task Save()
        {
            await service.AddNewCounter(counter.Name);
            await navigationService.Close(this);
        }

        public override async Task Initialize(Counter parameter)
        {
            counter = parameter;
        }

        public string Name
        {
            get { return counter.Name; }
            set
            {
                if (Name == value) return;
                counter.Name = value;
                RaisePropertyChanged();
            }
        }

        public override void Start()
        {
            base.Start();
        }

        public int Count => counter.Count;
    }
}
