using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventoura.Domain.Entities.Common
{
    public abstract class BaseNameableEntity:BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
