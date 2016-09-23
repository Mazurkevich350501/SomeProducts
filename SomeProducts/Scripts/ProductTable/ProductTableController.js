(function () {
    "use strict";

    var url, sortingOption;

    var productTableNamespace = Utils.getNamespace("ProductTable");
    productTableNamespace.init = function (params) {
        url = params.url;
        sortingOption = params.sortingOption;
    };

    $("document").ready(function () {
        setSortSymbol();
        $("div[data-type='sort']").click(sorting);
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
}());