namespace ECommerce.Identity.Domain.Aggregates
{
    public record Role
    {
        public string Name { get; }
        public Role(string name)
        {
            Name = name;
        }

        public static Role Admin => new Role("Admin");
        public static Role User => new Role("User");

        public static Role FromName(string name)
        {
            return name.ToLower() switch
            {
                "admin" => Admin,
                "user" => User,
                _ => throw new InvalidOperationException("Invalid role name")
            };
        }


    }
}
