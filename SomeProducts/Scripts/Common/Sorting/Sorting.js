(function () {
    "use strict";

    var urlWhithoutBy, sortingOption;

    var productTableNamespace = window.Utils.getNamespace("ProductTable");
    productTableNamespace.init = function (params) {
        urlWhithoutBy = params.url.replace(/amp;/g, "");
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
        console.log(e.target);
        var newSoringOption = $(e.target).attr("data-name");
        if (sortingOption === newSoringOption) {
            newSoringOption = "rev" + newSoringOption;
        }
        var redirectUrl = urlWhithoutBy.indexOf("filter") < 0
            ? urlWhithoutBy + "&by=" + newSoringOption
            : urlWhithoutBy.substring(0, urlWhithoutBy.indexOf("filter"))
                + "&by=" + newSoringOption + "&" + urlWhithoutBy.substring(urlWhithoutBy.indexOf("filter"));
        document.location.replace(redirectUrl);
    }
}());