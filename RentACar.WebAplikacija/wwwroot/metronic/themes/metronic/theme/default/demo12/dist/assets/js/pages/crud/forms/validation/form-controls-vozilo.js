var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {                
                proizvodjacId: {
                    required: !0
                },
                modelId: {
                    required: !0
                },
                boja: {
                    required: !0
                },
                godinaProizvodnje: {
                    required: !0
                },
                kubikaza: {
                    required: !0
                },
                snagaMotora: {
                    required: !0
                },
                potrosnja: {
                    required: !0
                },
                brojSjedista: {
                    required: !0
                },
                cijenaIznajmljivanja: {
                    required: !0
                },
                cijenaKaskoOsiguranja: {
                    required: !0
                }
            },
            messages: {                
                proizvodjacId: {
                    required : "Polje Proizvođač je obavezno!"
                },
                modelId: {
                    required: "Polje Model je obavezno!"
                },
                boja: {
                    required: "Polje Boja je obavezno!"
                },
                godinaProizvodnje: {
                    required: "Polje Godina proizvodnje je obavezno!"
                },
                kubikaza: {
                    required: "Polje Kubikaža je obavezno!"
                },
                snagaMotora: {
                    required: "Polje Snaga motora je obavezno!"
                },
                potrosnja: {
                    required: "Polje Potrošnja je obavezno!"
                },
                brojSjedista: {
                    required: "Polje Broj sjedišta je obavezno!"
                },
                cijenaIznajmljivanja: {
                    required: "Polje Cijena iznajmljivanja je obavezno!"
                },
                cijenaKaskoOsiguranja: {
                    required: "Polje Cijena kasko osiguranja je obavezno!"
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