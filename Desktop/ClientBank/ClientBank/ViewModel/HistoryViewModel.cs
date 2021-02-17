
using AccountData.History;
using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientBank.ViewModel
{
    class HistoryViewModel
    {
        private HIstoryRepository repository;
        private List<OperationHistory> operationsList;
        public HistoryViewModel()
        {
            this.repository = new HIstoryRepository();
            OperationsList = repository.LoadHistory();
        }

        public List<OperationHistory> OperationsList
        {
            get { return this.operationsList; }
            set { this.operationsList = value;  }
        }
    }
}
