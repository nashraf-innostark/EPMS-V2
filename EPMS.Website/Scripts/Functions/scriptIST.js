function addToCart(id) {
    var quantity = jQuery('#Quantity').val();
    if (quantity == 0 || quantity == "" || quantity == undefined) {
        quantity = 1;
    }
    var sizeId;
    $.each(productSize, function (key, value) {
        if (value.ProductId == id) {
            sizeId = value.SizeId;
        }
    });
    if (sizeId == undefined || sizeId == "") {
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
            if (data.response == "OK") {
                $(".shopping-cart").removeClass('cart_details_no_item');
                $(".shopping-cart").addClass('cart_details');
                var checkOutBtn = '<a href="../../ShoppingCart/Index" class="checkout">Checkout<span class="icon-chevron-right"></span></a>';
                var shoppingcart = 'Number of items ' + data.count;
                $(".shopping-cart").text(shoppingcart);
                $(".shopping-cart").append(checkOutBtn);
                new PNotify({
                    title: 'Added',
                    text: "\n" + data.itemName + ' has been successfully added to your Cart.',
                    delay: 1000
                });
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}