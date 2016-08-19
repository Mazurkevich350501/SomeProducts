var brandChangesModel = {
    RemovedBrands: [],
    AddedBrands: []
};
var idCounter;
$('document').ready(function () {
    $('#brandEditBtn').click(function () {
        $('#modal-content').remove();
        console.log("asdasda");
        brandChangesModel.RemovedBrands = [];
        brandChangesModel.AddedBrands = [];
        idCounter = 1;
        $.getJSON(getBrandsListUrl, function (brands) {
            $('#modal-body').append("<div id='modal-content'/>");
            brands.forEach(function (item, i, brands) {
                showBrand(item.BrandId, item.BrandName);
            })

        })
    })  
})



function removeBrand(obj) {
    var id = obj.getAttribute('id').substring(4);
    if (id.substring(0, 1) == "0") {
         brandChangesModel.AddedBrands.find(function (element, index, array) {
            if (element.brandId == id) {
                array.splice(index, 1);
                return true;
            }
            else {
                return false;
            }
        })
        $('#row-' + id).remove();
    }
    else {
        $.getJSON(isBrandUsingUrl + '/' + id, function (isUsing) {
            if (!isUsing) {
                addBrandTolist(id, $('#lbl-' + id).text(), brandChangesModel.RemovedBrands);
                $('#row-' + id).remove();
            }
        });
    }  
}

function addNewBrand() {
    if(checkForValidity(brandName)){
        addBrandTolist(0, brandName,  brandChangesModel.AddedBrands);
        addBrandTolist(0, brandName, brandsList);
        showBrand('0' + idCounter, document.getElementById("newBrandName").value);
        document.getElementById("newBrandName").value = '';
        idCounter++;    
    }
    else{
        showErrorMessage(getErrorMessage(brandName));
    }
}

function showBrand(brandId, brandName){
    var rowId = "#row-" + brandId;
    $('#modal-content').append("<div class='row' id='row-" + brandId + "'/>");
    $(rowId).append("<h4 class='col-lg-10' id='lbl-" + brandId + "'>" + brandName + "</h4>");
    $(rowId).append("<button class='btn btn-warning glyphicon glyphicon-minus-sign'onclick='removeBrand(this)' id='btn-" + brandId + "'/>");
}

function addBrandTolist(id, name, list) {
    var brand = {};
    brand.brandId = id;
    brand.brandName = name;
    list.push(brand);
}

function saveBrandsChanges() {
    postJSONData(JSON.stringify(brandChangesModel), saveBrandsChangesUrl);
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

function setBrandDropdownItems(items)
{
    var id = "#" + brandDropdownId;
    $(id).find('option').remove();
    items.forEach(function(item, index, array){
        addOptionToSelectBox(id, item.BrandId, item.BrandName);
    })
}

function addOptionToSelectBox(idSelectBox, key, value){
    $(idSelectBox).append('<option value="' + key + '">' + value + '</option>');
}

function closeModalWindow(){
    console.log('asddd');
    $('#BrandModal').modal("hide");
}

function checkForValidity(brandName){
    $('#newBrandValidation').empty();
    if(getErrorMessage(brandName) == ""){
        return true;
    }
    else{
        return false;
    }
}

function getErrorMessage(brandName){
    var message = "";
    if(brandName == "") {
        message = "brand name is empty";
    }
    brandsList.forEach(function(brand, index, array){
        if(brand.brandName == brandName){
            message = "brand name already exists";
        } 
    });
    console.log(message);
    return message;
}

function showErrorMessage(message){
    console.log(message);
    $('#newBrandValidation').append(message);
}