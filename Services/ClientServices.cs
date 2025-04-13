using AutoMapper;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.DTOs;
using Hotel_Reservations_Manager.Repositories;
using Hotel_Reservations_Manager.Repositories.Abstraction;
using Hotel_Reservations_Manager.Services.Abstraction;

namespace Hotel_Reservations_Manager.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientServices(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            await _clientRepository.CreateAsync(client);
        }

        public async Task DeleteAsync(int clientId)
        {
            await _clientRepository.DeleteByIdAsync(clientId);
        }


        public async Task<ICollection<ClientDTO>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return _mapper.Map<ICollection<ClientDTO>>(clients);
        }

        public async Task<ClientDTO> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return _mapper.Map<ClientDTO>(client);
        }

        public ICollection<ClientDTO> GetByName(string name)
        {
            var clients = _clientRepository.GetByFilter(client => client.FirstName == name || client.LastName == name);
            return _mapper.Map<ICollection<ClientDTO>>(clients);
        }

        public async Task UpdateAsync(ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            await _clientRepository.UpdateAsync(client);
        }
    }
}
