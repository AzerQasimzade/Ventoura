using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Application.ViewModels.Users
{
	public class ForgotPasswordVM
	{
		[Required]
		[MaxLength(256)]
		[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
