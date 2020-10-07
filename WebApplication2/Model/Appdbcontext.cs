using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Migrations;

namespace WebApplication2.Model
{
    public class Appdbcontext : IdentityDbContext<AppUsers>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public Appdbcontext(DbContextOptions<Appdbcontext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            modelBuilder.Entity<Message>()
                                          .HasOne<AppUsers>(a => a.Sender)
                                          .WithMany(d => d.Messages)
                                          .HasForeignKey(d => d.UserID);
            foreach (var foreignkey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }
    }

    }

