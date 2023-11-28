let queuedImgArray = [],
    savedForm = document.querySelector("#saved-form"),
    savedDiv = document.querySelector(".saved-div"),
    inputDiv = document.querySelector(".input-div"),
    input = document.querySelector(".input-div input"),
    serverMess = document.querySelector(".server-message"),
    deleteImg = [];


input.addEventListener("change", () => {
    const files = input.files
    console.log(files)
    console.log(files.length)
    for (let i = 0; i < files.length; i++) {
        queuedImgArray.push(files[i])
    }
    //savedForm.reset();
    displayQueuedImg();
})
//push hinh vao input
//inputDiv.addEventListener("drop", (e) => {
//    e.preventDefault();
//    const files = e.dataTransfer.files;
//    console.log(files)
//    for (let i = 0; i < files.length; i++) {
//        if (!files[i].type.match("image")) continue

//        if (queuedImgArray.every(image => image.name !== files[i].name))
//            queuedImgArray.push[files[i]]
//    }
//    displayQueuedImg();
//})

function displayQueuedImg() {
    console.log("demo img" + queuedImgArray.length)
    let images = "";
    queuedImgArray.forEach((image, index) => {
        images += `    
                <div class="image">
                <img src="${URL.createObjectURL(image)}" alt="image">
                <span onclick="deleteQueuedImage(${index})">&times;</span>
            </div>`
    })

    savedDiv.innerHTML = images;
}

function deleteQueuedImage(index) {
    queuedImgArray.splice(index, 1)
    displayQueuedImg();
}

