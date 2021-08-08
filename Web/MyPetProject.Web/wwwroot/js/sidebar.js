
function checkItem(el, num, cl) {
    let table = document.querySelector(`#${cl} > tbody`).children;
    let id = el.id;
    console.log(`id = ${id}`)
    if (id === undefined) {
        let item = el[0];
        console.log(`item = ${item}`)
        let id = item.id;
        console.log(`id = ${id}`)
        if (item.checked == false) {
            for (let row of table) {
                console.log(`row = ${row}`)
                let rowId = row.children[num].textContent;
                console.log(`rowId = ${rowId}`)
                if (rowId == id) {
                    row.style.display = "none";
                }
            }
        } else {
            for (let row of table) {
                let rowId = row.children[num].textContent;
                if (rowId == id) {
                    row.style.display = "";
                }
            }
        }

    } else {
        if (el.checked == false) {
            for (let row of table) {
                let rowId = row.children[num].textContent;
                console.log(rowId)
                if (rowId.replace(" ", "_") == id ||
                    rowId == id.replace("234", '') ||
                    rowId == id.replace("123", '')) {
                    row.style.display = "none";
                }
            }
        } else {
            for (let row of table) {
                let rowId = row.children[num].textContent;
                console.log(rowId)
                if (rowId.replace(" ", "_") == id ||
                    rowId == id.replace("234", '') ||
                    rowId == id.replace("123", '')) {
                    row.style.display = "";
                }
            }
        }
    }
}

function checkTheAnimal(el, num) {
    let table = document.querySelector("#kingdoms > tbody").children;

    for (let row of table) {
        if (el.checked == false) {
            if (row.children[num].children.length == 0) {
                row.style.display = "";
            }
        } else {
            if (row.children[num].children.length == 1) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }

            document.getElementById("wildAnimal").checked = false;
        }
    }
}

function checkForWild(el, num1, num2) {
    let table = document.querySelector("#kingdoms > tbody").children;

    for (let row of table) {
        if (el.checked == true) {
            if (row.children[num1].children.length == 0 && row.children[num2].children.length == 0) {
                row.style.display = "";
            }
            else {
                row.style.display = "none";
            }

            document.getElementById("petAnimal").checked = false;
            document.getElementById("farmAnimal").checked = false;

        } else {
            if (row.children[num1].children.length == 1 || row.children[num2].children.length == 1) {
                row.style.display = "";
            }
            else {
                row.style.display = "none";
            }
        }
    }
}
