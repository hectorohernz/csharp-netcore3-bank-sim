using System;
namespace BankApp.Models
{
    public class User
    {
        public User(string name, string email, string password, string username)
        {
            Name = name;
            Email = email;
            Password = password;
            Username = username;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Email: {Email}  Password: {Password}  Username: {Username}";
        }

        public static bool IsVaildName(string name)
        {
            
            return name.Length != 0 ? true : false;
        } 

        public static bool IsVaildEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsVaildPassword(string password)
        {
            return password.Length >= 8 ? true : false;
        }
    }
}
