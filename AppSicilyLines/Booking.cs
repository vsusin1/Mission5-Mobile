namespace AppSicilyLines
{
    public class Booking
    {
        int id;

        Client client;
        FerryCrossing crossing;
        DateTime date;

        public int Id
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

        public string Date
        {
            get
            {
                return $"Le {date.Day}/{date.Month}/{date.Year} à {date.Hour}h{date.Minute}.";
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (date < DateTime.Now)
                    return Colors.LightGray;
                else
                    return Colors.LightGreen;
            }
        }

        public Booking(int id, Client client, FerryCrossing crossing, DateTime date)
        {
            this.id = id;
            this.client = client;
            this.crossing = crossing;
            this.date = date;
        }
    }
}
