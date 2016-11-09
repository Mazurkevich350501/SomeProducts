(function () {
    var filters;
    var presentUrl;

    var filterNamespace = window.Utils.getNamespace("Filter");
    filterNamespace.init = function (params) {
        presentUrl = params.presentUrl.replace(/amp;/g, "");
        filters = params.filters.split(",");
    };

    $("document").ready(function () {
        $("#FilterBtn").click(filterProduct);
        $("#clearFiltersBtnId").click(clearFilters);
        filters.forEach(function (item) {
            $("#" + item + "ParameterId").val($("#" + item + "ParameterId").attr("value"));
        });
    });

    function filterProduct() {
        var validationModel = window.Utils.getNamespace("Validation");
        if (!validationModel.checkValidity())
            return;
        var filterInfo = {
            Filters: []
        }
        filters.forEach(function (intem) {
            var filter = getFilter(intem);
            if (filter !== null) {
                filterInfo.Filters.push(filter);
            }
        });
        document.location.replace(presentUrl + "&filter=" + encodeURIComponent(JSON.stringify(filterInfo.Filters)));
    }

    function getFilter(option) {
        var inputsIdPart = option.replace(/\./g, "\\.");
        var result = {
            Option: option,
            Parameter: $("#" + inputsIdPart + "ParameterId").val(),
            Value: $("#" + inputsIdPart + "ValueId").val()
        }
        if (isEmtyParameter(result.Parameter)) return result;
        if (result.Value !== "") return result;
        return null;
    }

    function isEmtyParameter(parameter) {
        switch (parameter) {
            case "IsEmty":
            case "IsNotEmty":
            case "IsNull":
            case "IsNotNull":
                return true;
            default:
                return false;
        }
    }

    function clearFilters() {
        filters.forEach(function (item) {
            $("input").val("");
            $("#" + item + "ParameterId").val("IsEqualTo");
        });
    }
}());
