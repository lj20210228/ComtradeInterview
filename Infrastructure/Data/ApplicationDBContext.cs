using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            
        }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<CampaignPurchase> CampaignPurchases { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Nomination>()
                .HasOne(n => n.Agent)
                .WithMany(a => a.Nominations)
                .HasForeignKey(n => n.AgentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Nomination>()
                .HasIndex(n => new { n.AgentId, n.NominatedAt });
            modelBuilder.Entity<Agent>().HasData([
                new Agent { Id=1,Name="Agent Lazar",Email="lazar@telekom.rs",PasswordHash="lazar123"},
                new Agent { Id=2,Name="Agent Nikola",Email="nikola@telekom.rs",PasswordHash="nikola123"},
                ]
                );

             
        }
    }
}
