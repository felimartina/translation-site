/*
  Jquery Validation using jqBootstrapValidation
   example is taken from jqBootstrapValidation docs 
  */
$(function () {

    $("input,textarea").jqBootstrapValidation({
        preventSubmit: true,
        submitError: function ($form, event, errors) {
            // something to have when submit produces an error ?
            // Not decided if I need it yet
        },
        submitSuccess: function ($form, event) {
            event.preventDefault(); // prevent default submit behaviour
            var fd = new FormData();
            fd.append("name", $("input#name").val());
            fd.append("phone", $("input#phone").val());
            fd.append("email", $("input#email").val());
            fd.append("projectType", $("select#projectType option:selected").text());
            fd.append("languages", $("select#languages option:selected").text());
            fd.append("file", document.getElementById('file').files[0]);
            fd.append("wordCount", $("input#wordCount").val());
            fd.append("deadline", $("input#deadline").val());
            fd.append("details", $("textarea#details").val());
            $.ajax({
                url: "/en/GetQuote/QuoteRequest",
                type: "POST",
                data: fd,
                processData: false,
                contentType: false,
                success: function() {
                    $('#success').removeClass('hidden');
                    $('#fail').addClass('hidden');
                    //clear all fields
                    $('#contactForm').trigger('reset');
                },
                error: function() {
                    $('#success').addClass('hidden');
                    $('#fail').removeClass('hidden');
                    //clear all fields
                    $('#contactForm').trigger("reset");
                },
            });
            //var fd = new FormData(); //FormData object
            //var fileInput = document.getElementById('file');
            ////Iterating through each files selected in fileInput
            //for (i = 0; i < fileInput.files.length; i++) {
            //    //Appending each file to FormData object
            //    fd.append(fileInput.files[i].name, fileInput.files[i]);
            //}
            //fd.append("name", $("input#name").val());
            //fd.append("phone", $("input#phone").val());
            //fd.append("email", $("input#email").val());
            //fd.append("projectType", $("select#projectType option:selected").text());
            //fd.append("languages", $("select#languages option:selected").text());
            //fd.append("file", $("input#file")[0]);
            //fd.append("wordCount", $("input#wordCount").val());
            //fd.append("deadline", $("input#deadline").val());
            //fd.append("details", $("input#details").val());
            ////Creating an XMLHttpRequest and sending
            //var xhr = new XMLHttpRequest();
            //xhr.open('POST', '/en/GetQuote/QuoteRequest');
            //xhr.send(fd);
            //xhr.onreadystatechange = function () {
            //    if (xhr.readyState == 4 && xhr.status == 200) {
            //        alert(xhr.responseText);
            //    }
            //}
            //return false;
        },
        filter: function () {
            return $(this).is(":visible");
        },
    });

    $("a[data-toggle=\"tab\"]").click(function (e) {
        e.preventDefault();
        $(this).tab("show");
    });
});