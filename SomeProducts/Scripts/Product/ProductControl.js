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
		if(checkInputData(this)){
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