
namespace HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts
{
   public interface IEMail
    {
       void SendEmail(string subject, string msg, string sender);
    }
}
