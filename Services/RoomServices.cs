using AutoMapper;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.DTOs;
using Hotel_Reservations_Manager.Repositories;
using Hotel_Reservations_Manager.Repositories.Abstraction;
using Hotel_Reservations_Manager.Services.Abstraction;

namespace Hotel_Reservations_Manager.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomServices(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _roomRepository.CreateAsync(room);
        }

        public async Task DeleteAsync(int roomId)
        {
            await _roomRepository.DeleteByIdAsync(roomId);
        }


        public async Task<ICollection<RoomDTO>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return _mapper.Map<ICollection<RoomDTO>>(rooms);
        }

        public async Task<RoomDTO> GetByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public ICollection<RoomDTO> GetByNumber(string number)
        {
            var rooms = _roomRepository.GetByFilter(room => room.Number == number);
            return _mapper.Map<ICollection<RoomDTO>>(rooms);
        }

        public async Task UpdateAsync(RoomDTO roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _roomRepository.UpdateAsync(room);
        }
    }
}
