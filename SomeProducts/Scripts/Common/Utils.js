var Utils = {};

(function () {
    Utils.getNamespace = function (namespace) {
        var obj = window[namespace];
        if (obj) {
            return obj;
        } else {
            window[namespace] = {};
            return window[namespace];
        }
    }
}());