namespace UserManager.Entities.Concrete
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }      
        public string Email { get; set; }
        public string Password { get; set; }
        public Status status  { get; set; } 
    }
    public enum Status      
        {
            Active = 1,
            Passive = 0
        }
}