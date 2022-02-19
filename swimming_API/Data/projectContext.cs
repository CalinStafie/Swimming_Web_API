using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Data
{
    public class projectContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public projectContext(DbContextOptions<projectContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<NormalWorkDay> NormalWorkDays { get; set; }
        public DbSet<VacationDay> VacationDays { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<NormalWorkDay>()
                .HasOne(a => a.Receptionist)
                .WithMany(d => d.NormalWorkDays);

            builder.Entity<VacationDay>()
                .HasOne(vd => vd.Receptionist)
                .WithMany(d => d.VacationDays);

            builder.Entity<VacationDay>()
                .HasOne(a => a.Address)
                .WithOne(v => v.VacationDay);

            builder.Entity<Purchase>()
                .HasKey(a => new { a.ClientId, a.ReceptionistId, a.SubscriptionId, a.StartTime });

            builder.Entity<Purchase>()
                .HasOne<Receptionist>(a => a.Receptionist)
                .WithMany(d => d.Purchases)
                .HasForeignKey(a => a.ReceptionistId);

            builder.Entity<Purchase>()
                .HasOne<Client>(a => a.Client)
                .WithMany(p => p.Purchases)
                .HasForeignKey(a => a.ClientId);

            builder.Entity<Purchase>()
                .HasOne<Subscription>(a => a.Subscription)
                .WithMany(p => p.Purchases)
                .HasForeignKey(a => a.SubscriptionId);

            base.OnModelCreating(builder);
        }
    }
}
