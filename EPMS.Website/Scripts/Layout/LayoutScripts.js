var signup =
    '<div class="login_register_stuff hide">' +
        '<div id="login_panel">' +
            '<div class="inner-container login-panel">' +
                '<h3 class="m_title">SIGN IN YOUR ACCOUNT TO HAVE ACCESS TO DIFFERENT FEATURES</h3>' +
                '<form action="/Account/Login" class="form-horizontal" enctype="multipart/form-data" id="LoginFrom" method="post" role="form">' +
                '<a class="create_account cursorHand" onclick="ppOpen(\'#register_panel\', \'280\');">CREATE ACCOUNT</a>' +
                '<input name="__RequestVerificationToken" type="hidden" value="koJQmo6TpKsbNn1QdmciQVndSse4PT5G7S_9YcBzUzUqv8wXzhBj1ZxYBxNfZ7uwCMZAp_kEFSa1UyYZ8S5lJefHyuPbCxlX9UfyXbpoh3Y1">' +
                '<input type="hidden" id="fromLogin" name="fromLogin" value="" />' +
                '<input class="toBeRequired inputbox " data-val="true" data-val-required="The User Name field is required." id="LoginUserName" name="Login.UserName" placeholder="UserName" type="text" value="">' +
                '<input class="toBeRequired inputbox " data-val="true" data-val-required="The Password field is required." id="LoginPassword" name="Login.Password" placeholder="Password" type="password">' +
                '<input type="submit" id="login" name="submit" value="LOG IN" onclick="setSignUpFromValue(1)">' +
                ' <a href="#" class="login_facebook">login with facebook</a>' +
                '</form>' +
            '<div class="links"><a href="#" onclick="ppOpen(\'#forgot_panel\', \'350\');">FORGOT YOUR PASSWORD?</a> / <a href="#" onclick="ppOpen(\'#resetpassword_panel\', \'350\');">RESET YOUR PASSWORD?</a></div>' +
            ' </div>' +
        '</div><!-- end login panel -->' +
        '<div id="register_panel">' +
            '<div class="inner-container register-panel">' +
                '<h3 class="m_title">CREATE ACCOUNT</h3>' +
                    '<form action="/Account/SignUp" class="form-horizontal" id="SignUpFrom" method="post" role="form" novalidate="novalidate">' +
                        '<input type="hidden" id="fromSignUp" name="fromSignUp" value="" />' +
                        '<p>' +
                        '<input class="toBeRequired" data-val="true" data-val-required="User Name is required." id="SignUp_UserName" name="SignUp.UserName" placeholder="UserName" type="text" value="">' +
                        '</p>' +
                        '<p>' +
                        '<input class="toBeRequired" data-val="true" data-val-required="Your Name is required" id="SignUp_CustomerNameEn" name="SignUp.CustomerNameEn" placeholder="Your Name" type="text" value="">' +
                        '</p>' +
                        '<p>' +
                        '<input class="toBeRequired" data-val="true" data-val-email="The Email field is not a valid e-mail address." data-val-required="Email is required." id="SignUp_Email" name="SignUp.Email" placeholder="Email" type="text" value="">' +
                        '<span class="field-validation-valid required" data-valmsg-for="SignUp.Email" data-valmsg-replace="true"></span>' +
                        '</p>' +
                        '<p>' +
                        '<input class="toBeRequired" data-val="true" data-val-length="The Password must be at least 6 characters long." data-val-length-max="100" data-val-length-min="6" data-val-required="Password is required." id="SignUp_Password" name="SignUp.Password" placeholder="Password" type="password">' +
                        '<span class="field-validation-valid required" data-valmsg-for="SignUp.Password" data-valmsg-replace="true"></span>' +
                        '</p>' +
                        '<p>' +
                        '<input class="toBeRequired" data-val="true" data-val-equalto="The password and confirmation password do not match." data-val-equalto-other="*.Password" data-val-required="Password is required." id="SignUp_ConfirmPassword" name="SignUp.ConfirmPassword" placeholder="Confirm Password" type="password">' +
                        '<span class="field-validation-valid required" data-valmsg-for="SignUp.ConfirmPassword" data-valmsg-replace="true"></span>' +
                        '</p>' +
                        '<p>' +
                        '<input type="submit" id="create" value="CREATE MY ACCOUNT" onclick="setSignUpFromValue(2)">' +
                        '</p>' +
                    '</form>' +
                    '<div class="links"><a href="#" onclick="ppOpen(\'#login_panel\', \'800\');">ALREADY HAVE AN ACCOUNT?</a></div>' +
                '</div>' +
            '</div><!-- end register panel -->' +
        '<div id="forgot_panel">' +
            '<div class="inner-container forgot-panel">' +
                '<h3 class="m_title">FORGOT YOUR DETAILS?</h3>' +
                '<form action="/Account/ForgotPassword" class="form-horizontal" id="ForgotPasswordFrom" method="post" role="form" novalidate="novalidate">' +
                    '<input type="hidden" id="fromForgot" name="fromForgot" value="" />' +
                    '<p>' +
                    '<input class="toBeRequired" data-val="true" data-val-required="The User Name field is required." id="ForgotPasswordEmail" name="ForgotPassword.UserName" placeholder="UserName" type="text" value="">' +
                    '</p>' +
                    '<p>' +
                    '<input type="submit" id="recover" name="submit" value="SEND MY DETAILS!" onclick="setSignUpFromValue(3)"> </p>' +
                '</form>' +
                '<div class="links"><a href="#" onclick="ppOpen(\'#login_panel\', \'800\');">AAH, WAIT, I REMEMBER NOW!</a></div>' +
            '</div>' +
        '</div><!-- end register panel -->' +
        '<div id="resetpassword_panel">' +
        '<div class="inner-container register-panel">' +
            '<h3 class="m_title">Reset Password</h3>' +
                '<form action="/Account/ResetPassword" class="form-horizontal" id="ResetPasswordFrom" method="post" role="form" novalidate="novalidate">' +
                '<input type="hidden" id="fromReset" name="fromReset" value="" />' +
                '<input id="reset-code" name="ResetPassword.Code" type="hidden" value="">' +
                '<input id="user-id" name="ResetPassword.UserId" type="hidden" value="">' +
                '<p>' +
                '<input class="toBeRequired" data-val="true" data-val-length="The Password must be at least 6 characters long." data-val-length-max="100" data-val-length-min="6" data-val-required="The Password field is required." id="ResetPassword_Password" name="ResetPassword.Password" placeholder="Password" type="password">' +
                '<span class="field-validation-valid required" data-valmsg-for="ResetPassword.Password" data-valmsg-replace="true"></span>' +
                '</p>' +
                '<p>' +
                '<input class="toBeRequired" data-val="true" data-val-equalto="The password and confirmation password do not match." data-val-equalto-other="*.Password" id="ResetPassword_ConfirmPassword" name="ResetPassword.ConfirmPassword" placeholder="Confirm Password" type="password">' +
                '<span class="field-validation-valid required" data-valmsg-for="ResetPassword.ConfirmPassword" data-valmsg-replace="true"></span>' +
                '</p>' +
                '<p>' +
                '<input type="submit" id="reset" value="RESET" onclick="setSignUpFromValue(4)">' +
                '</p>' +
                '</form>' +
            '<div class="links"><a href="#" onclick="ppOpen(\'#login_panel\', \'800\');">ALREADY RESET?</a></div>' +
        '</div>' +
    '</div>' +
