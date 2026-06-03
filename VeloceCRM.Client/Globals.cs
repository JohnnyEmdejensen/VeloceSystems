using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client
{
    public class Globals
    {
        public Internals.AppShareClass AppShare { get; private set; } = new Internals.AppShareClass();
        public Internals.DataShareClass DataShare { get; private set; } = new Internals.DataShareClass();
        public Internals.DialogHelperClass DialogHelper { get; private set; } = new Internals.DialogHelperClass();
        public Internals.EventHelperClass EventHelper { get; private set; } = new Internals.EventHelperClass();
        public Internals.ToolHelperClass ToolHelper { get; private set; } = new Internals.ToolHelperClass();

        public void Initialize()
        {

        }
    }
}
