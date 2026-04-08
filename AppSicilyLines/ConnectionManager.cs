using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AppSicilyLines
{
    internal class ConnectionManager
    {
        static ConnectionManager _instance = null;

        public static ConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConnectionManager();

                return _instance;
            }
        }

        Client? connectedClient = null;

        public Client? CurrentClient
        {
            get => connectedClient;
        }

        public bool IsConnected
        {
            get => CurrentClient != null;
        }

        public async Task Login(string login, string password)
        {

            byte[] passwordToHash = Encoding.ASCII.GetBytes(password);

            byte[] hashedPwd = SHA256.HashData(passwordToHash);

            string finalPwd = Convert.ToHexStringLower(hashedPwd);

            Console.WriteLine($"Sending a login request with login={login}, password={password}...");
            connectedClient = await Helper.GetHttpResource<Client>($"api/client/login/{login}/{finalPwd}");


        }
    }
}