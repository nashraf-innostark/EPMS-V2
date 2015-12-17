function getItemVariations(url, dir) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            direction: dir
        },
        success: function (data) {
            var variations = JSON.parse(data);
            itemVariations = variations;
            //$("#" + divId).jstree({
            //    'core': {
            //        'check_callback': true,
            //        'data': tree
            //    },
            //    "plugins": ["themes", "json_data", "ui"],
            //});
            //$("#" + divId).jstree("refresh");
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}
function getItemVariationDetail(id, callback) {
    $.ajax({
        url: $('#siteURL').val() + "/Api/ItemVariation/GetItemVariationDetail",
        type: 'GET',
        dataType: "json",
        data: {
            variationId: id
        },
        success: function (data) {
            if (typeof callback == "function") {
                callback(data);
            }
            $.unblockUI();
        },
        error: function (e) {
            $.unblockUI();
            alert('Error=' + e.toString());
        }
    });
}