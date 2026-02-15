using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pr14Avalonia.Models;

public partial class KinoteathorDbContext : DbContext
{
    public KinoteathorDbContext()
    {
    }

    public KinoteathorDbContext(DbContextOptions<KinoteathorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Hall> Halls { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionSeat> SessionSeats { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ABCR8CG;Database=Kinoteathor;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MovieName).HasMaxLength(50);
            entity.Property(e => e.Rating).HasMaxLength(3);
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.MovieId).HasColumnName("MovieID");

            entity.HasOne(d => d.GenrelNavigation).WithMany()
                .HasForeignKey(d => d.Genrel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieGenres_Genres");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieGenres_Movies");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BaseTicketPrice).HasColumnType("money");
            entity.Property(e => e.HallId).HasColumnName("HallID");
            entity.Property(e => e.MoviesId).HasColumnName("MoviesID");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Hall).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sessions_Halls1");

            entity.HasOne(d => d.Movies).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.MoviesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sessions_Movies");
        });

        modelBuilder.Entity<SessionSeat>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SeatsId).HasColumnName("SeatsID");
            entity.Property(e => e.SessionsId).HasColumnName("SessionsID");

            entity.HasOne(d => d.Seats).WithMany(p => p.SessionSeats)
                .HasForeignKey(d => d.SeatsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionSeats_Seats");

            entity.HasOne(d => d.Sessions).WithMany(p => p.SessionSeats)
                .HasForeignKey(d => d.SessionsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionSeats_Sessions");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.FinalPrice).HasColumnType("money");
            entity.Property(e => e.PerchaseDateTime).HasColumnType("datetime");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Ticket)
                .HasForeignKey<Ticket>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Users");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Seats");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
