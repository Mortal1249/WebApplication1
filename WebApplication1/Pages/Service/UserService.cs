using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly string _filePath = "users.txt";

        public UserService()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public bool Authenticate(string username, string password)
        {
            var users = File.ReadAllLines(_filePath);
            foreach (var user in users)
            {
                var parts = user.Split(',');
                if (parts[0] == username && parts[1] == password)
                {
                    return true;
                }
            }
            return false;
        }

        public void Register(string username, string password, bool isAdmin)
        {
            var userRecord = $"{username},{password},{isAdmin}";
            File.AppendAllLines(_filePath, new[] { userRecord });
        }

        public bool IsAdmin(string username)
        {
            var users = File.ReadAllLines(_filePath);
            foreach (var user in users)
            {
                var parts = user.Split(',');
                if (parts[0] == username && bool.TryParse(parts[2], out var isAdmin))
                {
                    return isAdmin;
                }
            }
            return false;
        }

        public string GetUserByUsername(string username)
        {
            var users = File.ReadAllLines(_filePath);
            foreach (var user in users)
            {
                var parts = user.Split(',');
                if (parts[0] == username)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
