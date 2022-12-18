using ClinicService.Data;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CliniscServiceV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5100, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5101, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1;
                });
            });

            builder.Services.AddGrpc().AddJsonTranscoding();
            builder.Services.AddDbContext<ClinicServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });

            builder.Services.AddGrpcSwagger();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ClinicService", Version = "v1" });
                var filepath = Path.Combine(System.AppContext.BaseDirectory, "CliniscServiceV2.xml");
                c.IncludeXmlComments(filepath);
                c.IncludeXmlComments(filepath, includeControllerXmlComments: true);

            });
          
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json","v1");
                });
            }
            app.UseRouting();
            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.MapGrpcService<CliniscServiceV2.Services.ClinicService>().EnableGrpcWeb();
            app.MapGet("/",
                () =>
                "Communication with gRPC endpoints must be through a gRPC client");
            app.Run();
        }
    }
}