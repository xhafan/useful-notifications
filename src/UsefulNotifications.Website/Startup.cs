using System;
using System.Data;
using System.IO;
using System.Reflection;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.Domain.Events;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Register.DependencyInjection;
using CoreDdd.Queries;
using CoreDdd.Register.DependencyInjection;
using DatabaseBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using UsefulNotifications.Commands.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Domain.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Infrastructure;
using UsefulNotifications.Queries.FilmsWithGoodRatingNotifications;
using UsefulNotifications.Website.BusRequestSenders;

namespace UsefulNotifications.Website
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCoreDdd();
            services.AddCoreDddNhibernate<NhibernateConfigurator>();

            // register query handlers and domain event handlers
            services.Scan(scan => scan
                
                .FromAssemblyOf<GetFilmsQuery>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()

                .FromAssemblyOf<FilmDataDownloadRequestedDomainEventHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            var rebusInputQueueName = "UsefulNotifications.Website"; // todo: move it to config file
            var rebusRabbitMqConnectionString = "amqp://admin:password01@localhost";

            services.AddRebus(configure => configure
                .Logging(l => l.Trace())
                .Transport(t => t.UseRabbitMq(rebusRabbitMqConnectionString, rebusInputQueueName))
                .Options(o => o.EnableSynchronousRequestReply())
                .Routing(x => x.TypeBased().MapAssemblyOf<DownloadLocationFilmDataCommand>("UsefulNotifications.ServiceApp"))
            );

            services.AddSingleton<IBusRequestSender, BusRequestSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();          
            app.UseCookiePolicy();

            app.ApplicationServices.UseRebus();

            app.UseMiddleware<UnitOfWorkDependencyInjectionMiddleware>(IsolationLevel.ReadCommitted);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            _BuildDatabase(app);
        }

        private void _BuildDatabase(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var nhibernateConfigurator = serviceProvider.GetService<INhibernateConfigurator>();
            var configuration = nhibernateConfigurator.GetConfiguration();
            var connectionString = configuration.Properties["connection.connection_string"];
            var scriptsDirectoryPath = Path.Combine(_GetAssemblyCodeBaseLocation(), "DatabaseScripts");
            var builderOfDatabase = new BuilderOfDatabase(() => new NpgsqlConnection(connectionString));
            builderOfDatabase.BuildDatabase(scriptsDirectoryPath);
        }

        // https://stackoverflow.com/a/283917/379279
        private string _GetAssemblyCodeBaseLocation()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}
