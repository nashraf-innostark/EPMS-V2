
function populateTree(id, divId, url) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            id: id
        },
        success: function (data) {
            populateTreeFromData(divId, data);
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}
function populateTreeFromData(divId, data) {
    var mainParent = $('#' + divId).jstree('get_selected');
    var parentNode;
    var newNode;
    var position = 'last';
    $.each(data, function(key, value) {
        if (value.ParentId != 0) {
            $('ul li').each(function () {
                if ($(this).attr('id') == value.ParentId) {
                    parentNode = this;
                }
            });
            if (dir == "ltr") {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.NodeTitleEn,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.NodeId },
                };
            } else {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.NodeTitleAr,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.NodeId },
                };
            }
            $('#' + divId).jstree("create_node", parentNode, position, newNode, false, false);
        } else {
            if (dir == "ltr") {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.NodeTitleEn,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.NodeId },
                };
            } else {
                newNode = {
                    state: "closed",
                    data: {
                        "title": value.NodeTitleAr,
                        "attr": { "href": "#", "onclick": "jsTreeClick(event)" }
                    },
                    "attr": { "id": value.NodeId },
                };
            }
            $('#' + divId).jstree("create_node", mainParent, position, newNode, false, false);
        }
    });

}
function populateTreeJson(url, divId, dir) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            id: 0,
            direction: dir
        },
        success: function (data) {
            var tree = JSON.parse(data);
            $("#" + divId).jstree({
                'core': {
                    'check_callback': true,
                    'data': tree
                    },
                "plugins": ["themes", "json_data", "ui", "checkbox"],
            });
            //$("#" + divId).jstree("refresh");
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}
function populateTreeJsonWTCkb(url, divId, dir) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            id: 0,
            direction: dir
        },
        success: function (data) {
            var tree = JSON.parse(data);
            $("#" + divId).jstree({
                'core': {
                    'check_callback': true,
                    'data': tree
                },
                "plugins": ["themes", "json_data", "ui" ],
            });
            //$("#" + divId).jstree("refresh");
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
}

//[
//    { "id": "GrandPa", "parent": "#", "text": "Grand Father" },
//    { "id": "Dad", "parent": "GrandPa", "text": "Dad", "state": { selected: true } },
//    { "id": "Son", "parent": "Dad", "text": "Son", "state": { selected: false } },
//    { "id": "Daughter", "parent": "Dad", "text": "Daughter", "state": { selected: true } },
//]