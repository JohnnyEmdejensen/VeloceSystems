using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class Country
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
            Entity.Country? country = new Entity.Country();
            country.IsoCode = "DK";
            country.Name = "Denmark";
            country.Number = 208;
            _repositories.CountryRepository.Create(country);
        }
    }
}
