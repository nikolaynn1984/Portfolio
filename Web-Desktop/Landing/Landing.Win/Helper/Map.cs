using System.IO;

namespace Landing.Win.Helper
{
    /// <summary>
    /// Карта 
    /// </summary>
    public class Map
    {
        private static string pathindex = Path.Combine(Directory.GetCurrentDirectory(), "Map", "Index.html");
        private static string pathscript = Path.Combine(Directory.GetCurrentDirectory(), "Map", "scripts.js");
        /// <summary>
        /// Проверяет наличие файлов карты, если нет то создает файлы
        /// </summary>
        /// <returns>true</returns>
        public static bool MapСhecking()
        {
            bool resul = true;
            try
            {
                if (!File.Exists(pathindex))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Map"));
                    File.WriteAllText(pathindex, mapindex);
                    File.WriteAllText(pathscript, mapscript);
                }
            }
            catch
            {
                resul = false;
            }
            return resul;
        }


        /// <summary>
        /// Скрип файла js
        /// </summary>
       static string mapscript = "document.addEventListener('DOMContentLoaded', function(){ SizeMap(); }) \n" +
                           " var coordinates = [55.755819,37.617644]; var mapcolor = '#f23d30'; \n" +
                          " window.SetCoordinate = function (item) {  var coordinat = item.Coordinates.split(',');   coordinates = coordinat; mapcolor = item.LabelColor; mapCart(); } \n" +
                           " window.onresize = function(){  SizeMap(); } \n" +
                           " window.OpenEdit = function(){  var menu = document.getElementById('menu'); if (menu.style.display == 'flex') { menu.style.display = 'none'  } else {  menu.style.display = 'flex' }  } \n" +
                           @" window.SizeMap = function(){ var width =  window.innerWidth; var heigt = document.documentElement.clientHeight; var mapel = document.getElementById('map');  mapel.style.width = width+'px'; mapel.style.height = heigt+'px';} "+
                           "\n var mapCart = function(){ \n ymaps.ready(init); function init(){ \n"+
                           " var myMap = new ymaps.Map("+"map"+", {  center: coordinates, zoom: 15, controls: [] },{ autoFitToViewport: 'always' }); \n"+
                          " var myPlacemark = new ymaps.Placemark(coordinates, {}, { iconLayout: 'islands#dotIcon', iconColor: mapcolor, iconImageSize: [45, 60], iconImageOffset: [-3, -42] }); \n"+
                          " var inpucoordinateleft = document.getElementById('coordicateleft'); var inpucoordinateright = document.getElementById('coordicateright'); var inputcolor = document.getElementById('color'); \n" +
                          " window.SaveSetting = function(){  myPlacemark.geometry.setCoordinates([inpucoordinateleft.value, inpucoordinateright.value]); myMap.setCenter([inpucoordinateleft.value, inpucoordinateright.value]);  myPlacemark.options.set({ iconColor: inputcolor.value, });  } \n" +
                          " document.getElementById('inputsubmit').onclick = function() { myPlacemark.geometry.setCoordinates([inpucoordinateleft.value, inpucoordinateright.value]);  myMap.setCenter([inpucoordinateleft.value, inpucoordinateright.value]);  myPlacemark.options.set({ iconColor: inputcolor.value,  }); coordinates[0] = inpucoordinateleft.value;  coordinates[1] = inpucoordinateright.value; mapcolor = inputcolor.value;  }; \n"+
                          " window.EditView = function()  { if (menu.style.display == 'flex') { menu.style.display = 'none'  } else {  menu.style.display = 'flex'; myMap.setCenter([coordinates[0], coordinates[1]]);  inpucoordinateleft.value = coordinates[0];  inpucoordinateright.value = coordinates[1]; inputcolor.value =  mapcolor;  } coordinates[0] = inpucoordinateleft.value; coordinates[1] = inpucoordinateright.value;  mapcolor = inputcolor.value;  var model = {};  model.color = inputcolor.value;  model.coodinatx = inpucoordinateleft.value;  model.coodinaty = inpucoordinateright.value;  var data = JSON.parse(JSON.stringify(model));   return data; };  myMap.geoObjects.add(myPlacemark);  } } \n";
        /// <summary>
        /// Разметка html файла
        /// </summary>
       static string mapindex = @"<!DOCTYPE html><html lang=""ru""><head><meta charset=""utf-8"" />" +
                          "<title>Карта</title>" +
                          "<script src=\"scripts.js\"></script>" +
                          "<style> body{  overflow: hidden; margin: 0; }" +
                          ".menu {  display: none;  flex-direction: column; position: absolute; top: 50%; left: 50%;  width: 250px;  background: white;  border: 1px solid #ccc; padding: 20px;  z-index: 100; }" +
                          ".btn{ margin: 5px; margin-left: auto; border: none; background-color: rgb(31, 133, 228); padding: 5px 10px; color: white; }" +
                          ".input{margin:5px  0; padding: 5px; width: 45%; border: 1px solid rgb(204, 199, 199); outline: none; text-align: center;}" +
                          ".input-box{ display: flex; } </style></head>" +
                           "<body><div id=\"map\"></div>" +
                           "<div id=\"menu\" class=\"menu\" >" +
                           "<label> Координаты:  <div class=\"input-box\">" +
                          "<input id=\"coordicateleft\" class=\"input\" type=\"text\" name=\"icon_text\" /><input id=\"coordicateright\" class=\"input\" type=\"text\" name=\"icon_text\" /></div></label>" +
                          "<label> Цвет: <input id=\"color\" type=\"color\" /></label>" +
                          "<button  id=\"inputsubmit\" class=\"btn\" > Проверить </button></div>" +
                           "<script src=\"https://api-maps.yandex.ru/2.1/?apikey=ваш API-ключ&lang=ru_RU\" type=\"text/javascript\"></script></body></html> ";
    }
}
