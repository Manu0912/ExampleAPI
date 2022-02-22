namespace ExampleAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }

        public User()
        {

        }
        public User(int id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }


    }
}
