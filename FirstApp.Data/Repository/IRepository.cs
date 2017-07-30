using FirstApp.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstApp.Data.Repository {
	public class IRepository<TType> : IDisposable where TType : IEntity { //I know it isn't an interface.
		protected DatabaseContext Context;
		protected DbSet<TType> Data;

		public bool AutoSave = true;

		public IRepository(DatabaseContext ctx) {
			Context = ctx;
			Data = ctx.Set<TType>();
		}

		public List<TType> All() {
			return Data.ToList();
		}

		public TType GetById(int id) {
			return Data.Where(x => x.Id == id).FirstOrDefault();
		}

		public void Insert(params TType[] entities) {
			Data.AddRange(entities);

			if(AutoSave)
				Context.SaveChanges();
		}

		public void Update(params TType[] entities) {
			foreach(var entity in entities) {
				Data.Attach(entity);
				Context.Entry(entity).State = EntityState.Modified;
			}
		}

		public void Delete(params TType[] entities) {
			foreach(var entity in entities) {
				if(Context.Entry(entity).State == EntityState.Detached) {
					Data.Attach(entity);
				}
				Data.Remove(entity);
			}
		}

		public void Delete(params int[] EntityIds) {
			foreach(var EntityId in EntityIds) {
				var entity = Data.Find(EntityId);
				Delete(entity);
			}
		}

		public TType First(Func<TType, bool> filter) {
			return Data.Where(filter).FirstOrDefault();
		}

		public bool Exists(Func<TType, bool> filter) {
			return Data.Any(filter);
		}

		public IEnumerable<TType> Select(Func<TType, bool> selector) {
			return Data.Where(selector);
		}

		public IEnumerable<TTo> Select<TTo>(Func<TType, TTo> selector, Func<TType, bool> filter = null) {
			if(filter != null) {
				return Data.Where(filter).Select(selector);
			} else
				return Data.Select(selector);
		}
		
		public void Save() => Context.SaveChanges();
		public void Dispose() => Context.Dispose();
	}
}
