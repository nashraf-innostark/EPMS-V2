(function($){
	$(document).ready(function() {
		$('#submit-form').click(function(e){
		
			e.preventDefault();
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var name  = $('#rp_name').val(),
				email  = $('#rp_email').val(),
				subject  = $('#rp_subject').val(),
				message  = $('#rp_message').val(),
				data_html,
				success = $('#success');
				
    		if(name == "")
                $('#rp_name').val('Please enter your name.');
				
			if(subject == "")
                $('#rp_subject').val('Please enter your name.');

            if(email == ""){
                $('#rp_email').val('Your email is required.');
            }else if(reg.test(email) == false){
                $('#rp_email').val('Invalid Email Address.');
            }
			
            if(message == "")
                $('#rp_message').val('Message is required.');

            if(message != "" && name != "" && reg.test(email) != false) {
            	data_html = "name=" + name + "&email="+ email + "&message=" + message + "&subject="+ subject;

                //alert(data_html);
                $.ajax({
                    type: 'POST',
                    url: 'php_helpers/contact_form.php',
                    data: data_html,
                    success: function(msg){
						
						if (msg == 'sent'){
                        	success.html('<div class="alert alert-success">Message <strong>successfully</strong> sent!</div>')  ;
                            $('#rp_name').val('');
							$('#rp_email').val('');
							$('#rp_message').val('');
                        }else{
                            success.html('<div class="alert alert-error">Message <strong>not</strong> sent! Please Try Again!</div>')  ; 
                        }
                    }
                });
    
            }
            return false;
        });
	});
})(jQuery);