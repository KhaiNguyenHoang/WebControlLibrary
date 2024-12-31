using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebControlLibrary.Models.Entites;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<Returning> Returnings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=GL66PULSE\\SQLEXPRESS;Database=LibraryDB;User ID=sa;Password=new123;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2279156CD0F");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA9250F971").IsUnique();

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Quantity).HasDefaultValue(0);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__Borrowin__4295F85F7F37564F");

            entity.ToTable("Borrowing");

            entity.Property(e => e.BorrowId).HasColumnName("BorrowID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ReaderId).HasColumnName("ReaderID");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Borrowing__BookI__5070F446");

            entity.HasOne(d => d.Reader).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("FK__Borrowing__Reade__4F7CD00D");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.ReaderId).HasName("PK__Readers__8E67A581499F9ACA");

            entity.Property(e => e.ReaderId).HasColumnName("ReaderID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Returning>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__Returnin__F445E988277AFD62");

            entity.ToTable("Returning");

            entity.Property(e => e.ReturnId).HasColumnName("ReturnID");
            entity.Property(e => e.BorrowId).HasColumnName("BorrowID");
            entity.Property(e => e.LateFee)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Borrow).WithMany(p => p.Returnings)
                .HasForeignKey(d => d.BorrowId)
                .HasConstraintName("FK__Returning__Borro__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
