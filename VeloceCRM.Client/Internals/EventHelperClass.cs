using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class EventHelperClass
    {
        public delegate void DefaultHandler(object sender, EventArgs e);
        public event DefaultHandler? Authenticated;
        public void RaiseAuthenticatedEvent()
        {
            Authenticated?.Invoke(this, EventArgs.Empty);
        }
    }
}
