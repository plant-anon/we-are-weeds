using FirstApp.Models.Abstract;
using FirstApp.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstApp.Models.Entities {
    [Table("User")]
    public partial class User : IEntity {
		[StringLength(24, MinimumLength = 4)]
		[Required]
		[Unique]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
		[Required]
		public string Salt { get; set; }

		[NotMapped]
		public string Identity { get; set; }
    }
}
