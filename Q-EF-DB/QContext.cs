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
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tag1 = new Tag { Id = 1, Description= "Sports"};
            var tag2 = new Tag { Id = 2, Description= "Politics"};
            var tag3 = new Tag { Id = 3, Description= "Religion"};
            modelBuilder.Entity<Tag>().HasData(tag1, tag2, tag3);

            var user1 = new User { Id = 1, Name = "Alejandro H. Carrillo" };
            var user2 = new User { Id = 2, Name = "Elmer O. Mcannon" };
            modelBuilder.Entity<User>().HasData(user1, user2);

            var q1 = new Question { Id = 1, Value = "Question 1?", Votes = 1, UserId = 1 };
            var q2 = new Question { Id = 2, Value = "Question 2?", Votes = 2, UserId = 2 };
            var q3 = new Question { Id = 3, Value = "Question 3?", Votes = 3, UserId = 1 };
            modelBuilder.Entity<Question>().HasData(q1, q2, q3);

            var a1q1 = new Answer { Id = 1, Value = "Answer 1 to question 1", Votes = 1, UserId = 1, QuestionId = 1 };
            var a2q1 = new Answer { Id = 2, Value = "Answer 2 to question 1", Votes = 2, UserId = 2, QuestionId = 1 };

            var a1q2 = new Answer { Id = 3, Value = "Answer 1 to question 2", Votes = 11, UserId = 1, QuestionId = 2 };
            var a2q2 = new Answer { Id = 6, Value = "Answer 2 to question 2", Votes = 12, UserId = 2, QuestionId = 2 };

            var a1q3 = new Answer { Id = 4, Value = "Answer 1 to question 3", Votes = 21, UserId = 1, QuestionId = 2 };
            var a2q3 = new Answer { Id = 5, Value = "Answer 2 to question 3", Votes = 22, UserId = 2, QuestionId = 2 };

            modelBuilder.Entity<Answer>().HasData(a1q1, a2q1, a1q2, a2q2, a1q3, a2q3);

            var q1t1 = new QuestionTag { Id = 1, QuestionId = 1, TagId = 1 };
            var q2t1 = new QuestionTag { Id = 2, QuestionId = 2, TagId = 2 };
            var q3t1 = new QuestionTag { Id = 3, QuestionId = 3, TagId = 1 };
            var q3t2 = new QuestionTag { Id = 4, QuestionId = 3, TagId = 2 };

            modelBuilder.Entity<QuestionTag>().HasData(q1t1, q2t1, q3t1, q3t2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
