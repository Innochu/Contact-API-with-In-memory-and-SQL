using City.Models;
using Microsoft.EntityFrameworkCore;

namespace City_Info.Data
{
	public class CityDBContext : DbContext  // next is to inject this DBcontext into my other services in our controller using dependency injection
	{
		public CityDBContext(DbContextOptions options) : base(options)
		{

		}

        public DbSet<Contact> Contacts { get; set; }
    }
}
