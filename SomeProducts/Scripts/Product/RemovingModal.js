(function(){
    var params = {};
    
    var RemovingModalNamespace = Utils.getNamespace("RemovingModal");
    RemovingModalNamespace.initRemovingModal = function(newParams){
        params = newParams;
    }

    $("button[id='removeBtnId']").click(function(){
        deleteProduct();
    });

    $("button[id='cancelDeleteBtnId']").click(function(){
       
        $("#RemovingModal").modal("hide");
    });

    function deleteProduct() {
        var id = { productId: $("#" + params.id.productId).val() };
        postRequest(JSON.stringify(id), params.url.deleteProductUrl);
    }

    function postRequest(data, url) {
    $.ajax({
            type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: function (newUrl) {
            window.location.replace(newUrl);
        }
    });
    } 
}());