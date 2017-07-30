using FirstApp.Data.Repository;
using FirstApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstApp.Data.UnitOfWork {
	public class UnitOfWork : IDisposable {
		public IRepository<User> UserRepository;
		

		private DatabaseContext db;
		private IDbContextTransaction state;
		public UnitOfWork() {
			db = DatabaseContext.Create();

			UserRepository = new IRepository<User>(db);

			state = db.Database.BeginTransaction();
		}

		public int Execute(string Sql, object[] @params = null) {
			return db.Database.ExecuteSqlCommand(Sql, @params);
		}

		public bool Commit() {
			try {
				if(state != null) {
					db.SaveChanges();
					state.Commit();
					state = null; //HasChanges should equal false by now, so Dispose() should clear things up no prob.
				}

				return true;
			} catch(Exception ex) { //TODO: Log
				return false;
			}
		}

		public bool HasChanges {
			get {
				return db.ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified);
			}
		}

		public void Dispose() {
			if(state != null && HasChanges) { //Set to null on commit, so obviously something went awry
				state.Rollback();
			}

			state.Dispose();
			db.Dispose();
		}
	}
}
