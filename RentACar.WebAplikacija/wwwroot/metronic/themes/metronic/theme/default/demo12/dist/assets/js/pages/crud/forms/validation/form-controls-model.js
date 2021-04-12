var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                proizvodjacId: {
                    required: !0
                },
                nazivModela: {
                    required: !0
                }
            },
            messages: {
                proizvodjacId : {
                    required : "Polje Proizvođač je obavezno!"                    
                },
                nazivModela: {
                    required : "Polje Naziv modela je obavezno!"
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