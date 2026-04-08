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
                ;
                return $"Le {date.Day.ToString().PadLeft(2, '0')}/{date.Month.ToString().PadLeft(2, '0')}/{date.Year.ToString().PadLeft(4, '0')} à {date.Hour.ToString().PadLeft(2, '0')}h{date.Minute.ToString().PadLeft(2, '0')}";
            }
        }

        private Dictionary<DayOfWeek, string> weekdays = new Dictionary<DayOfWeek, string>
        {
            { DayOfWeek.Monday, "Lundi" },
            { DayOfWeek.Tuesday, "Mardi" },
            { DayOfWeek.Wednesday, "Mercredi" },
            { DayOfWeek.Thursday, "Jeudi" },
            { DayOfWeek.Friday, "Vendredi" },
            { DayOfWeek.Saturday, "Samedi" },
            { DayOfWeek.Sunday, "Dimanche" }
        };

        private List<string> months = new List<string>
        {
            "Janvier",
            "Février",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Août",
            "Septembre",
            "Octobre",
            "Novembre",
            "Décembre"
        };
        public string PrettyDate
        {
            get => $"{weekdays[date.DayOfWeek]} {date.Day} {months[date.Month - 1]} {date.Year}";
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
