using BitirmeProjesi1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi1.Data.Context
{
    public class ProjectContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }
        public DbSet<FlightPayment> FlightPayments { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<HotelPayment> HotelPayments { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-BCGODK3K;Database=TatilSitesi;Trusted_Connection=true;TrustServerCertificate=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User - HotelBooking relationship (One-to-Many)
            modelBuilder.Entity<HotelBooking>()
                .HasOne(hb => hb.User)
                .WithMany(u => u.HotelBookings)
                .HasForeignKey(hb => hb.UserID);

            // User - FlightBooking relationship (One-to-Many)
            modelBuilder.Entity<FlightBooking>()
                .HasOne(fb => fb.User)
                .WithMany(u => u.FlightBookings)
                .HasForeignKey(fb => fb.UserID);

            // Room - Hotel relationship (One-to-Many)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelID);

            // Room - HotelBooking relationship (One-to-Many)
            modelBuilder.Entity<HotelBooking>()
                .HasOne(hb => hb.Room)
                .WithMany(r => r.HotelBookings)
                .HasForeignKey(hb => hb.RoomID);

            // Hotel - HotelImage relationship (One-to-Many)
            modelBuilder.Entity<HotelImage>()
                .HasOne(hi => hi.Hotel)
                .WithMany(h => h.HotelImages)
                .HasForeignKey(hi => hi.HotelID);

            // HotelBooking - HotelPayment relationship (One-to-One)
            modelBuilder.Entity<HotelPayment>()
                .HasOne(hp => hp.HotelBooking)
                .WithOne(hb => hb.HotelPayment)
                .HasForeignKey<HotelPayment>(hp => hp.HotelBookingID);

            // FlightBooking - FlightPayment relationship (One-to-One)
            modelBuilder.Entity<FlightPayment>()
                .HasOne(fp => fp.FlightBooking)
                .WithOne(fb => fb.FlightPayment)
                .HasForeignKey<FlightPayment>(fp => fp.FlightBookingID);

            // Flight - FlightBooking relationship (One-to-Many)
            modelBuilder.Entity<FlightBooking>()
                .HasOne(fb => fb.Flight)
                .WithMany(f => f.FlightBookings)
                .HasForeignKey(fb => fb.ID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
