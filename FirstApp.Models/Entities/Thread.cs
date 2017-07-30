using FirstApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstApp.Models.Entities {
	public class Thread : IEntity {
		public string Topic { get; set; }
		public int CreatorId { get; set; }

		[ForeignKey("CreatorId")]
		public virtual User Creator { get; set; }
		[ForeignKey("Id")]
		public virtual List<ThreadReply> Replies { get; set; }
	}
}
