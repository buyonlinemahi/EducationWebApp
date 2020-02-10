
namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IStorage
    {       
        string SetNewsPhotoStoragePath(string storagePath, string fileName);
        string SetModulePDFStoragePath(string storagePath, string fileName);
        string SetCoursePDFStoragePath(string storagePath, string fileName);
        string SetModulePPTStoragePath(string storagePath, string fileName);
        string SetModuleImagesStoragePath(string storagePath,string uid, string fileName);
        string SetModuleVideoStoragePath(string storagePath, string fileName);
        string SetModuleMultipleUploadStoragePath(string storagePath, string fileName);
        string SetProductImageStoragePath(string storagePath, string fileName);
        string SetProductFileStoragePath(string storagePath, string fileName);
        string SetOrganizationStoragePath(string storagePath, string fileName);
        string SetOrganizationStorageFaviconPath(string storagePath, string fileName);
        string OrgainzationLogoDelete(string storagePath, string fileName);
        string getVirtualPath();
    }
}
