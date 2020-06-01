//images creator
// version 2

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
    <button class="create_button add_h3" type="button">Добавить заголовок</button>
    <button class="create_button add_h4" type="button">Добавить подзаголовок</button>
    <button class="create_button add_p" type="button">Добавить параграф</button>
    <button class="create_button add_ol" type="button">Добавить список</button>
    <button class="create_button add_img" type="button">Добавить картинку</button>
    <button class="create_button add_video" type="button">Добавить видео</button>
    <button class="create_button add_author" type="button">Добавить автора</button>
    <button class="create_button add_date" type="button">Добавить дату</button>
    <div class="align_wrapper">
        <button class="edit_button align_wrapper__btn align_text align_left_btn" type="button"><img src="assets/img/icons/align_left.svg"></button>
        <button class="edit_button align_wrapper__btn align_text align_center_btn" type="button"><img src="assets/img/icons/align_center.svg"></button>
        <button class="edit_button align_wrapper__btn align_text align_right_btn" type="button"><img src="assets/img/icons/align_right.svg"></button>
        <button class="edit_button align_wrapper__btn align_text align_width_btn" type="button"><img src="assets/img/icons/align_width.svg"></button>
        <button class="edit_button align_wrapper__btn align_img img_in_text_btn" type="button"><img src="assets/img/icons/img_in_text.svg"></button>
        <button class="edit_button align_wrapper__btn align_img img_out_text_btn" type="button"><img src="assets/img/icons/img_out_text.svg"></button>
    </div>
    <button class="align_text align_img edit_button delete_block" type="button">Удалить блок</button>
    <button class="align_text align_img edit_button up_block" type="button">Блок вверх</button>
    <button class="align_text align_img edit_button down_block" type="button">Блок вниз</button>`;

    let news_section = document.querySelector(".news");
    news_section.insertBefore(edit_panel_dom, news_section.firstChild);

    document.querySelector(".add_h3").addEventListener('click', add_h3);
    document.querySelector(".add_h4").addEventListener('click', add_h4);
    document.querySelector(".add_p").addEventListener('click', add_p);
    document.querySelector(".add_ol").addEventListener('click', add_ol);
    document.querySelector(".add_img").addEventListener('click', add_img);
    document.querySelector(".add_video").addEventListener('click', add_video);

    document.querySelector(".align_left_btn").addEventListener('click', align_left);
    document.querySelector(".align_center_btn").addEventListener('click', align_center);
    document.querySelector(".align_right_btn").addEventListener('click', align_right);
    document.querySelector(".align_width_btn").addEventListener('click', align_width);
    document.querySelector(".img_in_text_btn").addEventListener('click', img_in_text);
    document.querySelector(".img_out_text_btn").addEventListener('click', img_out_text);

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

document.querySelector(".editor_button").addEventListener('click', edit_all);
document.querySelector(".save_button").addEventListener('click', save_all);

const edit_btn = document.querySelector(".editor_button");

function add_h3(){
    let focused_item = choose_block();
    let parent_elem = null;
    if (focused_item != null){
        parent_elem = focused_item.parentElement;
    }
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement("h3");  // то создать новый блок h3
    new_textarea.contentEditable = true;
    new_textarea.classList = "news__h3 news__editable text_center";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.name = "h3";
    if ((focused_item != null) & (parent_elem != null)){
        parent_elem.insertBefore(new_textarea, focused_item.nextSibling);
    }
    else{
        news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    }
    new_textarea.focus();
}

function add_h4(){
    let focused_item = choose_block();
    let parent_elem = null;
    if (focused_item != null){
        parent_elem = focused_item.parentElement;
    }
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement("h4");  // то создать новый блок textarea
    new_textarea.contentEditable = true;
    new_textarea.classList = "news__h4 news__editable text_center";   // с классом взятым из имени кнопки (h3, h4, p)
    new_textarea.name = "h4";
    if ((focused_item != null) & (parent_elem != null)){
        parent_elem.insertBefore(new_textarea, focused_item.nextSibling);
    }
    else{
        news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    }
    new_textarea.focus();
}

function add_p(){
    let focused_item = choose_block();
    let parent_elem = null;
    if (focused_item != null){
        parent_elem = focused_item.parentElement;
    }
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_textarea = document.createElement("p");  // то создать новый блок textarea
    new_textarea.contentEditable = true;
    new_textarea.classList = "news__p news__editable text_left";   // с классом параграфа
    if ((focused_item != null) & (parent_elem != null)){
        parent_elem.insertBefore(new_textarea, focused_item.nextSibling);
    }
    else{
        news_field.append(new_textarea);                            // вставить новый блок textarea в поле новости
    }
    new_textarea.focus();
}

function add_ol(){
    let focused_item = choose_block();
    let parent_elem = null;
    if (focused_item != null){
        parent_elem = focused_item.parentElement;
    }
    const news_field = document.querySelector(".news_field");   // поле с новостью
    let new_ol = document.createElement("ol");  // то создать новый блок textarea
    new_ol.contentEditable = true;
    new_ol.classList = "news__ol news__editable";   // с классом параграфа
    if ((focused_item != null) & (parent_elem != null)){
        parent_elem.insertBefore(new_ol, focused_item.nextSibling);
    }
    else{
        news_field.append(new_ol);                            // вставить новый блок textarea в поле новости
    }
    //news_field.append(new_ol);                            // вставить новый блок textarea в поле новости
    let new_li = document.createElement("li");
    new_li.contentEditable = true;
    new_li.innerText = "";
    new_ol.append(new_li);
    new_ol.focus();
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

    //----------------------------------Женек, ЗАкоменть!!!
    // let test_response = `[
    //     "https://avatars.mds.yandex.net/get-pdb/49816/68f4f375-08b6-4e33-ad9e-996e86871444/s1200",
    //     "https://avatars.mds.yandex.net/get-pdb/1004346/d59ecfca-4e4f-428f-982b-3d3ddac1b44d/s1200",
    //     "https://avatars.mds.yandex.net/get-pdb/2129646/a5fa83ae-8bb0-4ace-9d5b-1e5517125447/s1200",
    //     "https://avatars.mds.yandex.net/get-pdb/368827/30382812-14fb-4c02-b021-9673e778a3ac/s1200"
    // ]`;
    // let sources = JSON.parse(test_response);
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
    let focused_item = choose_block();
    let parent_elem = null;
    if (focused_item != null){
        parent_elem = focused_item.parentElement;
    }
    let news_field = document.querySelector(".news_field");
    let news_img_wrapper = document.createElement("div");
    news_img_wrapper.classList = "news_img_wrapper img_left img_out_text";
    if ((focused_item != null) & (parent_elem != null)){
        parent_elem.insertBefore(news_img_wrapper, focused_item.nextSibling);
    }
    else {
        news_field.appendChild(news_img_wrapper);
    }
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
    let edit_btns_arr = document.querySelectorAll(".edit_button");
    let flag = true;
    for (i of edit_btns_arr){
        if (event.target == i){
            flag = false;
            break;
        }
    }
    if (flag){
        for (i of focused_imgs){
            i.classList.remove("focused_img");
        }
    }
    if (this.tagName == "IMG"){
        this.parentElement.classList += " focused_img";
    }
}

