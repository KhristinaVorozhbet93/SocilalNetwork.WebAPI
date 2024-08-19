namespace SocialNetwork.UserSvc
{
    public interface IApplicationPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword
            (string hashedPassword, string providedPassword, out bool rehashNeeded);
    }
}
