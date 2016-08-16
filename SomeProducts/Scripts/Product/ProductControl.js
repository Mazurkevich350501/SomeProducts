$('document').ready(function () {
    $('#brandEditBtn').click(function () {
        $.getJSON('GetBrandsList', function (result) {
            console.log(result);
            for (var i = 0; i < 10; i++) {
                $('#modal-body').append("<p>asdasd</p>");
            }
        })
        
    })
})

function deleteBrand(brandId)
{

}