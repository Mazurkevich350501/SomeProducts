$('document').ready(function () {
    $('#brandEditBtn').click(function () {
        $.getJSON('GetBrandsList', function (brands) {
            console.log(brands);
            brands.forEach(function(item, i, brands){
                $('#modal-body').append("<div class='row'>")
                $('#modal-body').append("<h4 class='col-lg-10' id='lbl-" + item.BrandId + "'>" + item.BrandName + "</h4>");
                $('#modal-body').append("<button class='btn btn-warning glyphicon glyphicon-minus-sign' onclick='brandRemove(this)' id='btn-" + item.BrandId + "'/>");
                $('#modal-body').append("</div>");
            })
            
        })
        
    })

    function brandRemove(obj)
    {
        obj.get
    }
})
