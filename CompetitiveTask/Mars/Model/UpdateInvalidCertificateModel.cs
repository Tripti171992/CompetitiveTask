using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.Model
{
    public class UpdateInvalidCertificateModel
    {
        public string oldCertificate { get; set; }
        public string newCertificate { get; set; }
        public string newCertifiedFrom { get; set; }
        public string newYear { get; set; }

    }
}
