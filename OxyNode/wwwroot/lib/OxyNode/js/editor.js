//images creator

// Проверка загрузки изменений с ноута

//-------------------------- news creator-------------------------------------
// edit_panel

var movable = false;        // флаг перемещения

document.onclick = function(){
    // let target = event.target;
    if (event.target.classList[1] === "file_manager_active") {
        document.querySelector(".file_manager").classList.remove("file_manager_active");
    }
    else if ((event.target.tagName != "IMG") && (event.target.classList[1] != "up_block") && (event.target.classList[1] != "down_block")){
        // console.log(event.target.classList[1]);
        focus_img();
    }
}

function create_edit_panel(){
    let edit_panel_dom = document.createElement("div");
    edit_panel_dom.classList = "edit_panel";
    edit_panel_dom.setAttribute("onmousedown", "return false");
    edit_panel_dom.innerHTML = `<div class="edit_panel__move">
    <svg width="24" height="24" viewBox="0 0 24 24">
    <path d="M16,12A2,2 0 0,1 18,10A2,2 0 0,1 20,12A2,2 0 0,1 18,14A2,2 0 0,1 16,12M10,12A2,2 0 0,1 12,10A2,2 0 0,1 14,12A2,2 0 0,1 12,14A2,2 0 0,1 10,12M4,12A2,2 0 0,1 6,10A2,2 0 0,1 8,12A2,2 0 0,1 6,14A2,2 0 0,1 4,12Z" /></svg>
    </div>
    <button class="edit_panel__button add_h3" type="button">Добавить заголовок</button>
    <button class="edit_panel__button add_h4" type="button">Добавить подзаголовок</button>
    <button class="edit_panel__button add_p" type="button">Добавить параграф</button>
    <button class="edit_panel__button add_img" type="button">Добавить картинку</button>
    <button class="edit_panel__button add_video" type="button">Добавить видео</button>
    <button class="edit_panel__button add_author" type="button">Добавить автора</button>
    <button class="edit_panel__button add_date" type="button">Добавить дату</button>
    <button class="edit_panel__button delete_block" type="button">Удалить блок</button>
    <button class="edit_panel__button up_block" type="button">Блок вверх</button>
    <button class="edit_panel__button down_block" type="button">Блок вниз</button>`;

    let news_section = document.querySelector(".news");
    news_section.insertBefore(edit_panel_dom, news_section.firstChild);

    document.querySelector(".add_h3").addEventListener('click', add_h3);
    document.querySelector(".add_h4").addEventListener('click', add_h4);
    document.querySelector(".add_p").addEventListener('click', add_p);
    document.querySelector(".add_img").addEventListener('click', add_img);
    document.querySelector(".add_video").addEventListener('click', add_video);
    document.querySelector(".delete_block").addEventListener('click', delete_block);
    document.querySelector(".up_block").addEventListener('click', up_block);
    document.querySelector(".down_block").addEventListener('click', down_block);

    let edit_panel = document.querySelector(".edit_panel");
    let edit_panel__move = document.querySelector(".edit_panel__move");
    
    let edit_panel_x = 0,   // положение панели редактирования по Х
    edit_panel_y = 0,       // положение панели редактирования по У
    edit_panel_w = 0,       // ширина панели редактирования в рх
    move_start_x = 0,       // координата начала перемещения по Х
    move_start_y = 0,       // координата начала перемещения по У
    movable = false;        // флаг перемещения
    
    edit_panel__move.addEventListener('mousedown', movestart);  // по нажатию левой кнопки начало перемещения
    edit_panel__move.addEventListener('mouseup', moveend);      // по отпусканию левой кнопки конец перечещения
    window.addEventListener('mousemove', move);                 // перемещение мыши в окне
    
    edit_panel_x = edit_panel.getBoundingClientRect().left;     // чтение коорлинаты панели редактирования по Х
    edit_panel_w = edit_panel.getBoundingClientRect().width;    // чтение координаты панели редактирования по У
    
    if (edit_panel_x < edit_panel_w + 10)    // если координата по Х меньше ширины панели
    edit_panel.style.left = "0px";  // то не двигать панель
    else                                                            // иначе
    edit_panel.style.left = edit_panel_x - edit_panel_w - 10 +"px";  // сдвинуть панель влево на ширину панели
}

function movestart(){
    let edit_panel = document.querySelector(".edit_panel");
    movable = true;               // установка флага перемещения панели
    move_start_x = event.clientX; // запомнить координату начала перемещения по Х
    move_start_y = event.clientY; // запомнить координату начала перемещения по У
    edit_panel_x = edit_panel.getBoundingClientRect().left;   // запомнить исходное положение панели редактирования по Х
    edit_panel_y = edit_panel.getBoundingClientRect().top;    // запомнить исходное положение панели редактирования по У
}

function moveend(){
    movable = false;  // сброс флага перемещения панели
}

