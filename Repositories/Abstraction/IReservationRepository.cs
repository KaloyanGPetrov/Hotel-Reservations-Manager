using Hotel_Reservations_Manager.Data.Entities;

namespace Hotel_Reservations_Manager.Repositories.Abstraction
{
    public interface IReservationRepository 
    {
        Task CreateAsync(Reservation reservation);
        //TODO: edit visit
        //TODO: get all visits
        //TODO: get visit by id
        //TODO: delete visit
    }
}
