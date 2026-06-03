using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Repository;

namespace VeloceCRM.UT
{
    public class User
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
            Entity.User? user = new Entity.User();
            user.Email = "jej@emdesoft.dk";
            user.Firstname = "Johnny";
            user.ForceNewPassword = false;
            user.Initialis = "JEJ";
            user.IsActive = true;
            user.IsDeleted = false;
            user.LocationId = 1;
            user.Login = "jej";
            user.Middlename = "Emde";
            user.Mobile = "12345678";
            user.Password = "1Lillegris";
            user.Surname = "Jensen";
            //_repositories.UserRepository.Create(user);
        }

        [Test]
        public void Authenticate()
        {
            var user = _repositories.UserRepository.Authenticate("jej", "1Lillegris");

        }
        [Test]
        public void GetAll()
        {
            var users = _repositories.UserRepository.GetAll();
        }
    }
}
