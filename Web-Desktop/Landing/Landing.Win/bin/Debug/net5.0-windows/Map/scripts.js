document.addEventListener('DOMContentLoaded', function(){ SizeMap(); }) 
 var coordinates = [55.755819,37.617644]; var mapcolor = '#f23d30'; 
 window.SetCoordinate = function (item) {  var coordinat = item.Coordinates.split(',');   coordinates = coordinat; mapcolor = item.LabelColor; mapCart(); } 
 window.onresize = function(){  SizeMap(); } 
 window.OpenEdit = function(){  var menu = document.getElementById('menu'); if (menu.style.display == 'flex') { menu.style.display = 'none'  } else {  menu.style.display = 'flex' }  } 
 window.SizeMap = function(){ var width =  window.innerWidth; var heigt = document.documentElement.clientHeight; var mapel = document.getElementById('map');  mapel.style.width = width+'px'; mapel.style.height = heigt+'px';} 
 var mapCart = function(){ 
 ymaps.ready(init); function init(){ 
 var myMap = new ymaps.Map(map, {  center: coordinates, zoom: 15, controls: [] },{ autoFitToViewport: 'always' }); 
 var myPlacemark = new ymaps.Placemark(coordinates, {}, { iconLayout: 'islands#dotIcon', iconColor: mapcolor, iconImageSize: [45, 60], iconImageOffset: [-3, -42] }); 
 var inpucoordinateleft = document.getElementById('coordicateleft'); var inpucoordinateright = document.getElementById('coordicateright'); var inputcolor = document.getElementById('color'); 
 window.SaveSetting = function(){  myPlacemark.geometry.setCoordinates([inpucoordinateleft.value, inpucoordinateright.value]); myMap.setCenter([inpucoordinateleft.value, inpucoordinateright.value]);  myPlacemark.options.set({ iconColor: inputcolor.value, });  } 
 document.getElementById('inputsubmit').onclick = function() { myPlacemark.geometry.setCoordinates([inpucoordinateleft.value, inpucoordinateright.value]);  myMap.setCenter([inpucoordinateleft.value, inpucoordinateright.value]);  myPlacemark.options.set({ iconColor: inputcolor.value,  }); coordinates[0] = inpucoordinateleft.value;  coordinates[1] = inpucoordinateright.value; mapcolor = inputcolor.value;  }; 
 window.EditView = function()  { if (menu.style.display == 'flex') { menu.style.display = 'none'  } else {  menu.style.display = 'flex'; myMap.setCenter([coordinates[0], coordinates[1]]);  inpucoordinateleft.value = coordinates[0];  inpucoordinateright.value = coordinates[1]; inputcolor.value =  mapcolor;  } coordinates[0] = inpucoordinateleft.value; coordinates[1] = inpucoordinateright.value;  mapcolor = inputcolor.value;  var model = {};  model.color = inputcolor.value;  model.coodinatx = inpucoordinateleft.value;  model.coodinaty = inpucoordinateright.value;  var data = JSON.parse(JSON.stringify(model));   return data; };  myMap.geoObjects.add(myPlacemark);  } } 
