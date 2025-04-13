using Hotel_Reservations_Manager.DTOs;

namespace Hotel_Reservations_Manager.Services.Abstraction
{
    public interface IReservationServices
    {
        Task<ReservationDTO> GetByIdAsync(int id);
        Task<ICollection<ReservationDTO>> GetAllAsync();
        Task CreateAsync(ReservationDTO clientDto);
        Task UpdateAsync(ReservationDTO clientDto);
        Task DeleteAsync(int clientId);
        ICollection<ReservationDTO> GetByRoomNumber(string name);
    }
}
