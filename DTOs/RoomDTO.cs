using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservations_Manager.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int Capacity { get; set; }

        public string Type { get; set; }

        [Display(Name = "Is currently occupied")]
        public bool IsOccupied { get; set; }

        [Display(Name = "Price for adults")]
        public decimal PriceAdult { get; set; }

        [Display(Name = "Price for children")]
        public decimal PriceChild { get; set; }

        public string Number { get; set; }
    }
}
