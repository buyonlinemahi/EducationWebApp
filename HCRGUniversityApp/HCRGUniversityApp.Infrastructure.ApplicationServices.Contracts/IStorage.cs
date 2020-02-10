
namespace HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IStorage
    {
        string GetUserInfoStoragePath(string storagePath, string fileName);
        string GetCoursePDFStoragePath(string storagePath, string fileName);
        string SetUserInfoStoragePath(string storagePath, string fileName);
        string SetResumeUploadStoragePath(string storagePath, string fileName);
        string SetCertificateStoragePath(string storagePath, string fileName);
        string getVirtualPath();
        string SetOrganizationStoragePath(string storagePath, string fileName);
    }
}
