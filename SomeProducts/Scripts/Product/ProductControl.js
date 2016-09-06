(function () {
    "use strict";

    var productNamespace = Utils.getNamespace("Product");

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
            if (Utils.checkValidityData($(this).val()) && isProductValidationField(this.id)) {
                showValidationMessage(this);
                result = false;
            }
        });
        $("textarea").each(function () {
            if (Utils.checkValidityData($(this).val()) && isProductValidationField(this.id)) {
                showValidationMessage(this);
                result = false;
            }
        });
        return result;
    }

    function isProductValidationField(id) {
        return (id === idParams.productNameId || id === idParams.quantityId || id === idParams.descriptionId);
    }


    function showValidationMessage(obj) {
        $("span[class='field-validation-valid']").empty();
        $("span[class='field-validation-error']").empty();
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

    productNamespace.initPage = function (params) {
        idParams = params.id;
        colorpickerInitialization();
    }

    function colorpickerInitialization() {
        var id = "#" + idParams.colorpickerId;
        $(id).simplecolorpicker();
        $(id).simplecolorpicker("destroy");
        $(id).simplecolorpicker({
            picker: true
        });
    }
})();
