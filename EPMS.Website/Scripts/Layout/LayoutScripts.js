jQuery(document).ready(function ($) {
    // Notification
    debugger;
    var message = jQuery("#Message").val();
    var isSaved = jQuery("#IsSaved").val();
    var isUpdated = jQuery("#IsUpdated").val();
    var isError = jQuery("#IsError").val();
    var isInfo = jQuery("#IsInfo").val();
    if (isSaved || isUpdated) {
        new PNotify({
            title: 'Success',
            text: message
        });
    }
    else if(isError) {
        new PNotify({
            title: 'Error',
            text: message
        });
    }
    else if (isInfo) {
        new PNotify({
            title: 'Info',
            text: message
        });
    }

    jQuery("a[data-rel^='prettyPhoto'], .prettyphoto_link").prettyPhoto({ theme: 'pp_kalypso', social_tools: false, deeplinking: false });
    jQuery("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'pp_kalypso' });
    jQuery("a[data-rel^='prettyPhoto[login_panel]']").prettyPhoto({ theme: 'pp_kalypso', default_width: 800, social_tools: false, deeplinking: false });

    jQuery(".prettyPhoto_transparent").click(function (e) {
        e.preventDefault();
        jQuery.fn.prettyPhoto({ social_tools: false, deeplinking: false, show_title: false, default_width: 980, theme: 'pp_kalypso transparent', opacity: 0.95 });
        jQuery.prettyPhoto.open($(this).attr('href'), '', '');
    });

    // THIS SCRIPT DETECTS THE ACTIVE ELEMENT AND ADDS ACTIVE CLASS
    var pathname = window.location.pathname,
            page = pathname.split(/[/ ]+/).pop(),
            menuItems = $('#main_menu a');
    menuItems.each(function () {
        var mi = $(this),
            miHrefs = mi.attr("href"),
            miParents = mi.parents('li');
        if (page == miHrefs) {
            miParents.addClass("active").siblings().removeClass('active');
        }
    });
});

function ppOpen(panel, width) {
    jQuery.prettyPhoto.close();
    setTimeout(function () {
        
        jQuery.fn.prettyPhoto({ social_tools: false, deeplinking: false, show_title: false, default_width: width, theme: 'pp_kalypso' });
        jQuery.prettyPhoto.open(panel);
    }, 300);
}

$(document).ready(function () {
    (function ($) {
        // ** partners carousel
        $('#partners_carousel').carouFredSel({
            responsive: true,
            scroll: 1,
            auto: false,
            items: {
                width: 250,
                visible: {
                    min: 3,
                    max: 10
                }
            },
            prev: {
                button: ".partners_carousel .prev",
                key: "left"
            },
            next: {
                button: ".partners_carousel .next",
                key: "right"
            }
        });
        // *** end partners carousel
        // slider
        $('.iosSlider').iosSlider({
            snapToChildren: true,
            desktopClickDrag: true,
            keyboardControls: true,
            navNextSelector: $('.next'),
            navPrevSelector: $('.prev'),
            navSlideSelector: $('.selectors .item'),
            scrollbar: true,
            scrollbarContainer: '#slideshow .scrollbarContainer',
            scrollbarMargin: '0',
            scrollbarBorderRadius: '4px',
            onSlideComplete: slideComplete,
            onSliderLoaded: function (args) {
                var otherSettings = {
                    hideControls: true, // Bool, if true, the NAVIGATION ARROWS will be hidden and shown only on mouseover the slider
                    hideCaptions: false  // Bool, if true, the CAPTIONS will be hidden and shown only on mouseover the slider
                }
                sliderLoaded(args, otherSettings);
            },
            onSlideChange: slideChange,
            infiniteSlider: true,
            autoSlide: true
        });
    })(jQuery);
})