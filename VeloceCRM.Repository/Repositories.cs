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
            TitleRepository = new TitleRepository(Key, context);
            FollowuptypeRepository = new FollowuptypeRepository(Key, context);
            ActivityRepository = new ActivityRepository(Key, context);
            CanvasCaseRepository = new CanvasCaseRepository(Key, context);
            CanvasCaseParticipantRepository = new CanvasCaseParticipantRepository(Key, context);
            CanvasCaseParticipantRoleRepository = new CanvasCaseParticipantRoleRepository(Key, context);
            CanvasCaseCompanyLinkRepository = new CanvasCaseCompanyLinkRepository(Key, context);
            DocumentRepository = new DocumentRepository(Key, context);
        }

        public DocumentRepository DocumentRepository { get; set; }
        public CanvasCaseCompanyLinkRepository CanvasCaseCompanyLinkRepository { get; set; }
        public CanvasCaseParticipantRoleRepository CanvasCaseParticipantRoleRepository { get; set; }
        public CanvasCaseParticipantRepository CanvasCaseParticipantRepository { get; set; }
        public CanvasCaseRepository CanvasCaseRepository { get; set; }
        public ActivityRepository ActivityRepository { get; set; }
        public FollowuptypeRepository FollowuptypeRepository { get; set; }
        public TitleRepository TitleRepository { get; set; }
        public PersonRepository PersonRepository { get; set; }
        public CompanyRepository CompanyRepository { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public PostalzoneRepository PostalzoneRepository { get; set; }
        public CountryRepository CountryRepository { get; set; }
        public UserRepository UserRepository { get; set; }
        public LicenseRepository LicenseRepository { get; set; }
    }
}
