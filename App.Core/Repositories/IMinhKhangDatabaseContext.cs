using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Core.Repositories
{
    public interface IMinhKhangDatabaseContext : IDatabaseContext
    {
        DbContext MinhKhangDbContext { get;}
    }
}
