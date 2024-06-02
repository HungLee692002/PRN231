using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment2.Models
{
    public partial class Assignment2Context : DbContext
    {
        public Assignment2Context()
        {
        }

        public Assignment2Context(DbContextOptions<Assignment2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConStr");
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(config);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(225)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(225)
                    .HasColumnName("city");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(225)
                    .HasColumnName("email_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(225)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(225)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(225)
                    .HasColumnName("phone");

                entity.Property(e => e.State)
                    .HasMaxLength(225)
                    .HasColumnName("state");

                entity.Property(e => e.Zip)
                    .HasMaxLength(225)
                    .HasColumnName("zip");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Advance)
                    .HasMaxLength(225)
                    .HasColumnName("advance");

                entity.Property(e => e.Notes)
                    .HasMaxLength(225)
                    .HasColumnName("notes");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.PublishedDate)
                    .HasColumnType("date")
                    .HasColumnName("published_date");

                entity.Property(e => e.Royalty)
                    .HasMaxLength(225)
                    .HasColumnName("royalty");

                entity.Property(e => e.Title)
                    .HasMaxLength(225)
                    .HasColumnName("title");

                entity.Property(e => e.Type)
                    .HasMaxLength(225)
                    .HasColumnName("type");

                entity.Property(e => e.YtdSales)
                    .HasMaxLength(225)
                    .HasColumnName("ytd_sales");

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PubId)
                    .HasConstraintName("FK_Book_Publisher");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BookAuthor");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.AuthorOrder).HasColumnName("author_order");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.RoyalityPercentage).HasColumnName("royality_percentage");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookAuthor_Author");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookAuthor_Book");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PubId);

                entity.ToTable("Publisher");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.City)
                    .HasMaxLength(225)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(225)
                    .HasColumnName("country");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(225)
                    .HasColumnName("publisher_name");

                entity.Property(e => e.State)
                    .HasMaxLength(225)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleDesc)
                    .HasMaxLength(225)
                    .HasColumnName("role_desc");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(225)
                    .HasColumnName("email_address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(225)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate)
                    .HasColumnType("date")
                    .HasColumnName("hire_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(225)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(225)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(225)
                    .HasColumnName("password");

                entity.Property(e => e.PubId).HasColumnName("pub_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Source)
                    .HasMaxLength(225)
                    .HasColumnName("source");

                entity.HasOne(d => d.Pub)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PubId)
                    .HasConstraintName("FK_User_Publisher");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
