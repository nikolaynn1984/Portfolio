function MenuButton(x) {
    x.classList.toggle("change");
    var slider = document.getElementById('mySidenav');
    if (slider.style.height == '100%') {
        slider.style.height = '0';
    } else {
        slider.style.height = '100%';
    }
}

function openTab(evn, tabName) {
    var i, tabcontent, tablink;
    tabcontent = document.getElementsByClassName('tabcontent');
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = 'none';
    }
    tablink = document.getElementsByClassName('work-tab-item');
    for (i = 0; i < tablink.length; i++) {
        tablink[i].className = tablink[i].className.replace(' active', '');
    }
    document.getElementById(tabName).style.display = 'block';
    evn.currentTarget.className += ' active';
}

function searchRide() {
    var selectFrom = document.getElementById('fromSch');
    let form = document.getElementById('searchForm');
    var selectTo = document.getElementById('toSch');
    var erroMsg = document.getElementById('error-popur');
    if (selectFrom.value == 0) {
        erroMsg.textContent = 'Не выбран путь Откуда';

    } else if (selectFrom.value == selectTo.value) {
        erroMsg.textContent = 'Поездки осуществляются только меж городами!';
    } else {
        form.action = '/Offers/Index';
        form.method = 'Post';
        form.submit();
    }
    console.log(erroMsg.textContent);

}
