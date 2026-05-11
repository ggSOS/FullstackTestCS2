using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackTestCS2
{
    internal class HelpDeskContextFactory
    {
        public HelpDeskContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<MainWindow>()
                .Build();
            var conexao = ConnectionHelper.GetConnectionString(config);

            var optionsBuilder = new DbContextOptionsBuilder<HelpDeskContext>();
            optionsBuilder.UseMySql(
                conexao,
                ServerVersion.AutoDetect(conexao));

            return new HelpDeskContext(optionsBuilder.Options);
        }
    }
}
