using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutoCaffee
{
    public partial class AutoCaffeeBDContext : DbContext
    {
        public AutoCaffeeBDContext()
        {
        }

        public AutoCaffeeBDContext(DbContextOptions<AutoCaffeeBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Check> Checks { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Dolg> Dolgs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderstatus> Orderstatuses { get; set; }
        public virtual DbSet<Orderstring> Orderstrings { get; set; }
        public virtual DbSet<staff> staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=KPRK;Initial Catalog=AutoCaffeeBD;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Check>(entity =>
            {
                entity.ToTable("checks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idclient).HasColumnName("idclient");

                entity.Property(e => e.Idorder).HasColumnName("idorder");

                entity.Property(e => e.Totalsum).HasColumnName("totalsum");

                entity.HasOne(d => d.IdclientNavigation)
                    .WithMany(p => p.Checks)
                    .HasForeignKey(d => d.Idclient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__checks__idclient__36B12243");

                entity.HasOne(d => d.IdorderNavigation)
                    .WithMany(p => p.Checks)
                    .HasForeignKey(d => d.Idorder)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__checks__idorder__35BCFE0A");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("dishes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("price");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Dolg>(entity =>
            {
                entity.ToTable("dolgs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("price");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idstaff).HasColumnName("idstaff");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.HasOne(d => d.IdstaffNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Idstaff)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__idstaff__2F10007B");

                entity.HasOne(d => d.IdstatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Idstatus)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__idstatus__300424B4");
            });

            modelBuilder.Entity<Orderstatus>(entity =>
            {
                entity.ToTable("orderstatuses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Orderstring>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("orderstrings");

                entity.Property(e => e.Iddish).HasColumnName("iddish");

                entity.Property(e => e.Idorder).HasColumnName("idorder");

                entity.HasOne(d => d.IddishNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Iddish)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orderstri__iddis__32E0915F");

                entity.HasOne(d => d.IdorderNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idorder)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orderstri__idord__31EC6D26");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("firstname");

                entity.Property(e => e.Hashpass)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("hashpass");

                entity.Property(e => e.Iddolg).HasColumnName("iddolg");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(30)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("phonenumber");

                entity.Property(e => e.Secondname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("secondname");

                entity.HasOne(d => d.IddolgNavigation)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.Iddolg)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__staff__iddolg__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
