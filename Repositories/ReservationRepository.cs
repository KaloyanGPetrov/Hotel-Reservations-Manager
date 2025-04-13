using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Repositories.Abstraction;

namespace Hotel_Reservations_Manager.Repositories
{
    public class ReservationRepository : CrudRepository<Reservation>, IReservationRepository
    {

        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
            :base(context)
        {
        }
    }
}
