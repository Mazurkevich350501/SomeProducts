(function () {
    "use strict";

    var brandsList;
    var removedIds;
    var idCounter;
    var params = {};
    var isValid = true;

    var modalWindowNamespace = Utils.getNamespace("ModalWindow");
    modalWindowNamespace.initModalWindow = function (newParams) {
        params = newParams;
    };
    $("#saveBtnId").click(saveBrandsChanges);
    $("#addBtnId").click(addNewBrand);

    function startInit() {
        removedIds = [];
        brandsList = [];
        idCounter = -1;
    }

    function cleanNewBrandInput()
    {
        $("#newBrandValidation").empty();
        $("#newBrandName").val("");
    }

    $("document").ready(function () {
        $("#brandEditBtn").click(function () {
            $.getJSON(params.url.getBrandsListUrl, function (brands) {
                startInit();
                cleanNewBrandInput();
                brands.forEach(function (item) {
                    var version = btoa(String.fromCharCode.apply(null, new Uint8Array(item.Version)));
                    addToBrandsList(item.Name, item.Id, "dbBrand", version);
                });
                showBrandsList();
            });
        });
    });

    function addToBrandsList(name, id, type, version) {
        brandsList.push({
            name: name,
            id: id,
            version: version,
            type: type
        });
    }

    function removeFromBrandsList(id) {
        if (id > 0) {
            brandsList.find(function (element, index, array) {
                if (element.id === parseInt(id)) {
                    $.getJSON(params.url.isBrandUsingUrl + "/" + element.id, function (isUsing) {
                        if (!isUsing) {
                            array.splice(index, 1);
                            removedIds.push(element.id);
                            showBrandsList();
                        }
                    });
                }
            });
        }
        else {
            brandsList.find(function (element, index, array) {
                if (parseInt(id) === element.id) {
                    array.splice(index, 1);
                    return true;
                }
                return false;
            });
            showBrandsList();
        }
    }

    function editBrandList(id, name) {
        brandsList.find(function (element) {
            if (element.id === parseInt(id)) {
                if (element.type === "dbBrand") {
                    element.type = "editBrand";
                }
                element.name = name;
                return true;
            }
            return false;
        });
    }

    function showBrandsList() {
        $("#modal-content").empty();
        $("#brandsTemplate").tmpl(brandsList).appendTo("#modal-content");
        $("button[data-selector='removeBtn']").click(removeBrand);
        $("input[class='brand-edit-input']").focusout(editBrand);
    }

    function editBrand(e) {
        isValid = true;
        editBrandList($(e.target).attr("data-id"), $(e.target).val());
        brandListValidation();
    }

    function brandListValidation(){
        $("input[class='brand-edit-input']").each(function(index, element){
            var id = $(element).attr("data-id");
            var name = $(element).val();
            $("#brandValidation-" + id).empty();
            if (!validate(name, id)) {
                isValid = false;
                showErrorMessage("brandValidation-" + id, getErrorMessage(name, id));
            }
        });
    }

    function addNewBrand() {
        var name = $("#newBrandName").val();
        $("#newBrandValidation").empty();
        if (validate(name, 0)) {
            addToBrandsList(name, idCounter--, "newBrand");
            cleanNewBrandInput();
        }
        else {
            showErrorMessage("newBrandValidation", getErrorMessage(name, 0));
        }
        showBrandsList();
        brandListValidation();
    }

    function getTargetButton(target){
        if(target.tagName !== 'BUTTON')
            return target.parentNode;
        return target;
    }

    function removeBrand(e) {
        var object = getTargetButton(e.target);
        removeFromBrandsList(object.getAttribute("data-id"));
        showBrandsList();
    }

    function saveBrandsChanges() {
        if (isValid) {
            var data = JSON.stringify(getBrandChangeModel());
            Utils.postRequest(data, params.url.saveBrandsChangesUrl, 
                saveBrandsChangesSuccess, closeModalWindow,
                params.message.brandChangesSuccessMessage, params.message.requestErrorMessage);
        }
    }

    function saveBrandsChangesSuccess (data) {
        setBrandDropdownItems(data);
        closeModalWindow();
    }   

    function getBrandChangeModel() {
        var brandChangesModel = {
            AddedBrands: [],
            RemovedBrands: [],
            EditedBrands: []
        };
        brandsList.forEach(function (brand) {
            if (brand.type === "newBrand") {
                brandChangesModel.AddedBrands.push({
                    name: brand.name
                });
            } else if (brand.type === "editBrand") {
                brandChangesModel.EditedBrands.push({
                    id: brand.id,
                    name: brand.name,
                    version: brand.version
                });
            }
        });
        removedIds.forEach(function (id) {
            brandChangesModel.RemovedBrands.push({
                id: id
            });
        });
        return brandChangesModel;
    }

    function setBrandDropdownItems(items) {
        var id = "#" + params.id.brandDropdownId;
        $(id).find("option").remove();
        items.forEach(function (item) {
            addOptionToSelectBox(id, item.Id, item.Name);
        });
    }

    function addOptionToSelectBox(idSelectBox, key, value) {
        $(idSelectBox).append('<option value="' + key + '">' + value + "</option>");
    }

    function closeModalWindow() {
        $("#BrandModal").modal("hide");
    }

    function validate(name, id) {
        if (getErrorMessage(name, id) === "") {
            return true;
        }
        else {
            return false;
        }
    }

    function getErrorMessage(name, id) {
        var message = "";
        if (name === "") {
            message = params.error.EmptyBrandNameError;
        }
        if (brandsList.length !== 0) {
            brandsList.find(function (brand) {
                if (brand.name === name && brand.id !== parseInt(id)) {
                    message = params.error.BrandNameExistError;
                    return true;
                }
                return false;
            });
        }

        if (Utils.checkValidityData(name)) {
            message = params.error.IllegalCharsError;
        }
        return message;
    }

    function showErrorMessage(id, message) {
        $("#" + id).append(message);
    }
}());
