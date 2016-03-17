using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
     [Serializable]
    public abstract class EntityBase
    {
         [NotMapped]
         public ObjectState State { get; set; }
    }
}
