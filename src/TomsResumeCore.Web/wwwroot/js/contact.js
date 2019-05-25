/*!
 * Toms Resume Website v1.0.1 (https://tomhutson.com)
 * Copyright 2019 Tom Hutson
 * Licensed under MIT (https://opensource.org/licenses/MIT)
 */

$(function(){jQuery.validator.setDefaults({errorElement:"span",errorPlacement:function(e,s){e.addClass("invalid-feedback"),s.closest(".form-group").append(e)},highlight:function(e,s,a){$(e).addClass("is-invalid")},unhighlight:function(e,s,a){$(e).removeClass("is-invalid")}}),$("#contactForm").validate({rules:{name:"required",email:{required:!0,email:!0},message:"required"},messages:{name:"Please enter your name",email:"Please enter a valid email address",message:"Please enter a message"},submitHandler:function(e){if((event.preventDefault(),"undefined"!=typeof grecaptcha)&&0===grecaptcha.getResponse().length){return $("#success").html("<div class='alert alert-danger'>"),$("#success > .alert-danger").html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;").append("</button>"),$("#success > .alert-danger").append($("<strong>").text("Captcha verification failed.")),void $("#success > .alert-danger").append("</strong></div>")}var s=$("input#name").val();0<=s.indexOf(" ")&&(s=name.split(" ").slice(0,-1).join(" ")),$this=$("#sendMessageButton"),$this.prop("disabled",!0),$.ajax({url:"./api/Contact",type:"POST",data:{name:$("input#name").val(),email:$("input#email").val(),message:$("textarea#message").val(),recaptcha:$("#g-recaptcha-response").val()},cache:!1,success:function(){$("#success").html("<div class='alert alert-success'>"),$("#success > .alert-success").html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;").append("</button>"),$("#success > .alert-success").append("<strong>Your message has been sent. </strong>"),$("#success > .alert-success").append("</div>"),$("#contactForm").trigger("reset")},error:function(){$("#success").html("<div class='alert alert-danger'>"),$("#success > .alert-danger").html("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;").append("</button>"),$("#success > .alert-danger").append($("<strong>").text("Sorry "+s+", it seems that my mail server is not responding. Please try again later!")),$("#success > .alert-danger").append("</strong></div>")},complete:function(){setTimeout(function(){$this.prop("disabled",!1)},1e3)}})}}),$('a[data-toggle="tab"]').click(function(e){e.preventDefault(),$(this).tab("show")})}),$("#name").focus(function(){$("#success").html("")});