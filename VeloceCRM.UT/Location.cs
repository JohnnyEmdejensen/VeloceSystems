using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class Location
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
            Entity.Location? location = new Entity.Location();
            location.Street = "Rørmosen";
            location.House = "63";
            location.Floor = "1";
            location.Door = "tv";
            location.PostalzoneId = 1;
            //_repositories.LocationRepository.Create(location);
        }

        [Test]
        public void GetAll()
        {
            var locations = _repositories.LocationRepository.GetAll();
        }
    }
}
