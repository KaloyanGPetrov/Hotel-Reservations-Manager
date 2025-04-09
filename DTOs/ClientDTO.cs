using Hotel_Reservations_Manager.Data.Entities;

namespace Hotel_Reservations_Manager.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsOfAge { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
