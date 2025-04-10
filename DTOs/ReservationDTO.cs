using Hotel_Reservations_Manager.Data.Entities;

namespace Hotel_Reservations_Manager.DTOs
{
    public class ReservationDTO
    {
        public string RoomId { get; set; }

        public Room? Room { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }

        public virtual ICollection<Client>? Clients { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool BreackfastIncluded { get; set; }

        public bool AllInclusive { get; set; }

        public decimal Sum { get; }

    }
}
