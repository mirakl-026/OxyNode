let tags = document.querySelectorAll(".tag");
let search = document.querySelector(".search");
let popup = document.querySelector(".popup");

function popup_display() {
    popup.classList.add("display");
}

function popup_none() {
    popup.classList.remove("display");
}

tags.forEach(function (item, i, arr) {
    item.addEventListener('click', tag_input);
    console.log(item);
});

function tag_input() {
    search.value += "#" + this.innerHTML + " ";
    // this.classList.add("tag_disable");
    // this.classList.remove("tag");
    // this.removeEventListener('click', tag_input);
}