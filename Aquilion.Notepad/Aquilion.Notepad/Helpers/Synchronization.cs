using Aquilion.Notepad.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Aquilion.Notepad.Helper
{
    internal class Synchronization : ISynchronizationHelper
    {
        public async Task InvokeAsync(Action action)
            => await Application.Current.Dispatcher.InvokeAsync(action, DispatcherPriority.Background);
    }
}