function choose_block(){
    let focused_item = document.querySelector(".news__editable:focus");
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
        if (focused_item.previousElementSibling != null){
            focused_item.previousElementSibling.focus();
        }
        focused_item.remove();  // удалить блок с фокусом
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
        if (focused_item.classList[0] == "news_img_wrapper"){
            focused_item.classList += " focused_img";
        }
        // focus_img();
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
            // focus_img();
        }
    }
}

function edit_all(){
    create_edit_panel();
    // if_focused();
    window.addEventListener('click', if_focused);
    edit_btn.setAttribute("disabled", "true");
    const news_section = document.querySelector(".news");
    const saved_news = document.querySelector(".saved_news");
    let news_field = document.createElement("div");
    news_field.classList = "news_field";
    news_section.append(news_field);
    if (saved_news != null){
        const news_items = saved_news.children;
        console.log(news_items);
        for (item of news_items){
            if ((item.tagName == "H3") || (item.tagName == "H4") || (item.tagName == "P") || (item.tagName == "OL")){
                item.contentEditable = true;
                let new_item = item.cloneNode(true);
                new_item.classList.add("news__editable");
                news_field.append(new_item);
                }
            else if (item.classList.contains('news_img_wrapper')){
                let new_item = item.cloneNode(true);
                new_item.firstChild.addEventListener('click', focus_img);
                news_field.append(new_item);
            }
        }
        saved_news.remove();
    }
}

function save_all(){
    let news_field = document.querySelector(".news_field");   // поле с новостью
    if (news_field == null){
        news_field = document.querySelector('.saved_news');
    }
    const edit_panel = document.querySelector(".edit_panel");
    if (edit_panel != null)
        edit_panel.remove();

    const news_section = document.querySelector(".news");
    const news_items = news_field.childNodes;
    let saved_news = document.createElement("div");
    saved_news.classList = "saved_news";
    
    for (item of news_items){
        if ((item.tagName == "H3") || (item.tagName == "H4") || (item.tagName == "P") || (item.tagName == "OL")){
            item.classList.remove("news__editable");
            item.contentEditable = false;
            let new_item = item.cloneNode(true);
            saved_news.append(new_item);
        }
        else if (item.classList[0] == "news_img_wrapper"){
            let new_item = item.cloneNode(true);
            new_item.firstChild.removeEventListener('click', focus_img);
            saved_news.append(new_item);
        }
}
    news_field.remove();
    news_section.append(saved_news);

    edit_btn.removeAttribute("disabled");
    document.getElementById("formContent").value = saved_news.innerHTML;
}

