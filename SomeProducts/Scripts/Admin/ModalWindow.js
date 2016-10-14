(function () {
    var activeId;
    var deleteProductUrl;
    var changeAdminRoleUrl;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
        changeAdminRoleUrl = newParams.changeAdminRoleUrl;
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
        $("#userId").val(activeId);
    });

    $("th[class='th-setAdmin']>button").click(function (e) {
        $("#SetAdminModal").modal("show");
        activeId = parseInt($(e.target).attr("data-id"));
    });

    $("#cancelSetAdminBtnId").click(function () {
        $("#SetAdminModal").modal("hide");
    });

    $("#setAdminBtnId").click(function () {
        postRequest(JSON.stringify({ userId: activeId }), changeAdminRoleUrl);
    });

    function setButtonColor(isAdmin) {
        var button = $("#setAdminBtn" + activeId);
        if (isAdmin) {
            button.css("background-color", "green");
        }
        else {
            button.css("background-color", "red");
        }
        $("#SetAdminModal").modal("hide");
    }

    function postRequest(data, url) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: setButtonColor
        });
    }
}());