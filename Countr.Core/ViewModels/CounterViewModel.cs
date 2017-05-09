using System.Threading.Tasks;
using System.Windows.Input;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins;

namespace Countr.Core.ViewModels
{
    public class CounterViewModel : MvxViewModel
    {
        Counter counter;
        readonly ICountersService service;

        public CounterViewModel(ICountersService service)
        {
            this.service = service;
            IncrementCommand = new MvxAsyncCommand(IncrementCounter);
            DeleteCommand = new MvxAsyncCommand(DeleteCounter);
            CancelCommand = new MvxCommand(Cancel);
            SaveCommand = new MvxAsyncCommand(Save);
        }

        public IMvxAsyncCommand IncrementCommand { get; }
        public IMvxAsyncCommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
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

        void Cancel()
        {
            Close(this);
        }

        async Task Save()
        {
            await service.AddNewCounter(counter.Name);
            Close(this);
        }

        public void Init(Counter counter)
        {
            this.counter = counter;
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

        public int Count => counter.Count;
    }
}
