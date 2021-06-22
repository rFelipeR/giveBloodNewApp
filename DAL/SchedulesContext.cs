using Microsoft.EntityFrameworkCore;  //SOFT que auxilia trabalhar c/ BdD Relacional (SQL). Entender, carregar e transformar (traduzir/intermediar)
using Microsoft.EntityFrameworkCore.Proxies;
//using System.Data.Entity;

namespace DAL
{
    public class SchedulesContext : DbContext
    {
        public SchedulesContext() : base(Connection()) { }

        private static DbContextOptions<SchedulesContext> Connection()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchedulesContext>();
            optionsBuilder.UseLazyLoadingProxies(); //carregar info automaticamente e usar conexão MySQL;
            optionsBuilder.UseMySQL("server=remotemysql.com;database=31V2Jahjlh;user=31V2Jahjlh;password=Qby9r8QvZU;port=3306");
            return optionsBuilder.Options;
        }
        public DbSet<Scheduling> Schedules { get; set; } //p/ cada table no DB. OBS: DAL; cada camada é uma função, 
        public DbSet<BloodCenter> BloodCenters { get; set; }  
        public DbSet<City> Cities { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Employee> Customers { get; set; }

    }
}
//Context armazena tipo do Provider (nesse caso, MySQL, mas tb pode Oracle, PostGree...)
//Esse site + user senha;
