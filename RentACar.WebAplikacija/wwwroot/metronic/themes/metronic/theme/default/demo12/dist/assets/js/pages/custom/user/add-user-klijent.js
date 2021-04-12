"use strict";
var KTUserAdd = function() {
    var e, t, r;
    return {
        init: function() {
            var n;
            e = $("#kt_user_add_form"), (r = new KTWizard("kt_user_add_user", {
                startStep: 1,
                clickableSteps: !0
            })).on("beforeNext", function(e) {
                !0 !== t.form() && e.stop()
            }), r.on("change", function(e) {
                KTUtil.scrollTop()
            }), t = e.validate({
                ignore: ":hidden",
                rules: {
                    profile_avatar: {},
                    profile_ime: {
                        required: !0
                    },
                    profile_prezime: {
                        required: !0
                    },
                    profile_phone: {
                        required: !0
                    },
                    slikaProfila:{
                        required: !0
                    },
                    UserName:
                    {
                        required: !0 ,
                        UserNamePostoji: !0,
                        bezrazmaka: !0
                    },
                    password:
                    {
                        required: !0
                    },
                    passwordpotvrda:
                    {
                        required: !0,
                        equalTo: "#Password"
                    },
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
                    gradId: {
                        required: !0
                    },
                    Telefon: {
                        required: !0
                    }
                },
                messages:{
                    slikaProfila: {
                        required : "Slika profila je obavezna!"
                    },
                    UserName:{
                        required : "Polje Username je obavezno!"
                    },
                    password:
                    {
                        required: "Polje Password je obavezno!"
                    },
                    passwordpotvrda:
                    {
                        required: "Polje Password potvrda je obavezno!",
                        equalTo: "Password i Potvrda passworda se ne poklapaju!"
                    },
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
                    Telefon: {
                        required: "Polje Telefon je obavezno!",
                        pattern: "Unesite telefon u pravilnom formatu!"
                    }
                },
                invalidHandler: function(e, t) {
                    KTUtil.scrollTop(), swal.fire({
                        title: "",
                        text: " Neka polja nisu ispravno unesena! Ispravite ih i zatim ponovo sačuvajte promjene.",
                        type: "error",
                        buttonStyling: !1,
                        confirmButtonClass: "btn btn-brand btn-sm btn-bold"
                    })
                },
                submitHandler: function(e) {}
            }), (n = e.find('[data-ktwizard-type="action-submit"]')).on("click", function(r) {
                
                r.preventDefault(), t.form() && (KTApp.progress(n), e.ajaxSubmit({
                    success: function() {
                        KTApp.unprogress(n), swal.fire({
                            title: "",
                            text: "Novi klijent je uspješno dodan!",
                            type: "success",
                            confirmButtonClass: "btn btn-secondary"
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/KlijentiAdm"
                            }
                          })
                    }
                }))
            }), new KTAvatar("kt_user_add_avatar")
        }
    }
}();
jQuery(document).ready(function() {
    KTUserAdd.init()
});