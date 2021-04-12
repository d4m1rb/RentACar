var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                email: {
                    required: !0,
                    email: !0
                },
                ime: {
                    required: !0
                },
                prezime: {
                    required: !0
                },
                adresa: {
                    required: !0
                },
                datumRodjenja: {
                    required: !0
                },                            
                Telefon: {
                    required: !0
                }
            },
            messages: {
                email : {
                    required : "Polje Email je obavezno!",
                    email: "Upišite ispravan email."
                },
                ime: {
                    required : "Polje Ime je obavezno!"
                },
                prezime: {
                    required: "Polje Prezime je obavezno!"
                },
                adresa: {
                    required: "Polje Adresa je obavezno!"
                },
                datumRodjenja: {
                    required: "Polje Datum rođenja je obavezno!"
                },
                Telefon: {
                    required: "Polje Telefon je obavezno!",
                    pattern: "Unesite telefon u pravilnom formatu!"
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