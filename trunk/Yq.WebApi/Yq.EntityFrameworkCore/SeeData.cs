using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yq.EntityFrameworkCore.Models;

namespace Yq.EntityFrameworkCore
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WdDbContext(serviceProvider.GetRequiredService<DbContextOptions<WdDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;   // 已经初始化过数据，直接返回
                }
                Guid departmentId = Guid.NewGuid();
                //增加一个部门
                context.Departments.Add(
                   new Department
                   {
                       Id = departmentId,
                       DepartmentName = "Fonour集团总部",
                       ParentId = Guid.Empty
                   }
                );
                //增加一个超级管理员用户
                Guid userID = Guid.NewGuid();
                context.Users.Add(
                     new User
                     {
                         Id = userID,
                         Employee = "admin",
                         Password = "123456", //暂不进行加密
                         FullName = "超级管理员",
                         DepartmentId = departmentId,
                         IsAvailable=1,
                         IsDel=0
                     }
                );
                //角色
                Guid roleID = Guid.NewGuid();
                context.Roles.Add(
                    new Role
                    {
                        Id = roleID,
                        Code = "1",
                        RoleName = "超级管理员"
                    }
                    );

                //增加四个基本功能菜单
                Guid g1 = Guid.NewGuid();
                Guid g2 = Guid.NewGuid();
                Guid g3 = Guid.NewGuid();
                Guid g4 = Guid.NewGuid();
                context.Menus.AddRange(
                   new Menu
                   {
                       Id = g1,
                       MenuName = "组织机构管理",
                       Code = "Department",
                       SortNo = 0,
                       ParentId = Guid.Empty
                   },
                   new Menu
                   {
                       Id = g2,
                       MenuName = "角色管理",
                       Code = "Role",
                       SortNo = 1,
                       ParentId = Guid.Empty
                   },
                   new Menu
                   {
                       Id = g3,
                       MenuName = "用户管理",
                       Code = "User",
                       SortNo = 2,
                       ParentId = Guid.Empty
                   },
                   new Menu
                   {
                       Id = g4,
                       MenuName = "功能管理",
                       Code = "Department",
                       SortNo = 3,
                       ParentId = Guid.Empty
                   }
                );
                context.RoleMenus.AddRange(
                  new RoleMenu
                  {
                      RoleId = roleID,
                      MenuId = g1
                  },
                  new RoleMenu
                  {
                      RoleId = roleID,
                      MenuId = g2
                  },
                  new RoleMenu
                  {
                      RoleId = roleID,
                      MenuId = g3
                  },
                  new RoleMenu
                  {
                      RoleId = roleID,
                      MenuId = g4
                  }
                  );
                context.UserRoles.Add(new UserRole
                {
                    RoleId = roleID,
                    UserId = userID
                });
                context.SaveChanges();
            }
        }
    }
}