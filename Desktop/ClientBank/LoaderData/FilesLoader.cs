using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoaderData
{

        public class FilesLoader
        {
        

            /// <summary>
            /// Загрузка данных 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="model">Модель данных</param>
            /// <returns>Модель данных</returns>
            public static ObservableCollection<T> GetItems<T>(ObservableCollection<T> model)
            {
                string path = PathModel(model);
                if (path != null) return FileSerialize.Deserialize(model, path);
                else return null;
            }
            /// <summary>
            /// Сохранение данных
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="model">Модель данных</param>
            public static void SaveItems<T>(ObservableCollection<T> model)
            {
                string path = PathModel(model);
                if (path != null) FileSerialize.Serialize(model, path);
            }


            /// <summary>
            /// Метод определения модели данных и название таблицы
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="model">Модель данных</param>
            /// <returns>Название таблицы</returns>
            private static string PathModel<T>(ObservableCollection<T> model)
            {
                model = new ObservableCollection<T>();
                string result = string.Empty;

               
                switch (model)
                {
                    case ObservableCollection<Personal> item: result = "Personal"; break; // Обычные счета
                    case ObservableCollection<Accounts> item: result = "BankAccount"; break; // Все счета
                    case ObservableCollection<Cities> item: result = "Cities"; break; // Города
                    case ObservableCollection<Customers> item: result = "Customers"; break; //Клиенты
                    case ObservableCollection<Busines> item: result = "Business"; break; // Юр.лица - клиенты
                    case ObservableCollection<PhysicalClient> item: result = "PhysicalClient"; break; // Физ.лица - клиенты
                    case ObservableCollection<AccountType> item: result = "AccountType"; break; //Тип счета
                    case ObservableCollection<Operations> item: result = "Operations"; break; // Виды операций
                    case ObservableCollection<Repayment> item: result = "Repayment"; break; // Погашения кредитов
                    case ObservableCollection<Deposit> item: result = "Deposit"; break; // Вклады
                    case ObservableCollection<Credits> item: result = "Credit"; break; //Кредиты
                    case ObservableCollection<Statuses> item: result = "Statuses"; break; //Статусы клиентов
                    case ObservableCollection<OperationHistory> item: result = "OperationHIstory"; break; // История операций
                }

                return result;
            }

        }
}
