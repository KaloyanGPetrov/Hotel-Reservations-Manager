using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Models;

namespace Hotel_Reservations_Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Hotel_Reservations_Manager.Data.Entities.Room> Room { get; set; } = default!;
        public DbSet<Hotel_Reservations_Manager.Data.Entities.Client> Client { get; set; } = default!;
        public DbSet<Hotel_Reservations_Manager.Data.Entities.Reservation> Reservation { get; set; } = default!;
        public DbSet<Hotel_Reservations_Manager.Models.UserViewModel> UserViewModel { get; set; } = default!;


    }
}
