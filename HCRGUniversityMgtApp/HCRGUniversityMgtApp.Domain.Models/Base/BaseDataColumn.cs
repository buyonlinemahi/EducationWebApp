using System;

namespace HCRGUniversityMgtApp.Domain.Models.Base
{
    [Serializable]
    public class BaseDataColumn
    {
        public DateTime? ApproveOn { get; set; }
        public int? ApproveBy { get; set; }
    }
}
