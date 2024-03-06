using System.Reflection;

namespace AppointmentWebApp.API.Helper
{
    public static class ServiceCollection
    {
        public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.Namespace?.EndsWith(".Service") == true && type.Name.Contains("Service"))
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault();

                if (interfaceType != null)
                {
                    services.AddTransient(interfaceType, serviceType);
                }
            }
        }
    }
}
