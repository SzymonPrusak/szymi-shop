namespace SzymiShop.WebApi.Service.Crypto
{
    public interface ISignatureService
    {
        string Sign(string text);
    }
}
