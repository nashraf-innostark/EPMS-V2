$(document).ready(function () {
    var siteUrl = $('#siteURL').val();
    //#region Dashboard basic Scripts
    $(".widget-general-stats select").select2();
    var isDragActive = false;
    // Quicklaunch Widget
    $("#sortable").sortable({
        cancel: '#sortable li:last-child',
        start: function (event, ui) {
            isDragActive = true;
            $('.dashboard-quick-launch li img ').tooltip('hide');
        },
        stop: function (event, ui) {
            isDragActive = false;
        },
        containment: 'parent',
        tolerance: 'pointer'
    });

    // Make widgets sortable
    $("#photon_widgets").sortable({
        cancel: '.blank-widget, .flip-it',
        placeholder: 'dashboard-widget-placeholder',
        start: function (event, ui) {
            isDragActive = true;
            $('.widget-holder').addClass('noPerspective');
            $('.dashboard-quick-launch li img').tooltip('hide');
        },
        stop: function (event, ui, a, b) {
            var elems = document.getElementsByClassName("noPerspective");
            var arr = $.makeArray(elems);
            var widgets = new Array();
            for (var i = 0; i < arr.length; i++) {
                widgets.push(arr[i].id);
            }
            // Save Preferences
            var siteURL = $('#siteURL').val();
            var url = siteURL + "/Dashboard/SaveWidgetPreferences";
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                traditional: true,
                data: {
                    preferences: widgets
                },
                success: function (data) {
                    
                },
                error: function (e) {
                    alert('Error=' + e.toString());
                    $(".tempLoader").click();
                }
            });
            isDragActive = false;
            $('.widget-holder').removeClass('noPerspective');
            //var selectedElement = $(ui.item).attr('id');
            //alert("SelectedElement = " + selectedElement);
        },
        tolerance: 'pointer'
    });

    // Make QUickLaunchItems Sortable sortable
    $("#quickLaunchItems").sortable({
        start: function (event, ui) {
            isDragActive = true;
            $('.liItem').addClass('testClass');
            //$('.dashboard-quick-launch li img').tooltip('hide');
        },
        stop: function (event, ui, a, b) {
            var elems = document.getElementsByClassName("liItem");
            var elements = [];
            $(".liItem[id^='Menu_']").each(function() {
                var result = (this.id).replace('Menu_', '');
                elements.push(result);
            });
            // Save Preferences
            var siteURL = $('#siteURL').val();
            var url = siteURL + "/Dashboard/SaveQuickLaunchPrefrences";
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                traditional: true,
                data: {
                    preferences: elements
                },
                success: function (data) {

                },
                error: function (e) {
                    alert('Error=' + e.toString());
                    $(".tempLoader").click();
                }
            });
            isDragActive = false;
            $('.widget-holder').removeClass('noPerspective');
            //var selectedElement = $(ui.item).attr('id');
            //alert("SelectedElement = " + selectedElement);
        },
        tolerance: 'pointer'
    });


    $('.dashboard-quick-launch li img ').not('.dashboard-quick-launch li:last-child').tooltip({
        placement: 'top',
        html: true,
        trigger: 'manual',
        title: '<a href="javascript:;"><span class="right deleteTip">Delete</span></a>'
    });



    $(".predefined-icons ul li").on("click", function () {
        $(this).toggleClass("selectedPredefinedIcon");

    });

    //Delete Quick Launch Items
    $("body").delegate(".deleteTip", "click", function () {
        var aa = $(this).closest('li').attr('id');
        var result = aa.replace('Menu_', '');
        //var widgets = document.getElementById('userMenuId').innerText;
        var siteURL = $('#siteURL').val();
        var url = siteURL + "/Dashboard/DeleteItem";
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            traditional: true,
            data: {
                menuId: result
            },
            success: function (data) {

            },
            error: function (e) {
                alert('Error=' + e.toString());
                $(".tempLoader").click();
            }
        });
        $(this).closest('li').remove();
        
    });

    $("#addSelectedQuickLink").on("click", function () {
        var hml = "";
        var widgets = new Array();
        $(".predefined-icons ul li").each(function () {

            var ttis = $(this);
            var siteURL = $('#siteURL').val();
            if (ttis.hasClass("selectedPredefinedIcon")) {
                var title = ttis.find("img").attr("title");
                var img = ttis.find("img").attr('src');
                var href = ttis.find("img").attr('data-hrf');
                var ok = "ok";
                var tt12 = $(this).attr('id');
                widgets.push(tt12);
                
                
                $("#sortable li").each(function () {
                    

                    if ($(this).find("p").text() == title) {
                        ok = "no";
                        return false;
                    }
                    else {
                        ok = "ok";
                    }
                });

                //hml+="<li><img src="+img+" alt='Quick Launch Icon' data-original-title title><p>"+title+"</p></li>"
                if (ok == "ok") {
                    hml += "<li><a href=" /+siteURL +"/"+ href + "><img src=" + img + " alt='Quick Launch Icon' data-original-title title><p>" + title + "</p></a></li>"
                }

            }


        });
        var siteURL = $('#siteURL').val();
        var url = siteURL + "/Dashboard/SaveQuickLaunchItems";
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            traditional: true,
            data: {
                menuIds: widgets
            },
            success: function (data) {
                // Add new items in Quick Launch (append html)
                for (var item in data)
                {
                    var liHtml = "<li class='liItem' id=Menu_" + data[item].MenuId + "><span id='userMenuId' style='display: none'>"+ data[item].MenuId + "'</span><a href=" +siteURL + "/"+ data[item].Url + " data-toggle='modal'><img src="+siteURL+"/Images/photon/icons/" + data[item].ImagePath + " alt='predefined'></a><p>" + data[item].Title + "</p></li>";
                    $(liHtml).insertBefore('#addItemButton');
                }
                var vals = $(".predefined-icons li");
                for (var items in data) {
                    for (var li in vals) {
                        if (parseInt(vals[li].id) == data[items].MenuId) {
                            $(vals[li]).hide();
                        }
                    }
                }
            },
            error: function (e) {
                alert('Error=' + e.toString());
                $(".tempLoader").click();
            }
        });

        $("#sortable ").prepend(hml);


        $('.dashboard-quick-launch li img ').removeData('tooltip');
        $('.dashboard-quick-launch li a img ').not('.dashboard-quick-launch li:last-child').tooltip({
            placement: 'top',
            html: true,
            trigger: 'manual',
            title: '<a href="javascript:;"><span class="right deleteTip">Delete</span></a>'
        });

        var hoverTimeout;
        $('.dashboard-quick-launch li ').hover(function () {
            if (!$(this).find('.tooltip').length) {
                $activeQL = $(this);
                clearTimeout(hoverTimeout);
                hoverTimeout = setTimeout(function () {
                    if (isDragActive) return;
                    $activeQL.find('img').tooltip('show');
                }, 1000);
            }
        }, function () {
            clearTimeout(hoverTimeout);
            $('.dashboard-quick-launch li').find('img').tooltip('hide');
        });

        $("body").delegate(".deleteTip", "click", function () {
            $(this).closest('li').remove();

        });

        $(".predefined-icons ul li").each(function () {

            var tttis = $(this);

            if (tttis.hasClass("selectedPredefinedIcon")) {
                tttis.removeClass("selectedPredefinedIcon");
            }


        });
    });


    var hoverTimeout;
    $('.dashboard-quick-launch li').hover(function () {
        if (!$(this).find('.tooltip').length) {
            $activeQL = $(this);
            clearTimeout(hoverTimeout);
            hoverTimeout = setTimeout(function () {
                if (isDragActive) return;
                $activeQL.find('img').tooltip('show');
            }, 1000);
        }
    }, function () {
        clearTimeout(hoverTimeout);
        $('.dashboard-quick-launch li').find('img').tooltip('hide');
    });
    var firstHover = true;
    
    if (widgetsLoaded['task-completion']) return;
    widgetsLoaded['task-completion'] = true;
    setTimeout(function () {
        var target = parseInt($('.processed-pct .bar').attr('data-target'));
        $('.processed-pct .bar').attr('style', 'width: ' + target + '%');
        var target = parseInt($('.processed-pct2 .bar').attr('data-target'));
        $('.processed-pct2 .bar').attr('style', 'width: ' + target + '%');
    }, 1000);

    $(".task-completion select").select2();
    ProjectWidgetEvents();
    MyTasksWidgetEvents();
    $(".tempLoader").on('click', removeTempLoader);
    $("#multiFilter").select2();

    //#endregion
    
});
//#region Dashboard basic functions
$(function () {
    setTimeout(function () {
        $('.nav-fixed-topright').removeAttr('style');
    }, 300);

    $(window).scroll(function () {
        if ($('.breadcrumb-container').length) {
            var scrollState = $(window).scrollTop();
            if (scrollState > 0) $('.nav-fixed-topright').addClass('nav-released');
            else $('.nav-fixed-topright').removeClass('nav-released');
        }
    });
    $('.user-sub-menu-container').on('click', function () {
        $(this).toggleClass('active-user-menu');
    });
    $('.user-sub-menu .light').on('click', function () {
        if ($('body').is('.light-version')) return;
        $('body').addClass('light-version');
        setTimeout(function () {
            $.cookie('themeColor', 'light', {
                expires: 7,
                path: '/'
            });
        }, 500);
    });
    $('.user-sub-menu .dark').on('click', function () {
        if ($('body').is('.light-version')) {
            $('body').removeClass('light-version');
            $.cookie('themeColor', 'dark', {
                expires: 7,
                path: '/'
            });
        }
    });
});
$(function () {

    $(document).on('scroll', function () {

        if ($(window).scrollTop() > 100) {
            $('.scroll-top-wrapper').addClass('show');
        } else {
            $('.scroll-top-wrapper').removeClass('show');
        }
    });


    $(document).on('scroll', function () {

        if ($(window).scrollTop() > 100) {
            $('.scroll-top-wrapper').addClass('show');
        } else {
            $('.scroll-top-wrapper').removeClass('show');
        }
    });

    $('.scroll-top-wrapper').on('click', scrollToTop);


});
function scrollToTop() {
    verticalOffset = typeof (verticalOffset) != 'undefined' ? verticalOffset : 0;
    element = $('body');
    offset = element.offset();
    offsetTop = offset.top;
    $('html, body').animate({ scrollTop: offsetTop }, 500, 'linear');
}

