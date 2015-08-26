﻿function addToCart(id) {
    var quantity = jQuery('#Quantity').val();
    if (quantity == "" || quantity == undefined) {
        quantity = 0;
    }
    var sizeId = jQuery('#Size').val();
    if (sizeId == "" || sizeId == undefined) {
        sizeId = 0;
    }
    var url = siteUrl + "/ShoppingCart/AddToCart";
    jQuery.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        traditional: true,
        data: {
            productId: id,
            sizeId: sizeId,
            quantity: quantity
        },
        success: function (data) {
            if (data == "Success") {
                $.pnotify({
                    title: 'Success',
                    type: 'info',
                    text: 'Item has been successfully added to your Cart.'
                });
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}