function move (){
    let edit_panel = document.querySelector(".edit_panel");
    if (movable) {    // если флаг перемещения установлен
        edit_panel.style.left = (edit_panel_x - (move_start_x - event.clientX)) + "px"; // сдвинуть панель редактирования по Х
        edit_panel.style.top = (edit_panel_y - (move_start_y - event.clientY)) + "px";  // сдвинуть панель редактирования по У
    }
}



// news fields

document.querySelector(".edit_button").addEventListener('click', edit_all)
document.querySelector(".save_button").addEventListener('click', save_all);

const edit_btn = document.querySelector(".edit_button");

function add_h3(){
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__h3 news__item";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.placeholder = "Заголовок";
    new_textarea.name = "h3";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}

function add_h4(){
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__h4 news__item";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.placeholder = "Подзаголовок";
    new_textarea.name = "h4";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}

function add_p(){
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__p news__item";   // с классом параграфа
    new_textarea.placeholder = "Параграф";
    new_textarea.name = "p";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}

function add_img(){
    let imgs_body = document.querySelector(".file_manager__body");
    imgs_body.innerHTML = "";
    //----------------------------------Женек, раскоменть!!!
    
    let request = new XMLHttpRequest();
    request.open("GET", "/admin/ImageManager/GetAllImages", false);
    request.send();
    let sources = JSON.parse(request.response);
    
    //----------------------------------до сюда



    for (src of sources){
        let new_img_wrapper = document.createElement("div");
        new_img_wrapper.classList = "file_manager__image";
        imgs_body.appendChild(new_img_wrapper);
        let new_img = document.createElement("img");
        new_img.src = src;
        new_img_wrapper.appendChild(new_img);
        new_img.addEventListener("click", append_img);
    }
    let file_manager = document.querySelector(".file_manager");
    file_manager.classList.add("file_manager_active");
}

function append_img(){
    let news_field = document.querySelector(".news_field");
    let news_img_wrapper = document.createElement("div");
    news_img_wrapper.classList = "news_img_wrapper";
    news_field.appendChild(news_img_wrapper);
    let news_img = document.createElement("img");
    news_img.src = this.src;
    news_img.addEventListener('click', focus_img);
    news_img_wrapper.appendChild(news_img);
}

function add_video(){
    let new_iframe = document.createElement("iframe");
}

function focus_img(){
    let focused_imgs = document.querySelectorAll(".focused_img");
    for (let item of focused_imgs){
        item.classList.remove("focused_img");
    }
    if (this.tagName == "IMG"){
        this.parentElement.classList += " focused_img";
        let old_div = document.querySelector(".news__div");             // получить старый скрытый блок
        if (old_div != null){   // если он получен
            old_div.remove();   // то удалить его
        }
    }
}

function choose_block(){
    let focused_item = document.querySelector(".news__item:focus");
    if (focused_item == null){
        focused_item = document.querySelector(".focused_img");
    }
    return focused_item;
}

function delete_block(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item == null){              // если нет блока с фокусом
        alert("Выбери блок для удаления")   // предупреждение
    }
    else {                      // иначе
        focused_item.remove();  // удалить блок с фокусом
        let old_div = document.querySelector(".news__div"); // получить скрытый блок для блока с фокусом
        if (old_div != null){   // если он есть
            if (old_div.previousElementSibling != null) // если есть предыдущий блок
                old_div.previousElementSibling.focus(); // то уставновить на него фокус
            old_div.remove();   // то удалить его
        }
    }
}

function up_block(){
    let focused_item = choose_block();
    if (focused_item == null)
        alert("Блок не выбран");
    else {
        let parent_elem = focused_item.parentElement;
        let prev_elem = focused_item.previousElementSibling;
        if (prev_elem != null){
            parent_elem.insertBefore(focused_item, prev_elem);
        }
        
        // if (focused_item.classList[0] == "news_img_wrapper"){
        //     // console.log(focused_item.classList[0]);
        //     focused_item.classList += " focused_img";
        // }
        // else
            focused_item.focus();
    }
}

function down_block(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item == null)
    alert("Блок не выбран");
    else {
        let parent_elem = focused_item.parentElement;
        if (focused_item.nextElementSibling != null){
            let next_elem = focused_item.nextElementSibling.nextElementSibling;
            parent_elem.insertBefore(focused_item, next_elem);
            focused_item.focus();
        }
    }
}

function new_div(){
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let old_div = document.querySelector(".news__div");             // получить старый скрытый блок
    if (old_div != null){   // если он получен
        old_div.remove();   // то удалить его
    }
    let main_class = this.classList[0]; // получить класс для нового скрытого блока (первый в списке классов блока textarea с фокусом)
    let new_div = document.createElement('div');    // создать новый скрытый блок
    new_div.classList = main_class + " news__div ";  // с собственным классом и таким же классом как у teaxtarea с фокусом
    new_div.innerText = this.value; // новому скрытому блоку присвоить значение текста teaxtarea с фокусом
    news_field.append(new_div);     // вставить новый скрытый блок в поле новости
}


