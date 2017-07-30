using FirstApp.Data;
using FirstApp.Data.Repository;
using FirstApp.Data.UnitOfWork;
using FirstApp.Models.Abstract;
using FirstApp.Models.Entities;
using FirstApp.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FirstApp.Services {
    public class UserService : BaseService {
		public UserService(UnitOfWork uow) : base(uow) { }

        public List<User> GetAllUsers() {
            return uow.UserRepository.All();
        }

        public User GetUserByName(string Username) {
            return uow.UserRepository.First(x => x.Username == Username);
        }

        public User GetUserById(int Id) {
            return uow.UserRepository.First(x => x.Id == Id);
        }

        public User CreateUser(User user) {
			var salt = EncryptionService.GenerateSalt();
			var NewUser = new User();
			NewUser.Username = user.Username;
			NewUser.Password = EncryptionService.Hash(user.Password + salt);
			NewUser.Salt = salt;

			uow.UserRepository.Insert(NewUser);

			return NewUser;
		}

		public bool UserExists(string Username) {
			return uow.UserRepository.Exists(x => x.Username == Username);
		}

		public User Login(LoginModel model) {
			if(model.Username == null) throw new Exception("Invalid username");
			if(model.Password == null) throw new Exception("Invalid password");

			var user = uow.UserRepository.First(x => x.Username == model.Username);

			if(user != null) {
				var localHash = EncryptionService.Hash(model.Password + user.Salt);
				
				if(user.Password == localHash) {
					return user;
				} else throw new Exception("Incorrect password");
			} else throw new Exception("User does not exist");
		}

		public User Register(RegisterModel model) {
			if(model.Username == null) throw new Exception("Invalid username");
			if(model.Password == null) throw new Exception("Invalid password");
			if(Regex.IsMatch(model.Username, @"[^A-Za-z0-9_\-]")) throw new Exception("Username must contain only letters, numbers, or underscores");

			if(UserExists(model.Username)) throw new Exception("Username is taken");

			if(model.Password != model.RePassword) throw new Exception("Passwords do not match");

			return CreateUser(new User {
				Username = model.Username,
				Password = model.Password
			});
		}
    }
}
