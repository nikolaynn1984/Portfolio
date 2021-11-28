using Landing.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Model.ViewModel
{
    public class RequisitionsViewModel
    {
        public string Total { get; set; }
        public string CurrentTotal { get; set; }
        public List<Requisition> RequisitionsList { get; set; }
        public List<RequisitionStatus> StatusesList { get; set; }
    }
}
