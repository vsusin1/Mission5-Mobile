namespace SicilyLinesAPI
{
    public class Booking
    {
        long id;

        Client client;
        FerryCrossing crossing;
        DateTime date;

        public long Id
        {
            get => id; set => id = value;
        }
        public Client Client
        {
            get => client; set => client = value;
        }

        public FerryCrossing Crossing
        {
            get => crossing; set => crossing = value;
        }

        public DateTime Date
        {
            get => date; set => date = value;
        }

        public Booking(long id, Client client, FerryCrossing crossing, DateTime date)
        {
            this.id = id;

            Client = client;
            Crossing = crossing;
            Date = date;
        }
    }
}
