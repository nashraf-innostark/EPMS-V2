

var domainroot="http://masar.pro";

var hasChaser = 1;			// Enable Chaser menu (open on scroll) ?   1 - Yes / 0 - No

;(function($){
	
	if(hasChaser == 1) {
		$(window).load(function(){
			var doc = $(document), 
				win = $(window), chaser, forch,
				forchBottom, visible;
			function shown() { visible = true; }
			function hidden() { visible = false; }
			chaser = $('#main_menu ul.sf-menu').clone().hide()
				.appendTo(document.body)
				.wrap("<div class='chaser'><div class='container'><div class='row'><div class='span12'></div></div></div></div>");
			forch = $('#content').first();
			forchBottom = forch.offset().top + 2;
			hidden();
			win.on('scroll', function () {
				var top = doc.scrollTop();
				if (!visible && top > forchBottom) {
					//chaser.slideDown(300, shown);
					chaser.fadeIn(300, shown);
				} else if (visible && top < forchBottom) {
					//chaser.slideUp(200, hidden);
					chaser.fadeOut(200, hidden);
				}
			});
			/* Activate Superfish Menu for Chaser */
			$('.chaser ul.sf-menu').supersubs({ minWidth: 12, maxWidth: 27, extraWidth: 1}).superfish({delay:250, dropShadows:false, autoArrows:true, speed:300});
		});
	}

	$(document).ready(function(e) {
		
		// activate placeholders for older browsers
        $('input, textarea').placeholder();
		
		/* Activate Superfish Menu */
		var sfDelay = 200;
		if($('html').hasClass('isie'))
			sfDelay = 300;
		$('#main_menu > ul')
		.supersubs({ 
            minWidth:    12,   // minimum width of sub-menus in em units 
            maxWidth:    27,   // maximum width of sub-menus in em units 
            extraWidth:  1     // extra width can ensure lines don't sometimes turn over 
        }).superfish({
			delay:sfDelay,
			dropShadows:false,
			autoArrows:true,
			speed:300
		}).mobileMenu({
			switchWidth: 960,
			topOptionText: document.mobileMenuText,
			indentString: '&nbsp;&nbsp;&nbsp;'
		});
		
		// --- search panel
		var searchBtn = $('#search').children('.searchBtn'),
			searchPanel = searchBtn.next(),
			searchP = searchBtn.parent();
		searchBtn.click(function(e){
			e.preventDefault();
			var _t = $(this);
			if(!_t.hasClass('active')) {
				_t.addClass('active')
				.find('span')
				.removeClass('icon-search icon-white')
				.addClass('icon-remove');
				searchPanel.show();
			} else {
				_t.removeClass('active')
				.find('span')
				.addClass('icon-search icon-white')
				.removeClass('icon-remove');
				searchPanel.hide();
			}
		}); // searchBtn.click //
		$(document).click(function(){
			searchBtn.removeClass('active')
				.find('span')
				.addClass('icon-search icon-white')
				.removeClass('icon-remove');
			searchPanel.hide(0);
		});
		searchP.click(function(event){
			event.stopPropagation();
		});
		// --- end search panel
		
		/* sliding panel toggle (support panel) */
		var sliding_panel = $('#sliding_panel');
		$('#open_sliding_panel').toggle(function(e){
			e.preventDefault();
			sliding_panel.animate({  height:130 }, {duration:100, queue:false, easing:'easeOutQuint'});
			$(this).addClass('active');
		}, function(){
			sliding_panel.animate({  height:0 }, {duration:100, queue:false, easing:'easeOutQuint'});
			$(this).removeClass('active');
		});
		// --- end sliding panel
		
		/* scroll to top */
        function totop_button(a) {
            var b = $("#totop");
            b.removeClass("off on");
            if (a == "on") { b.addClass("on") } else { b.addClass("off") }
        }
        window.setInterval(function () {
            var b = $(this).scrollTop();
            var c = $(this).height();
            if (b > 0) { var d = b + c / 2 } else { var d = 1 }
            if (d < 1e3) { totop_button("off") } else { totop_button("on") }
        }, 300);
		
        $("#totop").click(function (e) {
            e.preventDefault();
            $('body,html').animate({scrollTop:0},800, 'easeOutExpo');
        });
		// --- end scroll to top
		
		// activate tooltips
		$('*[data-rel="tooltip"], *[rel="tooltip"]').tooltip();
		
		//activate collapsible accordions
		$(".collapse").collapse();

		
		
		
		// PREPARE THE NEWSLETTER AND SEND DATA TO MAILCHIMP
		$('#newsletter_subscribe').submit(function() {
			$.ajax({
				url: 'php_helpers/newsletter_subscribe.php',
				type: 'POST',
				data: {
					email: $('#nl-email').attr('value'),
					yname: 'Kalypso Template User' // Change here with something different
				},
				success: function(data){
					$('#result').html(data).css('color', 'green');
				},
				error: function() {
					$('#result').html('Sorry, an error occurred.').css('color', 'red');
				}
			});
			return false;
		});
		

	
	
	
	
	
	
	
	/* Set Carousels */
	function installCarousels(){
		
		$('.owl-carousel').each(function(){
		
			/* Max items counting */
			var max_items = $(this).attr('data-max-items');
			var tablet_items = max_items;
			if(max_items > 1){
				tablet_items = max_items - 1;
			}
			var mobile_items = 1;
			
			
			/* Install Owl Carousel */
			$(this).owlCarousel({
				
				items:max_items,
				pagination : false,
				itemsDesktop : [1199,max_items],
				itemsDesktopSmall : [1000,max_items],
				itemsTablet: [920,tablet_items],
				itemsMobile: [560,mobile_items],
			});
		
			
			var owl = $(this).data('owlCarousel');
			
			/* Arrow next */
			$(this).parent().parent().find('.icon-left-dir').click(function(e){
				owl.prev();
			});
			
			/* Arrow previous */
			$(this).parent().parent().find('.icon-right-dir').click(function(e){
				owl.next(); 
			});
			
		});
		
	}
	
	});	// doc.ready end //
	
	$(window).load(function(){
		
		/*
		* ### HOW TO CREATE A VALID ID TO USE: ### 
		* Go to www.twitter.com and sign in as normal, go to your settings page. 
		* Go to Widgets on the left hand side. 
		* Create a new widget for what you need eg user timeline or search etc.  
		* Feel free to check exclude replies if you dont want replies in results. 
		* Now go back to settings page, and then go back to widgets page, you should 
		* see the widget you just created. Click edit. 
		* Now look at the URL in your web browser, you will see a long number like this: 
		* 345735908357048478 
		* Use this as your ID below instead! 
		*/
		//twitterFetcher.fetch('000000000000000', 'twitterFeed', 1, true, false);
		
		if($('body').hasClass('slider_fixed')) {
			
			//$('.slider_fixed #slideshow').
			$('.inner-page').css({'top':(parseInt($('.slider_fixed #slideshow .item').first().css('height'))-10)+'px', 'background-color': $('body').css('background-color')});
		}
		var pageLoading = $("#page-loading");
		if(pageLoading.length > 0){
			setTimeout(function() {
				pageLoading.fadeOut();
			}, 1000);
		}
		
		$('.hoverBorder').each(function(index, element) {
            $(this).find('img').wrap('<span class="hoverBorderWrapper"/>').after('<span class="theHoverBorder"></span>');
        });
		
		//grayscale effect
	    var grayHover = $('.grayHover'),
	        grayHoverImg = grayHover.find('img'),
	        spd = 200;
		
	    grayHoverImg.each(function(){
	        var el = $(this);
			el.parent().css({width:el.width(), height:el.height()});
	        el.clone().addClass('grayscale').css({"z-index":"998","opacity":"0"}).insertBefore(el).queue(function(){
	            var el = $(this);
	            el.dequeue();
	        });
	        this.src = grayscale(this.src);
	        el.css({"top":"-"+ el.height() +"px"});
	    });
	    grayHover.mouseover(function(){
	        $(this).find('img:first').animate({opacity:1}, {queue:false, duration: spd});
	    });
	    grayHover.mouseout(function(){
	        $(this).find('img.grayscale').animate({opacity:0}, {queue:false, duration: spd});
	    }); // end grayscale efect
		
		// hover effect
		$('.hover_effect').each(function(){
			var hoverLink = $(this),
				hoverLinkImg = hoverLink.find('img'),
				hoverLinkTitle = hoverLink.attr('title');
			hoverLink.css({'width':hoverLinkImg.width(), 'height':hoverLinkImg.height()}).append("<span class=\"title\">"+hoverLinkTitle+"</span>");
		});
		
		//hoverlink
		$("a.hoverLink").each(function(index, element) {
            var $t = $(this),
				dtype = $t.data('type'),
				img = $t.find('img'),
				sp = 'fast',
				app = '<span class="icon_wrap"><span class="icon '+dtype+'"></span></span>';
			$t.append(app);
			
			$t.hover(function(){
				img.animate({'opacity': 0.5}, sp);
				$t.find('.icon_wrap').animate({'opacity': 1}, sp);
			}, function(){
				img.animate({'opacity': 1}, sp);
				$t.find('.icon_wrap').animate({'opacity': 0}, sp);
			});
        });
		
	});
	
})(jQuery);

