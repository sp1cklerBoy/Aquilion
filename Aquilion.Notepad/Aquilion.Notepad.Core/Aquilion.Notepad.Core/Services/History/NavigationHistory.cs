using System;
using System.Collections;
using System.Collections.Generic;

namespace Aquilion.Notepad.Core
{
    public class NavigationHistory : IHistory
    {
        #region Private Fields
        private Node _head;
        #endregion

        public NavigationHistory(string path, string pathname)
        {
            _head = new Node(path, pathname);
            Current = _head;
        }

        #region Properties
        public bool CanMoveBack => Current.PreviousNode != null;

        public bool CanMoveForward => Current.NextNode != null;

        public Node Current { get; private set; }
        #endregion

        #region Events
        public event EventHandler? HistoryChanged;
        #endregion

        #region Public Methods
        public void Add(string path, string pathname)
        {
            var node = new Node(path, pathname);

            Current.NextNode = node;
            node.PreviousNode = Current;

            Current = node;

            RaiseHistoryChanged();
        }

        public void MoveBack()
        {
            var prev = Current.PreviousNode;

            Current = prev;

            RaiseHistoryChanged();
        }

        public void MoveForward()
        {
            var next = Current.NextNode;

            Current = next;

            RaiseHistoryChanged();
        }
        #endregion

        #region Private Methods
        private void RaiseHistoryChanged()
        {
            HistoryChanged.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Enumerator
        public IEnumerator<Node> GetEnumerator()
        {
            yield return Current;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }

}
