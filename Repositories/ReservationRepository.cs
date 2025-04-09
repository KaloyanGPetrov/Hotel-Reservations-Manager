using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Repositories.Abstraction;

namespace Hotel_Reservations_Manager.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
