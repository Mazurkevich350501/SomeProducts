﻿(function () {
    var activeId;
    var deleteProductUrl;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
    }

    $("th[class='th-remove']>button").click(function (e) {
        $("#RemovingModal").modal("show");
        activeId = parseInt($(e.target).attr("data-id"));
        event.cancelBubble = true;
    });

    $("#cancelDeleteBtnId").click(function (e) {
        $("#RemovingModal").modal("hide");
        e.preventDefault();
    });

    $("#removeBtnId").click(function () {
        $("#productId").val(activeId);
    });

}());