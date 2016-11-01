(function () {
    var activeId;
    var companyId;
    var deleteProductUrl;
    var changeAdminRoleUrl;
    var setCompanyUrl;
    var adminCompanyId
    var addUserToCompanyQuestion;
    var removeUserFromCompanyQuestion;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        deleteProductUrl = newParams.deleteProductUrl;
        changeAdminRoleUrl = newParams.changeAdminRoleUrl;
        setCompanyUrl = newParams.setCompanyUrl;
        adminCompanyId = newParams.adminCompanyId;
    }

    $(function () {
        InitRemovingModal();
        InitSetAdminModal();
        InitChangeCompanyModal();
        InitAddOrRemoveModal();
    });
    
    function InitRemovingModal(){
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
    }

    function InitSetAdminModal(){
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
    }

    function InitChangeCompanyModal(){
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
    }

    function InitAddOrRemoveModal(){
        $("th[class='th-addOrRemove']>button").click(function (e) {
            activeId = parseInt($(e.target).attr("data-id"));
            tempCompanyId = parseInt($(e.target).attr("data-company"));
            $("#AddingOrRemovingModalMessageId").empty();
            if (tempCompanyId === adminCompanyId){
                companyId = 1;
                $("#AddingOrRemovingModalMessageId").append(removeUserFromCompanyQuestion);
            }
            else{
                companyId = adminCompanyId;
                $("#AddingOrRemovingModalMessageId").append(addUserToCompanyQuestion);
            }
            $("#AddingOrRemovingModal").modal("show");
        });

        $("#cancelAddingOrRemovingBtnId").click(function () {
            $("#AddingOrRemovingModal").modal("hide");
        });

        $("#addOrRemoveBtnId").click(function () {
            var data = JSON.stringify({ userId: activeId, companyId: companyId});
            postRequest(data, setCompanyUrl, ShowAddOrremoveChanges);
        });
    }

    function ShowAddOrremoveChanges(result){
        var button = $("#addOrRemoveCompanyUserBtn" + activeId);
        if(result.CompanyId == adminCompanyId){
            button.css("color", "red");
            button.attr("class", "glyphicon glyphicon-remove");
            $("#setAdminBtn" + activeId).show();
        }
        else{
            button.css("color", "green");
            button.attr("class", "glyphicon glyphicon-ok");
            $("#setAdminBtn" + activeId).hide();
        }
        $("#addOrRemoveCompanyUserBtn" + activeId).attr("data-company", result.CompanyId);
        $("#AddingOrRemovingModal").modal("hide");
    }

    function ShowCompanyChanges(result){
        var item = $("span[class='companyDisplay-" + activeId + "']");
        item.empty();
        item.append(result.CompanyName);
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