namespace Application.Services
{
    public interface IUserService
    {
        string FindUsernameByToken(string token);
    }
}
