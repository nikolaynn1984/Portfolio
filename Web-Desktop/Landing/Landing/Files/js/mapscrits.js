document.addEventListener('DOMContentLoaded', function () {
    SetMapParamm();
    mapCart();
    
})
var coordinates = [55.755819, 37.617644];
var mapcolor = '#f23d30';
/**Получаем данные контактов */
function SetMapParamm() {
    fetch('/Home/ContactsMap').then(responce => {
        if (responce.ok) {
            return responce.json();
        }
    }).then(contacts => {
        coordinates = contacts.coordinates.split(',');
        mapcolor = contacts.labelColor;
        let blok = document.querySelector('.contakts-info');

        let value = contacts.contactInfo.split("\r\n");
        let text = '';
        for (let i = 0; i < value.length; i++) {
            if (value[i] === '') value.splice(i, 1);

            text = text + value[i] + '</br>'
        }
        blok.innerHTML = text;
    });
}
/**Яндекс карта */
var mapCart = function () {
    ymaps.ready(init);
    function init() {
        // Создание карты.
        var myMap = new ymaps.Map(map, {
            center: coordinates,
            zoom: 15, controls: []
        }, { autoFitToViewport: 'always' });

        var myPlacemark = new ymaps.Placemark(coordinates, {}, {
            iconLayout: 'islands#dotIcon',
            iconColor: mapcolor,
            iconImageSize: [45, 60],
            iconImageOffset: [-3, -42]
        });
        myMap.geoObjects.add(myPlacemark);

    }
}