var sP = jQuery.noConflict(),
	sparkles_container = sP(document.getElementById("sparkles"));
var Spark=function(){var a=this;this.b="/images/sparkles/";this.s=["spark.png","spark2.png","spark3.png","spark4.png","spark5.png","spark6.png"];this.i=this.s[this.random(this.s.length)];this.f=this.b+this.i;this.n=document.createElement("img");this.newSpeed().newPoint().display().newPoint().fly()};Spark.prototype.display=function(){sP(this.n).attr("src",this.f).css("position","absolute").css("z-index",this.random(-3)).css("top",this.pointY).css("left",this.pointX);sparkles_container.append(this.n);return this};Spark.prototype.fly=function(){var a=this;sP(this.n).animate({top:this.pointY,left:this.pointX},this.speed,"linear",function(){a.newSpeed().newPoint().fly()})};Spark.prototype.newSpeed=function(){this.speed=(this.random(10)+5)*1100;return this};Spark.prototype.newPoint=function(){this.pointX=this.random(sparkles_container.width());this.pointY=this.random(sparkles_container.height());return this};Spark.prototype.random=function(a){return Math.ceil(Math.random()*a)-1};sP(function(){if(sP.browser.msie&&sP.browser.version<9){return}var a=40;var b=[];for(i=0;i<a;i++){b[i]=new Spark()}});
 
function grayscale(src) {
      
	var canvas = document.createElement('canvas');
	var ctx = canvas.getContext('2d');
	var imgObj = new Image();
	imgObj.src = src;
	canvas.width = imgObj.width;
	canvas.height = imgObj.height;
	ctx.drawImage(imgObj, 0, 0);
	var imgPixels = ctx.getImageData(0, 0, canvas.width, canvas.height);
	for(var y = 0; y < imgPixels.height; y++){
		for(var x = 0; x < imgPixels.width; x++){
			var i = (y * 4) * imgPixels.width + x * 4;
			var avg = (imgPixels.data[i] + imgPixels.data[i + 1] + imgPixels.data[i + 2]) / 3;
			imgPixels.data[i] = avg;
			imgPixels.data[i + 1] = avg;
			imgPixels.data[i + 2] = avg;
		}
	}
	ctx.putImageData(imgPixels, 0, 0, 0, 0, imgPixels.width, imgPixels.height);
	return canvas.toDataURL();
   
}

function Gsitesearch(curobj) {
    curobj.q.value = "site:" + domainroot + " " + curobj.qfront.value;
}