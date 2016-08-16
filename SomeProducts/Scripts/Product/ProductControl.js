var removeList = [];
$('document').ready(function () {
    $('#brandEditBtn').click(function () {
        $('#modal-content').remove();
        renoveList = [];
        $.getJSON('GetBrandsList', function (brands) {
            $('#modal-body').append("<div id='modal-content'/>");
            brands.forEach(function (item, i, brands) {
                var id = "#row-" + item.BrandId;
                $('#modal-content').append("<div class='row' id='row-" + item.BrandId + "'/>");
                $(id).append("<h4 class='col-lg-10' id='lbl-" + item.BrandId + "'>" + item.BrandName + "</h4>");
                $(id).append("<button class='btn btn-warning glyphicon glyphicon-minus-sign'onclick='brandRemove(this)' id='btn-" + item.BrandId + "'/>");
            })

        })
    })
         
})

function brandRemove(obj) {
    var id = obj.getAttribute('id').substring(4);
    $.getJSON('IsBrandUsing/' + id, function (isUsing) {
        if(!isUsing)
        {
            $('#row-' + id).remove();
            removeList.push(id);
        }
    });
}
function addNewBrand() {
    id = 99;
    var rowId = "#row-" + id;
    $('#modal-content').append("<div class='row' id='row-" + id + "'/>");
    $(rowId).append("<h4 class='col-lg-10' id='lbl-" + id + "'>" + document.getElementById("newBrandName").value + "</h4>");
    $(rowId).append("<button class='btn btn-warning glyphicon glyphicon-minus-sign'onclick='brandRemove(this)' id='btn-" + id + "'/>");
    document.getElementById("newBrandName").value = '';
}

function saveBrandsChanges() {

}