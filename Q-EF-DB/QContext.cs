using Microsoft.EntityFrameworkCore;
using Q_EF_DB.Entities;

namespace Q_EF_DB
{
    public class QContext : DbContext
    {
        public QContext(DbContextOptions<QContext> options) : base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
