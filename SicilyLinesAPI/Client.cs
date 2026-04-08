namespace SicilyLinesAPI
{
    public class Client
    {
        public enum Gender
        {
            Male = 0,
            Female,
            Other
        }
        long id;
        string lastName;
        string firstName;
        DateOnly birthDate;
        Gender clientGender;

        string login;
        string password;

        public long Id { get => id; set => id = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public DateOnly BirthDate { get => birthDate; set => birthDate = value; }

        public Gender ClientGender { get => clientGender; set => clientGender = value; }

        public string Login { get => login; }
        public string Password { get => password; set => password = value; }

        public Client(long id, string lastName, string firstName, DateOnly birthDate, Gender clientGender, string login, string password = "f3029a66c61b61b41b428963a2fc134154a5383096c776f3b4064733c5463d90" ) // pwd= azerty123
        {
            // Id auto-increment feature
            this.id = id;

            this.lastName = lastName;
            this.firstName = firstName;
            this.birthDate = birthDate;
            this.clientGender = clientGender;
            this.login = login;
            this.password = password;
        }
    }
}
