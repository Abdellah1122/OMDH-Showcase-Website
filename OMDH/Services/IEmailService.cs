namespace OMDH.Services
{
    public interface IEmailService
    {
        void SendEmail(Models.EmailDTO request);
    }
}
