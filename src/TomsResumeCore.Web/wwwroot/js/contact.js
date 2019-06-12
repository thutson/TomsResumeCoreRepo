/*!
 * Toms Resume Website v1.0.5 (https://tomhutson.com)
 * Copyright 2019 Tom Hutson
 * Licensed under MIT (https://opensource.org/licenses/MIT)
 */

$(function () {

    jQuery.validator.setDefaults({
        errorElement: "span",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid");
        }
    });

    $("#contactForm").validate({
        rules: {
            name: "required",
            email: {
                required: true,
                email: true
            },
            message: "required"
        },
        // Specify validation error messages
        messages: {
            name: "Please enter your name",
            email: "Please enter a valid email address",
            message: "Please enter a message"
        },
        submitHandler: function () {
            event.preventDefault(); // prevent default submit behaviour
            if (grecaptcha !== "undefined") {
                var response = grecaptcha.getResponse();
                if (response.length === 0) {
                    $("#success").html("<div class=\"alert alert-danger\">");
                    $("#success > .alert-danger")
                        .html("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;")
                        .append("</button>");
                    $("#success > .alert-danger").append($("<strong>").text("Captcha verification failed."));
                    $("#success > .alert-danger").append("</strong></div>");
                    return;
                }
            }
            var firstName = $("input#name").val();
            if (firstName.indexOf(" ") >= 0) {
                firstName = firstName.split(" ").slice(0, -1).join(" ");
            }
            $("#sendMessageButton").prop("disabled", true);
            $.ajax({
                url: "./api/Contact",
                type: "POST",
                data: {
                    name: $("input#name").val(),
                    email: $("input#email").val(),
                    message: $("textarea#message").val(),
                    recaptcha: $("#g-recaptcha-response").val()
                },
                cache: false,
                success: function () {
                    // Success message
                    $("#success").html("<div class=\"alert alert-success\">");
                    $("#success > .alert-success")
                        .html("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;")
                        .append("</button>");
                    $("#success > .alert-success")
                        .append("<strong>Your message has been sent. </strong>");
                    $("#success > .alert-success")
                        .append("</div>");
                    //clear all fields
                    $("#contactForm").trigger("reset");
                },
                error: function () {
                    // Fail message
                    $("#success").html("<div class=\"alert alert-danger\">");
                    $("#success > .alert-danger")
                        .html("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;")
                        .append("</button>");
                    $("#success > .alert-danger").append($("<strong>").text("Sorry " + firstName + ", it seems that my mail server is not responding. Please try again later!"));
                    $("#success > .alert-danger").append("</strong></div>");
                },
                complete: function () {
                    setTimeout(function () {
                        $("#sendMessageButton").prop("disabled", false);
                    }, 1000);
                }
            });
        }
    });
});

/*When clicking on Full hide fail/success boxes */
$("#name").focus(function () {
    $("#success").html("");
});