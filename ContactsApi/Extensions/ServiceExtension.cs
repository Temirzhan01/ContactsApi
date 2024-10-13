using ContactsApi.Data;
using ContactsApi.Services;
using ContactsApi.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Extensions
{
    public static class ServiceExtension
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddScoped<IContactsService, ContactsService>();

            builder.Services.AddDbContext<ContactsDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("NpgSql"));
            });

            return builder;
        }
    }
}
