namespace Hotel_Reservations_Manager.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Client> ? Clients { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool BreackfastIncluded { get; set; }

        public bool AllInclusive { get; set; }

        public decimal Sum
        {
            get
            {
                int children = Clients.Where(x => x.IsOfAge == false).Count();
                int adults = Clients.Where(x => x.IsOfAge == true).Count();
                decimal sum = Room.PriceChild*children + Room.PriceChild*adults;

                if (BreackfastIncluded)
                {
                    sum += children * 5 + adults * 7;
                }

                if (AllInclusive)
                {
                    sum += children * 12 + adults * 20;
                }
                return sum;
            }
        }

        public void UpdateRoom()
        {
            if (EndDate < DateTime.Now) Room.IsOccupied = false;
            else if (StartDate < DateTime.Now) Room.IsOccupied = true;
        }

    }
}
