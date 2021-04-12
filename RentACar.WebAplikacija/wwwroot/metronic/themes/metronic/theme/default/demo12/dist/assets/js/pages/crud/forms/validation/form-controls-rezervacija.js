var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                rezervacijaOd: {
                    required: !0
                },
                rezervacijaDo: {
                    required: !0
                },
                lokacijaPreuzimanja: {
                    required: !0
                },
                popust: {
                    required: !0
                }
            },
            messages: {
                rezervacijaOdString : {
                    required : "Polje Datum početka je obavezno!"                    
                },
                rezervacijaDoString: {
                    required : "Polje Datum završetka je obavezno!"
                },
                lokacijaPreuzimanja: {
                    required: "Polje Lokacija preuzimanja je obavezno!"
                },
                popust: {
                    required: "Polje Popust je obavezno!"
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