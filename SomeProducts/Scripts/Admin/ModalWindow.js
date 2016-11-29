(function () {
    var activeId;
    var companyId;
    var params;

    var removingModalNamespace = Utils.getNamespace("RemovingModal");
    removingModalNamespace.initRemovingModal = function (newParams) {
        params = newParams;
    }

    $(function () {
        initRemovingModal();
        initSetAdminModal();
        initChangeCompanyModal();
        initAddOrRemoveModal();
    });
    
    function initRemovingModal(){
        $("button[data-type='remove-btn']").click(function (e) {
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

    function initSetAdminModal(){
        $("button[data-type='setAdmin-btn']").click(function (e) {
            $("#SetAdminModal").modal("show");
            activeId = parseInt($(e.target).attr("data-id"));
        });

        $("#cancelSetAdminBtnId").click(function () {
            $("#SetAdminModal").modal("hide");
        });

        $("#setAdminBtnId").click(function () {
            var data = JSON.stringify({ userId: activeId });
            postRequest(data, params.changeAdminRoleUrl, showAdminChanges);
        });
    }

    function initChangeCompanyModal(){
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
            postRequest(data, params.setCompanyUrl, showCompanyChanges);
        });
    }

    function initAddOrRemoveModal(){
        $("button[data-type='addOrRemove-btn']").click(function (e) {
            activeId = parseInt($(e.target).attr("data-id"));
            var tempCompanyId = parseInt($(e.target).attr("data-company"));
            $("#AddingOrRemovingModalMessageId").empty();
            if (tempCompanyId === params.adminCompanyId){
                companyId = 1;
                $("#AddingOrRemovingModalMessageId").append(params.removeUserFromCompanyQuestion);
            }
            else{
                companyId = params.adminCompanyId;
                $("#AddingOrRemovingModalMessageId").append(params.addUserToCompanyQuestion);
            }
            $("#AddingOrRemovingModal").modal("show");
        });

        $("#cancelAddingOrRemovingBtnId").click(function () {
            $("#AddingOrRemovingModal").modal("hide");
        });

        $("#addOrRemoveBtnId").click(function () {
            var data = JSON.stringify({ userId: activeId, companyId: companyId});
            postRequest(data, params.setCompanyUrl, showAddOrremoveChanges);
        });
    }

    function showAddOrremoveChanges(result){
        var button = $("#addOrRemoveCompanyUserBtn" + activeId);
        if(result.CompanyId === params.adminCompanyId){
            button.attr("class", "btn btn-small btn-danger");
            button.children().attr("class", "halflings-icon white remove-sign");
            button.attr("title", params.removeFromCompany);
            $("#setAdminBtn" + activeId).show();
        }
        else{
            button.attr("class", "btn btn-success btn-small");
            button.children().attr("class", "halflings-icon white ok-sign");
            button.attr("title", params.addToCompany);
            $("#setAdminBtn" + activeId).hide();
        }
        button.attr("data-company", result.CompanyId);
        button.children().attr("data-company", result.CompanyId);
        $("#AddingOrRemovingModal").modal("hide");
    }

    function showCompanyChanges(result){
        var item = $("span[class='companyDisplay-" + activeId + "']");
        item.empty();
        item.append(result.CompanyName);
        $("#ChangeCompanyModal").modal("hide");
    }

    function setButtonStyle(roles) {
        var button = $("#setAdminBtn" + activeId);
        if (roles.indexOf("Admin") !== -1) {
            button.attr("class", "btn btn-small btn-success");
            button.attr("title", params.unsetAdmin);
        }
        else {
            button.attr("class", "btn btn-small btn-danger");
            button.attr("title", params.setAdmin);
        }
    }

    function showUserRoles(roles){
        var rolesItem = $("#tr-" + activeId + ">th").eq(2);
        rolesItem.empty();
        rolesItem.append(roles.join());
    }

    function showAdminChanges(result){
        setButtonStyle(result);
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