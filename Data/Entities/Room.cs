namespace Hotel_Reservations_Manager.Data.Entities
{
    public class Room : BaseEntity
    {
        public int Capacity { get; set; }

        public string Type { get; set; }

        public bool IsOccupied { get; set; }

        public decimal PriceAdult { get; set; }
        public decimal PriceChild { get; set; }

        public string Number { get; set; }

    }
}
