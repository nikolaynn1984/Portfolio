using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using AccountData.DataWork;

namespace AccountData.History
{
    public class HIstoryRepository
    {
        internal static List<OperationHistory> OperationHistories;
        internal static List<Operations> OperationsList;

        public HIstoryRepository()
        {
            OperationsList = new List<Operations>();
            OperationHistories = new List<OperationHistory>();
            LoadingData.LoadOperation();
            LoadingData.LoadHistory();
        }
        /// <summary>
        /// Загрузка истории
        /// </summary>
        /// <returns></returns>
        public List<OperationHistory> LoadHistory() { return OperationHistories; }

        internal static string GetOperationNameId(int id)
        {
            string name = OperationsList.Single(s => s.Id == id)?.OperationName ?? null;
            return name;
        }

        /// <summary>
        /// Добавление истории
        /// </summary>
        /// <param name="history"></param>
        public void AddHistory(OperationHistory history)
        {

            if (history != null)
            {
                history.OperationDate = DateTime.Now;
                OperationHistories.Add(history);
                AddData.AddHistory(history);
            }
        }
       
    }
}
