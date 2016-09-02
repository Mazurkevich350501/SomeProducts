(function() {
    "use strict";

    var brandsList;
    var brandChangesModel = {
        RemovedBrands: [],
        AddedBrands: []
    };
    var idCounter;
    var params = {};

    var modalWindowNamespace = Utils.getNamespace("ModalWindow");
    modalWindowNamespace.initModalWindow = function(newParams){
        params = newParams;
    }

    $("button[id='saveBtnId']").click(function(){
        saveBrandsChanges();
    });

    $("button[id='addBtnId']").click(function(){
        addNewBrand();
    });

    function startInit(){
        createBrandsDiv();
        brandChangesModel.RemovedBrands = [];
        brandChangesModel.AddedBrands = [];
        brandsList =[];
        idCounter = 1;
    }

    function createBrandsDiv(){
        $('#newBrandValidation').empty();
        $('#modal-content').remove();       
        $('#modal-body').append("<div id='modal-content'/>");
    }

    $('document').ready(function () {
        $('#brandEditBtn').click(function () {
            $.getJSON(params.url.getBrandsListUrl, function (brands) {
                startInit();
                brands.forEach(function (item, i, brands) {
                    addBrandTolist(item.BrandId, item.BrandName, brandsList);
                    showBrand(item.BrandId, item.BrandName, "dbBrand");
                })
            })
        }) 
    })

    function removeBrand(obj) {
        var id = obj.getAttribute('id').substring(4);
        var name = obj.getAttribute('data-name');
        console.log(obj.getAttribute('data-info'));
        if (obj.getAttribute('data-info') === "newBrand") {
            removeBrandFromList(name, brandChangesModel.AddedBrands);
            removeBrandFromList(name, brandsList);  
            $('#row-' + id).remove();
        }
        else {
            var isRemove;
            $.getJSON(params.url.isBrandUsingUrl + '/' + id, function (isUsing) {
                if (!isUsing) {
                    addBrandTolist(id, $('#lbl-' + id).text(), brandChangesModel.RemovedBrands);
                    removeBrandFromList(name, brandsList);  
                    $('#row-' + id).remove();
                }
            });
        }
    }

    function addNewBrand() {
        var brandName = document.getElementById("newBrandName").value;
        if(checkForValidity(brandName)){
            addBrandTolist(0, brandName,  brandChangesModel.AddedBrands);
            addBrandTolist(0, brandName, brandsList);
            showBrand('0' + idCounter, document.getElementById("newBrandName").value, "newBrand");
            document.getElementById("newBrandName").value = '';
            idCounter++;    
        }
        else{
            showErrorMessage(getErrorMessage(brandName));
        }
    }

    function showBrand(brandId, brandName, info){
        var rowId = "#row-" + brandId;
        $('#modal-content').append("<div class='row' id='row-" + brandId + "'/>");
        $(rowId).append("<h4 class='col-lg-10' id='lbl-" + brandId + "'>" + brandName + "</h4>");
        $(rowId).append("<button class='btn btn-warning glyphicon glyphicon-minus-sign' data-info='" + info + "' data-name='"+ brandName +"' id='btn-" + brandId + "'/>");
        $("button[id='btn-" + brandId + "']").click(function(){
            removeBrand(this);
        });
    }

    function addBrandTolist(id, name, list) {
        var BrandModel = {};
        BrandModel.BrandId = id;
        BrandModel.BrandName = name;
        list.push(BrandModel);
    }

    function removeBrandFromList(name, list){
        list.find(function (element, index, array) {
            if (element.BrandName === name) {
                array.splice(index, 1);
                return true;
            }
            else {
                return false;
            }
        })
    }

    function saveBrandsChanges() {
        console.log(JSON.stringify(brandChangesModel));
        postJSONData(JSON.stringify(brandChangesModel), params.url.saveBrandsChangesUrl);
    }

    function postJSONData(JSONData, url) { 
        $.ajax({
            type: 'POST',
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSONData,
            success: function (data) {
                setBrandDropdownItems(data);        
                closeModalWindow();
            }   
        });
    }

    function setBrandDropdownItems(items){
        var id = "#" + params.id.brandDropdownId;
        $(id).find('option').remove();
        items.forEach(function(item, index, array){
            addOptionToSelectBox(id, item.BrandId, item.BrandName);
        })
    }

    function addOptionToSelectBox(idSelectBox, key, value){
        $(idSelectBox).append('<option value="' + key + '">' + value + '</option>');
    }

    function closeModalWindow(){
        $('#BrandModal').modal("hide");
    }

    function checkForValidity(brandName){
        $('#newBrandValidation').empty();
        if(getErrorMessage(brandName) === ""){
            return true;
        }
        else{
            return false;
        }
    }

    function getErrorMessage(brandName){
        var message = "";
        if(brandName === "") {
            message = "Brand name is empty.";
        }
        brandsList.forEach(function(brand, index, array){
            if(brand.BrandName === brandName){
                message = "Brand name already exists.";
            } 
        });
        if(/[\!@_%<>\$\^\[\]\+\-\/\{\}]/.test(brandName)){
            message = "Used illegal characters";
        }
        return message;
    }

    function showErrorMessage(message){
        $('#newBrandValidation').append(message);
    }
}());
