using System;
using System.Collections.Generic;
using System.Text;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Domain.Repository
{
    public class DepartmentRepository:Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(WdDbContext dbcontext, DapperContext db) : base(dbcontext, db)
        {

        }
    }
}
