using FirstApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstApp.Models.Entities {
	public class ThreadReply : IEntity{
		public int ThreadId { get; set; }
		public int? AuthorId { get; set; }

		public string Message { get; set; }
		public string PosterName { get; set; }
		public string PosterId { get; set; }
		
		public bool Anonymous { get; set; } //TODO: Consolidate, check against AuthorId & Author instead of a database column

		[ForeignKey("ThreadId")]
		public virtual Thread Thread { get; set; }

		[NotMapped]
		public User Author { get; set; } = null;
	}
}
