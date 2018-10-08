using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntakeApi.Models
{
    public enum DocType
    {
        E837P,
        E835,
        EOB,
        HospitalCharges,
        ClinicCharges,
        ClientQuestion,
        ClientReport
    }

    public class IntakeItem
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string DocumentName { get; set; }
        public DocType DocumentType { get; set; }
        public string ReceiveDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
