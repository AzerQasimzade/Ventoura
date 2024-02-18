﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities
{
	public class Category:BaseNameableEntity
	{
		public ICollection<Tour>? Tours { get; set; }
	}
}