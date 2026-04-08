namespace AppSicilyLines
{
    public class FerryCrossing
    {
        static int registeredIds = 1;

        int id;
        string startPort;
        string endPort;

        public string StartPort
        {
            get => startPort; set => startPort = value;
        }

        public string EndPort
        {
            get => endPort; set => endPort = value;
        }

        public string Name
        {
            get => $"{StartPort} → {EndPort}";
        }
        
        public FerryCrossing(string startPort, string endPort)
        {
            id = registeredIds;
            id++;

            this.startPort = startPort;
            this.endPort = endPort;
        }
    }
}
