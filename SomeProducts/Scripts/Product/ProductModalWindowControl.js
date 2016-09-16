(function () {
    "use strict";

    var brandsList;
    var brandChangesModel = {
        RemovedBrands: [],
        AddedBrands: []
    };

    var idCounter;
    var params = {};

    var modalWindowNamespace = Utils.getNamespace("ModalWindow");
    modalWindowNamespace.initModalWindow = function (newParams) {
        params = newParams;
    };
    $("#saveBtnId").click(saveBrandsChanges);
    $("#addBtnId").click(addNewBrand);

    function startInit() {
        brandChangesModel.RemovedBrands = [];
        brandChangesModel.AddedBrands = [];
        brandsList = [];
        idCounter = 1;
    }


    $("document").ready(function () {
        $("#brandEditBtn").click(function () {
            $.getJSON(params.url.getBrandsListUrl, function (brands) {
                startInit();
                brands.forEach(function (item) {
                    addToBrandsList(item.Name, item.Id, "dbBrand");
                });
                showBrandsList();
            });
        });
    });

    function addToBrandsList(name, id, type) {
        brandsList.push({
            name: name,
            id: id,
            type: type
        });

        if (type === "newBrand") {
            brandChangesModel.AddedBrands.push({
                name: name,
                id: 0
            });
        }
    }

    function removeFromBrandsList(name, type) {
        function removeBrand(list, name){
            list.find(function (element, index, array) {
                if (element.name === name) {
                    array.splice(index, 1);
                    return true;
                }
                return false;
            });
        }
        if (type === "dbBrand") {
            brandsList.find(function (element, index, array) {
                if (element.name === name) {
                    $.getJSON(params.url.isBrandUsingUrl + "/" + element.id, function (isUsing) {
                        if (!isUsing) {
                            array.splice(index, 1);
                            brandChangesModel.RemovedBrands.push({
                                id: element.id,
                                name: name
                            });
                            showBrandsList();
                        }
                    });
                }
            });
        }
        else {
            removeBrand(brandsList, name);
            removeBrand(brandChangesModel.AddedBrands, name);
            showBrandsList();
        }
    }

    function showBrandsList() {
        $("#modal-content").empty();
        $("#brandsTemplate").tmpl(brandsList).appendTo("#modal-content");
        $("button[data-selector='removeBtn']").click(removeBrand);
    }

    function addNewBrand() {
        var name = $("#newBrandName").val();
        if (validate(name)) {
            addToBrandsList(name, idCounter++, "newBrand");
            $("#newBrandName").val("");
        }
        else {
            showErrorMessage(getErrorMessage(name));
        }
        showBrandsList();
    }

    function removeBrand(e) {
        var object = e.target;
        removeFromBrandsList(object.getAttribute("data-name"), object.getAttribute("data-type"));
        showBrandsList();
    }

    function saveBrandsChanges() {
        postJsonData(JSON.stringify(brandChangesModel), params.url.saveBrandsChangesUrl);
    }

    function postJsonData(jsonData, url) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            traditional: true,
            success: function (data) {
                setBrandDropdownItems(data);
                closeModalWindow();
            }
        });
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

    function validate(name) {
        $("#newBrandValidation").empty();
        if (getErrorMessage(name) === "") {
            return true;
        }
        else {
            return false;
        }
    }

    function getErrorMessage(name) {
        var message = "";
        if (name === "") {
            message = "Brand name is empty.";
        }
        if (brandsList.length !== 0) {
            brandsList.forEach(function (brand) {
                if (brand.name === name) {
                    message = "Brand name already exists.";
                }
            });
        }

        if (Utils.checkValidityData(name)) {
            message = "Used illegal characters";
        }
        return message;
    }

    function showErrorMessage(message) {
        $("#newBrandValidation").append(message);
    }
}());
