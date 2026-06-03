using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class License
    {
        private Repositories _repositories;
        [SetUp]
        public void Setup()
        {
            _repositories = new Repositories(Guid.Empty.ToString());
        }

        [Test]
        public void Create()
        {
            Entity.License? license = new Entity.License();

            if (!LicenseExists("TST", out license))
            {
                if (license == null) license = new Entity.License();
                license.CompanyName = "Test";
                license.Email = "Test@emdesoft.dk";
                license.IsActive = true;
                license.IsDeleted = false;
                license.IsLocked = false;
                license.Key = Guid.NewGuid().ToString();
                license.LocationId = 0;
                license.Phone = "12345678";
                license.Prefix = "TST";
                license.TaxNumber = "12345678";

                _repositories.LicenseRepository.Create(license);
            }
            else
            {
                if (license == null) license = new Entity.License();
                license.CompanyName = "Test2";
                _repositories.LicenseRepository.Update(license);
            }
        }

        private bool LicenseExists(string prefix, out Entity.License? license)
        {
            license = _repositories.LicenseRepository.GetByPrefix(prefix);
            return license != null;
        }
    }
}
