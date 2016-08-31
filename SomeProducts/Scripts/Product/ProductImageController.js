function onFileSelect(e) {
    var file = e.target.files[0];
    var reader = new FileReader;
    var place = $("#image");
    reader.readAsDataURL(file);

    reader.onload = function (e) { // Как только картинка загрузится
        place.attr("src", e.target.result);
    }
};

if (window.File && window.FileReader && window.FileList && window.Blob) {
    document.querySelector("input[type=file]").addEventListener("change", onFileSelect, false);
};

$("document").ready(function (){
	var image = {
		data : $("#" + imageDataId).val(),
		type : $("#" + imageTypeId).val()
	};
	if(image.data !== "" && image.type !== ""){
		$("#image").attr("src", "data:" + image.type + ";base64," + image.data);
	}
    else{
        $("#image").attr("src", "https://i.vimeocdn.com/portrait/1274237_300x300");
    }
})