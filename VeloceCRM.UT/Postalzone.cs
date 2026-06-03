using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class Postalzone
    {
        private Repositories _repositories;
        [SetUp]
        public void Setup()
        {
            _repositories = new Repositories(Guid.Parse("25235ae8-6ca4-43d5-8115-2bcc794b41c2").ToString());
        }

        [Test]
        public void Create()
        {
            Entity.Postalzone? postalzone = new Entity.Postalzone();
            postalzone.City = "Roskilde";
            postalzone.CountryId = 1;
            postalzone.Zipcode = "4000";
            //_repositories.PostalzoneRepository.Create(postalzone);
        }

    }
}
