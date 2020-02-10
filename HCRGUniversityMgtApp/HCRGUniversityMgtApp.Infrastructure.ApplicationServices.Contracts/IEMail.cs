
namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IEMail
    {
        string SendEmail(string msg, string sender, string subject);
        string SendRandomPasswordEmail(string msg, string sender, string subject);
    }
}
