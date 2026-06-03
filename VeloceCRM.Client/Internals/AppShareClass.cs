using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    public class AppShareClass
    {
        public Entity.User? ActiveUser { get; set; }
        public Repository.Repositories Repositories { get; set; } = new Repository.Repositories(Guid.Empty.ToString());
    }
}
