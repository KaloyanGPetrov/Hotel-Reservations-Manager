using AutoMapper;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.DTOs;

namespace Hotel_Reservations_Manager.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>()
                .ReverseMap();
        }
    }
}
