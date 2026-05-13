using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackTestCS2
{
    public class HelpDeskContextFactory : IDesignTimeDbContextFactory<HelpDeskContext>
    {
        public HelpDeskContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<HelpDeskContextFactory>()
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
