using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using DocumentsCSVReader.Services;
using DocumentsCSVReader.Services.Interfaces;
using System;

namespace DocumentsCSVReader
{
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IFileReader, CSVReader>();
            services.AddTransient<IDocumentService, DocumentService>();

            serviceProvider = services.BuildServiceProvider();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {

            var startForm = serviceProvider.GetRequiredService<MainWindow>();

            startForm.Show();
        }

    }
}

