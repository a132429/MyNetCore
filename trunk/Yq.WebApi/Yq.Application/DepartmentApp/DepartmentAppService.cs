using System;
using System.Collections.Generic;
using System.Text;
using Yq.Domain.IRepository;
using Yq.EntityFrameworkCore.Models;

namespace Yq.Application.DepartmentApp
{
    public class DepartmentAppService : IDepartmentAppService
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentAppService(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        //public bool Update(Department department)
        //{
        //    _repository.Update(department);
        //    return _repository.Save();
        //}
    }
}
