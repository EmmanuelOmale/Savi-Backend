using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Savi.Data.Domains;

namespace Savi.Data.Context
{
    public class SaviDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {


        public DbSet<UserTransaction> UserTransactions { get; set; }
        public DbSet<GroupTransaction> GroupTransactions { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<SavingGoal> SavingGoals { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletFunding> WalletFundings { get; set; }
        public DbSet<SetTarget> SetTargets { get; set; }
        public DbSet<GroupSavings> GroupSavings { get; set; }

        public DbSet<GroupSavingsMembers> GroupSavingsMembers { get; set; }
        public DbSet<GroupSavingsFunding> GroupSavingsFundings { get; set; }
        public DbSet<SavingsFrequency> SavingFrequencys { get; set; }

        public SaviDbContext(DbContextOptions<SaviDbContext> Options) : base(Options)
        {



        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.ModifiedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        item.Entity.IsDeleted = true;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid().ToString();
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SetTarget>()
           .HasKey(e => e.Id)
           .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            // UserTransaction (User to UserTransaction: 1 to M)
            builder.Entity<UserTransaction>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTransactions)
                .HasForeignKey(ut => ut.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            // GroupTransaction (User to GroupTransaction: 1 to M)
            builder.Entity<GroupTransaction>()
                .HasOne(gt => gt.User)
                //.HasOne(gt => gt.Group)
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

            builder.Entity<Wallet>().HasKey(w => w.WalletId);
            builder.Entity<WalletFunding>().HasKey(wf => wf.Id);

            builder.Entity<Wallet>()
                .HasMany(w => w.WalletFunding)
                .WithOne(wf => wf.Wallet)
                .HasForeignKey(wf => wf.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne(u => u.Wallet)
                .HasForeignKey<Wallet>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
