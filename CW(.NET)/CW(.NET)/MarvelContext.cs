using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_.NET_
{
    public class MarvelContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Character> Characters { get; set; }

        public MarvelContext():base("name=MarvelCS")
        {

        }
    }
}
