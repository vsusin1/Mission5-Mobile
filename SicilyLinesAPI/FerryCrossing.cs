namespace SicilyLinesAPI
{
    public class FerryCrossing
    {
        long id;
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
        
        public FerryCrossing(long id, string startPort, string endPort)
        {
            this.id = id;
            this.startPort = startPort;
            this.endPort = endPort;
        }
    }
}
