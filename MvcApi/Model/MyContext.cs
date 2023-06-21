using System;
using Microsoft.EntityFrameworkCore;

namespace DBcon.Model
{
    public class MyContext: DbContext
    {
        public DbSet<Login> Logins { get; set; }
        public DbSet<Highscore> Highscores { get; set; }
        public MyContext(DbContextOptions<MyContext> options) :base(options)
        {

        }
    }
}
