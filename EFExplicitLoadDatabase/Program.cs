using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EFExplicitLoadDatabase
{
    public class City
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
    }
    public class Country
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public int CapitalId { set; get; }
        public City? Capital { set; get; }
        public List<Company> Companies { get; set; } = new List<Company>();
    }
    public class Company
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public int CountryId { set; get; }
        public Country? Country { set; get; }
        public List<Employe> Employes { get; set; } = new List<Employe>();
    }

    public class Position
    {
        public int Id { set; get; }
        public string? Title { set; get; } = null!;
        public List<Employe> Employes { get; set; } = new List<Employe>();
    }
    public class Employe
    {
        public int Id { set; get; }
        public string? Name { set; get; } = null!;
        public DateTime BirthDate { set; get; }
        public int? CompanyId { set; get; } // свойство - внешний ключ
        public Company? Company { set; get; } // навигационное свойство
        public int? PositionId { set; get; } // свойство - внешний ключ
        public Position? Position { set; get; } // навигационное свойство

    }

    public class AppContext : DbContext
    {
        public DbSet<Employe> Employes { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public AppContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CompaniesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            using(AppContext context = new())
            {

                //Company company = context.Companies.FirstOrDefault();
                //context.Employes.Where(e => e.CompanyId == company.Id).Load();

                //Employe employe1 = context.Employes.FirstOrDefault();
                //context.Entry(employe1).Reference(e => e.Company).Load();
                //Console.WriteLine($"{employe1.Name} {employe1.Company.Title}");


                //Console.WriteLine("\n--- Employes: -----");
                //foreach (Employe employe in context.Employes)
                //{
                //    Console.WriteLine($"{employe.Name} {employe?.Position?.Title}");
                //    Console.WriteLine($"{employe?.Company?.Title}");
                //    Console.WriteLine();
                //}

                /*
                Company company = context.Companies.FirstOrDefault();
                context.Entry(company).Collection(c => c.Employes).Load();

                Console.WriteLine(company.Title);
                foreach(Employe employe in company.Employes)
                {
                    Console.WriteLine($"\t{employe.Name}");
                }
                */

                Country country = context.Countries.FirstOrDefault();
                context.Entry(country).Collection(c => c.Companies).Load();

                Console.WriteLine(country.Title);
                foreach (Company company in country.Companies)
                    Console.WriteLine($"\t{company.Title}");

                }
            
        }
    }
}