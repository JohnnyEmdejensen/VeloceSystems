using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class WorkerItem
    {
        public enum WorkerItemStatus
        {
            Unknown,
            Startet,
            Ended
        }
        public string Name { get; set; } = "";
        public WorkerItemStatus Status { get; set; } = WorkerItemStatus.Unknown;
        public DateTime Timer { get; set; }
        public WorkerItem(string name, WorkerItemStatus status)
        {
            Name = name;
            Status = status;
            Timer = DateTime.Now;
        }

    }
}
