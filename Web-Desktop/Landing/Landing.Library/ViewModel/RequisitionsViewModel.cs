using Landing.Library.Model;
using System.Collections.Generic;

namespace Landing.Library.ViewModel
{
    public class RequisitionsViewModel
    {
        public string Total { get; set; }
        public string CurrentTotal { get; set; }
        public List<Requisition> RequisitionsList { get; set; }
        public List<RequisitionStatus> StatusesList { get; set; }
    }
}
