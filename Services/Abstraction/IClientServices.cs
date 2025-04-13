using Hotel_Reservations_Manager.DTOs;

namespace Hotel_Reservations_Manager.Services.Abstraction
{
    public interface IClientServices
    {
        Task<ClientDTO  > GetByIdAsync(int id);
        Task<ICollection<ClientDTO>> GetAllAsync();
        Task CreateAsync(ClientDTO clientDto);
        Task UpdateAsync(ClientDTO clientDto);
        Task DeleteAsync(int clientDto);
        ICollection<ClientDTO> GetByName(string name);
    }
}
