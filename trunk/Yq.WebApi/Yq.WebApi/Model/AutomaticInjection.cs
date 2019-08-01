using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Yq.Domain.IRepository;
using Yq.Domain.Repository;

namespace Yq.WebApi.Model
{
    /// <summary>
    /// 自动注入依赖关系扩展类
    /// </summary>
    public static class AutomaticInjection
    {
        /// <summary>  
        /// 自动注入依赖关系
        /// </summary>  
        /// <param name="assemblyNames">需要扫描的项目名称集合</param>
        public static void ResolveAllTypes(this IServiceCollection services, params string[] assemblyNames)
        {
            //assemblyNames 需要扫描的项目名称集合
            //注意: 如果使用此方法，必须提供需要扫描的项目名称
            if (assemblyNames.Length > 0)
            {
                foreach (var assemblyName in assemblyNames)
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    List<Type> ts = assembly.GetTypes().ToList();
                    //string filter = "Repository";

                    var definedTypes = assembly.ExportedTypes;
                    if (definedTypes != null && definedTypes.Any())
                    {
                        definedTypes = assembly.DefinedTypes.ToList();
                    }
                    if (definedTypes != null && definedTypes.Any())
                    {
                        //var serviceList = definedTypes.Where(w => w.Name.EndsWith(filter) && w.IsInterface == false).ToList();
                        var serviceList = definedTypes.Where(w => (w.Name.EndsWith("Repository") || w.Name.EndsWith("AppService")) && w.IsInterface == false).ToList();
                        if (serviceList != null)
                        {
                            Type instance;
                            foreach (Type item in serviceList)
                            {
                                instance = item.GetInterface(string.Format("I{0}", item.Name)); //definedTypes.FirstOrDefault(w => w.Name==string.Format("I{0}", item.Name));
                                if (instance != null)
                                {
                                    services.AddScoped(instance, item);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
