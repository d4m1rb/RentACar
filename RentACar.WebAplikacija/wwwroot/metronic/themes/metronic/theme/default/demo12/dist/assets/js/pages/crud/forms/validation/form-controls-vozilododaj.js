var KTFormControls = {
    init: function() {
        $("#kt_form_1").validate({
            rules: {
                slikaVozila: {
                    required: !0
                },
                proizvodjacId: {
                    required: !0
                },
                modelId: {
                    required: !0
                },
                kategorijaId: {
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
                },
                registrovanDoStr: {
                    required: !0
                },
                emisioniStandard: {
                    required: !0
                },
                gorivo: {
                    required: !0
                },
                transmisija: {
                    required: !0
                },
                brojVrata: {
                    required: !0
                },
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
                },
                datumRegistracijeStr: {
                    required: !0
                }
            },
            messages: {
                slikaVozila : {
                    required : "Polje Slika je obavezno!"                    
                },
                proizvodjacId: {
                    required : "Polje Proizvođač je obavezno!"
                },
                modelId: {
                    required: "Polje Model je obavezno!"
                },
                kategorijaId: {
                    required: "Polje Kategorija je obavezno!"
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
                },
                registrovanDoStr: {
                    required: "Polje Datum isteka registracije je obavezno!"
                },
                emisioniStandard: {
                    required: "Polje Emisioni standard je obavezno!"
                },
                gorivo: {
                    required: "Polje Gorivo je obavezno!"
                },
                transmisija: {
                    required: "Polje Transmisija je obavezno!"
                },
                brojVrata: {
                    required: "Polje Broj vrata je obavezno!"
                },
                proizvodjacModel : {
                    required : "Polje Naziv vozila je obavezno!"                    
                },
                registarskaOznaka: {
                    required : "Polje Registarske oznake je obavezno!"
                },
                registrovanDoStr: {
                    required: "Polje Registracija važi do je obavezno!"
                },
                trajanje: {
                    required: "Polje Trajanje registracije je obavezno!"
                },
                iznosRegistracije: {
                    required: "Polje Iznos registracije je obavezno!"
                },
                datumRegistracijeStr: {
                    required: "Polje Datum registracije registracije je obavezno!"
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