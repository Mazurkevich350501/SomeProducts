var Utils = {};

(function () {
	var validationExp = /(<[\?\!\/])|(&#)/;
    
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
        return validationExp.test(data);
    }

    Utils.noty = {};
    Utils.noty.createWarningNoty = function(text)
    {
        return {
            text: text,
            layout: "topRight",
            type:"error"
        }
    }

    Utils.noty.createSuccessNoty = function(text)
    {
        return {
            text: text,
            layout: "bottomRight",
            type:"success"
        }
    }
}());