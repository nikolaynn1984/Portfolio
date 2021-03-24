document.addEventListener('DOMContentLoaded', function () {  //Отлавливаем событие - загрузка всей страницы
    tabList();
    checkTab();
})
var folderId = 0;
var fileId = 0;
var parentid = 0;

var tabList = function () {
    document.querySelectorAll('.openfiles-link').forEach(tabBtn => {
        tabBtn.addEventListener('click', event => {  
            const path = event.currentTarget.dataset.path;

            document.querySelectorAll('.file-content').forEach(tabContent => {  
                tabContent.classList.remove('tab-content-active')
            })
            document.querySelectorAll('.openfiles-item').forEach(tabContent => {
                tabContent.classList.remove('is-tab-active')
            })
            document.querySelector(`[data-target="${path}"]`).classList.add('tab-content-active') 
            var btn = document.querySelector(`[data-path="${path}"]`);
            btn.parentElement.classList.add('is-tab-active') 
            console.log(path);
        })
    })
}




$('body').on('click', '.close-file', function (e) {
    var item = $(e.currentTarget).data().close
    removeTab(item);
});

var removeTab = function (item) {
    console.log(item)
    $('[data-path="' + item + '"] ').parent('.openfiles-item').remove();
    $('[data-target="' + item + '"] ').remove();
    checkTab();
}

$('body').on('click', '.item-folder', function (e) {
    $(this).toggleClass('is-open');
    $(this).toggleClass('is-close');
    const folderEvent = jQuery(e.currentTarget).data().folder;
    folderId = folderEvent;
    fileId = 0;
});
$('body').on('click', '.item-file', function (e) {
    const fileEvent = jQuery(e.currentTarget).data().file;
    fileId = fileEvent;
    $(".download-btn").attr("href", "/Home/DowloadFile/" + fileId)
});
$('body').on('dblclick', '.item-file', function (e) {
    const fileEvent = jQuery(e.currentTarget).data().file;
    fileId = fileEvent;
    var findResult = true;
    var tab = 'tab-' + fileEvent;
    $('.openfiles-link').each(function () {
        var path = $(this).attr('data-path');
        if (path == tab) { findResult = false;}
    });

    document.querySelectorAll('.file-content').forEach(tabContent => {
        tabContent.classList.remove('tab-content-active')
    })
    document.querySelectorAll('.openfiles-item').forEach(tabContent => {
        tabContent.classList.remove('is-tab-active')
    })
    if (findResult) {
        e.preventDefault();
        $.ajax({
            url: "Files/Get", // Получение данных по идентификатору
            type: "GET",
            data: { 'id': fileId },
            success: function (response) {
                if (response == null) {
                    console.log("Что то пошло не так");
                }
                else {
                    
                    $('.openfiles-list').append('<li class="openfiles-item is-tab-active">' +
                        ' <button class="openfiles-link " data-path="tab-' + response.Id + '">' + response.Name + '</button><button class="close-file" data-close="tab-' + response.Id + '"></button>' +
                        '</li > ');
                    $('.content-box').append('<div class="file-content  tab-content-active" data-target="tab-' + response.Id + '">' +
                        '<textarea name="content"  class="content-text">' + response.Content + ' </textarea>' +
                        '</div > ');
                    
                    
                    tabList();
                    checkTab();
                }
            },
        });
    } else {
        
        $('[data-target="tab-' + fileId + '"] ').addClass('tab-content-active');
        document.querySelector(`[data-path="tab-${fileId}"]`).parentElement.classList.add('is-tab-active') 
    }
    
});
var removeFolder = function () {
    if (folderId != 0) {
        var folder = document.querySelector(`[data-folder="${folderId}"]`);
        result = confirm('Удалить папку - ' + folder.textContent + '?');
        if (result) {
                $.ajax({
                    url: "Home/Delete",  //Удаление папки
                    type: "POST",
                    data: { 'id': folderId },
                    success: function (response) {
                        if (response == true) {
                            $(folder).parent('.files-item').remove()
                        }
                    },
                });
        }

    } else {
        alert('Папка не выбрана');
    }
}
var removeFile = function () {
    if (fileId != 0) {
        var file = document.querySelector(`[data-file="${fileId}"]`);
        result = confirm('Удалить фаил - ' + file.textContent + '?');
        if (result) {
            $.ajax({
                url: "Files/Delete",  //Удаление файла
                type: "POST",
                data: { 'id': fileId },
                success: function (response) {
                    if (response == true) {
                        file.remove()
                        removeTab('tab-' + fileId);
                    }
                },
            });
            
        }

    } else {
        alert('Фаил не выбран');
    }
}
var checkTab = function () {
    var count = 0;
    var nullcount = 0;
    document.querySelectorAll('.openfiles-item').forEach(tabitems => { count++;  })
    document.querySelectorAll('.null-item').forEach(tabitems => {  nullcount++; })
    if (count == 0 && nullcount == 0) {
        $('.openfiles-list').append('<li class="null-item">Выберите фаил для просмотра</li > ');
    }
    if(count == 1) {
        $(`.null-item`).remove();
    }
}

