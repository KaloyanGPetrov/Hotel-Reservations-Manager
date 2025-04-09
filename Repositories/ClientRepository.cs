using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Repositories.Abstraction;

namespace Hotel_Reservations_Manager.Repositories
{
    public class ClientRepository : CrudRepository<Client>, IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
