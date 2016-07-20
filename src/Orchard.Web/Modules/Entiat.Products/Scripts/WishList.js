$(document).ready(function () {
    var dialog,
      form,
      emailRegex = /^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/,
      toEmail = $("#toEmail"),
      fromEmail = $("#fromEmail"),
            toAllEmail = $("#toAllEmail"),
      fromAllEmail = $("#fromAllEmail"),
      channelProductId,
      tips = $(".validateTips");

    function updateTips(t) {
        tips
          .text(t)
          .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    };

    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    };

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
              min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    };

    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    dialog = $("#dialog-message").dialog({
        modal: true,
        autoOpen: false,
        draggable: false,
        resizable: false,
        show: 'blind',
        hide: 'blind',
        width: 440,
        dialogClass: 'ui-dialog-osx',
        buttons: {
            "Send Email": function () {               
                var valid = true;
                valid = valid && checkLength(toEmail, "To(Email)", 3, 80);
                valid = valid && checkLength(fromEmail, "From(Email)", 3, 80);
                valid = valid && checkRegexp(toEmail, emailRegex, "eg. sample@maildomain.com");
                valid = valid && checkRegexp(fromEmail, emailRegex, "eg. sample@maildomain.com");
                if (valid) {
                    var to = toEmail.val();
                    var from = fromEmail.val();
                    $.ajax({
                        url: "/EmailMyPoolsideProduct",
                        type: "POST",
                        data: ({ __RequestVerificationToken: token, 'fromAddress': from, 'toAddress': to, 'channelProductId': channelProductId }),
                        success: function (data) {
                            if (data.status == "Success") {
                                console.log('good job');
                            } 
                        },
                       
                    });
                    $(this).dialog("close");

                }
            }
        }
    });

    dialogAll = $("#dialog-allMessages").dialog({
        modal: true,
        autoOpen: false,
        draggable: false,
        resizable: false,
        show: 'blind',
        hide: 'blind',
        width: 440,
        dialogClass: 'ui-dialog-osx',
        buttons: {
            "Send Email for All Items in Wish List": function () {
                var valid = true;
                valid = valid && checkLength(toAllEmail, "To(Email)", 3, 80);
                valid = valid && checkLength(fromAllEmail, "From(Email)", 3, 80);
                valid = valid && checkRegexp(toAllEmail, emailRegex, "eg. sample@maildomain.com");
                valid = valid && checkRegexp(fromAllEmail, emailRegex, "eg. sample@maildomain.com");
                if (valid) {
                    var to = toAllEmail.val();
                    var from = fromAllEmail.val();
                    $.ajax({
                        url: "/allProductsEmail",
                        type: "POST",
                        data: ({ __RequestVerificationToken: token, 'fromAddress': from, 'toAddress': to }),
                        success: function (data) {
                            if (data.status == "Success") {
                                console.log('good job');
                            }
                        },

                    });
                    $(this).dialog("close");

                }
            }
        }
    });

    $(".emailItem").click(function () {
        console.log(this);
        channelProductId = $(this).attr("id");
        console.log(channelProductId);
        dialog.dialog("open");
    });

    $("#allWishListItemsEMail").click(function () {
        console.log(this);
        dialogAll.dialog("open");
    });
});

