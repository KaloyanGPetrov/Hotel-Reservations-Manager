using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Repositories.Abstraction;

namespace Hotel_Reservations_Manager.Repositories
{
    public class RoomRepository : CrudRepository<Room>, IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
