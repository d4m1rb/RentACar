var KTFormControls = {
    init: function() {
        $("#kt_user_add_form").validate({
            rules: {
                email: {
                    required: !0,
                    email: !0,
                    minlength: 10
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
                gradId: {
                    required: !0
                },
                chbUloge: {
                    required: !0,
                    minlength: 1
                },
                Telefon: {
                    required: !0
                }
            },
            messages:{
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
                gradId: {
                    required: "Polje Grad je obavezno!"
                },
                chbUloge: {
                    required: "Polje uloge je obavezno!",
                    minlength: "Morate odabrati najmanje {0} ulogu!"
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
                $("#kt_user_add_form_msg").removeClass("kt-hide").show(), KTUtil.scrollTop()                
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