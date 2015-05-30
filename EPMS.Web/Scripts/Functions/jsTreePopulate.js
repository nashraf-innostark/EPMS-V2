var siteUrl = $('#siteURL').val();
function populateTree(id, divId, url) {
    $.ajax({
        url: siteUrl + url,
        type: 'GET',
        dataType: "json",
        data: {
            id: id
        },
        success: function (data) {
            populateTreeFromData(divId, data);
        },
        error: function (e) {
            $.unblockUI();
            alert('Error=' + e.toString());
        }
    });
}
function populateTreeFromData(divId, data) {
    var mainParent = $('#' + divId).jstree('get_selected');;
    var parentNode;
    var newNode;
    $.each(data, function(key, value) {
        if (value.ParentId != null) {
            $('ul li').each(function () {
                if ($(this).attr('id') == value.ParentId) {
                    parentNode = this;
                }
            });
            if (dir == "ltr") {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.DepartmentNameEn,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.DepartmentId },
                };
            } else {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.DepartmentNameAr,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.DepartmentId },
                };
            }
            $('#jstree').jstree("create_node", parentNode, position, newNode, false, false);
        }
    });

}