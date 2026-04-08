namespace AppSicilyLines
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
        public string FormattedDate
        {
            get
            {
                return $"Le {date.Day}/{date.Month}/{date.Year} à {date.Hour}h{date.Minute}.";
            }
        }

        public Color BackgroundColor
        {
            get => (date < DateTime.Now) ? Colors.LightGray : Colors.LightGreen;
        }

        public Booking(long id, Client client, FerryCrossing crossing, DateTime date)
        {
            this.id = id;
            this.client = client;
            this.crossing = crossing;
            this.date = date;
        }
    }
}
