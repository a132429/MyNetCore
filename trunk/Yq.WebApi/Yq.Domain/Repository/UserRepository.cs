using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Domain.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WdDbContext dbcontext,DapperContext db) : base(dbcontext,db)
        {

        } 
    }
}
