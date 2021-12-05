using System;

namespace Landing.Library.Interfaces
{
    public interface IRequisition
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор Статуса
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Дата заявки
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Номер заявки
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Согласен на обработку персональных данных
        /// </summary>
        public bool Agree { get; set; }
        /// <summary>
        /// Описание заявки
        /// </summary>
        public string Description { get; set; }
    }
}
