using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }
}
