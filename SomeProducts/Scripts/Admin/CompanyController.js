(function () {
    var activeId;
    var activeValue;
    var params;

    var companynamespace = Utils.getNamespace("Company");
    companynamespace.init = function (newParams) {
        params = newParams;
    };

    $(function() {   
        $("#newCompanyBtn").click(openNewCompanyModal)     
        $("button[data-type='setCompany']").click(setCompany);
        $("button[data-type='acceptCompany']").click(setNewCompanyName);
        $("button[data-type='cancelCompany']").click(cancelCompany);
        $("button[data-type='removeCompany']").click(removeCompany);
        initRemovingModal();
        initCreateCompanyModal();
    });

    function setCompany(e){
        var target = getTargetButton(e.target);
        activeId = parseInt($(target).attr("setCompany-id"));
        activeValue = $(target).val();
        setDisplay(true);
    }

    function cancelCompany(e){
        var target = getTargetButton(e.target);
        activeId = parseInt($(target).attr("cancelCompany-id"));
        activeValue = $(target).val();
        setDisplay(false);
    }

    function removeCompany(e){
        $("#RemovingModal").modal("show");
        var target = getTargetButton(e.target);
        activeId = parseInt($(target).attr("removeCompany-id"));
    }

    function openNewCompanyModal(){
        $("#CompanyName").val("");
        $("#CreateCompanyModal").modal("show");
    }

    function initRemovingModal(){
        $("#cancelDeleteBtnId").click(function (e) {
            $("#RemovingModal").modal("hide");
            e.preventDefault();
        });

        $("#removeBtnId").click(function () {
            $("#CompanyId").val(activeId);
        });
    }

    function initCreateCompanyModal(){
        $("#cancelCompanyBtnId").click(function (e) {
            $("#CreateCompanyModal").modal("hide");
            $("#newCompanyValidation").empty();
            e.preventDefault();
        });

        $("#createCompanyBtnId").click(function(e){
            validateResult = validate($("#CompanyName").val());
            if(validateResult !== ""){
                showError(validateResult, "newCompanyValidation");
                e.preventDefault();
            }
        });
    }

    function setDisplay(value){

        $("button[acceptCompany-id='" + activeId + "']").css("display", value ? "inline-block" : "none");
        $("button[cancelCompany-id='" + activeId + "']").css("display", value ? "inline-block" : "none");
        $("input[input-company-id='" + activeId + "']").css("display", value ? "block" : "none");
        $("input[input-company-id='" + activeId + "']").val("");
        $("#input-company-id" + activeId).empty();
        $("button[setCompany-id='" + activeId + "']").css("display", value ? "none" : "inline-block");
        $("button[removeCompany-id='" + activeId + "']").css("display", value ? "none" : "inline-block");
    }

    function setNewCompanyName(e){
        var newName = $("input[input-company-id='" + activeId + "']").val();
        var validateResult = validate(newName);
        if(validateResult !== ""){
            showError(validateResult, "input-company-id" + activeId);
            return;
        }
        var company = {
            CompanyId: activeId,        
            CompanyName: newName
        };
        var data = JSON.stringify(company);
        postRequest(data, params.updateUrl, setNewName)
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

    function setNewName(result){
        if(result !== undefined)
        {
            $("h2[company-name-id='" + activeId + "']").empty();
            $("h2[company-name-id='" + activeId + "']").append(result.CompanyName);
        }
        setDisplay(false);
    }

    function validate(value){
        if(Utils.checkValidityData(value)){
            return params.incorectSymbolError;
        }
        if(value === ""){
            return params.emptyValueError;
        }
        return "";
    }

    function showError(value, id){
        $("#" + id).empty();
        $("#" + id).append(value);
    }

    function getTargetButton(target){
        if(target.tagName !== 'BUTTON')
            return target.parentNode;
        return target;
    }
}());