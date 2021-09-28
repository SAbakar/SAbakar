using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Principal.Divers;
using Contracts;
using Repositories;
using Principal.Divers.Pagination;
using Microsoft.AspNetCore.Http;
using Principal.Divers.FileWriter;
using Divers.RealTime;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Logging;

namespace RamatBank
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(dbContextOp => dbContextOp.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));

            //services.AddDbContext<AppDBContext>(dbContextOp=>dbContextOp.UseSqlServer("name=ConnectionStrings:LocalDBConnection"));
            //services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IMessageNotification, MessageNotification>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("http://localhost:4200","http://localhost:4201")
                    .AllowAnyMethod()//WithMethods("POST", "GET")
                    .AllowAnyHeader()//WithHeaders("accept", "content-type")
                    .AllowCredentials());                 
            });
            services.AddTransient<IFileHandler, FileHandler>();

            services.AddTransient<IFileWriter, FileWriter>();
            services.AddSignalR(o=>{
                
            });
            
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<SwaggerFilter>();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zestock", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //services.AddLogging(builder => builder.set(LogLevel.Trace));
            services.AddControllers();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*  loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Trace); // ⇦ you're not passing the LogLevel! */

            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zestock v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>  
            {  
                endpoints.MapHub<FromApiBroadcastHub>("/notifier-from");  
                endpoints.MapHub<ToApiBroadCastHub>("/notifier-to");  
            });  
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
