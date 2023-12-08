﻿



var popups = document.querySelectorAll('.filter-popup');
console.log("list"+popups)
function showPopup(popupType) {
    console.log("type"+popupType)
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

function selectLocation(selectedOption, cityName, districtCode) {
    var diaDiem = document.getElementById('diadiem');
    var inputTinh = document.getElementById('inputTinh');
    var inputHuyen = document.getElementById('inputHuyen');
    inputTinh.value = cityName;
    inputHuyen.value = selectedOption.textContent.trim();
    diaDiem.innerHTML = selectedOption.textContent.trim() + ", " + cityName;
    closePopup('khuvuc');
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
    var intputMin = document.getElementById('inputDienTichMin');
    var intputMax = document.getElementById('inputDienTichMax');
    intputMin.value = min;
    intputMax.value = max;
    console.log("inputDienTic" + intputMin.value + " " + intputMax.value)

    closePopup('dientich');
}

//PRICE
const rangeInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    progress = document.getElementById("progress");
//console.log(rangeInput)
console.log(progress)

let priceGap = 500000;

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

        if ((maxVal - minVal >= priceGap) && maxVal <= 15000000) {
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

    var intputMin = document.getElementById('inputGiaMin');
    var intputMax = document.getElementById('inputGiaMax');
    intputMin.value = minInput;
    intputMax.value = maxInput;
    console.log("Gia" + intputMin.value + " " + intputMax.value)

    closePopup('price');

}

var thanhPhoDaChon = document.getElementById("inputCityCode").value;
fetch('https://provinces.open-api.vn/api/')
    .then(response => response.json())
    .then(data => {
        const citylist = document.getelementbyid('city-list');

        data.foreach(city => {
            if (city.name.indexOf(thanhPhoDaChon) !== -1)
            const listitem = document.createelement('li');
            listitem.textcontent = city.name;
            listitem.onclick = function () {
                getkhuvucdata(city.code, city.name)
            };
            citylist.appendchild(listitem);
        });
    })
    .catch(error => {
        console.error(error);
    });
function GetKhuVucData(cityCode, cityName) {
    fetch('https://provinces.open-api.vn/api/?depth=2')
        .then(response => response.json())
        .then(data => {
            const cityChoose = data.find(city => city.code === cityCode);
            const districts = cityChoose.districts;
            const khuvucList = document.getElementById('khuvuc-list');
            khuvucList.innerHTML = '';
            districts.forEach(district => {
                const listItem = document.createElement('li');
                listItem.textContent = district.name;
                listItem.onclick = function () {
                    selectLocation(this, cityName, cityCode,);
                };
                khuvucList.appendChild(listItem);
            });
        })
        .catch(error => {
            console.error('Lỗi khi gọi API danh sách quận/huyện: ', error);
        });
};
GetKhuVucData(