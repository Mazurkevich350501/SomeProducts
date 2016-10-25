(function () {
    var activeId;
    var deleteProductUrl;
    var changeAdminRoleUrl;
    var setCompanyUrl;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
        changeAdminRoleUrl = newParams.changeAdminRoleUrl;
        setCompanyUrl = newParams.setCompanyUrl;
    }
    $(function () {
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
            data = JSON.stringify({ userId: activeId });
            postRequest(data, changeAdminRoleUrl, ShowAdminChanges);
        });

        $("div[class='userCompany']>button").click(function (e) {
            $("#ChangeCompanyModal").modal("show");
            activeId = parseInt($(e.target).attr("data-id"));
        });

        $("#cancelChangeCompanyBtnId").click(function () {
            $("#ChangeCompanyModal").modal("hide");
        });

        $("#changeCompanyBtnId").click(function () {
            var companyId = $("#SelectCompany").val();
            var data = JSON.stringify({ userId: activeId, companyId: companyId});
            postRequest(data, setCompanyUrl, ShowCompanyChanges);
        });
    });
    
    function ShowCompanyChanges(result){
        var item = $("span[class='companyDisplay-" + activeId + "']");
        item.empty();
        item.append(result);
        $("#ChangeCompanyModal").modal("hide");
    }

    function setButtonColor(roles) {
        var button = $("#setAdminBtn" + activeId);
        if (roles.indexOf("Admin") !== -1) {
            button.css("background-color", "green");
        }
        else {
            button.css("background-color", "red");
        }
    }

    function showUserRoles(roles){
        var rolesItem = $("#tr-" + activeId + ">th").eq(2);
        rolesItem.empty();
        rolesItem.append(roles.join());
    }

    function ShowAdminChanges(result){
        setButtonColor(result);
        showUserRoles(result);
        $("#SetAdminModal").modal("hide");
    }
    function postRequest(data, url, success) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: success
        });
    }
}());