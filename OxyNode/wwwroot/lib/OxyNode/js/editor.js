// window.onload = function (){
//-------------------------- news creator-------------------------------------
// edit_panel
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
    edit_panel.style.left = edit_panel_x - edit_panel_w - 10 + "px";  // сдвинуть панель влево на ширину панели

function movestart() {
    movable = true;               // установка флага перемещения панели
    move_start_x = event.clientX; // запомнить координату начала перемещения по Х
    move_start_y = event.clientY; // запомнить координату начала перемещения по У
    edit_panel_x = edit_panel.getBoundingClientRect().left;   // запомнить исходное положение панели редактирования по Х
    edit_panel_y = edit_panel.getBoundingClientRect().top;    // запомнить исходное положение панели редактирования по У
}

function moveend() {
    movable = false;  // сброс флага перемещения панели
}

function move() {
    if (movable) {    // если флаг перемещения установлен
        edit_panel.style.left = (edit_panel_x - (move_start_x - event.clientX)) + "px"; // сдвинуть панель редактирования по Х
        edit_panel.style.top = (edit_panel_y - (move_start_y - event.clientY)) + "px";  // сдвинуть панель редактирования по У
    }
}



// news fields
const news_field = document.querySelector(".news_field");   // поле с новостью

document.querySelector(".add_h3").addEventListener('click', add_h3);
document.querySelector(".add_h4").addEventListener('click', add_h4);
document.querySelector(".add_p").addEventListener('click', add_p);
document.querySelector(".add_img").addEventListener('click', add_img);
document.querySelector(".add_video").addEventListener('click', add_video);
document.querySelector(".delete_block").addEventListener('click', delete_block);
document.querySelector(".up_block").addEventListener('click', up_block);
document.querySelector(".down_block").addEventListener('click', down_block);


function add_h3() {
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__item news__h3";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.placeholder = "Заголовок";
    new_textarea.name = "h3";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}


function add_h4() {
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__item news__h4";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.placeholder = "Подзаголовок";
    new_textarea.name = "h4";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}

function add_p() {
    let new_textarea = document.createElement('textarea');  // то создать новый блок textarea
    new_textarea.classList = "news__item news__p";   // с классом параграфа
    new_textarea.placeholder = "Параграф";
    new_textarea.name = "p";
    news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    new_textarea.addEventListener('focus', new_div);            // добавить прослушку на фокус нового блока textarea
    new_textarea.addEventListener('input', write);              // добавить прослушку на ввод текста в новый блок textarea
    new_textarea.focus();
}

function add_img() {
    let file_manager = document.querySelector(".file_manager");
    file_manager.classList.add("file_manager_active");
}

function add_video() {
    let new_iframe = document.createElement("iframe");
}

function delete_block() {
    let focused_item = document.querySelector(".news__item:focus");     // то получить элемент, на котором стоит фокус
    if (focused_item == null) {              // если нет блока с фокусом
        alert("Выбери блок для удаления")   // предупреждение
    }
    else {                      // иначе
        focused_item.remove();  // удалить блок с фокусом
        let old_div = document.querySelector(".news__div"); // получить скрытый блок для блока с фокусом
        if (old_div != null) {   // если он есть
            if (old_div.previousElementSibling != null) // если есть предыдущий блок
                old_div.previousElementSibling.focus(); // то уставновить на него фокус
            old_div.remove();   // то удалить его
        }
    }
}

function up_block() {
    let focused_item = document.querySelector(".news__item:focus");
    if (focused_item == null)
        alert("Блок не выбран");
    else {
        let parent_elem = focused_item.parentElement;
        let prev_elem = focused_item.previousElementSibling;
        if (prev_elem != null) {
            parent_elem.insertBefore(focused_item, prev_elem);
        }
        focused_item.focus();
    }
}

function down_block() {
    let focused_item = document.querySelector(".news__item:focus");     // то получить элемент, на котором стоит фокус
    if (focused_item == null)
        alert("Блок не выбран");
    else {
        let parent_elem = focused_item.parentElement;
        if (focused_item.nextElementSibling != null) {
            let next_elem = focused_item.nextElementSibling.nextElementSibling;
            parent_elem.insertBefore(focused_item, next_elem);
            focused_item.focus();
        }
    }
}

function new_div() {
    let old_div = document.querySelector(".news__div");             // получить старый скрытый блок
    if (old_div != null) {   // если он получен
        old_div.remove();   // то удалить его
    }
    // console.log(this.classList[this.classList.length - 1]);
    let main_class = this.classList[this.classList.length - 1]; // получить класс для нового скрытого блока (последний в списке классов блока textarea с фокусом)
    let new_div = document.createElement('div');    // создать новый скрытый блок
    new_div.classList = "news__div " + main_class;  // с собственным классом и таким же классом как у teaxtarea с фокусом
    new_div.innerText = this.value; // новому скрытому блоку присвоить значение текста teaxtarea с фокусом
    news_field.append(new_div);     // вставить новый скрытый блок в поле новости
}


function write() {   // при вводе в textarea
    let news_div = document.querySelector(".news__div");    // получить скрытый блок
    let value = this.value; // получить текст из textarea
    let new_value = "";     // новая строка для записи
    //------- фильтрация от нескольких пробелов подряд -------//
    for (let i in value) {
        if (new_value.endsWith(" ") && value[i] == " ") { // если новая строка заканчивается на пробел и следующий символ тоже пробел,
        }                                                 // то ничего не делать
        else                        // иначе
            new_value += value[i];  // добавить следующий символ в новую строку
    }

    this.value = new_value; // переписать текст в textarea
    if ((this.value.endsWith("\n")) | (this.value.endsWith("\n "))) // если нажат enter или enter и пробел 
        news_div.innerText = new_value + "\n";  // то в конец строки добавить \n (чтобы не было пустой строки)
    else                                // иначе
        news_div.innerText = new_value; // вставить новый текст в скрытый блок

    let div_height = news_div.getBoundingClientRect().height;   // получить высоту скрытого блока
    // console.log(div_height);
    if (div_height > 0) {                        // если высота больше 0
        this.style.height = div_height + "px";  // то присвоить такую же высоту для textarea
    }
    else {                          // иначе
        this.style.height = null;   // сбросить стиль высоты для textarea
    }
}

document.querySelector(".file_manager__close").addEventListener('click', function () {
    document.querySelector(".file_manager").classList.remove("file_manager_active");
});

document.onclick = function (e) {
    // console.log(event.target.classList[1]);
    if (event.target.classList[1] === "file_manager_active") {
        document.querySelector(".file_manager").classList.remove("file_manager_active");
    }
}

const save_btn = document.querySelector(".save_button");
save_btn.addEventListener('click', save_all);

function save_all() {
    // const news_field = document.querySelector(".news_field");
    if (document.querySelector(".news__div") != null)
        document.querySelector(".news__div").remove();
    // news_div.remove();
    const news_items = news_field.childNodes;
    let news_value = "";
    for (item of news_items) {
        if (item.name == "h3") {
            news_value += `<h3 class="` + item.classList + `">` + item.value + `</h3>`;
        }
        else if (item.name == "h4") {
            news_value += `<h4 class="` + item.classList + `">` + item.value + `</h4>`;
        }
        else if (item.name == "p") {
            news_value += `<p class="` + item.classList + `">` + item.value + `</p>`;
        }
        else false
    }
    console.log(news_value);
    let new_item = document.createElement("div");
    new_item.innerHTML = news_value;
    news_field.append(new_item);
}

    // window.onbeforeunload = function() {
    //     return "Есть несохранённые изменения. Всё равно уходим?";
    // };

//   } //onload