(function($){ 
	$(document).ready(function(){
		var style = $('<style type="text/css" id="theme_color" />').appendTo('head');
		
		var $op = $('#options_panel'),
			$op_btn = $('#options_panel h3');
			
		$op_btn.click(function(){
			if($op.hasClass('opened')) {
				$op.removeClass('opened').animate({'left':'-'+180+'px', opacity:.7}, 500,'easeOutExpo');
				$(this).find('span').removeClass('icon-remove').addClass('icon-wrench');
				
			} else {
				$op.addClass('opened').animate({'left':0, opacity:1}, 500,'easeOutBounce');
				$(this).find('span').removeClass('icon-wrench').addClass('icon-remove');
			}
		});
		
		$('#header_style').change(function(){
			$('#header').attr('class','');
			$('#header').addClass('style'+$(this).val());
		});
		
		$('#theme_switcher').change(function(){
			if($(this).val() == 1)
				var style = $('<link rel="stylesheet" href="css/dark-theme.css" type="text/css" id="theme_style" />').appendTo('head');
			else if($(this).val() == 0)
				$('#theme_style').remove();
		});
		
		function changeColor(hex) {
			style.html(
				'a:hover, .cart_details .checkout, .info_pop .buyit, .m_title, .smallm_title, .circle_title, .feature_box .title, .services_box .title, .latest_posts.default-style .hoverBorder:hover h6, .latest_posts.style2 ul.posts .title, .latest_posts.style3 ul.posts .title, .recentwork_carousel li .details h4, .acc-group.default-style > button, .acc-group.style3 > button:after, .screenshot-box .left-side h3.title, .vertical_tabs .tabbable .nav>li>a:hover, .vertical_tabs .tabbable .nav>li.active>a, .services_box.style2 .box .list li, .shop-latest .tabbable .nav li.active a, .product-list-item:hover .details h3, .statbox h4 {color:' + hex + ';}'+
				'.acc-group.style3 > button:hover, .acc-group.style3 > button:hover:after { color:' + hex + ' !important;}'+
				'.tabs_style1 > ul.nav > li.active > a, header.style1, header.style2 #logo a, header.style3 #logo a {border-top: 3px solid ' + hex + ';}'+
				'nav#main_menu > ul > li.active > a, nav#main_menu > ul > li > a:hover, nav#main_menu > ul > li:hover > a, .social-icons li a:hover, #action_box, .circlehover, .iosSlider .item .caption.style2 .more  {background-color:' + hex + ';}'+
				'.how_to_shop .number, .newsletter-signup input[type=submit], .vertical_tabs .tabbable .nav>li.active>a>span, .vertical_tabs .tabbable .nav>li>a:hover>span, #map_controls, .hg-portfolio-sortable #portfolio-nav li.current a, .ptcarousel .controls > a:hover, .itemLinks span a:hover, .product-list-item .details .actions a, .shop-features .shop-feature:hover, .btn-flat, .redbtn, #sidebar ul.menu li a:hover, .imgboxes_style1 .hoverBorder h6, .feature_box.style3 .box:hover, .services_box .box:hover .icon, .latest_posts.default-style .hoverBorder h6, .process_steps .step.intro, .recentwork_carousel.style2 li a .details .plus, .gobox.ok, .hover-box:hover, .recentwork_carousel li .details > .bg, .circlehover:before,.iosSlider .item .caption.style1 .more:before, .iosSlider .item .caption.style1 .more:after {background:' + hex + ';}'+
				'#action_box:before {border-top-color:' + hex + ';}'+
				'.process_steps .step.intro:after {border-left-color:' + hex + '; }'+
				'.theHoverBorder:hover {box-shadow:0 0 0 5px ' + hex + ' inset;}'+
				'.offline-page .containerbox {border-bottom:5px solid ' + hex + '; }'+
				'.offline-page .containerbox:after {border-top: 20px solid ' + hex + ';}'+
				'.iosSlider .item .caption.style2 .title_big, .iosSlider .item .caption.style2 .title_small {border-left: 5px solid ' + hex + '; }'+
				'.iosSlider .item .caption.style2.fromright .title_big, .iosSlider .item .caption.style2.fromright .title_small {border-right: 5px solid ' + hex + '; }');
		}
		
		// Attach callbacks
		$('.color-picker').miniColors({
			change: function(hex, rgba) {
				changeColor(hex);
			}
		});

		$('#options_panel .color_suggestions li').click(function(){
			var hex = $(this).css('background-color');
			changeColor(hex);
		});
		
	});
})(jQuery);