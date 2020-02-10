using System;

namespace HCRGUniversityMgtApp.Domain.Models.StorageModel
{
   public class StorageModel
    {
        public int ID { get; set; }
        public string path { get; set; }
        public string FolderName { get; set; }
        public string SubFolderName { get; set; }
        public int ClientID { get; set; }
    }
}
