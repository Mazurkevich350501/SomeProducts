(function () {

    var illegalCharsError;

    var productTableNamespace = window.Utils.getNamespace("AccountValidation");
    productTableNamespace.init = function (params) {
        illegalCharsError = params.IllegalCharsError;
    };

    $("input[type='submit']").click(function (e) {
        if (!checkValidity()) {
            e.preventDefault();
        }
    });

    function checkValidity() {
        var result = true;
        $("span[class='field-validation-error']").empty();
        $("div[class='input-div']>input").each(function () {
            if (window.Utils.checkValidityData($(this).val())) {
                showValidationMessage(this);
                result = false;
            }
        });
        return result;
    }

    function showValidationMessage(obj) {
        $("span[data-valmsg-for='" + obj.id + "']").attr("class", "field-validation-error");
        $("span[data-valmsg-for='" + obj.id + "']").append(illegalCharsError);
    }
})();