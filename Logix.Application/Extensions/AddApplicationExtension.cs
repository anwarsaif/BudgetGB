using Microsoft.Extensions.DependencyInjection;
using AutoMapper.Extensions.ExpressionMapping;


namespace Logix.Application.Extensions
{
    public static class AddApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddAutoMapper(cx => { cx.AddExpressionMapping(); }, AppDomain.CurrentDomain.Load("Logix.Application"));
                       
            return services;

        }



    }
}
