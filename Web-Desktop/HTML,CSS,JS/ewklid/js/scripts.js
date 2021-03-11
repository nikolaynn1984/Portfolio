document.addEventListener('DOMContentLoaded', function() {  //Отлавливаем событие - загрузка всей страницы
  tabClick();
  menuClick();
  swiperImage();
  accordionIndex();
  accordionIndexClick();
})

var tabClick = function(){
  document.querySelectorAll('.howwork-list-link').forEach(tabBtn =>{
    tabBtn.addEventListener('click', event =>{  //Отлавливаем нажатие кнопки
      const path = event.currentTarget.dataset.path;

      document.querySelectorAll('.howwork-tab-block').forEach(tabContent =>{  //Удаляем класс tab-content-active у всех елементов
        tabContent.classList.remove('tab-content-active')
      })
      document.querySelector(`[data-target="${path}"]`).classList.add('tab-content-active')  //добавляем класс tab-content-active выбранному элементу
    })
  })
}
var menuClick = function(){
  document.getElementById('burger').addEventListener('click', event =>{
    document.getElementById('nav').classList.toggle('nav-active')
    document.getElementById('nav').classList.remove('nav-visible')
  })
  document.getElementById('close-menu').addEventListener('click', event =>{
    document.getElementById('nav').classList.remove('nav-active');
    setTimeout(function(){document.getElementById('nav').classList.add('nav-visible');},200)

  })
}
$( function() {
  $( "#accordion" ).accordion({
    collapsible: true,
    icons: false,
    active: false,
    heightStyle: false,
    clearStyle: false

  });
  $('button').button();
} );

var accordionIndex = function(){
  var index = 16;
  document.querySelectorAll('.ui-accordion-header').forEach(tabIndex =>{
    $(`#${tabIndex.id}`).attr(`tabindex`,index);
    index = index + 1;

  })
}
var accordionIndexClick = function(){
  document.querySelectorAll('.ui-accordion-header').forEach(tabIndex =>{
    tabIndex.addEventListener('click', tabEvent =>{
      accordionIndex();
    })
    tabIndex.addEventListener('keydown', tabEvent =>{
      accordionIndex();
    })

  })

}
var swiperImage = function(){
  var swiper = new Swiper('.swiper-container', {
    spaceBetween: 30,
    autoplay: {
      delay: 2500,
      disableOnInteraction: false,
    },
    loop:true,
    effect: 'fade',
    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },
    navigation: {
      nextEl: false,
      prevEl: false,
    },
  });

}