var editFilderClick = function () {
    document.querySelector('.edit-box-folder')
}
$('body').on('click', '.tree-box', function (event) {
    if (event.target == this) {
        folderId = 0;
        fileId = 0;
        console.log("Папка " + folderId + " Фаил " + fileId)
    }

});
$('body').on('click', '.edit-box-folder', function (event) {
    if (event.target == this) {
        $(this).removeClass('is-visible');
    }

});
$('body').on('click', '.edit-box-file', function (event) {
    if (event.target == this) {
        $(this).removeClass('is-visible');
    }
});
$('body').on('click', '.load-box-file', function (event) {
    if (event.target == this) {
        $(this).removeClass('is-visible');
    }
});

var addFolder = function () {
    document.querySelector('.edit-box-folder').classList.add('is-visible');
    parentid = folderId;
    folderId = 0;
}
var loaderView = function () {
    document.querySelector('.load-box-file').classList.add('is-visible');
}
var editFile = function () {
    if (fileId != 0) {
        document.querySelector('.edit-box-file').classList.add('is-visible');
    } else if (fileId == 0 && folderId != 0) {
        document.querySelector('.edit-box-folder').classList.add('is-visible');
    } else alert('Не выбран не один объект')


}

$('#EditFolderForm').submit(function (e) {
    var name = document.querySelector('#EditFolderForm').name.value;
    e.preventDefault();
    $.ajax({
        url: "Home/Edit", //Переименовывание/добавление папки
        type: "POST",
        data: { 'id': folderId, 'name': name, 'parentid': parentid },
        success: function (response) {
            addItemFolder(response);
            document.querySelector('.edit-box-folder').classList.remove('is-visible');
        },
    });

});
$('#EditFileForm').submit(function (e) {
    var name = document.querySelector('#EditFileForm').filename.value;
    var file = document.querySelector(`[data-file="${fileId}"]`);
    e.preventDefault();
    $.ajax({
        url: "Files/Edit", //Переименовывание файла
        type: "POST",
        data: { 'id': fileId, 'name': name},
        success: function (response) {
            if (response) {
                file.innerHTML = '<span class="file-icon" style="background-image: url(' + "../Content/icons/tree/default.png" + ');"></span>' + name;
                document.querySelector('.edit-box-file').classList.remove('is-visible');
            }
        },
    });

});
$('#LoadFileForm').submit(function (e) {
    
        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
    document.querySelector('.load-box-file').classList.remove('is-visible');
        var fileData = new FormData();
        fileData.append(files[0].name, files[0]);
    var urlfile = "/Files/Upload/" + folderId;  //Загрузка файла на сервер
    e.preventDefault();
    $.ajax({
        url: urlfile,
        type: 'POST',
        datatype: 'json',
        contentType: false,
        processData: false,
        async:true,
        data: fileData,
        success: function (responce) {
            if (responce == null) {
                console.log(responce.error);
            } else {

                if (responce.FolderId != 0) {
                    var parent = document.querySelector(`[data-folder="${responce.FolderId}"]`);
                    $(parent).after('<li class= "files-item">' +
                        ' <button data-file="' + responce.Id + '" class="files-btn item-file"><span class="file-icon" style="background-image: url(../Content/icons/tree/' + responce.Type.Icon + ');"></span>' + responce.Name + '</button>' +
                        '</li > ');
                }
                else {
                    $('.tree-box').prependTo('<ul class="files-list">' +
                        '<li class= "files-item">' +
                        ' <button data-file="' + responce.Id + '" class="files-btn item-file"><span class="file-icon" style="background-image: url(../Content/icons/tree/' + responce.Type.Icon + ');"></span>' + responce.Name + '</button>' +
                        '</li > ' +
                        ' </ul >');
                }
            }
        }
    });

});
function addItemFolder(response) {
    if (response != false) {
        var parent = document.querySelector(`[data-folder="${response.ParentId}"]`);
        var folder = document.querySelector(`[data-folder="${response.Id}"]`);
        if (folder == null) {
            if (parent != null) {
                $(parent).after('<ul class="files-list">' +
                    '<li class= "files-item">' +
                    '<button data-folder="' + response.Id + '" class= "files-btn item-folder is-close"><span class="folder-icon" "></span>' + response.Name + '</button>' +
                    '</li > ' +
                    ' </ul >');
            } else {
                $('.tree-box').prependTo('<ul class="files-list">' +
                    '<li class= "files-item">' +
                    '<button data-folder="' + response.Id + '" class= "files-btn item-folder is-close"><span class="folder-icon" "></span>' + response.Name + '</button>' +
                    '</li > ' +
                    ' </ul >');
            }
        } else {
            folder.innerHTML = '<span class="folder-icon"></span>' +   response.Name;
        }
       
        
    }

    
}
