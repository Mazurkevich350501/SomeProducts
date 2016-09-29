(function () {
    "use strict";

    var url, presentUrl, sortingOption;

    var productTableNamespace = Utils.getNamespace("ProductTable");
    productTableNamespace.init = function (params) {
        url = params.url;
        presentUrl = params.presentUrl;
        sortingOption = params.sortingOption;
    };

    $("document").ready(function () {
        setSortSymbol();
        $("div[data-type='sort']").click(sorting);
        $("#FilterBtn").click(filterProduct);
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
        document.location.replace(url + "&by=" + newSoringOption);
    }

    function filterProduct() {
        var filterInfo = {
            Filters: []
        }
        var filrers = ["Name", "Description", "Brand", "Quantity"];
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
        if (IsEmtyParameter(result.Parameter)) return result;
        if (result.Value !== "") return result;
        return null;
    }

    function IsEmtyParameter(parameter) {
        switch (parameter) {
            case "IsEmty":
            case "IsNotEmty":
            case "IsNull":
            case "IsNotNull":
                return true;
                break;
            default:
                return false;
        }
    }

    function postJsonData(jsonData, url) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            traditional: true
        });
    }
}());