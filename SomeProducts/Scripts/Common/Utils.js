var Utils = {};

(function () {
	var validationExp = /[\!@_%<>\$\^\[\]\+\-\/\{\}]/;
    
    Utils.getNamespace = function (namespace) {
        var obj = window[namespace];
        if (obj) {
            return obj;
        } else {
            window[namespace] = {};
            return window[namespace];
        }
    }

    Utils.checkValidityData = function(data)
    {
        console.log(validationExp.test(data));
        return validationExp.test(data);
    }
}());