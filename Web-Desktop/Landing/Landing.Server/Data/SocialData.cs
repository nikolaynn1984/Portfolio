using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Landing.Library.Interfaces;
using Landing.Library.Model;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    /// <summary>
    /// Обработка данных соц.юетей
    /// </summary>
    public class SocialData : ISocial
    {
        private readonly BaseImages baseImages;
        private readonly BaseSocials baseSocial;
        public SocialData()
        {
            this.baseSocial = new BaseSocials(Connection.GetConnectionString());
            this.baseImages = new BaseImages(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Social>> GetItems()
        {
            List<Social> list = await baseSocial.GetItems();
            List<Images> images = await baseImages.GetItemByLocation("Social");

            foreach (var item in list)
            {
                if(item.ImageId != 0)
                {
                    item.GetImages = images.SingleOrDefault(i => i.Id == item.ImageId);
                }
                
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Social> GetItemById(int id)
        {
            Social item = await baseSocial.GetItemById(id);
            if (item != null)
            {
                if(item.ImageId != 0)
                {
                    item.GetImages = await baseImages.GetItemById(item.ImageId);
                }
                
            }
            return item;
        }


        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="social">Объект Social </param>
        /// <returns>Объект Social с заданым идентификатором</returns>
        public async Task<Social> Add(Social social)
        {
            try
            {
                if(social.ImageId != 0)
                {
                    social.GetImages = await baseImages.GetItemById(social.ImageId);
                }
                social = await baseSocial.Add(social);
            }
            catch
            {
                social = null;
            }
            return social;
        }
        /// <summary>
        /// Удаление записи 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            bool result = true;
            string error = "Ok";
            try
            {
                var item = await baseSocial.Remove(id);
                if (!item)
                {
                    return (Result: false, Error: "Идентификатор не найден");
                }

            }
            catch
            {
                result = false;
                error = "Что то пошло не так";
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="social">Объект Social с обновлеными даными</param>
        /// <returns>Social если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Social> Edit(Social social)
        {
            try
            {
                if(social.ImageId != 0)
                {
                    social.GetImages = await baseImages.GetItemById(social.ImageId);
                }
                Social item = await baseSocial.Edit(social);

                return social;
            }
            catch
            {
                return null;
            }
        }


    }
}
