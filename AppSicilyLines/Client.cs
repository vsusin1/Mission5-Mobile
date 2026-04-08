namespace AppSicilyLines
{
    public class Client
    {
        public enum Gender
        {
            Male = 0,
            Female,
            Other
        }

        int id;
        string lastName;
        string firstName;
        DateOnly birthDate;
        Gender clientGender;

        string login;
        string password;

        public int Id { get => id; set => id = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public DateOnly BirthDate { get => birthDate; set => birthDate = value; }
        public Gender ClientGender { get => clientGender; set => clientGender = value; }

        public string Login { get => login; }
        public string Password { get => password; set => password = value; }

        public Client(int id, string lastName, string firstName, DateOnly birthDate, Gender clientGender, string login, string password = "azerty123")
        {
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
