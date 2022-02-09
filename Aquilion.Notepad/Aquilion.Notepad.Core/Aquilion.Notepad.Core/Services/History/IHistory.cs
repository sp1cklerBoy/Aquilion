using System;
using System.Collections.Generic;
using System.Text;

namespace Aquilion.Notepad.Core
{
    public interface IHistory : IEnumerable<Node>
    {
        bool CanMoveBack { get; }
        bool CanMoveForward { get; }
        event EventHandler HistoryChanged;
        Node Current { get; }
        void MoveBack();
        void MoveForward();
        void Add(string path, string pathname);
    }

}
