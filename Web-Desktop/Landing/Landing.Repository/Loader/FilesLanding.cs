
using Landing.Model.Data;
using Landing.Model.Hash;
using Landing.Repository.Interfase;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Landing.Repository.Loader
{
    class FilesLanding : IFilesLanding
    {
        private HttpClient client;
        private Images ImagesItem;
        public FilesLanding(string server, string token)
        {
            this.client = new HttpClient();
            this.ImagesItem = new Images();
            client.BaseAddress = new Uri(server + "api/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="bytes">Массив файла</param>
        /// <param name="images">Информация о файле</param>
        /// <returns>Информация о файле</returns>
        public async Task<Images> Upload(byte[] bytes, Images images)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                var byteArray = new ByteArrayContent(bytes);
                byteArray.Headers.ContentType = MediaTypeHeaderValue.Parse(images.Extension);

                var responce = client.PostAsync($"Upload/{images.Location}/{images.Description}", new MultipartFormDataContent
                {
                    {byteArray, "\"file\"", images.Name }
                }).Result;

                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Images>(await responce.Content.ReadAsStringAsync());
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }

            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
            finally
            {
                
            }
        }

        /// <summary>
        /// Загрузка файла в локальную папку 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Download(Images images)
        {
            try
            {
                if (string.IsNullOrEmpty(images.Location)) ImagesItem = await GetImage(images.Id);
                else ImagesItem = images;
                var path = Path.Combine("Files", ImagesItem.Location, ImagesItem.Name);
                var folder = Path.Combine("Files", ImagesItem.Location);
                if (!File.Exists(folder)) Directory.CreateDirectory(folder);
                bool result = CheckedFileWithLocal(path);
                if (!result)
                {
                    return await Loader(images.Id, path);
                }
                else
                {
                    return path;
                }
                

               
            }catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// Сохранение файла в папку
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Путь к файлу</returns>
        private async Task<string> Loader(int id, string path)
        {
            var responce = client.GetAsync($"Download/{id}").Result;
            if (responce.IsSuccessStatusCode)
            {
                var item = await client.GetByteArrayAsync($"Download/{id}");
                using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    file.Write(item, 0, (int)ImagesItem.Size);
                }
                return path;
            }
            else
            {
                Failt.ErrorsHandler.Create(responce);
                return null;
            }
        }

        /// <summary>
        /// Проверяет фаил на наличие и сравнивает по ХэшКоду
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>true - если фаил найден и Хэш-код не соответствует, в противном случае false</returns>
        private bool CheckedFileWithLocal(string path)
        {
            bool result = false;
            if(ImagesItem != null)
            {
                if (File.Exists(path))
                {
                    string hash = HaxCodeFile.GetSHA1(path);
                    if (hash == ImagesItem.HashCode)
                    {
                        result = true;
                    }
                }
            }     

            return result;
        }

        /// <summary>
        /// Получение информации о файте
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Информация о файле</returns>
        private async Task<Images> GetImage(int id)
        {
            try
            {
                var respince = await client.GetAsync($"Images/{id}");
                if (respince.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Images>(respince.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(respince);
                    return null;
                }
            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }

 
    }
}
