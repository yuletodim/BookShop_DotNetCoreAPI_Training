namespace BookShop.Api.Infarstructure.Mappings
{
    using AutoMapper;
    using System;
    using System.Linq;

    using Common.Mappings;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var assemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()// BUT this get system assemblies as well ~ 12000!!!! -> So filter it by name
                .Where(a => a.GetName().Name.Contains("BookShop"));

            var exportedTypes = assemblies
                .SelectMany(a => a.GetExportedTypes());

            exportedTypes
                .Where(t =>
                    t.IsClass
                    && !t.IsAbstract
                    && t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First(),

                })
                .ToList()
                .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            exportedTypes
                .Where(t =>
                    t.IsClass
                    && !t.IsAbstract
                    && t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .Contains(typeof(IMapTo<>)))
                .Select(t => new
                {
                    Source = t,
                    Destination = t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapTo<>))
                        .SelectMany(i => i.Arguments)
                        .First(),

                })
                .ToList()
                .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            exportedTypes
                .Where(t =>
                    t.IsClass
                    && !t.IsAbstract
                    && typeof(IHaveCustomMappings).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMappings>()
                .ToList()
                .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}
