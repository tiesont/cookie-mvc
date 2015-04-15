// Avoid "console" errors in browsers that lack a console.
if (!(window.console && console.log)) {
    (function () {
        var noop = function () { };
        var methods = ['assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error', 'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log', 'markTimeline', 'profile', 'profileEnd', 'markTimeline', 'table', 'time', 'timeEnd', 'timeStamp', 'trace', 'warn'];
        var length = methods.length;
        var console = window.console = {};
        while (length--) {
            console[methods[length]] = noop;
        }
    }());
}

// jQuery closure - helps prevent '$' conflicts with other libraries
; (function ($) {
    if ($.validator) {
        /*
        *   Extension to apply unobtrusive validation to dynamically-added elements
        *   @@see: stackoverflow.com/questions/4406291/jquery-validate-unobtrusive-not-working-with-dynamic-injected-elements
        *   @@requires: jquery.validate.js, jquery.validate.unobtrusive.js
        */
        $.fn.updateValidation = function () {
            var form = this.closest("form")
            .removeData("validator")
            .removeData("unobtrusiveValidation");

            $.validator.unobtrusive.parse(form);

            return this;
        };


        /* 
        *   Show jQuery Validate errors in Bootstrap tooltips rather than inline elements
        *   @@requires: jquery.validate.js, bootstrap.js
        */
        $.validator.setDefaults({
            showErrors: function (errorMap, errorList) {
                this.defaultShowErrors();

                // destroy tooltips on valid elements                              
                $("." + this.settings.validClass).tooltip("destroy");

                // add/update tooltips 
                for (var i = 0; i < errorList.length; i++) {
                    var error = errorList[i];

                    $("#" + error.element.id)
                        .tooltip({ trigger: 'focus', placement: 'right', html: true, container: 'body' })
                        .attr('data-tooltip', 'tooltip-danger')
                        .attr("data-original-title", error.message);
                }
            }
        });
    }

})(jQuery);


$(function () {
    $(document)
        .on('click', 'a[href^=#]', function (e) { e.preventDefault(); });

});