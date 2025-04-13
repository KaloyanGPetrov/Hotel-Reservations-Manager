using Hotel_Reservations_Manager.DTOs;

namespace Hotel_Reservations_Manager.Services.Abstraction
{
    public interface IRoomServices
    {
        Task<RoomDTO> GetByIdAsync(int id);
        Task<ICollection<RoomDTO>> GetAllAsync();
        Task CreateAsync(RoomDTO roomDto);
        Task UpdateAsync(RoomDTO roomDto);
        Task DeleteAsync(int roomId);
        ICollection<RoomDTO> GetByNumber(string number);
    }
}
