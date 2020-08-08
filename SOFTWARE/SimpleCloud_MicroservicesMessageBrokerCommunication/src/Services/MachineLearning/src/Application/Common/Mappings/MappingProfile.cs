using AutoMapper;
using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace MachineLearning_SimpleCloud_Broker.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
            ApplyMappingForEntities();
        }

        private void ApplyMappingForEntities()
        {
            var assembly = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == "MachineLearning_SimpleCloud_Broker.Domain");

            var types = assembly.GetTypes().Where(myType => myType.IsClass && myType.IsSubclassOf(typeof(Entity)));
  
            foreach (Type type in types)
            {
                CreateMap(type, type);
            }
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") 
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");
                
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}