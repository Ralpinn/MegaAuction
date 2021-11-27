(function($) {
    'use strict';
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;

    var isMobile = {
        Android: function() {
            return navigator.userAgent.match(/Android/i);
        },
        BlackBerry: function() {
            return navigator.userAgent.match(/BlackBerry/i);
        },
        iOS: function() {
            return navigator.userAgent.match(/iPhone|iPad|iPod/i);
        },
        Opera: function() {
            return navigator.userAgent.match(/Opera Mini/i);
        },
        Windows: function() {
            return navigator.userAgent.match(/IEMobile/i);
        },
        any: function() {
            return (
                isMobile.Android() ||
                isMobile.BlackBerry() ||
                isMobile.iOS() ||
                isMobile.Opera() ||
                isMobile.Windows()
            );
        },
    };

    function parallax() {
        $('.bg--parallax').each(function() {
            var el = $(this),
                xpos = '50%',
                windowHeight = $(window).height();
            if (isMobile.any()) {
                $(this).css('background-attachment', 'scroll');
            } else {
                $(window).scroll(function() {
                    var current = $(window).scrollTop(),
                        top = el.offset().top,
                        height = el.outerHeight();
                    if (
                        top + height < current ||
                        top > current + windowHeight
                    ) {
                        return;
                    }
                    el.css(
                        'backgroundPosition',
                        xpos + ' ' + Math.round((top - current) * 0.2) + 'px'
                    );
                });
            }
        });
    }

    function backgroundImage() {
        var databackground = $('[data-background]');
        databackground.each(function() {
            if ($(this).attr('data-background')) {
                var image_path = $(this).attr('data-background');
                $(this).css({
                    background: 'url(' + image_path + ')',
                });
            }
        });
    }

    function siteToggleAction() {
        var navSidebar = $('.navigation--sidebar'),
            filterSidebar = $('.mk-filter--sidebar');
        $('.menu-toggle-open').on('click', function(e) {
            e.preventDefault();
            $(this).toggleClass('active');
            navSidebar.toggleClass('active');
            $('.mk-site-overlay').toggleClass('active');
        });

        $('.mk-toggle--sidebar').on('click', function(e) {
            e.preventDefault();
            var url = $(this).attr('href');
            $(this).toggleClass('active');
            $(this)
                .siblings('a')
                .removeClass('active');
            $(url).toggleClass('active');
            $(url)
                .siblings('.mk-panel--sidebar')
                .removeClass('active');
            $('.mk-site-overlay').toggleClass('active');
        });

        $('#filter-sidebar').on('click', function(e) {
            e.preventDefault();
            filterSidebar.addClass('active');
            $('.mk-site-overlay').addClass('active');
        });

        $('.mk-filter--sidebar .mk-filter__header .mk-btn--close').on(
            'click',
            function(e) {
                e.preventDefault();
                filterSidebar.removeClass('active');
                $('.mk-site-overlay').removeClass('active');
            }
        );

        $('body').on('click', function(e) {
            if (
                $(e.target)
                    .siblings('.mk-panel--sidebar')
                    .hasClass('active')
            ) {
                $('.mk-panel--sidebar').removeClass('active');
                $('.mk-site-overlay').removeClass('active');
            }
        });
    }

    function subMenuToggle() {
        $('.menu--mobile .menu-item-has-children > .sub-toggle').on(
            'click',
            function(e) {
                e.preventDefault();
                var current = $(this).parent('.menu-item-has-children');
                $(this).toggleClass('active');
                current
                    .siblings()
                    .find('.sub-toggle')
                    .removeClass('active');
                current.children('.sub-menu').slideToggle(350);
                current
                    .siblings()
                    .find('.sub-menu')
                    .slideUp(350);
                if (current.hasClass('has-mega-menu')) {
                    current.children('.mega-menu').slideToggle(350);
                    current
                        .siblings('.has-mega-menu')
                        .find('.mega-menu')
                        .slideUp(350);
                }
            }
        );
        $('.menu--mobile .has-mega-menu .mega-menu__column .sub-toggle').on(
            'click',
            function(e) {
                e.preventDefault();
                var current = $(this).closest('.mega-menu__column');
                $(this).toggleClass('active');
                current
                    .siblings()
                    .find('.sub-toggle')
                    .removeClass('active');
                current.children('.mega-menu__list').slideToggle(350);
                current
                    .siblings()
                    .find('.mega-menu__list')
                    .slideUp(350);
            }
        );
        var listCategories = $('.mk-list--categories');
        if (listCategories.length > 0) {
            $('.mk-list--categories .menu-item-has-children > .sub-toggle').on(
                'click',
                function(e) {
                    e.preventDefault();
                    var current = $(this).parent('.menu-item-has-children');
                    $(this).toggleClass('active');
                    current
                        .siblings()
                        .find('.sub-toggle')
                        .removeClass('active');
                    current.children('.sub-menu').slideToggle(350);
                    current
                        .siblings()
                        .find('.sub-menu')
                        .slideUp(350);
                    if (current.hasClass('has-mega-menu')) {
                        current.children('.mega-menu').slideToggle(350);
                        current
                            .siblings('.has-mega-menu')
                            .find('.mega-menu')
                            .slideUp(350);
                    }
                }
            );
        }
    }

    function stickyHeader() {
        var header = $('.header'),
            scrollPosition = 0,
            checkpoint = 50;
        header.each(function() {
            if ($(this).data('sticky') === true) {
                var el = $(this);
                $(window).scroll(function() {
                    var currentPosition = $(this).scrollTop();
                    if (currentPosition > checkpoint) {
                        el.addClass('header--sticky');
                    } else {
                        el.removeClass('header--sticky');
                    }
                });
            }
        });

        var stickyCart = $('#cart-sticky');
        if (stickyCart.length > 0) {
            $(window).scroll(function() {
                var currentPosition = $(this).scrollTop();
                if (currentPosition > checkpoint) {
                    stickyCart.addClass('active');
                } else {
                    stickyCart.removeClass('active');
                }
            });
        }
    }

    function owlCarouselConfig() {
        var target = $('.owl-slider');
        if (target.length > 0) {
            target.each(function() {
                var el = $(this),
                    dataAuto = el.data('owl-auto'),
                    dataLoop = el.data('owl-loop'),
                    dataSpeed = el.data('owl-speed'),
                    dataGap = el.data('owl-gap'),
                    dataNav = el.data('owl-nav'),
                    dataDots = el.data('owl-dots'),
                    dataAnimateIn = el.data('owl-animate-in')
                        ? el.data('owl-animate-in')
                        : '',
                    dataAnimateOut = el.data('owl-animate-out')
                        ? el.data('owl-animate-out')
                        : '',
                    dataDefaultItem = el.data('owl-item'),
                    dataItemXS = el.data('owl-item-xs'),
                    dataItemSM = el.data('owl-item-sm'),
                    dataItemMD = el.data('owl-item-md'),
                    dataItemLG = el.data('owl-item-lg'),
                    dataItemXL = el.data('owl-item-xl'),
                    dataNavLeft = el.data('owl-nav-left')
                        ? el.data('owl-nav-left')
                        : "<i class='icon-chevron-left'></i>",
                    dataNavRight = el.data('owl-nav-right')
                        ? el.data('owl-nav-right')
                        : "<i class='icon-chevron-right'></i>",
                    duration = el.data('owl-duration'),
                    datamouseDrag =
                        el.data('owl-mousedrag') == 'on' ? true : false;
                if (
                    target.children('div, span, a, img, h1, h2, h3, h4, h5, h5')
                        .length >= 2
                ) {
                    el.addClass('owl-carousel').owlCarousel({
                        animateIn: dataAnimateIn,
                        animateOut: dataAnimateOut,
                        margin: dataGap,
                        autoplay: dataAuto,
                        autoplayTimeout: dataSpeed,
                        autoplayHoverPause: true,
                        loop: dataLoop,
                        nav: dataNav,
                        mouseDrag: datamouseDrag,
                        touchDrag: true,
                        autoplaySpeed: duration,
                        navSpeed: duration,
                        dotsSpeed: duration,
                        dragEndSpeed: duration,
                        navText: [dataNavLeft, dataNavRight],
                        dots: dataDots,
                        items: dataDefaultItem,
                        responsive: {
                            0: {
                                items: dataItemXS,
                            },
                            480: {
                                items: dataItemSM,
                            },
                            768: {
                                items: dataItemMD,
                            },
                            992: {
                                items: dataItemLG,
                            },
                            1200: {
                                items: dataItemXL,
                            },
                            1680: {
                                items: dataDefaultItem,
                            },
                        },
                    });
                }
            });
        }
    }

    function masonry($selector) {
        var masonry = $($selector);
        if (masonry.length > 0) {
            if (masonry.hasClass('filter')) {
                masonry.imagesLoaded(function() {
                    masonry.isotope({
                        columnWidth: '.grid-sizer',
                        itemSelector: '.grid-item',
                        isotope: {
                            columnWidth: '.grid-sizer',
                        },
                        filter: '*',
                    });
                });
                var filters = masonry
                    .closest('.masonry-root')
                    .find('.mk-masonry-filter > li > a');
                filters.on('click', function(e) {
                    e.preventDefault();
                    var selector = $(this).attr('href');
                    filters.find('a').removeClass('current');
                    $(this)
                        .parent('li')
                        .addClass('current');
                    $(this)
                        .parent('li')
                        .siblings('li')
                        .removeClass('current');
                    $(this)
                        .closest('.masonry-root')
                        .find('.mk-masonry')
                        .isotope({
                            itemSelector: '.grid-item',
                            isotope: {
                                columnWidth: '.grid-sizer',
                            },
                            filter: selector,
                        });
                    return false;
                });
            } else {
                masonry.imagesLoaded(function() {
                    masonry.masonry({
                        columnWidth: '.grid-sizer',
                        itemSelector: '.grid-item',
                    });
                });
            }
        }
    }

    function slickConfig() {
        var product = $('.mk-product--detail');
        if (product.length > 0) {
            var primary = product.find('.mk-product__gallery'),
                second = product.find('.mk-product__variants'),
                vertical = product
                    .find('.mk-product__thumbnail')
                    .data('vertical');
            primary.slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                asNavFor: '.mk-product__variants',
                fade: true,
                dots: false,
                infinite: false,
                arrows: primary.data('arrow'),
                prevArrow: "<a href='#'><i class='fa fa-angle-left'></i></a>",
                nextArrow: "<a href='#'><i class='fa fa-angle-right'></i></a>",
            });
            second.slick({
                slidesToShow: second.data('item'),
                slidesToScroll: 1,
                infinite: false,
                arrows: second.data('arrow'),
                focusOnSelect: true,
                prevArrow: "<a href='#'><i class='fa fa-angle-up'></i></a>",
                nextArrow: "<a href='#'><i class='fa fa-angle-down'></i></a>",
                asNavFor: '.mk-product__gallery',
                vertical: vertical,
                responsive: [
                    {
                        breakpoint: 1200,
                        settings: {
                            arrows: second.data('arrow'),
                            slidesToShow: 4,
                            vertical: false,
                            prevArrow:
                                "<a href='#'><i class='fa fa-angle-left'></i></a>",
                            nextArrow:
                                "<a href='#'><i class='fa fa-angle-right'></i></a>",
                        },
                    },
                    {
                        breakpoint: 992,
                        settings: {
                            arrows: second.data('arrow'),
                            slidesToShow: 4,
                            vertical: false,
                            prevArrow:
                                "<a href='#'><i class='fa fa-angle-left'></i></a>",
                            nextArrow:
                                "<a href='#'><i class='fa fa-angle-right'></i></a>",
                        },
                    },
                    {
                        breakpoint: 480,
                        settings: {
                            slidesToShow: 3,
                            vertical: false,
                            prevArrow:
                                "<a href='#'><i class='fa fa-angle-left'></i></a>",
                            nextArrow:
                                "<a href='#'><i class='fa fa-angle-right'></i></a>",
                        },
                    },
                ],
            });
        }
    }

    function tabs() {
        $('.mk-tab-list  li > a ').on('click', function(e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $(this)
                .closest('li')
                .siblings('li')
                .removeClass('active');
            $(this)
                .closest('li')
                .addClass('active');
            $(target).addClass('active');
            $(target)
                .siblings('.mk-tab')
                .removeClass('active');
        });
        $('.mk-tab-list.owl-slider .owl-item a').on('click', function(e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $(this)
                .closest('.owl-item')
                .siblings('.owl-item')
                .removeClass('active');
            $(this)
                .closest('.owl-item')
                .addClass('active');
            $(target).addClass('active');
            $(target)
                .siblings('.mk-tab')
                .removeClass('active');
        });
    }
    /**
     * !rating
     */
    function rating() {
        $('select.mk-rating').each(function() {
            var readOnly;
            if ($(this).attr('data-read-only') == 'true') {
                readOnly = true;
            } else {
                readOnly = false;
            }
            $(this).barrating({
                theme: 'fontawesome-stars',
                readonly: readOnly,
                emptyValue: '0',
            });
        });
    }

    function productLightbox() {
        var product = $('.mk-product--detail');
        if (product.length > 0) {
            $('.mk-product__gallery').lightGallery({
                selector: '.item a',
                thumbnail: true,
                share: false,
                fullScreen: false,
                autoplay: false,
                autoplayControls: false,
                actualSize: false,
            });
            if (product.hasClass('mk-product--sticky')) {
                $('.mk-product__thumbnail').lightGallery({
                    selector: '.item a',
                    thumbnail: true,
                    share: false,
                    fullScreen: false,
                    autoplay: false,
                    autoplayControls: false,
                    actualSize: false,
                });
            }
        }
        $('.mk-gallery--image').lightGallery({
            selector: '.mk-gallery__item',
            thumbnail: true,
            share: false,
            fullScreen: false,
            autoplay: false,
            autoplayControls: false,
            actualSize: false,
        });
        $('.mk-video').lightGallery({
            thumbnail: false,
            share: false,
            fullScreen: false,
            autoplay: false,
            autoplayControls: false,
            actualSize: false,
        });
    }

    function backToTop() {
        var scrollPos = 0;
        var element = $('#back2top');
        $(window).scroll(function() {
            var scrollCur = $(window).scrollTop();
            if (scrollCur > scrollPos) {
                // scroll down
                if (scrollCur > 500) {
                    element.addClass('active');
                } else {
                    element.removeClass('active');
                }
            } else {
                // scroll up
                element.removeClass('active');
            }

            scrollPos = scrollCur;
        });

        element.on('click', function() {
            $('html, body').animate(
                {
                    scrollTop: '0px',
                },
                800
            );
        });
    }

    function modalInit() {
        var modal = $('.mk-modal');
        if (modal.length) {
            if (modal.hasClass('active')) {
                $('body').css('overflow-y', 'hidden');
            }
        }
        modal.find('.mk-modal__close, .mk-btn--close').on('click', function(e) {
            e.preventDefault();
            $(this)
                .closest('.mk-modal')
                .removeClass('active');
        });
        $('.mk-modal-link').on('click', function(e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $(target).addClass('active');
            $('body').css('overflow-y', 'hidden');
        });
        $('.mk-modal').on('click', function(event) {
            if (!$(event.target).closest('.mk-modal__container').length) {
                modal.removeClass('active');
                $('body').css('overflow-y', 'auto');
            }
        });
    }

    function searchInit() {
        var searchbox = $('.mk-search');
        $('.mk-search-btn').on('click', function(e) {
            e.preventDefault();
            searchbox.addClass('active');
        });
        searchbox.find('.mk-btn--close').on('click', function(e) {
            e.preventDefault();
            searchbox.removeClass('active');
        });
    }

    function countDown() {
        var time = $('.mk-countdown');
        time.each(function() {
            var el = $(this),
                value = $(this).data('time');
            var countDownDate = new Date(value).getTime();
            var timeout = setInterval(function() {
                var now = new Date().getTime(),
                    distance = countDownDate - now;
                var days = Math.floor(distance / (1000 * 60 * 60 * 24)),
                    hours = Math.floor(
                        (distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
                    ),
                    minutes = Math.floor(
                        (distance % (1000 * 60 * 60)) / (1000 * 60)
                    ),
                    seconds = Math.floor((distance % (1000 * 60)) / 1000);
                el.find('.days').html(days);
                el.find('.hours').html(hours);
                el.find('.minutes').html(minutes);
                el.find('.seconds').html(seconds);
                if (distance < 0) {
                    clearInterval(timeout);
                    el.closest('.mk-section').hide();
                }
            }, 1000);
        });
    }

    function productFilterToggle() {
        $('.mk-filter__trigger').on('click', function(e) {
            e.preventDefault();
            var el = $(this);
            el.find('.mk-filter__icon').toggleClass('active');
            el.closest('.mk-filter')
                .find('.mk-filter__content')
                .slideToggle();
        });
        if ($('.mk-sidebar--home').length > 0) {
            $('.mk-sidebar--home > .mk-sidebar__header > a').on(
                'click',
                function(e) {
                    e.preventDefault();
                    $(this)
                        .closest('.mk-sidebar--home')
                        .children('.mk-sidebar__content')
                        .slideToggle();
                }
            );
        }
    }

    function mainSlider() {
        var homeBanner = $('.mk-carousel--animate');
        homeBanner.slick({
            autoplay: true,
            speed: 1000,
            lazyLoad: 'progressive',
            arrows: false,
            fade: true,
            dots: true,
            prevArrow: "<i class='slider-prev ba-back'></i>",
            nextArrow: "<i class='slider-next ba-next'></i>",
        });
    }

    function subscribePopup() {
        var subscribe = $('#subscribe'),
            time = subscribe.data('time');
        setTimeout(function() {
            if (subscribe.length > 0) {
                subscribe.addClass('active');
                $('body').css('overflow', 'hidden');
            }
        }, time);
        $('.mk-popup__close').on('click', function(e) {
            e.preventDefault();
            $(this)
                .closest('.mk-popup')
                .removeClass('active');
            $('body').css('overflow', 'auto');
        });
        $('#subscribe').on('click', function(event) {
            if (!$(event.target).closest('.mk-popup__content').length) {
                subscribe.removeClass('active');
                $('body').css('overflow-y', 'auto');
            }
        });
    }

    function stickySidebar() {
        var sticky = $('.mk-product--sticky'),
            stickySidebar,
            checkPoint = 992,
            windowWidth = $(window).innerWidth();
        if (sticky.length > 0) {
            stickySidebar = new StickySidebar(
                '.mk-product__sticky .mk-product__info',
                {
                    tomkpacing: 20,
                    bottomSpacing: 20,
                    containerSelector: '.mk-product__sticky',
                }
            );
            if ($('.sticky-2').length > 0) {
                var stickySidebar2 = new StickySidebar(
                    '.mk-product__sticky .sticky-2',
                    {
                        tomkpacing: 20,
                        bottomSpacing: 20,
                        containerSelector: '.mk-product__sticky',
                    }
                );
            }
            if (checkPoint > windowWidth) {
                stickySidebar.destroy();
                stickySidebar2.destroy();
            }
        } else {
            return false;
        }
    }

    function accordion() {
        var accordion = $('.mk-accordion');
        accordion.find('.mk-accordion__content').hide();
        $('.mk-accordion.active')
            .find('.mk-accordion__content')
            .show();
        accordion.find('.mk-accordion__header').on('click', function(e) {
            e.preventDefault();
            if (
                $(this)
                    .closest('.mk-accordion')
                    .hasClass('active')
            ) {
                $(this)
                    .closest('.mk-accordion')
                    .removeClass('active');
                $(this)
                    .closest('.mk-accordion')
                    .find('.mk-accordion__content')
                    .slideUp(350);
            } else {
                $(this)
                    .closest('.mk-accordion')
                    .addClass('active');
                $(this)
                    .closest('.mk-accordion')
                    .find('.mk-accordion__content')
                    .slideDown(350);
                $(this)
                    .closest('.mk-accordion')
                    .siblings('.mk-accordion')
                    .find('.mk-accordion__content')
                    .slideUp();
            }
            $(this)
                .closest('.mk-accordion')
                .siblings('.mk-accordion')
                .removeClass('active');
            $(this)
                .closest('.mk-accordion')
                .siblings('.mk-accordion')
                .find('.mk-accordion__content')
                .slideUp();
        });
    }

    function progressBar() {
        var progress = $('.mk-progress');
        progress.each(function(e) {
            var value = $(this).data('value');
            $(this)
                .find('span')
                .css({
                    width: value + '%',
                });
        });
    }

    function select2Cofig() {
        $('select.mk-select').select2({
            placeholder: $(this).data('placeholder'),
            minimumResultsForSearch: -1,
        });
    }

    function carouselNavigation() {
        var prevBtn = $('.mk-carousel__prev'),
            nextBtn = $('.mk-carousel__next');
        prevBtn.on('click', function(e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $(target).trigger('prev.owl.carousel', [1000]);
        });
        nextBtn.on('click', function(e) {
            e.preventDefault();
            var target = $(this).attr('href');
            $(target).trigger('next.owl.carousel', [1000]);
        });
    }

    function filterSlider() {
        var nonLinearSlider = document.getElementById('nonlinear');
        if (typeof nonLinearSlider != 'undefined' && nonLinearSlider != null) {
            noUiSlider.create(nonLinearSlider, {
                connect: true,
                behaviour: 'tap',
                start: [0, 1000],
                range: {
                    min: 0,
                    '10%': 100,
                    '20%': 200,
                    '30%': 300,
                    '40%': 400,
                    '50%': 500,
                    '60%': 600,
                    '70%': 700,
                    '80%': 800,
                    '90%': 900,
                    max: 1000,
                },
            });
            var nodes = [
                document.querySelector('.mk-slider__min'),
                document.querySelector('.mk-slider__max'),
            ];
            nonLinearSlider.noUiSlider.on('update', function(values, handle) {
                nodes[handle].innerHTML = Math.round(values[handle]);
            });
        }
    }

    function handleLiveSearch() {
        $('body').on('click', function(e) {
            if (
                $(e.target).closest('.mk-form--search-header') ||
                e.target.className === '.mk-form--search-header'
            ) {
                $('.mk-panel--search-result').removeClass('active');
            }
        });
        $('#input-search').on('keypress', function() {
            $('.mk-panel--search-result').addClass('active');
        })
    }

    $(function() {
        backgroundImage();
        owlCarouselConfig();
        siteToggleAction();
        subMenuToggle();
        masonry('.mk-masonry');
        productFilterToggle();
        tabs();
        slickConfig();
        productLightbox();
        rating();
        backToTop();
        stickyHeader();
        modalInit();
        searchInit();
        countDown();
        mainSlider();
        parallax();
        stickySidebar();
        accordion();
        progressBar();
        select2Cofig();
        carouselNavigation();
        $('[data-toggle="tooltip"]').tooltip();
        filterSlider();
        handleLiveSearch();
        $('.mk-product--quickview .mk-product__images').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            fade: true,
            dots: false,
            arrows: true,
            infinite: false,
            prevArrow: "<a href='#'><i class='fa fa-angle-left'></i></a>",
            nextArrow: "<a href='#'><i class='fa fa-angle-right'></i></a>",
        });
    });

    $('#product-quickview').on('shown.bs.modal', function(e) {
        $('.mk-product--quickview .mk-product__images').slick('setPosition');
    });

    $(window).on('load', function() {
        $('body').addClass('loaded');
        subscribePopup();
    });
})(jQuery);

(function ($) {

    "use strict";

    var fullHeight = function () {

        $('.js-fullheight').css('height', $(window).height());
        $(window).resize(function () {
            $('.js-fullheight').css('height', $(window).height());
        });

    };
    fullHeight();

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

})(jQuery);