//#endregion

//#region Widgets Loaders
function NoRecord(control, msg) {
    $(control).empty();
    $(".tempLoader").click();
    $(control).append('<li class="processed-pct"><span>' + msg + '</span></li>');
}
function ProjectWidgetEvents() {
    $("#progressId li.dashTask").on("click", function () {
        var target = 0;
        var progressText = this.innerText;
        if ($(this).find("a").is('[data-task]')) {
            target = parseInt($(this).find("a").attr('data-task'));
            //$('.processed-pct').find("span").text($(this).find("a").text());
        }
        else if ($(this).find("a").is('[data-overall]')) {

            target = parseInt($(this).find("a").attr('data-overall'));
            //$('.processed-pct').find("span").text($(this).find("a").text());

        }

        setTimeout(function () {

            $('.processed-pct .bar').attr('style', 'width: ' + target + '%');
            $('.processed-pct .bar').text(target + "%");
            $('span.pText').text(progressText);
        }, 200);
    });
    var count = 0;
    $("#progressId li.dashTask").each(function () {

        if (count >= 4) {
            $(this).addClass("hideIt");
        }
        count++;
    });

    if (count > 4) {
        $("#progressId .dashNext").show();
    }
    $("#progressId .dashNext").on("click", function () {
        var liCont = $("#progressId li.dashTask").length;
        var firstLiShow = 0;
        var lastLiShow = 0;
        var lastShow = 0;
        var firstShowIndex = 0;
        var lastShowIndex = 0;

        $("#progressId li").each(function () {

            if ($(this).hasClass("dashTask")) {

                if (!$(this).hasClass("hideIt")) {



                    if (firstLiShow == 0) {
                        firstShowIndex = $(this).index();
                    }
                    firstLiShow++;

                    if (lastLiShow == 3) {
                        lastShowIndex = $(this).index();
                    }
                    lastLiShow++;

                    $(this).addClass("hideIt");
                }
            }


        });
        var n = 0;
        var nextNum = lastShowIndex + 1;
        while (liCont > nextNum && n < 4) {

            $("#progressId li.dashTask").eq(nextNum).removeClass("hideIt");
            nextNum++;
            n++;
            var dd = nextNum;
            if (dd == liCont) {
                $("#progressId .dashNext").hide();
            }
        }







        $("#progressId .dashBack").show();



    });

    $("#progressId .dashBack").on("click", function () {
        var liCont = $("#progressId li.dashTask").length;
        var firstLiShow = 0;
        var lastLiShow = 0;
        var lastShow = 0;
        var firstShowIndex = 0;
        var lastShowIndex = 0;

        $("#progressId li").each(function () {

            if ($(this).hasClass("dashTask")) {

                if (!$(this).hasClass("hideIt")) {



                    if (firstLiShow == 0) {
                        firstShowIndex = $(this).index();
                    }
                    firstLiShow++;

                    if (lastLiShow == 3) {
                        lastShowIndex = $(this).index();
                    }
                    lastLiShow++;

                    $(this).addClass("hideIt");
                }
            }


        });
        var n = 0;

        var nextNum = firstShowIndex - 1;

        while ((liCont >= nextNum) && n < 4) {
            var dd = nextNum;
            $("#progressId li.dashTask").eq(nextNum).removeClass("hideIt");
            nextNum = nextNum - 1;
            n++;

            if (nextNum == 0) {
                $("#progressId .dashBack").hide();
            }
        }
        $("#progressId .dashNext").show();
    });
}
function MyTasksWidgetEvents() {
    $("#progressId2 li.dashTask").on("click", function () {
        //$(".progress")
        //if($(this).hasClass("processed-pct")){
        var target = 0;
        if ($(this).find("a").is('[data-task]')) {
            target = parseInt($(this).find("a").attr('data-task'));
            $('.processed-pct2').find("span").text($(this).find("a").text());
        }
        else if ($(this).find("a").is('[data-overall]')) {

            target = parseInt($(this).find("a").attr('data-overall'));
            $('.processed-pct2').find("span").text($(this).find("a").text());

        }

        setTimeout(function () {

            $('.processed-pct2 .bar').attr('style', 'width: ' + target + '%');
            $('.processed-pct2 .bar').text(target + "%");
        }, 200);
    });

    var count = 0;
    $("#progressId2 li.dashTask").each(function () {

        if (count >= 4) {
            $(this).addClass("hideIt");
        }
        count++;

    });

    if (count >= 4) {
        $("#progressId2 .dashNext").show();
    }
    $("#progressId2 .dashNext").on("click", function () {
        var liCont = $("#progressId2 li.dashTask").length;
        var firstLiShow = 0;
        var lastLiShow = 0;
        var lastShow = 0;
        var firstShowIndex = 0;
        var lastShowIndex = 0;

        $("#progressId2 li").each(function () {

            if ($(this).hasClass("dashTask")) {

                if (!$(this).hasClass("hideIt")) {



                    if (firstLiShow == 0) {
                        firstShowIndex = $(this).index();
                    }
                    firstLiShow++;

                    if (lastLiShow == 3) {
                        lastShowIndex = $(this).index();
                    }
                    lastLiShow++;

                    $(this).addClass("hideIt");
                }
            }


        });
        var n = 0;
        var nextNum = lastShowIndex + 1;
        while (liCont > nextNum && n < 4) {

            $("#progressId2 li.dashTask").eq(nextNum).removeClass("hideIt");
            nextNum++;
            n++;
            var dd = nextNum;
            if (dd == liCont) {
                $("#progressId2 .dashNext").hide();
            }
        }







        $("#progressId2 .dashBack").show();



    });

    $("#progressId2 .dashBack").on("click", function () {
        var liCont = $("#progressId2 li.dashTask").length;
        var firstLiShow = 0;
        var lastLiShow = 0;
        var lastShow = 0;
        var firstShowIndex = 0;
        var lastShowIndex = 0;

        $("#progressId2 li").each(function () {

            if ($(this).hasClass("dashTask")) {

                if (!$(this).hasClass("hideIt")) {



                    if (firstLiShow == 0) {
                        firstShowIndex = $(this).index();
                    }
                    firstLiShow++;

                    if (lastLiShow == 3) {
                        lastShowIndex = $(this).index();
                    }
                    lastLiShow++;

                    $(this).addClass("hideIt");
                }
            }


        });
        var n = 0;

        var nextNum = firstShowIndex - 1;

        while ((liCont >= nextNum) && n < 4) {
            var dd = nextNum;
            $("#progressId2 li.dashTask").eq(nextNum).removeClass("hideIt");
            nextNum = nextNum - 1;
            n++;

            if (nextNum == 0) {
                $("#progressId2 .dashBack").hide();
            }
        }
        $("#progressId2 .dashNext").show();
    });
}
function Loader(control) {
    if (control.className != "refresher") {
        flipit(control);
    } else {
        $(control).parents('.widget-holder').find(".tempLoader").show();
    }
}

