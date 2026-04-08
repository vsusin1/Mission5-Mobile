using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AppSicilyLines
{
    internal class ConnectionManager
    {
        public enum LoginResult
        {
            Success,
            EmptyLogin,
            EmptyPassword,
            WrongCredentials
        }
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

        public async Task<LoginResult> Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                return LoginResult.EmptyLogin;

            if (string.IsNullOrEmpty(password))
                return LoginResult.EmptyPassword;

            byte[] passwordToHash = Encoding.ASCII.GetBytes(password);

            byte[] hashedPwd = SHA256.HashData(passwordToHash);

            string finalPwd = Convert.ToHexStringLower(hashedPwd);

            Console.WriteLine($"Sending a login request with login={login}, password={password}...");
            connectedClient = await Helper.GetHttpResource<Client>($"api/client/login/{login}/{finalPwd}");

            return (connectedClient != null) ? LoginResult.Success : LoginResult.WrongCredentials;

        }

        public void Disconnect()
        {
            connectedClient = null;
        }
    }
}