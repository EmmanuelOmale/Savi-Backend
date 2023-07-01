using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Savi.Data.Domains;

namespace Savi.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<UserTransaction> UserTransactions { get; set; }
        public DbSet<GroupTransaction> GroupTransactions { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // UserTransaction (User to UserTransaction: 1 to M)
            builder.Entity<UserTransaction>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTransactions)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // GroupTransaction (User to GroupTransaction: 1 to M)
            builder.Entity<GroupTransaction>()
                .HasOne(gt => gt.User)
                .WithMany(u => u.GroupTransactions)
                .HasForeignKey(gt => gt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Saving (User to Saving: 1 to 1)
            builder.Entity<Saving>()
                .HasOne(s => s.User)
                .WithOne(u => u.Saving)
                .HasForeignKey<Saving>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // CardDetail (User to CardDetail: 1 to M)
            builder.Entity<CardDetail>()
                .HasOne(cd => cd.User)
                .WithMany(u => u.CardDetails)
                .HasForeignKey(cd => cd.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // OTP (User to OTP: 1 to 1)
            builder.Entity<OTP>()
                .HasOne(o => o.User)
                .WithOne(u => u.OTP)
                .HasForeignKey<OTP>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // GroupTransaction (Group to GroupTransaction: 1 to M)
            builder.Entity<GroupTransaction>()
                .HasOne(gt => gt.Group)
                .WithMany(g => g.GroupTransactions)
                .HasForeignKey(gt => gt.GroupId)
                .OnDelete(DeleteBehavior.Cascade);


    }
    }
}
