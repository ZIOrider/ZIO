using CourseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseManager.Models.ValidatableObjects;

namespace CourseManager.Migrations.Seeds
{
    public class UserCreator
    {
        private readonly CourseManager.Models.CourseManagerEntities _context;
        public UserCreator(CourseManager.Models.CourseManagerEntities context)
        {
            _context = context;
        }


        public void Seed()
        {
            var iniitialUsers = new List<Users>
            {
                new Users
                {
                    Name = "Aadmin",
                    Account = "admin",
                    Password = "123456".MD5Encoding()
                }
            };
            foreach (var user in iniitialUsers)
            {
                if (_context.Users.Any(r => r.Name == user.Name))
                {
                    continue;
                }
                _context.Users.Add(user);
            }
            _context.SaveChanges();

        }
    }
}