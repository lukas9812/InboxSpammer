namespace SuperSpammer.Infastructure;

public interface IAccountService
{
    Task<bool> ValidateCredentials(string username, string password);
}