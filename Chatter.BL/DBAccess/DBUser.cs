using System;
using System.Collections.Generic;
using System.Linq;
using Chatter.BL.DTO.User;
using Chatter.DAL.Model;
using Chatter.BL.DBAccess.Interfaces;
using Chatter.DAL.Factories;

namespace Chatter.BL.DBAccess
{
    public class DBUser : IDBUser
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBUser(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public UserDetailModelDTO AddUser(UserDetailModelDTO user)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var newUser = mapper.MapDetailToEntity(user);
                newUser.Id = Guid.NewGuid();

                var pass = Crypto.Encrypt(user.Password);

                newUser.Password = pass.Hash;
                newUser.Salt = pass.Salt;

                Connection.Users.Add(newUser);

                Connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newUser);
            }
        }

        public void EditUser(UserDetailModelDTO user)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                bool edited = false;

                User edit_user = new User() { Id = user.Id };
                edit_user = Connection.Users.Where(x => x.Id == user.Id).SingleOrDefault();
                Connection.Users.Attach(edit_user);

                if (edit_user.FirstName != user.FirstName)
                {
                    edited = true;
                    edit_user.FirstName = user.FirstName;
                }

                if (edit_user.LastName != user.LastName)
                {
                    edited = true;
                    edit_user.LastName = user.LastName;
                }

                if (edit_user.Gender != user.Gender)
                {
                    edited = true;
                    edit_user.Gender = user.Gender;
                }

                if (edit_user.Description != user.Description)
                {
                    edited = true;
                    edit_user.Description = user.Description;
                }

                if (edit_user.Email != user.Email)
                {
                    edited = true;
                    edit_user.Email = user.Email;
                }

                edit_user.Password = user.Hash;
                edit_user.Salt = user.Salt;

                if (edited)
                    Connection.SaveChanges();
            }
        }

        public void EditPassword(UserDetailModelDTO user)
        {
            string pass = user.Password;
            user = GetUserById(user.Id);
            user.Password = pass;

            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                User edit_user = new User() { Id = user.Id };
                edit_user = Connection.Users.Where(x => x.Id == user.Id).SingleOrDefault();
                Connection.Users.Attach(edit_user);

                var (Salt, Hash) = Crypto.Encrypt(user.Password);

                edit_user.Salt = Salt;
                edit_user.Password = Hash;

                Connection.SaveChanges();
            }
        }

        public UserDetailModelDTO GetUserById(Guid Id)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var user_id = Connection.Users.SingleOrDefault(x => x.Id == Id);
                return user_id == null ? null : mapper.MapEntityToDetailModel(user_id);
            }
        }

        public UserDetailModelDTO GetUserByUsername(string username)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var user_name = Connection.Users.SingleOrDefault(x => x.Username == username);
                return user_name == null ? null : mapper.MapEntityToDetailModel(user_name);
            }
        }

        public UserDetailModelDTO GetUserByEmail(string mail)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var user_name = Connection.Users.SingleOrDefault(x => x.Email == mail);
                return user_name == null ? null : mapper.MapEntityToDetailModel(user_name);
            }
        }

        public UserDetailModelDTO GetUserByModel(UserDetailModelDTO user)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var user_name = Connection.Users.SingleOrDefault(x => x.Email == user.Email || x.Username == user.Username || x.Id == user.Id);
                return user_name == null ? null : mapper.MapEntityToDetailModel(user_name);
            }
        }

        public List<UserListModelDTO> GetUserList()
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                return Connection.Users
                    .Select(e => mapper.MapEntityToListModel(e))
                    .ToList();
            }
        }

        public void RemoveUser(Guid Id)
        {
            using (var Connection = chatterDbContextFactory.CreateDbContext())
            {
                var user = Connection.Users.First(x => x.Id == Id);
                Connection.Remove(user);
                Connection.SaveChanges();
            }
        }

        public bool Authenticate(UserDetailModelDTO user)
        {
            var userLogin = GetUserByUsername(user.Username);

            if (userLogin == null)
            {
                userLogin = GetUserByEmail(user.Username);
            }
                        
            return userLogin != null ? Crypto.Decrypt(userLogin.Salt, userLogin.Hash, user.Password) : false;
        }
    }
}
