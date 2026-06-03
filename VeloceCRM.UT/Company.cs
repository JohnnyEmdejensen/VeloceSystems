using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class Company
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
            Entity.Company? company = new Entity.Company();
            company.Email = "info@emdesoft.dk";
            company.LocationId = 1;
            company.Name = "Emdesoft Developments A/S";
            company.Nickname = "Emdesoft";
            company.Number = "12345678";
            company.Phone = "70202020";
            company.Phone2 = "70202021";
            company.Taxnumber = "DK12345678";
            company.Website = "www.emdesoft.dk";
            //_repositories.CompanyRepository.Create(company);
        }
    }
}
