var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                nazivKategorije: {
                    required: !0
                },
                opis: {
                    required: !0
                }
            },
            messages: {
                nazivKategorije : {
                    required : "Polje Naziv kategorije je obavezno!"                    
                },
                opis: {
                    required : "Polje Opis kategorije je obavezno!"
                }
            },
            errorPlacement: function(e, r) {
                var i = r.closest(".input-group");
                i.length ? i.after(e.addClass("invalid-feedback")) : r.after(e.addClass("invalid-feedback"))
            },
            invalidHandler: function(e, r) {
                $("#kt_form_1_msg").removeClass("kt-hide").show(), KTUtil.scrollTop()                
            },
            submitHandler: function(e) {
                e.submit();
            }
        })
    }
};
jQuery(document).ready(function() {
    KTFormControls.init()
});