function write(){   // при вводе в textarea
    let news_div = document.querySelector(".news__div");    // получить скрытый блок
    let value = this.value; // получить текст из textarea
    let new_value = "";     // новая строка для записи
    for (let i of value){
        if (i == " "){
            // new_value += "&nbsp";
            new_value += " ";
        }
        else{
            new_value += i;
        }
    }
    news_div.innerText = new_value;

    if ((this.value.endsWith("\n")) | (this.value.endsWith("\n "))) // если нажат enter или enter и пробел 
        news_div.innerText = new_value + "<br>";  // то в конец строки добавить \n (чтобы не было пустой строки)
    else                                // иначе
        news_div.innerText = new_value; // вставить новый текст в скрытый блок
    
    let div_height = news_div.getBoundingClientRect().height;   // получить высоту скрытого блока
    
    if (div_height > 0){                        // если высота больше 0
        this.style.height = div_height + "px";  // то присвоить такую же высоту для textarea
    }
    else {                          // иначе
        this.style.height = null;   // сбросить стиль высоты для textarea
    }
}

document.querySelector(".file_manager__close").addEventListener('click', function(){
    document.querySelector(".file_manager").classList.remove("file_manager_active");
});



function edit_all(){
    create_edit_panel();
    edit_btn.setAttribute("disabled", "true");

    const news_section = document.querySelector(".news");
    const saved_news = document.querySelector(".saved_news");
    let news_field = document.createElement("div");

    news_field.classList = "news_field";
    news_section.append(news_field);

    if (saved_news != null){
        const news_items = saved_news.childNodes;
        for (item of news_items){
            if (item.tagName == "H3"){
                let new_item = document.createElement("textarea");
                new_item.classList = item.classList;
                new_item.value = item.innerText;
                new_item.name = "h3";
                new_item.style.height = item.getBoundingClientRect().height + "px";
                new_item.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
                new_item.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea           
                news_field.append(new_item);
            }
            else if (item.tagName == "H4"){
                let new_item = document.createElement("textarea");
                new_item.classList = item.classList;
                new_item.value = item.innerText;
                new_item.name = "h4";
                new_item.style.height = item.getBoundingClientRect().height + "px";
                new_item.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
                new_item.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
                news_field.append(new_item);
            }
            else if (item.tagName == "P"){
                let new_item = document.createElement("textarea");
                new_item.classList = item.classList;
                new_item.value = item.innerText;
                new_item.name = "p";
                new_item.style.height = item.getBoundingClientRect().height + "px";
                new_item.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
                new_item.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
                news_field.append(new_item);
            }
            else if (item.classList == "news_img_wrapper"){
                let new_item = item.cloneNode(true);
                new_item.firstChild.addEventListener('click', focus_img);
                news_field.append(new_item);
            }
        }

        saved_news.remove();

    }

}

function preview(){
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let edit_panel = document.querySelector(".edit_panel");
    edit_panel.remove();

    if (document.querySelector(".news__div") != null)
        document.querySelector(".news__div").remove();

    const news_section = document.querySelector(".news");
    let saved_news = document.createElement("div");
    saved_news.classList = "saved_news";
    
    const news_items = news_field.childNodes;
    for (item of news_items){
        if (item.name == "h3"){
            let new_item = document.createElement("h3");
            new_item.classList = item.classList;
            new_item.innerHTML = norm_string(item.value);
            saved_news.append(new_item);
        }
        else if (item.name == "h4"){
            let new_item = document.createElement("h4");
            new_item.classList = item.classList;
            new_item.innerHTML = norm_string(item.value);
            saved_news.append(new_item);
        }
        else if (item.name == "p"){
            let new_item = document.createElement("p");
            new_item.classList = item.classList;
            new_item.innerHTML = norm_string(item.value);
            saved_news.append(new_item);
        }
        else if (item.classList[0] == "news_img_wrapper"){
            let new_item = item.cloneNode(true);
            saved_news.append(new_item);
        }
    }
    news_field.remove();
    news_section.append(saved_news);

    edit_btn.removeAttribute("disabled");
}

function norm_string(str){
    let new_str = "";
    for (let i of str){
        if (i == " "){
            new_str += "&nbsp";
        }
        else if (i == "\n"){
            new_str += "<br>";
        }
        else{
            new_str += i;
        }
    }
    return new_str;
}

function save_all(){
    preview();
    let saved_news = document.querySelector(".saved_news");
    document.getElementById("formContent").value = saved_news.innerHTML;
    // console.log(document.getElementById("formContent").value);
}



//----------------------------------Женек, раскоменть!!!

document.getElementById("loadImageButton").addEventListener("change", sendRequestImage);

// обработчик выбора файла
function sendRequestImage(event) {
    var form = document.getElementById("form_forImgLoad");

    event.preventDefault();

    var formData = new FormData(form);

    var request = new XMLHttpRequest();
    request.open("POST", form.action);

    request.send(formData);

    request.onload = function () {
        add_img();
    }
}
//----------------------------------до сюда




// window.onbeforeunload = function() {
//     return "Есть несохранённые изменения. Всё равно уходим?";
// };
