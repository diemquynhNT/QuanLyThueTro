
const host = "https://provinces.open-api.vn/api/";
var callAPI = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data, "province");
        });
}
callAPI('https://provinces.open-api.vn/api/?depth=1');
var callApiDistrict = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.districts, "district");
        });
}
var callApiWard = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.wards, "ward");
        });
}

var renderData = (array, select) => {
    let row = ' <option disable value="">Chọn</option>';
    array.forEach(element => {
        row += `<option value="${element.code}">${element.name}</option>`
    });
    document.querySelector("#" + select).innerHTML = row
}

$("#province").change(() => {
    callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
    callApiWard(host + "d/" + $("#district").val() + "?depth=1");
    printResult();
    $("#result").text("")
    if (!$("#province").val()) {
        $("#district").text("Abc")
        //renderData("Chọn", "district");
    }
});
$("#district").change(() => {
    callApiWard(host + "d/" + $("#district").val() + "?depth=2");
    printResult();
    if (!$("#district").val()) {
        $("#ward").text("Abc")
    }
});
$("#ward").change(() => {
    printResult();
    if (!$("#ward").val()) {
        $("#result").text("")
    }
})

var printResult = () => {
    if ($("#district").val() != "" && $("#province").val() != "" &&
        $("#ward").val() != "") {
        let result = $("#province option:selected").text() +
            " | " + $("#district option:selected").text() + " | " +
            $("#ward option:selected").text();
        $("#result").text(result)
    }
}
