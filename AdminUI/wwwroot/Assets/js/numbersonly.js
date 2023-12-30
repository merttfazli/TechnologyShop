(function ($) {
    'use strict';
    $(".only-numeric").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode
        if (!(keyCode >= 48 && keyCode <= 57)) {
            return false;
        } else {
            return true;
        }
    });

    $(".only-decimal").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9\.]/g, ''));
        if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });
    $(".only-decimal-comma").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9\,]/g, ''));
        if ((evt.which != 46 || self.val().indexOf(',') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });

}).apply(this, [jQuery]);