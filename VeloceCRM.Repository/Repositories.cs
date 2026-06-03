using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class Repositories
    {
        public Repositories(string Key) 
        { 
            ApiContextFactory factory = new ApiContextFactory();
            ApiContext context = factory.CreateDbContext(null);

            LicenseRepository = new LicenseRepository(Key, context);
        }

        public LicenseRepository LicenseRepository { get; set; }
    }
}
