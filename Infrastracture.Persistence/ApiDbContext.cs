using BlastAsia.DigiBook.Infrastructure.Security;
using Domain.Models.Samples;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastracture.Persistence
{
    public class ApiDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApiDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }
        //tables
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Token> Tokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Token
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Tokens).WithOne(i => i.User);

            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");

            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens");


            modelBuilder.Entity<Token>().ToTable("Tokens");
            modelBuilder.Entity<Token>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Token>().HasOne(i => i.User).WithMany(u => u.Tokens);
            #endregion
        }
    }
}