function if_focused(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    // console.log(focused_item);
    if (focused_item == null){
        let align_btn_arr = document.querySelectorAll(".edit_button");
        for (i of align_btn_arr){
            i.setAttribute("disabled", "true");
        }
    }
    else if ((focused_item.tagName == "H3") || (focused_item.tagName == "H4") || (focused_item.tagName == "P") || (focused_item.tagName == "OL")){
        let align_img_btn_arr = document.querySelectorAll(".align_img");
        for (i of align_img_btn_arr){
            i.setAttribute("disabled", "true");
        }
        let align_text_btn_arr = document.querySelectorAll(".align_text");
        for (i of align_text_btn_arr){
            i.removeAttribute("disabled");
        }
    }
    else if (focused_item.classList[0] == "news_img_wrapper"){
        let align_text_btn_arr = document.querySelectorAll(".align_text");
        for (i of align_text_btn_arr){
            i.removeAttribute("disabled");
        }
        let align_img_btn_arr = document.querySelectorAll(".align_img");
        for (i of align_img_btn_arr){
            i.removeAttribute("disabled");
        }
    }
}

function align_left(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        focused_item.classList.remove('img_left');
        focused_item.classList.remove('img_center');
        focused_item.classList.remove('img_right');
        if (focused_item.classList.contains('img_in_text_left')){
        }
        else if (focused_item.classList.contains('img_in_text_right')){
            focused_item.classList.remove('img_in_text_right');
            focused_item.classList.add('img_in_text_left');
        }
        else if (focused_item.classList.contains('img_out_text')){
            focused_item.classList.add('img_left');
        }
    }
    else{
        focused_item.classList.remove('text_left');
        focused_item.classList.remove('text_center');
        focused_item.classList.remove('text_right');
        focused_item.classList.remove('text_width');
        focused_item.classList.add('text_left');
    }
}

function align_center(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        if (focused_item.classList.contains('img_in_text_left')){
            alert('Нельзя выровнять по центру пока картинка обтекается текстом');
        }
        else if (focused_item.classList.contains('img_in_text_right')){
            alert('Нельзя выровнять по центру пока картинка обтекается текстом');
        }
        else if (focused_item.classList.contains('img_out_text')){
            focused_item.classList.remove('img_left');
            focused_item.classList.remove('img_center');
            focused_item.classList.remove('img_right');
            focused_item.classList.add('img_center');
        }
    }
    else{
        focused_item.classList.remove('text_left');
        focused_item.classList.remove('text_center');
        focused_item.classList.remove('text_right');
        focused_item.classList.remove('text_width');
        focused_item.classList.add('text_center');
    }
}

function align_right(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        focused_item.classList.remove('img_left');
        focused_item.classList.remove('img_center');
        focused_item.classList.remove('img_right');
        if (focused_item.classList.contains('img_in_text_left')){
            focused_item.classList.remove('img_in_text_left');
            focused_item.classList.add('img_in_text_right');
        }
        else if (focused_item.classList.contains('img_in_text_right')){
        }
        else if (focused_item.classList.contains('img_out_text')){
            focused_item.classList.add('img_right');
        }
    }
    else{
        focused_item.classList.remove('text_left');
        focused_item.classList.remove('text_center');
        focused_item.classList.remove('text_right');
        focused_item.classList.remove('text_width');
        focused_item.classList.add('text_right');
    }
}

function align_width(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        alert('Картинку нельзя выровнять по ширине');
    }
    else{
        focused_item.classList.remove('text_left');
        focused_item.classList.remove('text_center');
        focused_item.classList.remove('text_right');
        focused_item.classList.remove('text_width');
        focused_item.classList.add('text_width');
    }
}

function img_in_text(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        if (focused_item.classList.contains('img_left')){
            focused_item.classList.remove('img_out_text');
            focused_item.classList.remove('img_in_text_right');
            focused_item.classList.remove('img_left');
            focused_item.classList.add('img_in_text_left');
        }
        else if (focused_item.classList.contains('img_center')){
            alert('Нельзя сделать обтекание по центру, сначала выровняй по левому или правому краю')
        }
        else if (focused_item.classList.contains('img_right')){
            focused_item.classList.remove('img_out_text');
            focused_item.classList.remove('img_in_text_left');
            focused_item.classList.remove('img_right');
            focused_item.classList.add('img_in_text_right');
        }
    }
    else{}
}

function img_out_text(){
    let focused_item = choose_block();     // то получить элемент, на котором стоит фокус
    if (focused_item.classList.contains('focused_img')){
        if (focused_item.classList.contains('img_in_text_right')){
            focused_item.classList.remove('img_in_text_right');
            focused_item.classList.remove('img_in_text_left');
            focused_item.classList.add('img_right');
            focused_item.classList.add('img_out_text');
        }
        else if (focused_item.classList.contains('img_in_text_left')){
            focused_item.classList.remove('img_in_text_right');
            focused_item.classList.remove('img_in_text_left');
            focused_item.classList.add('img_left');
            focused_item.classList.add('img_out_text');
        }

    }
    else{}
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
