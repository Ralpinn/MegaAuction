$(document).ready(function() 
{

    $('.mk-form--contact-us').on('submit',function()
    {

        // Add text 'loading...' right after clicking on the submit button. 
        $('.contact-form-btn').text('Loading...'); 

        var form = $(this);
            $.ajax({
            url: "php/contact-form.php",
            method: form.attr('method'),
            data: form.serialize(),
            success: function(result)
            {
                $('.contact-form-btn').text('Message Sent!'); 
                document.getElementById('mk-form--contact-us').reset(); 
            },
            error: function(xhr, status, error)
            {
                $('.contact-form-btn').text('Error Sending email!');

            }
        });

        // Prevents default submission of the form after clicking on the submit button. 
        return false;   
    });
});

