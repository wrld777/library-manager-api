using System;
using System.Collections.Generic;
using System.Text;
using LibraryManager.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
namespace Librarymanager.Infrastructure.context
{
    public class LmDbContext : DbContext
    {
        public LmDbContext(DbContextOptions<LmDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.CopiesAvailable).IsRequired();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemberId);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.LoanId);
                entity.Property(e => e.LoanDate).IsRequired();
                entity.Property(e => e.DueDate).IsRequired();
            });

        }

    }
}
