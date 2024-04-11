using CargoShipping.Plugins.Repository.ADONET;
using CargoShipping.UseCases;
using CargoShipping.UseCases.RepositoryPlugins;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargoShipping.WinForms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var frmTrips = serviceProvider.GetRequiredService<frmTrips>();
                Application.Run(frmTrips);
            }            
        }

        static void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<frmTrips>();

            services.AddTransient<ITripSegmentRepository, TripSegmentRepository>();
            services.AddTransient<IPortRepository, PortRepository>();

            services.AddTransient<ISearchByTripNumberUseCase, SearchByTripNumberUseCase>();
            services.AddTransient<IViewAllPortsUseCase, ViewAllPortsUseCase>();
            services.AddTransient<ISearchByPortUseCase, SearchByPortUseCase>();
            
        }
    }
}