$("#projectStatusDDL").on("change", function () {
    var projectStatus = $(this).val();
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadProjectsDDL";
    $("#projectLoader").show();
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            projectStatus: projectStatus
        },
        success: function (data) {
            populateProjectDDL(data);
        },
        error: function (e) {
            alert('Error=' + e.toString());
        }
    });
});
function populateProjectDDL(data) {
    $("#projectIdFilter").empty();
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            $("#projectIdFilter").append(
                $('<option></option>').val(data[i].ProjectId).html(data[i].NameE)
            );
        }
    } else {
        $("#projectIdFilter").append(
                $('<option></option>').val("").html("none")
            );
    }
    $("#projectLoader").hide();
}


function LoadEmployeeRequests(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var id = $("#employeeId").val();
    if (control.className == "refresher" || id == "") {
        id = 0;
    }
    var url = siteUrl + "/Dashboard/LoadEmployeeRequests";
        $.ajax({
            url: url,
            type: 'GET',
            dataType: "json",
            data: {
                employeeId: id
            },
            success: function (data) {
                $(".tempLoader").click();
                //we have searchResult and now convert it in list item form.
                $('#employeeRequests').empty();
                if (data.length > 0) {
                    $.each(data, function (itemIndex, item) {
                        if (item.IsReplied) {
                            $('#employeeRequests').append('<li><a href="' + siteUrl + '/HR/Request/Create/' + item.RequestId + '"><span title="' + item.RequestTopic + '">' + item.RequestTopicShort + '</span></a><div><span title="' + item.EmployeeNameE + '">' + item.EmployeeNameEShort + ' </span><img src="' + siteUrl + '/Images/photon/workDone.png" alt="Replied" title="Replied" class="status"></div></li>');
                        } else {
                            $('#employeeRequests').append('<li><a href="' + siteUrl + '/HR/Request/Create/' + item.RequestId + '"><span title="' + item.RequestTopic + '">' + item.RequestTopicShort + '</span></a><div><span title="' + item.EmployeeNameE + '">' + item.EmployeeNameEShort + ' </span><img src="' + siteUrl + '/Images/photon/pending.png" alt="Pending" title="Pending" class="status"></div></li>');
                        }
                        
                    });
                }
                else {
                    $(".tempLoader").click();
                    $('#employeeRequests').append('<li>No record found</li>');
                }
            },
            error: function (e) {
                alert('Error=' + e.toString());
                $(".tempLoader").click();
            }
        });
};
function LoadComplaints(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var id = $("#customerId").val();
    if (control.className == "refresher" || id == "") {
        id = 0;
    }
    var url = siteUrl + "/Dashboard/LoadComplaints";
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            customerId: id
        },
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#complaints').empty();
            if (data.length > 0) {
                $.each(data, function (itemIndex, item) {
                    if (item.IsReplied) {
                        $('#complaints').append('<li><a href="' + siteUrl + '/CMS/Complaint/Create/' + item.ComplaintId + '"><span title="' + item.Topic + '">' + item.TopicShort + '</span></a><div><span title="' + item.ClientName + '">' + item.ClientNameShort + '</span> <img src="' + siteUrl + '/Images/photon/workDone.png" alt="Replied" title="Replied" class="status"></div></li>');
                    } else {
                        $('#complaints').append('<li><a href="' + siteUrl + '/CMS/Complaint/Create/' + item.ComplaintId + '"><span title="' + item.Topic + '">' + item.TopicShort + '</span></a><div><span title="' + item.ClientName + '">' + item.ClientNameShort + '</span> <img src="' + siteUrl + '/Images/photon/notDone.png" alt="Pending" title="Pending" class="status"></div></li>');
                    }
                });
                if ($("#userRole").val() == "Customer")
                {
                    $('#complaints').append('<li><a href="' + siteUrl + '/CMS/Complaint/Create" class="btn" style="margin-left: 23%; color: black">' + $("#makeComplaint").val() + '</a></li>');
                }
            }
            else {
                $(".tempLoader").click();
                $('#complaints').append('<li>No record found</li>');
                if ($("#userRole").val() == "Customer") {
                    $('#complaints').append('<li><a href="' + siteUrl + '/CMS/Complaint/Create" class="btn" style="margin-left: 23%; color: black">' + $("#makeComplaint").val() + '</a></li>');
                }
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadOrders(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var id = $("#customerIdForOrder").val();
    var status = $("#orderStatus").val();
    if (control.className == "refresher" || id == "") {
        id = 0;
        status = 0;
    }
    var url = siteUrl + "/Dashboard/LoadOrders";
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        data: {
            customerId: id,
            orderStatus: status
        },
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#customerOrders').empty();
            if (data.length > 0) {
                $.each(data, function (itemIndex, item) {
                     switch (item.OrderStatus)
                    {
                         case 2: $('#customerOrders').append('<li><a href="' + siteUrl + '/CMS/Orders/Create/' + item.OrderId + '"><span>' + item.OrderNo + '</span></a><div>' + item.OrderDate + ' <img src="' + siteUrl + '/Images/photon/pending.png" alt="Pending" title="Pending" class="status"></div></li>');
                            break;
                         case 1: $('#customerOrders').append('<li><a href="' + siteUrl + '/CMS/Orders/Create/' + item.OrderId + '"><span>' + item.OrderNo + '</span></a><div>' + item.OrderDate + ' <img src="' + siteUrl + '/Images/photon/ongoing.png" alt="On Going" title="On Going" class="status"></div></li>');
                            break;
                         case 3: $('#customerOrders').append('<li><a href="' + siteUrl + '/CMS/Orders/Create/' + item.OrderId + '"><span>' + item.OrderNo + '</span></a><div>' + item.OrderDate + ' <img src="' + siteUrl + '/Images/photon/notDone.png" alt="Canceled" title="Canceled" class="status"></div></li>');
                            break;
                         case 4: $('#customerOrders').append('<li><a href="' + siteUrl + '/CMS/Orders/Create/' + item.OrderId + '"><span>' + item.OrderNo + '</span></a><div>' + item.OrderDate + ' <img src="' + siteUrl + '/Images/photon/workDone.png" alt="Finished" title="Finished" class="status"></div></li>');
                            break;
                    }
                });
            }
            else {
                $(".tempLoader").click();
                $('#customerOrders').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadRecruitments(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadRecruitments";
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#recruitments').empty();
            if (data.length > 0) {
                $.each(data, function (itemIndex, item) {
                    $('#recruitments').append('<li><a href="' + siteUrl + '/HR/JobOffered/Create/' + item.JobOfferedId + '"><span>' + item.TitleE + '</span></a><div>' + item.NoOfApplicants + ' </div></li>');
                });
            }
            else {
                $(".tempLoader").click();
                $('#recruitments').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadRecentEmployees(control) {
    Loader(control);
    var id = $("#departmentId").val();
    if (control.className == "refresher" || id == "") {
        id = 0;
    }
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadRecentEmployees";
    $.ajax({
        url: url,
        type: 'GET',
        data: {
            departmentId: id
        },
        dataType: "json",
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#recentEmployees').empty();
            if (data.length > 0) {
                $.each(data, function (itemIndex, item) {
                    $('#recentEmployees').append('<li><div class="avatar-image"><img src="' + item.EmployeeImagePath + '" alt="profile image" /></div><a href="' + siteUrl + '/HR/Employee/Create/' + item.EmployeeId + '"><span title="' + item.EmployeeNameE + '">' + item.EmployeeNameEShort + '</span></a><div>' + item.EmployeeJobId + ' </div></li>');
                });
            }
            else {
                $(".tempLoader").click();
                $('#recentEmployees').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadMyProfile(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadMyProfile";
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#myprofile').empty();
            if (data!=null) {
                $('#myprofile').append('<li><div class="avatar-image"><img src="' + data.EmployeeImagePath + '" alt="profile"></div><div title="'+data.EmployeeNameE+'">'+data.EmployeeNameEShort+'</div></li><li><span>Job ID</span><div>' + data.EmployeeJobId + '</div></li><li><span>Job Title</span><div>' + data.EmployeeJobTitleE + '</div></li><li><span>ID Expiry Date</span><div>' + data.EmployeeIqamaExpiryDt + '</div></li>');
            }
            else {
                $(".tempLoader").click();
                $('#myprofile').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error:' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadPayroll(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadPayroll";
    var month = $("#selectedmonth").val();
    if (control.className == "refresher") {
        month = 1;
    }
    $.ajax({
        url: url,
        type: 'GET',
        data: {
            month: month
        },
        dataType: "json",
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#mypayroll').empty();
            if (data != null) {
                $('#mypayroll').append('<li id="basicSal"><span>Basic Salary</span><div>'+data.BasicSalary+'</div></li><li id="totalAllownces"><span>Allowances</span><div>'+data.Allowances+'</div></li><li id="totalDeductions"><span>Deductions</span><div>'+data.Deductions+'</div></li><li id="totalSal"><span>Total</span><div>'+data.Total+'</div></li>');
            }
            else {
                $(".tempLoader").click();
                $('#mypayroll').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error:' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadMeetings(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var url = siteUrl + "/Dashboard/LoadMeetings";
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        success: function (data) {
            $(".tempLoader").click();
            //we have searchResult and now convert it in list item form.
            $('#meetings').empty();
            if (data.length > 0) {
                $.each(data, function (itemIndex, item) {
                    $('#meetings').append('<li><a href="' + siteUrl + '/Meeting/Meeting/Create/' + item.MeetingId + '"><span title="' + item.Topic + '">' + item.TopicShort + '</span></a><div>' + item.MeetingDate + '</div></li>');
                });
                if ($("#userRole").val() == "Admin") {
                    $('#meetings').append('<li><a href="' + siteUrl + '/Meeting/Meeting/Create" class="btn" style="margin-left: 32%; color: black">' + $("#makeMeeting").val() + '</a></li>');
                }
            }
            else {
                $(".tempLoader").click();
                $('#meetings').append('<li>No record found</li>');
                if ($("#userRole").val() == "Admin") {
                    $('#meetings').append('<li><a href="' + siteUrl + '/Meeting/Meeting/Create" class="btn" style="margin-left: 32%; color: black">' + $("#makeMeeting").val() + '</a></li>');
                }
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
};
function LoadProjects(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var id = $("#projectIdFilter").val();
    if (id != "" || control.className == "refresher") {
        if (control.className == "refresher") {
            id = 0;
        }
        var url = siteUrl + "/Dashboard/LoadProjects";
        $.ajax({
            url: url,
            type: 'GET',
            dataType: "json",
            data: {
                projectId: id
            },
            success: function (data) {
                $(".tempLoader").click();
                $('#progressId').empty();
                //$('#projectDetailId').empty();
                if (data != null) {
                    $("#projectDetailId").attr("href", "/PMS/Project/Details/" + data.Project.ProjectId);
                    if (dir == "ltr") {
                        $('#progressId').append(
                        '<li class="dashProject dashTask">' +
                        '<a data-overall=' + data.Project.ProgressTotal + ' title="' + data.Project.NameE + '">' + "Project - " + data.Project.NameEShort + '</a>' +
                        '</li>');
                    } else {
                        if (data.Project.NameA.length > 5) {
                            $('#progressId').append(
                        '<li class="dashProject dashTask">' +
                        '<a data-overall=' + data.Project.ProgressTotal + ' title="' + data.Project.NameA + '">' + "..." + "مشاريع" + " - " + data.Project.NameA.substring(0, 5) + '</a>' +
                        '</li>');
                        } else {
                            $('#progressId').append(
                        '<li class="dashProject dashTask">' +
                        '<a data-overall=' + data.Project.ProgressTotal + ' title="' + data.Project.NameA + '">' + "مشاريع" + " - " + data.Project.NameA + '</a>' +
                        '</li>');
                        }
                    }
                    var projectWidgetTitle;
                    if (dir == "ltr") {
                        projectWidgetTitle = "Project - " + data.Project.NameEShort;
                    }
                    else {
                        if (data.Project.NameA.length > 5) {
                            projectWidgetTitle = "..." + "مشاريع" + " - " + data.Project.NameA.substring(0, 5);
                        } else {
                            projectWidgetTitle = "مشاريع" + " - " + data.Project.NameA;
                        }
                    }
                    $('#projectDetailId').attr('href', $("#siteURL").val() + '/PMS/Project/Details/' + data.Project.ProjectId);
                    $('#projectDetailId').text(projectWidgetTitle);
                    if (data.ProjectTasks != null) {
                        $.each(data.ProjectTasks, function (itemIndex, item) {
                            if (dir == "ltr") {
                                $('#progressId').append(
                                '<li class="dashTask">' +
                                '<a data-task=' + item.TaskProgress + ' title="' + item.TaskNameE + '">' + item.TaskNameEShort + '</a>' +
                                '</li>');
                            } else {
                                if (item.TaskNameA.length > 15) {
                                    $('#progressId').append(
                                '<li class="dashTask">' +
                                '<a data-task=' + item.TaskProgress + ' title="' + item.TaskNameA + '">' + "..." + item.TaskNameA.substring(0, 15) + '</a>' +
                                '</li>');
                                } else {
                                    $('#progressId').append(
                                '<li class="dashTask">' +
                                '<a data-task=' + item.TaskProgress + ' title="' + item.TaskNameA + '">' + item.TaskNameA + '</a>' +
                                '</li>');
                                }
                            }
                        });
                    }
                    if (dir == "ltr") {
                        $('#progressId').append(
                    '<li class="dashBack">' +
                        '<a><i class="icon-photon arrow_left"></i></a>' +
                    '</li>' +
                    '<li class="dashNext">' +
                        '<a><i class="icon-photon arrow_right"></i></a>' +
                    '</li>' +
                    '<li class="processed-pct">' +
                        '<span class="pText" title="' + data.Project.NameE + '">' + "Project - " + data.Project.NameEShort + '</span>' +
                        '<div class="progress progress-info">' +
                            '<div class="bar" data-target=' + data.Project.ProgressTotal + ' style="width:' + data.Project.ProgressTotal + '%;">' + data.Project.ProgressTotal + '%</div>' +
                        '</div>' +
                    '</li>');
                    } else {
                        if (data.Project.NameA.length > 5) {
                            $('#progressId').append(
                    '<li class="dashBack">' +
                        '<a><i class="icon-photon arrow_left"></i></a>' +
                    '</li>' +
                    '<li class="dashNext">' +
                        '<a><i class="icon-photon arrow_right"></i></a>' +
                    '</li>' +
                    '<li class="processed-pct">' +
                        '<span class="pText" title="' + data.Project.NameA + '" style="margin-right: 33%; direction: ltr">' + "..." + "مشاريع" + " - " + data.Project.NameA.substring(0, 5) + '</span>' +
                        '<div class="progress progress-info" style="clear: both;">' +
                            '<div class="bar" data-target=' + data.Project.ProgressTotal + ' style="width:' + data.Project.ProgressTotal + '%;">' + data.Project.ProgressTotal + '%</div>' +
                        '</div>' +
                    '</li>');
                        } else {
                            $('#progressId').append(
                    '<li class="dashBack">' +
                        '<a><i class="icon-photon arrow_left"></i></a>' +
                    '</li>' +
                    '<li class="dashNext">' +
                        '<a><i class="icon-photon arrow_right"></i></a>' +
                    '</li>' +
                    '<li class="processed-pct">' +
                        '<span class="pText" title="' + data.Project.NameA + '" style="margin-right: 33%; direction: ltr">' + "مشاريع" + " - " + data.Project.NameA + '</span>' +
                        '<div class="progress progress-info" style="clear: both;">' +
                            '<div class="bar" data-target=' + data.Project.ProgressTotal + ' style="width:' + data.Project.ProgressTotal + '%;">' + data.Project.ProgressTotal + '%</div>' +
                        '</div>' +
                    '</li>');
                        }
                    }
                    
                    ProjectWidgetEvents();
                }
                else {
                    NoRecord('#progressId',"No record found");
                }
            },
            error: function (e) {
                alert('Error=' + e.toString());
                $(".tempLoader").click();
            }
        });
    } else {
        var msg = "Select a project.";
        NoRecord('#progressId', msg);
    }
};
function LoadMyTasks(control) {
    Loader(control);
    var siteUrl = $('#siteURL').val();
    var id = $("#projectIdTaskFilter").val();
   
        if (id == "" || control.className == "refresher") {
            id = 0;
        }
        var url = siteUrl + "/Dashboard/LoadMyTasks";
        $.ajax({
            url: url,
            type: 'GET',
            dataType: "json",
            data: {
                projectId: id
            },
            success: function (data) {
                $(".tempLoader").click();
                $('#progressId2').empty();
                if (data != null && data.length>0) {
                    $.each(data, function (itemIndex, item) {
                        $('#progressId2').append(
                            '<li class="dashTask">' +
                            '<a data-task=' + item.TaskProgress + ' title="' + item.TaskNameE + '">' + item.TaskNameEShort + '</a>' +
                            '</li>');
                    });
                    $('#progressId2').append(
                    '<li class="dashBack">' +
                        '<a><i class="icon-photon arrow_left"></i></a>' +
                    '</li>' +
                    '<li class="dashNext">' +
                        '<a><i class="icon-photon arrow_right"></i></a>' +
                    '</li>' +
                    '<li class="processed-pct2">' +
                        '<span title="' + data[0].TaskNameE + '">' + data[0].TaskNameEShort + '</span>' +
                        '<div class="progress progress-info">' +
                            '<div class="bar" data-target=' + data[0].TaskProgress + ' style="width:' + data[0].TaskProgress + '%;">' + data[0].TaskProgress + '%</div>' +
                        '</div>' +
                    '</li>');
                    MyTasksWidgetEvents();
                }
                else {
                    NoRecord('#progressId2', "No record found");
                }
            },
            error: function (e) {
                alert('Error=' + e.toString());
                $(".tempLoader").click();
            }
        });
};

//#endregion