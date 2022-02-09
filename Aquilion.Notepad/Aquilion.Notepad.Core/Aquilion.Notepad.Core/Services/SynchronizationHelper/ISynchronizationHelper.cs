using System;
using System.Threading.Tasks;

namespace Aquilion.Notepad.Core
{
    public interface ISynchronizationHelper
    {
        Task InvokeAsync(Action action);
    }
}
