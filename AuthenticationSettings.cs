namespace RestaurantAPI
{
    public class AuthenticationSettings
    {
        public AuthenticationSettings() { }
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
