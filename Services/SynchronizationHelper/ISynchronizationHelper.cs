using System;
using System.Threading.Tasks;

namespace Aquilion.Notepad.Services.SynchronizationHelper
{
    public interface ISynchronizationHelper
    {
        Task InvokeAsync(Action action);
    }
}
