using FirstApp.Models.Abstract;
using FirstApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.Entity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FirstApp.Data {
    public class DatabaseContext : DbContext { //TODO: Wrap in UoW implementation!
        public DatabaseContext() : base() { }

        public static DatabaseContext Create() { //Copied from an old project that was pre-Core, and turned off Proxies and LazyLoading. Too lazy to remove it, since neither is supported yet.
            var context = new DatabaseContext();
            return context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseMySQL(@"host=localhost;database=generic;userid=generic;password=password;");
        }

		//This whole section was an experiment. I have no idea how it'll perform, but I wager not well.
		//Subject to change
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
			//Dynamically set indexes as unique where the attribute exists
			Type[] Entities = GetTypesInNamespace(typeof(FirstApp.Models.Attributes.Unique).GetTypeInfo().Assembly, "FirstApp.Models.Entities");

			foreach(var entity in Entities) {
				PropertyInfo[] properties = entity.GetProperties();
				foreach(var property in properties) {
					var attribute = property.GetCustomAttributes(true).Where(x => x.GetType() == typeof(FirstApp.Models.Attributes.Unique)).FirstOrDefault();

					if(attribute != null) {
						modelBuilder.Entity(entity).HasIndex(property.Name).IsUnique(true);
					}
				}
			}
		}

		//Ripped off StackOverflow, like most of the project.
		private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace) {
			return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
		}
    }
}
