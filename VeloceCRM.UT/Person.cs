using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class Person
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
            Entity.Person? person = new Entity.Person();
            person.CompanyId = 1;
            person.Email = "jej@emdesoft.dk";
            person.Firstname = "Johnny";
            person.LocationId = 1;
            person.Middlename = "Emde";
            person.Mobile = "12345678";
            person.Surname = "Emdesoft";
            _repositories.PersonRepository.Create(person);
        }
    }
}
