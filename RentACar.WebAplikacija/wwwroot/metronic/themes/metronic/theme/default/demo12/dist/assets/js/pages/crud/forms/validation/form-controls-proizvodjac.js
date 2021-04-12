var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                slikaProizvodjaca: {
                    required: !0
                },
                nazivProizvodjaca: {
                    required: !0
                },
                drzavaId: {
                    required: !0
                }
            },
            messages: {
                slikaProizvodjaca : {
                    required : "Polje Slika je obavezno!"                    
                },
                nazivProizvodjaca: {
                    required : "Polje Naziv proizvođača je obavezno!"
                },
                drzavaId: {
                    required: "Polje Država proizvođača je obavezno!"
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