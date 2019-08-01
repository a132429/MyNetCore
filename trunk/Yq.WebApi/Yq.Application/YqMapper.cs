using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yq.Application
{
    /// <summary>
    /// model转换dto配置
    /// </summary>
    public class YqMapper
    {
        /// <summary>
        /// 配置DTO映射关系
        /// </summary>
        public static void Initialize()
        {
            //利用反射动态映射
            Mapper.Initialize(MappingAutoMapper);
        }
        private static void MappingAutoMapper(IMapperConfigurationExpression cfg)
        {
            //string[] assemblyNames = { "Yq.Application", "Yq.EntityFrameworkCore" };
            //先找出Model所有实体类
            List<Type> modelTypes = Assembly.Load("Yq.EntityFrameworkCore").GetTypes().Where(t => t.Namespace=="Yq.EntityFrameworkCore.Models").ToList();
            //model对应的Dto实体(注意:这里我做了命名约定,所有对应实体的Dto对象都以Dto结尾)
            List<Type> modelDtoTypes = Assembly.Load("Yq.Application").GetTypes().Where(t => t.Name.EndsWith("Dto")).ToList();
            foreach (var dtoType in modelDtoTypes)
            {
                var modelType = modelTypes.Where(t => t.Name.StartsWith(dtoType.Name.Replace("Dto", ""))).First();
                cfg.CreateMap(dtoType, modelType);
                cfg.CreateMap(modelType, dtoType);
            }
        }
    }
}
