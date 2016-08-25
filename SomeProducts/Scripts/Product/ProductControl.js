var validationExp = /[\!@_%<>\$\^\[\]\+\-\/\{\}]/;
colorpickerItnitialization();

$("form").submit(function(){
	console.log("form");
	result = checkValidity();
	checkQuantityValue();
	if(!result){
		location.reload();
	}
	return result;
});

$('span').click(function(){
	colorpickerItnitialization();
});

$(colorpickerId).click(function(){
	console.log(colorpickerOptions);
	document.getElementById(colorpickerId).options = colorpickerOptions;
})

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
	return (inputId === imageDataId || inputId === imageTypeId);
}

function checkInputData(obj){
	return validationExp.test( $(obj).val());
}

function showValidationMessage(obj){
	$('#' + obj.id + "_Validation").append("Used illegal characters");
}

function checkQuantityValue(){
	if($('#' + quantityId).val() === ""){
		setQuantityValue();
	}
}

function setQuantityValue(){
	$('#' + quantityId).val(0);
}

function deleteProduct(){
	var id = {productId: $('#' + productId).val()};
	postRequest(JSON.stringify(id), deleteProductUrl);
}

function postRequest(data, url) { 
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: function (newUrl) {
			window.location.replace(newUrl);
        }
    });
}

function colorpickerItnitialization(){
	var id = '#' + colorpickerId;
    $(id).simplecolorpicker();
    $(id).simplecolorpicker('destroy');
    $(id).simplecolorpicker({
        picker: true
    });
}

function setCreateAndModifiedDate(){
	console.log(isEdit);
	if(isEdit){
		$('#' + modifiedDateId).val(new Date().toUTCString());
	}
	else{
		$('#' + createDateId).val(new Date().toUTCString());
	};
}

function addDateToForm(){
	setCreateAndModifiedDate();
}