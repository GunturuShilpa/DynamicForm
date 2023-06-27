using Infrastructure.ControlType.Repository;
using Infrastructure.Field.Repository;
using Infrastructure.Form.Repository;
using Infrastructure.FieldOptions.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Ioc
{
    public static class ServiceModuleExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFormRepository, FormRepository>();
            serviceCollection.AddTransient<IControlTypesRepository, ControlTypesRepository>();
            serviceCollection.AddTransient<IFieldRepository, FieldRepository>();
            serviceCollection.AddTransient<IFormFieldOptionsRepository, FormFieldOptionsRepository>();
        }
    }
}
