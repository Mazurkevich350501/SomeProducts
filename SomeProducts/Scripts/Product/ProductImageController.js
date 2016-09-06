(function () {
    "use strict";

    var imageNamespace = Utils.getNamespace("Image");

    imageNamespace.initImage = function (params) {
        var image = {
            data: $("#" + params.id.imageDataId).val(),
            type: $("#" + params.id.imageTypeId).val()
        };
        if (image.data !== "" && image.type !== "") {
            $("#image").attr("src", "data:" + image.type + ";base64," + image.data);
        }
    }

    function onFileSelect(e) {
        var maxSize = 512;
        $("#imageMessageId").empty();
        if (checkFileSize(maxSize)) {
            showImage(e);
        }
        else {
            $("#file").val(null);
            showMessage("File size should be less than " + maxSize + " kb");
        }
    }

    function checkFileSize(maxSize) {
        if ($("#file").get(0).files.length) {
            var fileSize = $("#file").get(0).files[0].size / 1024;
            if (fileSize < maxSize) {
                return true;
            }
        }
        return false;
    }

    function showImage(e) {
        var file = e.target.files[0];
        var reader = new FileReader;
        var place = $("#image");
        reader.readAsDataURL(file);

        reader.onload = function (e) {
            place.attr("src", e.target.result);
        }
    }

    function showMessage(message) {
        $("#imageMessageId").append(message);
    }

    if (window.File && window.FileReader && window.FileList && window.Blob) {
        document.querySelector("input[type=file]").addEventListener("change", onFileSelect, false);
    };
})();
