(function () {

    $("input[type='submit']").click(function (e) {
        if (!checkValidity()) {
            e.preventDefault();
        }
    });

    function checkValidity() {
        var result = true;
        $("span[class='field-validation-error']").empty();
        $("input").each(function () {
            if (window.Utils.checkValidityData($(this).val())) {
                console.log($(this).val());
                console.log(window.Utils.checkValidityData($(this).val()));
                showValidationMessage(this);
                result = false;
            }
        });
        return result;
    }

    function showValidationMessage(obj) {
        console.log($("#" + obj.id + "_Validation"));
        $("span[data-valmsg-for='" + obj.id + "']").attr("class", "field-validation-error");
        $("span[data-valmsg-for='" + obj.id + "']").append("Used illegal characters");
    }
})();