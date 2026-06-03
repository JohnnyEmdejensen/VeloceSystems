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
            UserRepository = new UserRepository(Key, context);
            CountryRepository = new CountryRepository(Key, context);
            PostalzoneRepository = new PostalzoneRepository(Key, context);
            LocationRepository = new LocationRepository(Key, context);
            CompanyRepository = new CompanyRepository(Key, context);
            PersonRepository = new PersonRepository(Key, context);
        }

        public PersonRepository PersonRepository { get; set; }
        public CompanyRepository CompanyRepository { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public PostalzoneRepository PostalzoneRepository { get; set; }
        public CountryRepository CountryRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public LicenseRepository LicenseRepository { get; set; }
    }
}
