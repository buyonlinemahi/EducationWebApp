using HCRGUniversityMgtApp.Domain.Models.Client;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.ClientViewModel
{
    public class ClientViewModel
    {
        public IEnumerable<Client> ClientDetail { get; set; }
    }
}
