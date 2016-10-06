(function () {
    var deletedId;
    var deleteProductUrl;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
    }

    $("th[class='th-remove']>button").click(function (e) {
        $("#RemovingModal").modal("show");
        deletedId = parseInt($(e.target).attr("data-id"));
        event.cancelBubble = true;
    });

    $("#cancelDeleteBtnId").click(function () {
        $("#RemovingModal").modal("hide");
    });

    $("#removeBtnId").click(function () {
        postRequest(JSON.stringify({ productId: deletedId }), deleteProductUrl);
    });

    function postRequest(data, url) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: function (newUrl) {
                window.location.reload();
            }
        });
    }
}());