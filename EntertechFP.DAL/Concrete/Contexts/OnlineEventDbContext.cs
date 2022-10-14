using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete.Contexts
{
    public partial class OnlineEventDbContext : DbContext
    {
        public OnlineEventDbContext()
        {
        }

        public OnlineEventDbContext(DbContextOptions<OnlineEventDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Entegrator> Entegrators { get; set; } = null!;
        public virtual DbSet<EntegratorEvent> EntegratorEvents { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventAttendance> EventAttendances { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityName).HasMaxLength(20);
            });

            modelBuilder.Entity<Entegrator>(entity =>
            {
                entity.Property(e => e.ApiKey).HasMaxLength(100);

                entity.Property(e => e.DomainName).HasMaxLength(50);

                entity.Property(e => e.EmailAdress).HasMaxLength(50);

                entity.Property(e => e.EntegratorName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(32);
            });

            modelBuilder.Entity<EntegratorEvent>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Entegrator)
                    .WithMany()
                    .HasForeignKey(d => d.EntegratorId)
                    .HasConstraintName("FK_EntegratorEvents_Entegrators");

                entity.HasOne(d => d.Event)
                    .WithMany()
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_EntegratorEvents_Events");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Address).HasColumnType("ntext");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventName).HasMaxLength(100);

                entity.Property(e => e.Fare)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LastAttendDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Events_Categories");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Events_Cities");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Events_Users");
            });

            modelBuilder.Entity<EventAttendance>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Event)
                    .WithMany()
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventAttendances_Events");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_EventAttendances_Users");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.NotificationDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Notifications_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
