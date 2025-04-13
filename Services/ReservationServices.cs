using AutoMapper;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.DTOs;
using Hotel_Reservations_Manager.Repositories.Abstraction;
using Hotel_Reservations_Manager.Services.Abstraction;

namespace Hotel_Reservations_Manager.Services
{
    public class ReservationServices : IReservationServices
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationServices(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(ReservationDTO reservationDto)
        {
            var client = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.CreateAsync(client);
        }

        public async Task DeleteAsync(int reservationId)
        {
            await _reservationRepository.DeleteByIdAsync(reservationId);
        }


        public async Task<ICollection<ReservationDTO>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<ICollection<ReservationDTO>>(reservations);
        }

        public async Task<ReservationDTO> GetByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDTO>(reservation);
        }

        public ICollection<ReservationDTO> GetByRoomNumber(string roomNumber)
        {
            var reservations = _reservationRepository.GetByFilter(reservation => reservation.Room.Number  == roomNumber);
            return _mapper.Map<ICollection<ReservationDTO>>(reservations);
        }

        public async Task UpdateAsync(ReservationDTO reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.UpdateAsync(reservation);
        }
    }
}
