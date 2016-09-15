(function () {
    var params = {};

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        params = newParams;
    }

    $("#removeBtnId").click(deleteProduct);

    $("#cancelDeleteBtnId").click(function () {

        $("#RemovingModal").modal("hide");
    });

    function deleteProduct() {
        var id = { productId: $("#" + params.id.productId).val() };
        postRequest(JSON.stringify(id), params.url.deleteProductUrl);
    }

    function postRequest(data, url) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: function (newUrl) {
                window.location.replace(newUrl);
            }
        });
    }
}());