'</div>';
$(document).ready(function ($) {
    //$("#signup_div").empty();
    //$("#signup_div").append(signup);
    // Notification
    var message = $("#Message").val();
    var isSaved = $("#IsSaved").val();
    var isUpdated = $("#IsUpdated").val();
    var isError = $("#IsError").val();
    var isInfo = $("#IsInfo").val();
    if (isSaved || isUpdated) {
        new PNotify({
            title: 'Success',
            text: message
        });
    }
    else if (isError) {
        new PNotify({
            title: 'Error',
            text: message
        });
    }
    else if (isInfo) {
        new PNotify({
            title: 'Info',
            text: message
        });
    }

    $("a[data-rel^='prettyPhoto'], .prettyphoto_link").prettyPhoto({ theme: 'pp_kalypso', social_tools: false, deeplinking: false });
    $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'pp_kalypso' });
    $("a[data-rel^='prettyPhoto[login_panel]']").prettyPhoto({ theme: 'pp_kalypso', default_width: 800, social_tools: false, deeplinking: false });

    $(".prettyPhoto_transparent").click(function (e) {
        e.preventDefault();
        $.fn.prettyPhoto({ social_tools: false, deeplinking: false, show_title: false, default_width: 980, theme: 'pp_kalypso transparent', opacity: 0.95 });
        $.prettyPhoto.open($(this).attr('href'), '', '');
    });

    // THIS SCRIPT DETECTS THE ACTIVE ELEMENT AND ADDS ACTIVE CLASS
    var pathname = window.location.pathname,
            page = pathname.split(/[/ ]+/).pop(),
            menuItems = $('#main_menu a');
    menuItems.each(function () {
        var mi = $(this),
            miHrefs = mi.attr("href"),
            miParents = mi.parents('li');
        if (page == miHrefs) {
            miParents.addClass("active").siblings().removeClass('active');
        }
    });
});

function ppOpen(panel, width) {
    $.prettyPhoto.close();
    setTimeout(function () {
        $.fn.prettyPhoto({ social_tools: false, deeplinking: false, show_title: false, default_width: width, theme: 'pp_kalypso' });
        $.prettyPhoto.open(panel);
    }, 300);
}
function Gsitesearch(curobj) {
    //curobj.q.value = "site:" + domainroot + " " + curobj.qfront.value;
}

$(document).ready(function() {
    (function($) {
        
        
    })($);
});