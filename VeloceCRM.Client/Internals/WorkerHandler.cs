using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static VeloceCRM.Client.Internals.WorkerItem;

namespace VeloceCRM.Client.Internals
{
    public class WorkerHandler : IDisposable
    {
        private bool disposedValue;
        private Cursor _cursor = Cursors.None;
        private WorkerItem _item;
        public WorkerHandler(string Name)
        {
            _item = new WorkerItem(Name, WorkerItemStatus.Startet);
            App.Globals.DataShare.WorkerItems.Add(_item);
            _cursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
            App.Globals.DataShare.WorkerItems.Add(new WorkerItem(_item.Name, WorkerItemStatus.Ended));
            Mouse.OverrideCursor = _cursor;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
