(function() {
    "use strict";

    var productNamespace = Utils.getNamespace("Product");
    var validationExp = /[\!@_%<>\$\^\[\]\+\-\/\{\}]/;
    var idParams = {};
    $("input[type='submit']").click(function (e) {
        if (!checkValidity()) {
            e.preventDefault();
        }
    checkQuantityValue();
    });

    function checkValidity() {
    var result = true;
        $("input").each(function () {
            if (checkInputData(this) && !isFileInput(this.id)) {
                showValidationMessage(this);
                result = false;
            }
    });
        $("textarea").each(function () {
            if (checkInputData(this)) {
            showValidationMessage(this);
            result = false;
            }
    });
    return result;
    }

    function checkInputData(obj) {
        return validationExp.test($(obj).val());
    }

    function isFileInput(inputId) {
        return (inputId === idParams.imageDataId || inputId === idParams.imageTypeId);
    }


    function showValidationMessage(obj) {
        $("span[class='field-validation-valid']").empty();
        $("#" + obj.id + "_Validation").append("Used illegal characters");
    }

    function checkQuantityValue() {
        if ($("#" + idParams.quantityId).val() === "") {
        setQuantityValue();
    }
    }   

    function setQuantityValue() {
        $("#" + idParams.quantityId).val(0);
    }

    productNamespace.initPage = function(params) {
        
        colorpickerItnitialization();
        idParams.imageDataId = params.id.imageDataId;
        idParams.imageTypeId = params.id.imageTypeId;
        idParams.quantityId = params.id.quantityId;
        function colorpickerItnitialization() {
            var id = "#" + params.id.   colorpickerId;
    $(id).simplecolorpicker();
            $(id).simplecolorpicker("destroy");
    $(id).simplecolorpicker({
        picker: true
    });
        }
    }
})();
