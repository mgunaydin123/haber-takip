(function ($) {
    $('#side-nav').metisMenu();

    $(function () {
        $('[data-toggle="popover"]').popover()
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    });

    $(".mobile-menu-icon").click(function (event) {
        event.preventDefault();
    });

    var $window = $(window), $container = $('div.page-container');

    $(".sidebar-collapse-icon").click(function (event) {
        event.preventDefault();
		$container.toggleClass('sidebar-collapsed').toggleClass('can-resize');
		$(".page-container").addClass("sidebar-collapsing");
		if($is_collapsed == true) {
			$(".page-sidebar, .sidebar-fixed").animate({
		   		width: 280
		   	}, function() {
				$(".page-container").removeClass("sidebar-collapsing");
			});
		}
		else {
			$(".page-sidebar, .sidebar-fixed").animate({
			   width: 66
			}, function() {
				$(".page-container").removeClass("sidebar-collapsing");
			});
		}
		
		$is_collapsed = !$is_collapsed;		 
        
    });

    var $is_collapsed = false;
    if ($container.hasClass('sidebar-collapsed')) {
        $is_collapsed = true;
		$(".page-sidebar, .sidebar-fixed").animate({
		   width: 66
		});
    }
    $window.resize(function resize() {

        var window_width = $window.outerWidth();
        if (window_width < 951 && window_width > 767) {
            if ($container.hasClass('can-resize') === false) {
                $container.addClass('sidebar-collapsed');
				$(".page-sidebar, .sidebar-fixed").animate({
				   width: 66
				});
				$is_collapsed = true;
            }
        } else if (window_width < 767) {
            $container.removeClass('sidebar-collapsed');
            $container.removeClass('can-resize');
        } else {
            if ($container.hasClass('can-resize') === false && $is_collapsed === false) {
                $container.removeClass('sidebar-collapsed');
            }
        }

    }).trigger('resize');

    $('body').on('click', '.panel > .panel-heading > .panel-tool-options li > a[data-rel="reload"]', function (ev)
    {
        ev.preventDefault();

        var $this = $(this).closest('.panel');

        $this.block({
            message: '',
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#fff',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff',
                width: '50%'
            },
            overlayCSS: {backgroundColor: '#FFF'}
        });
        $this.addClass('reloading');

        setTimeout(function ()
        {
            $this.unblock();
            $this.removeClass('reloading');
        }, 900);

    }).on('click', '.panel > .panel-heading > .panel-tool-options li > a[data-rel="close"]', function (ev)
    {
        ev.preventDefault();

        var $this = $(this);
        var $panel = $this.closest('.panel');

        $panel.fadeOut(500, function ()
        {
            $panel.remove();
        });

    }).on('click', '.panel > .panel-heading > .panel-tool-options li > a[data-rel="collapse"]', function (ev)
    {
        ev.preventDefault();

        var $this = $(this),
                $panel = $this.closest('.panel'),
                $body = $panel.children('.panel-body, .table'),
                do_collapse = !$panel.hasClass('panel-collapse');

        if ($panel.is('[data-collapsed="1"]'))
        {
            $panel.attr('data-collapsed', 0);
            $body.hide();
            do_collapse = false;
        }

        if (do_collapse)
        {
            $body.slideUp('normal');
            $panel.addClass('panel-collapse');
        }
        else
        {
            $body.slideDown('normal');
            $panel.removeClass('panel-collapse');
        }
    });

    // removeable-list -- remove parent elements
    var $removalList = $(".removeable-list");
    $(".removeable-list .remove").each(function () {
        var $this = $(this);
        $this.click(function (event) {
            event.preventDefault();

            var $parent = $this.parent('li');
            $parent.slideUp(500, function () {
                $parent.delay(3000).remove();

                if ($removalList.find("li").length == 0) {
                    $removalList.html('<li class="text-danger"><p>All items has been deleted.</p></li>');
                }
            });
        });
    });

    var $filterBtn = $(".toggle-filter");
    var $filterBoxId = $filterBtn.attr('data-block-id');
    var $filterBox = $('#' + $filterBoxId);
    
    if ($filterBox.hasClass('visible-box')) {
        $filterBtn.parent('li').addClass('active');
    }

    $filterBtn.click(function (event) {
        event.preventDefault();
        
        if ($filterBox.hasClass('visible-box')) {
            $filterBtn.parent('li').removeClass('active');
            $filterBox.removeClass('visible-box').addClass('hidden-box').slideUp();
        } else {
            $filterBtn.parent('li').addClass('active');
            $filterBox.removeClass('hidden-box').addClass('visible-box').slideDown();
        }
    });
	
	$('.dropdown').on('show.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).slideDown(200);
	});
	
	// Add slideUp animation to Bootstrap dropdown when collapsing.
	$('.dropdown').on('hide.bs.dropdown', function() {
		$(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
	});
})(jQuery);

function showTooltip(x, y, contents) {
    var $windowWidth = $(window).width();
    var leftValue = x + 20;
    var toolTipWidth = 160;
    if ($windowWidth < (leftValue + toolTipWidth)) {
        leftValue = x - 32 - toolTipWidth;
    }

    $('<div id="flotTip" > ' + contents + ' </div>').css({
        top: y - 16,
        left: leftValue,
        position: 'absolute',
        padding: '5px 10px',
        border: '1px solid #111111',
        'min-width': toolTipWidth,
        background: '#000000',
        background: '-moz-linear-gradient(top,  #000 0%, #f9f9f9 100%)',
                background: '-webkit-gradient(linear, left top, left bottom, color-stop(0%,#000), color-stop(100%,#f9f9f9))',
                background: '-webkit-linear-gradient(top,  #000 0%,#f9f9f9 100%)',
                background: '-o-linear-gradient(top,  #000 0%,#f9f9f9 100%)',
                background: '-ms-linear-gradient(top,  #000 0%,#f9f9f9 100%)',
                background: 'linear-gradient(to bottom,  #000 0%,#f9f9f9 100%)',
                '-webkit-border-radius': '5px',
        '-moz-border-radius': '5px',
        'border-radius': '5px',
        'z-index': '100'
    }).appendTo('body').fadeIn();
}

/*
 * This function will remove its parent element
 * 
 * @param $eleObj
 * @param $parentEle
 */

function removeElement($ele, $parentEle) {
    var $this = $($ele);
    $this.parent($parentEle).css({
        opacity: '0'
    });
}