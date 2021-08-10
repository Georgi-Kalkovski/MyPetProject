var header = document.document.querySelector("#kingdoms_paginate > span > a");
var btns = header.getElementsByClassName("paginate_button");
for (var i = 0; i < btns.length; i++) {
    btns[i].addEventListener("click", function () {
        var current = document.getElementsByClassName("current");
        current[0].className = current[0].className.replace(" current", "");
        this.className += " current";
    });
}