using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FirstApp.Models.Abstract {
	public abstract class IEntity {
		[Key]
		public int Id { get; set; }

		public bool IsActive { get; set; } = true;
	}
}
