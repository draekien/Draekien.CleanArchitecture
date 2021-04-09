﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoMapper;

namespace WeatherForecast.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public MappingProfile(Assembly assembly)
        {
            ApplyMappingsFromAssembly(assembly);
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            List<Type> types = assembly.GetExportedTypes()
                                       .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                                       .ToList();

            foreach (var type in types)
            {
                object? instance = Activator.CreateInstance(type);

                MethodInfo? methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object?[] { this });
            }
        }
    }
}
