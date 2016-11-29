(function () {
    var activeId;
    var deleteProductUrl;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
    }

    $("button[data-type='remove-btn']").click(function (e) {
        $("#RemovingModal").modal("show");
        var target = getTargetButton(e.target);
        activeId = parseInt($(target).attr("data-id"));
        event.cancelBubble = true;
    });

    $("#cancelDeleteBtnId").click(function (e) {
        $("#RemovingModal").modal("hide");
        e.preventDefault();
    });

    $("#removeBtnId").click(function () {
        $("#productId").val(activeId);
    });

    function getTargetButton(target){
        if(target.tagName !== 'BUTTON')
            return target.parentNode;
        return target;
    }

}());