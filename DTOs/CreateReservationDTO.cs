using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Reservations_Manager.DTOs
{
    public class CreateReservationDTO : ReservationDTO
    {
        public List<SelectListItem> Rooms { get; set; }
        public List<SelectListItem> Users { get; set; }

        public List<SelectListItem> Clients { get; set; }

    }
}
