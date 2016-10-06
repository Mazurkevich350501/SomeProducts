(function () {
    "use strict";

    var filrers = ["Name", "Description", "Brand_Name", "Quantity"];

    var urlWhithoutBy, presentUrl, sortingOption;

    var productTableNamespace = window.Utils.getNamespace("ProductTable");
    productTableNamespace.init = function (params) {
        urlWhithoutBy = params.url;
        presentUrl = params.presentUrl;
        sortingOption = params.sortingOption;
    };

    $("document").ready(function () {
        setSortSymbol();
        $("div[data-type='sort']").click(sorting);
        $("#FilterBtn").click(filterProduct);
        filrers.forEach(function (item) {
            $("#" + item + "ParameterId").val($("#" + item + "ParameterId").attr("value"));
        });
    });

    function setSortSymbol() {
        var id = sortingOption.replace("rev", "").toLowerCase();
        var isRev = sortingOption.substring(0, 3) === "rev";
        $("#th-" + id).append(isRev ? "&uarr;" : "&darr;");
    }

    function sorting(e) {
        var newSoringOption = $(e.target).attr("data-name");
        if (sortingOption === newSoringOption) {
            newSoringOption = "rev" + newSoringOption;
        }
        var redirectUrl = urlWhithoutBy.indexOf("filterJson") < 0
            ? urlWhithoutBy + "&by=" + newSoringOption
            : urlWhithoutBy.substring(0, urlWhithoutBy.indexOf("filterJson"))
                + "&by=" + newSoringOption + "&" + urlWhithoutBy.substring(urlWhithoutBy.indexOf("filterJson"));
        document.location.replace(redirectUrl);
    }

    function filterProduct() {
        var filterInfo = {
            Filters: []
        }
        filrers.forEach(function (intem) {
            var filter = getFilter(intem);
            if (filter !== null) {
                filterInfo.Filters.push(filter);
            }
        });
        document.location.replace(presentUrl + "&filterJson=" + JSON.stringify(filterInfo));
    }

    function getFilter(option) {
        var result = {
            Option: option,
            Parameter: $("#" + option + "ParameterId").val(),
            Value: $("#" + option + "ValueId").val()
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
}());