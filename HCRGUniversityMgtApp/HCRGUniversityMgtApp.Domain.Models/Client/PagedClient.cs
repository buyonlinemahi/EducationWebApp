using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Client
{
    public class PagedClient
    {      
        public IEnumerable<Client> Clients { get; set; }
        public Client Client { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
