using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace FullstackTestCS2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }


        public App()
        {
            try
            {
                var config = new ConfigurationBuilder()
                                    .AddUserSecrets<App>()
                                    .Build();

                var conexao = ConnectionHelper.GetConnectionString(config);

                var services = new ServiceCollection();

                services.AddDbContext<HelpDeskContext>(options =>
                    options.UseMySql(
                        conexao,
                        ServerVersion.AutoDetect(conexao)));

                services.AddSingleton<MainWindow>();

                ServiceProvider = services.BuildServiceProvider();
            }
            catch (Exception e)
            {
                MessageBox.Show("Operação Invalida:\n" + e.Message);

            }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

    }

}
