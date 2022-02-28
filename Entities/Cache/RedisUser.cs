namespace BlazorApp.Entities.Cache
{
    public class RedisUser
    {
        public int Id { get; set; }
        public string Email { get; set; }


        public RedisUser(int Id, string Email)
        {
            this.Id = Id;
            this.Email = Email;

        }

    }
}