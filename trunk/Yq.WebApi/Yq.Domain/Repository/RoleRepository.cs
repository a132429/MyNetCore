using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Domain.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(WdDbContext dbcontext, DapperContext db) : base(dbcontext, db)
        {


        }
    }
}
