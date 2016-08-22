var validationExp = /[\!@_%<>\$\^\[\]\+\-\/\{\}]/;

$("form").submit(function(){
	result = checkValidity();
	checkQuantityValue();
	return result;
});

function checkValidity(){
	var result = true;
	$('span').empty();
	$('input').each(function() {
		if(checkInputData(this) && !isFileInput(this.id)){
			console.log(this.id);
			showValidationMessage(this);
			result = false;
		}	
	});
	$('textarea').each(function() {
		if(checkInputData(this)){
			showValidationMessage(this);
			result = false;
		}	
	});
	return result;
}

function isFileInput(inputId){
	return (inputId == imageDataId || inputId == imageTypeId);
}

function checkInputData(obj){
	return validationExp.test( $(obj).val());
}

function showValidationMessage(obj){
	$('#' + obj.id + "_Validation").append("Used illegal characters");
}

function checkQuantityValue(){
	if($('#' + quantityId).val() == ""){
		setQuantityValue();
	}
}

function setQuantityValue(){
	$('#' + quantityId).val(0);
}

function deleteProduct(){
	var data =  $('#' + productId).val();
	postRequest(JSON.stringify(data), deleteProductUrl);
}

function postRequest(data, url) { 
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: function (data) {
            console.log(123);
        }
    });
}