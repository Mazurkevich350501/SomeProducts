(function () {

    var illegalCharsError;

    var productTableNamespace = window.Utils.getNamespace("Validation");
    productTableNamespace.initValidation = function (params) {
        illegalCharsError = params.IllegalCharsError;
    };

    productTableNamespace.checkValidity = function () {
        var result = true;
        $("span[class='field-validation-error']").empty();
        $("input[data-type='validate']").each(function () {
            if (window.Utils.checkValidityData($(this).val())) {
                showValidationMessage(this);
                result = false;
            }
        });
        return result;
    }

    function showValidationMessage(obj) {
        $("#validation-" + obj.id).attr("class", "field-validation-error");
        $("#validation-" + obj.id).append(illegalCharsError);
    }
})();