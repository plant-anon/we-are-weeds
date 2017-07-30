using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Models.Attributes {
	[AttributeUsage(AttributeTargets.Property)]
	public class Unique : Attribute { }
}
