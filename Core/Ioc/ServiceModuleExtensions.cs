﻿using Infrastructure.ControlType.Repository;
using Infrastructure.Field.Repository;
using Infrastructure.Form.Repository;
using Infrastructure.FieldOptions.Repository;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.UserFormValue.Repository;
using Infrastructure.UserForm.Repository;

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
            serviceCollection.AddTransient<IUserFormValuesRepository, UserFormValuesRepository>();
            serviceCollection.AddTransient<IUserFormRepository, UserFormRepository>();


        }
    }
}
