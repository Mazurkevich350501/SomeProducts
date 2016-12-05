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
    Utils.noty.warningNoty = function(text)
    {
        noty({
            text: text,
            layout: "topRight",
            type:"error"
        });
    }

    Utils.noty.successNoty = function(text)
    {
        noty( {
            text: text,
            layout: "bottomRight",
            type:"success"
        });
    }

    Utils.postRequest = function (data, url, successf, errorf, successNotyMessage, warningNotyMessage) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: function(result){
                if(successf !== undefined && successf !== null){
                    successf(result);
                }
                if(successNotyMessage !== "" && successNotyMessage !== undefined){
                    Utils.noty.successNoty(successNotyMessage);
                }
            },
            error: function(result){
                if(errorf !== undefined && errorf !== null){
                    errorf(result);
                }
                if(warningNotyMessage !== "" && warningNotyMessage !== undefined){
                    Utils.noty.warningNoty(warningNotyMessage);
                }
            }
        });
    }
}());