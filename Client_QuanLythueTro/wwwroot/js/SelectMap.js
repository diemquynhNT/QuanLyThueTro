fetch('https://provinces.open-api.vn/api/')
    .then(response => response.json())
    .then(data => {
        var selectElement = document.getElementById('citychoose');
        data.forEach(city => {
            var option = document.createElement('option');
            option.value = city.code;
            option.text = city.name;
            selectElement.add(option);
        });

        selectElement.addEventListener('change', function () {
            var selectedOption = selectElement.options[selectElement.selectedIndex];
            console.log(selectedOption.value)
            GetListKhuVucData(selectedOption.value)
        });
    })
    .catch(error => {
        console.error('Lỗi khi gọi API: ', error);
    });



function GetListKhuVucData(cityCode) {
    var url = `https://localhost:7034/api/KhuVucs/GetKhuVucsByIdTP?id=${cityCode}`;
    console.log(url);
    fetch(url)
        .then(response => response.json())
        .then(data => {
            var selectElement = document.getElementById('districtchoose');
            data.forEach(khuvuc => {
                var option = document.createElement('option');
                option.value = khuvuc.idKhuVuc;
                option.text = khuvuc.tenKhuVuc;
                selectElement.add(option);
            });

        })
        .catch(error => {
            console.error(error);
        });
}