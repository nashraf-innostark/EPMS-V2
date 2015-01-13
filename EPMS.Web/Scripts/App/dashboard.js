$(document).ready(function () {
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
        stop: function (event, ui) {
            isDragActive = false;
            $('.widget-holder').removeClass('noPerspective');
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

    $("body").delegate(".deleteTip", "click", function () {
        $(this).closest('li').remove();

    });

    $("#addSelectedQuickLink").on("click", function () {
        var hml = "";

        $(".predefined-icons ul li").each(function () {

            var ttis = $(this);

            if (ttis.hasClass("selectedPredefinedIcon")) {
                var title = ttis.find("img").attr("title");
                var img = ttis.find("img").attr('src');
                var href = ttis.find("img").attr('data-hrf');
                var ok = "ok"
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
                    hml += "<li><a href=" + href + "><img src=" + img + " alt='Quick Launch Icon' data-original-title title><p>" + title + "</p></a></li>"
                }

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
    setTimeout(function () {
        $.pnotify({
            title: 'Drag & Drop',
            type: 'info',
            text: 'Reorder Widgets or Quicklaunch bar items by dragging & dropping them.'
        });
    }, 7000);
    setTimeout(function () {
        $.pnotify({
            title: 'Welcome',
            type: 'info',
            text: $("#SessionUserName").val()
        });
    }, 2000);
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

    $("#progressId li.dashTask").on("click", function () {
        //$(".progress")
        //if($(this).hasClass("processed-pct")){
        var target = 0;
        if ($(this).find("a").is('[data-task]')) {
            target = parseInt($(this).find("a").attr('data-task'));
            $('.processed-pct').find("span").text($(this).find("a").text());
        }
        else if ($(this).find("a").is('[data-overall]')) {

            target = parseInt($(this).find("a").attr('data-overall'));
            $('.processed-pct').find("span").text($(this).find("a").text());

        }

        setTimeout(function () {

            $('.processed-pct .bar').attr('style', 'width: ' + target + '%');
            $('.processed-pct .bar').text(target + "%");
        }, 200);
    });


    $(".tempLoader").on('click', removeTempLoader);

    var count = 0;
    $("#progressId li.dashTask").each(function () {

        if (count >= 4) {
            $(this).addClass("hideIt");
        }
        count++;
    });

    if (count >= 4) {
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
    
    count = 0;
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
//#region Employee Requests Loaders
function Loader(control) {
    if (control.className != "refresher") {
        flipit(control);
    } else {
        $(control).parents('.widget-holder').find(".tempLoader").show();
    }
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
                            $('#employeeRequests').append('<li><a href="/HR/Request/Create/' + item.RequestId + '"><span>' + item.RequestTopic + '</span></a><div>' + item.EmployeeNameE + '<img src="/Images/photon/workDone.png" alt="Read" title="Read" class="status"></div></li>');
                        } else {
                            $('#employeeRequests').append('<li><a href="/HR/Request/Create/' + item.RequestId + '"><span>' + item.RequestTopic + '</span></a><div>' + item.EmployeeNameE + '<img src="/Images/photon/notDone.png" alt="Read" title="Read" class="status"></div></li>');
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
                        $('#complaints').append('<li><a href="/CMS/Complaint/Create/' + item.ComplaintId + '"><span>' + item.Topic + '</span></a><div>' + item.ClientName + '<img src="/Images/photon/workDone.png" alt="Read" title="Read" class="status"></div></li>');
                    } else {
                        $('#complaints').append('<li><a href="/CMS/Complaint/Create/' + item.ComplaintId + '"><span>' + item.Topic + '</span></a><div>' + item.ClientName + '<img src="/Images/photon/notDone.png" alt="Read" title="Read" class="status"></div></li>');
                    }
                });
            }
            else {
                $(".tempLoader").click();
                $('#complaints').append('<li>No record found</li>');
            }
        },
        error: function (e) {
            alert('Error=' + e.toString());
            $(".tempLoader").click();
        }
    });
    //} else {
    //    $(".tempLoader").click();
    //}
};
//#endregion
//#endregion