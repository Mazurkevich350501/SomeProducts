var validationExp = /[\!@_%<>\$\^\[\]\+\-\/\{\}]/;
$("form").submit(function(){
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
	console.log(result);
	return result;
});

function checkInputData(obj){
	return validationExp.test( $(obj).val());
}

function showValidationMessage(obj){
	console.log($('#' + obj.id + "_Validation"));
	$('#' + obj.id + "_Validation").append("Used illegal characters");
}


	