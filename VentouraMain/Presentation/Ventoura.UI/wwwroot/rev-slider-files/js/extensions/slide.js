
    var tpj = jQuery;

    var revapi44;
    tpj(document).ready(function () {
            if (tpj("#rev_slider_44").revolution == undefined) {
        revslider_showDoubleJqueryError("#rev_slider_44");
            } else {
        revapi44 = tpj("#rev_slider_44").show().revolution({
            sliderType: "standard",
            jsFileLocation: "revolution-slider/js/",
            sliderLayout: "fullscreen",
            dottedOverlay: "none",
            delay: 4500,
            navigation: {
                keyboardNavigation: "on",
                keyboard_direction: "horizontal",
                mouseScrollNavigation: "off",
                mouseScrollReverse: "default",
                onHoverStop: "off",
                touch: {
                    touchenabled: "on",
                    touchOnDesktop: "on",
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: "horizontal",
                    drag_block_vertical: false
                },
                arrows: {
                    enable: true,
                    style: 'erinyen',
                    tmp: '',
                    rtl: false,
                    hide_onleave: true,
                    hide_onmobile: true,
                    hide_under: 767,
                    hide_over: 9999,
                    hide_delay: 0,
                    hide_delay_mobile: 0,

                    left: {
                        container: 'slider',
                        h_align: 'left',
                        v_align: 'center',
                        h_offset: 60,
                        v_offset: 0
                    },

                    right: {
                        container: 'slider',
                        h_align: 'right',
                        v_align: 'center',
                        h_offset: 60,
                        v_offset: 0
                    }
                },
                bullets: {
                    enable: true,
                    style: 'zeus',
                    direction: 'horizontal',
                    rtl: false,

                    container: 'slider',
                    h_align: 'center',
                    v_align: 'bottom',
                    h_offset: 0,
                    v_offset: 30,
                    space: 7,

                    hide_onleave: false,
                    hide_onmobile: false,
                    hide_under: 0,
                    hide_over: 767,
                    hide_delay: 200,
                    hide_delay_mobile: 1200
                },
            },
            responsiveLevels: [1240, 1025, 778, 480],
            visibilityLevels: [1920, 1500, 1025, 768],
            gridwidth: [1200, 991, 778, 480],
            gridheight: [1025, 1366, 1025, 868],
            lazyType: "none",
            shadow: 0,
            spinner: "spinner4",
            stopLoop: "off",
            stopAfterLoops: -1,
            stopAtSlide: -1,
            shuffle: "off",
            autoHeight: "on",
            fullScreenAutoWidth: "on",
            fullScreenAlignForce: "off",
            fullScreenOffsetContainer: "",
            disableProgressBar: "on",
            hideThumbsOnMobile: "on",
            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLimit: 0,
            debugMode: false,
            fallbacks: {
                simplifyAll: "off",
                nextSlideOnWindowFocus: "off",
                disableFocusListener: false,
            }
        });
            }
        });