var popups = document.querySelectorAll('.filter-popup');
console.log(popups)

console.log("demo" + document.getElementById('diadiemDaChon').value)
function showPopup(popupType) {
    console.log(popupType)
    popups.forEach(function (popup) {
        if (popup.getAttribute('data-popup-type') === popupType) {
            popup.style.display = 'block';
        } else {
            popup.style.display = 'none';
        }
    });
}



function closePopup(type) {
    var popupToClose = document.querySelector('.js-filter-popup-' + type);
    popupToClose.style.display = 'none';
}

function selectLocation(selectedOption) {
    var diaDiem = document.getElementById('diadiem');
    diaDiem.innerHTML = "meme";
    document.getElementById('diadiemDaChon').value = selectedOption;
    alert(selectedOption)
    closePopup('city');
}
function searchData{

}

function selectDienTich(option) {
    var dientichChon = document.getElementById('dientichchon');
    var min;
    var max;

    if (option.textContent.includes('Dưới')) {
        min = 0;
        max = parseInt(option.textContent.match(/\d+/)[0]);
    } else {
        var ranges = option.textContent.match(/\d+/g);
        min = parseInt(ranges[0]);
        max = parseInt(ranges[1]);
    }

    dientichChon.innerHTML = `Từ ${min}m - ${max}m`;
    closePopup('dientich');
}

//PRICE
const rangeInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    progress = document.getElementById("progress");
//console.log(rangeInput)
console.log(progress)

let priceGap = 1000;

rangeInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(rangeInput[0].value),
            maxVal = parseInt(rangeInput[1].value);

        if (maxVal - minVal < priceGap) {
            console.log(e.target.className + "check")
            if (e.target.className === "range-min") {
                rangeInput[0].value = maxVal - priceGap;
            } else {
                rangeInput[1].value = minVal + priceGap;
            }

        } else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            console.log(rangeInput[0].max, rangeInput[1].max)
            progress.style.left = (minVal / rangeInput[0].max) * 100 + "%";
            progress.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }
    });
})

priceInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(priceInput[0].value),
            maxVal = parseInt(priceInput[1].value);

        if ((maxVal - minVal >= priceGap) && maxVal <= 15000) {
            console.log(e.target.className + "check")
            if (e.target.className === "input-min") {
                rangeInput[0].value = minVal;
                progress.style.left = (minVal / rangeInput[0].max) * 100 + "%";

            } else {
                rangeInput[1].value = maxVal;
                progress.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";

            }

        }
    });
})

function getDatatoPrice() {
    var minInput = document.getElementById("range-min").value;
    var maxInput = document.getElementById("range-max").value;
    console.log(minInput, maxInput)

    var giachon = document.getElementById('giachon');
    giachon.innerHTML = minInput + " - " + maxInput
    closePopup('price');

}



