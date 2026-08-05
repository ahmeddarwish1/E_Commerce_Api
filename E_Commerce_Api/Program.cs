
using E_Commerce_Application;
using E_Commerce_Application.Profiles;
using E_Commerce_Application.Service;
using E_Commerce_Infrastructure;
using Microsoft.Extensions.FileProviders;

namespace E_Commerce_Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfraStructureService(builder.Configuration);
            builder.Services.AddAplicationServices();
            builder.Services.Configure<UrlSetting>(builder.Configuration.GetSection("UrlSetting"));
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await app.SeedAndMigrationData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath ,"Files")),
                RequestPath="/Files"
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
