using HCRGUniversityMgtApp.Domain.Models.StorageModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.DirectoryServices;
using System.IO;
using System.Web;

namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices
{
    public class StorageService : IStorage
    {      
        public const string NewsPhotos = "NewsPhotos";
        public const string ModulePPT = "ModulePPT";
        public const string ModuleImages = "ModuleImages";
        public const string ModulePDF = "ModulePDF"; 
        public const string CoursePDF = "CoursePDF";
        public const string ModuleVideo = "ModuleVideo";
        public const string ModuleUploads = "ModuleUploads";
        public const string ProductImage = "ProductImage";
        public const string ProductFile = "ProductFile";
        public const string LogoImage = "LogoImage";
        public const string FaviconImage = "FaviconImage";


       public string SetNewsPhotoStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, NewsPhotos);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }
        public string SetCoursePDFStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, CoursePDF);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }

        public string SetProductImageStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ProductImage);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }

        public string SetProductFileStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ProductFile);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }

        public string SetModulePDFStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ModulePDF);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }
        public string SetModuleMultipleUploadStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ModuleUploads);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }
        public string SetModulePPTStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ModulePPT);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }


       
        public string SetModuleImagesStoragePath(string storagePath, string uid , string fileName)
        {
            string path = Path.Combine(storagePath, ModuleImages);
            path = path + '/' + uid;
            CreateDirectory(path);
            return path;
        }
        public string SetModuleVideoStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, ModuleVideo);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }

        public string SetOrganizationCssStoragePath(string storagePath, string fileName)
        {
            CreateDirectory(storagePath);
            return Path.Combine(storagePath, fileName);
        }

        public string SetOrganizationStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, LogoImage);          
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }
        public string SetOrganizationStorageFaviconPath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, FaviconImage);
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }
        public string OrgainzationLogoDelete(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, LogoImage);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return Path.Combine(path, fileName);
        }
        public bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }

        public string GeneateStorage(StorageModel _storageModel)
        {
            string path = "";
            path = Path.Combine(_storageModel.path, _storageModel.FolderName, _storageModel.ID.ToString());
            CreateDirectory(path);
            return path;
        }

        public string GeneateClientStorage(StorageModel _storageModel)
        {
            string path = "";
            path = Path.Combine(_storageModel.path, _storageModel.FolderName, _storageModel.ID.ToString() , _storageModel.ClientID.ToString());
            CreateDirectory(path);
            return path;
        }
        public string getVirtualPath()
        {
            DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
            HttpRequest httprequest = System.Web.HttpContext.Current.Request;
            DirectoryEntry entry = new DirectoryEntry("IIS://localhost/W3SVC/" + httprequest.ServerVariables["INSTANCE_ID"].ToString().Trim() + "/Root/Storage");
            return entry.Properties["Path"].Value.ToString().Trim();
           // return "E:\\TFS\\Education\\Web\\HCRGUniversityMgtApp\\HCRGUniversityMgtApp\\Storage";
        }
    }
}
