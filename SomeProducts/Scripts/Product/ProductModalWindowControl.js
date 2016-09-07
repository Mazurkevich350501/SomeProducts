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
    }
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
                    addToBrandsList(item.BrandName, item.BrandId, "dbBrand");
                });
                showBrandsList();
            });
        });
    });

    function addToBrandsList(brandName, brandId, type) {
        brandsList.push({
            brandName: brandName,
            brandId: brandId,
            type: type
        });

        if (type === "newBrand") {
            brandChangesModel.AddedBrands.push({
                brandName: brandName,
                brandId: 0
            });
        }
    }

    function removeFromBrandsList(brandName, type) {
        function removeBrand(list, brandName){
            list.find(function (element, index, array){
                if (element.brandName === brandName) {
                    array.splice(index, 1);
                    return true;
                }
            });
        }
        if (type === "dbBrand") {
            brandsList.find(function (element, index, array) {
                if (element.brandName === brandName) {
                    $.getJSON(params.url.isBrandUsingUrl + "/" + element.brandId, function (isUsing) {
                        if (!isUsing) {
                            array.splice(index, 1);
                            brandChangesModel.RemovedBrands.push({
                                brandId: element.brandId,
                                brandName: brandName
                            });
                            showBrandsList();
                        }
                    });
                }
            });
        }
        else {
            removeBrand(brandsList, brandName);
            removeBrand(brandChangesModel.AddedBrands, brandName);
            showBrandsList();
        }
    }

    function showBrandsList() {
        $("#modal-content").empty();
        $("#brandsTemplate").tmpl(brandsList).appendTo("#modal-content");
        $("button[data-selector='removeBtn']").click(removeBrand);
    }

    function addNewBrand() {
        var brandName = $("#newBrandName").val();
        if (checkForValidity(brandName)) {
            addToBrandsList(brandName, idCounter++, "newBrand");
            $("#newBrandName").val("");
        }
        else {
            showErrorMessage(getErrorMessage(brandName));
        }
        showBrandsList();
    }

    function removeBrand(e) {
        var object = e.target;
        removeFromBrandsList(object.getAttribute("data-name"), object.getAttribute("data-type"));
        showBrandsList();
    }

    function saveBrandsChanges() {
        console.log(JSON.stringify({ brandChangesModel }));
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
            addOptionToSelectBox(id, item.BrandId, item.BrandName);
        });
    }

    function addOptionToSelectBox(idSelectBox, key, value) {
        $(idSelectBox).append('<option value="' + key + '">' + value + "</option>");
    }

    function closeModalWindow() {
        $("#BrandModal").modal("hide");
    }

    function checkForValidity(brandName) {
        $("#newBrandValidation").empty();
        if (getErrorMessage(brandName) === "") {
            return true;
        }
        else {
            return false;
        }
    }

    function getErrorMessage(brandName) {
        var message = "";
        if (brandName === "") {
            message = "Brand name is empty.";
        }
        if (brandsList.length !== 0) {
            brandsList.forEach(function (brand) {
                if (brand.brandName === brandName) {
                    message = "Brand name already exists.";
                }
            });
        }

        if (Utils.checkValidityData(brandName)) {
            message = "Used illegal characters";
        }
        return message;
    }

    function showErrorMessage(message) {
        $("#newBrandValidation").append(message);
    }
}());
