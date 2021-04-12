var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                proizvodjacModel: {
                    required: !0
                },
                registarskaOznaka: {
                    required: !0
                },
                registrovanDo: {
                    required: !0
                },
                trajanje: {
                    required: !0
                },
                iznosRegistracije: {
                    required: !0
                }
            },
            messages: {
                proizvodjacModel : {
                    required : "Polje Naziv vozila je obavezno!"                    
                },
                registarskaOznaka: {
                    required : "Polje Registarske oznake je obavezno!"
                },
                registrovanDo: {
                    required: "Polje Registracija važi do je obavezno!"
                },
                trajanje: {
                    required: "Polje Trajanje registracije je obavezno!"
                },
                iznosRegistracije: {
                    required: "Polje Iznos registracije je obavezno!"
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