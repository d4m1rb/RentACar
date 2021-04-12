"use strict";
var KTDatatableHtmlTableDemo = {
    init: function() {       
        var t;
        t = $(".kt-datatable").KTDatatable({
            data: {
                saveState: {
                    cookie: !1
                }
            },            
            search: {
                input: $("#generalSearch")
            },
            columns: [
                {
                    field: "Vozilo ID",
                    type: "number",
                    autoHide: !0
                },
                {
                field: "Ime i prezime",
                autoHide: !1
                
            }, {
                field: "Od",
                title: "Od",
                autoHide: !1
            }, {
                field: "Do",
                title: "Do",
                autoHide: !1          
            },{
                field: "Boja",
                title: "Boja",
                autoHide: !1        
            }, {
                field: "Registrovan do",
                type: "date",
                format: "DD-MM-YYYY",
                autoHide:!0
            }, {
                field: "Cijena iznajmljivanja",
                type: "number",
                autoHide:!1           
            },{
                field: "Drođenja",
                type: "date",
                format: "DD-MM-YYYY",
                autoHide: !1
            },{
                field: "Username",                
                autoHide: !1
            }, {
                field: "Ime i prezime",                
                autoHide: !1
            }, {
                field: "Akcija",
                autoHide: !1
            }, {
                
                field: "VracanjePoslovnica",
                title: "VracanjePoslovnica",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0: {
                            title: "NE",
                            class: " kt-badge--danger"
                        },
                        1: {
                            title: "DA",
                            class: " kt-badge--success"
                        }
                    };
                    return '<span class="kt-badge ' + e[t.VracanjePoslovnica].class + ' kt-badge--inline kt-badge--pill">' + e[t.VracanjePoslovnica].title + "</span>"
                }                
            },{
                
                field: "Zavrsena",
                title: "Zavrsena",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0: {
                            title: "NE",
                            class: " kt-badge--danger"
                        },
                        1: {
                            title: "DA",
                            class: " kt-badge--success"
                        }
                    };
                    return '<span class="kt-badge ' + e[t.Zavrsena].class + ' kt-badge--inline kt-badge--pill">' + e[t.Zavrsena].title + "</span>"
                }                
            },
            {
                field: "Kasko",
                title: "Kasko",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0:{
                            title: "NE",
                            state: "danger"
                        },
                        1:{
                            title: "DA",
                            state: "success"
                        }
                    };
                    return '<span class="kt-badge kt-badge--' + e[t.Kasko].state + ' kt-badge--dot"></span>&nbsp;<span class="kt-font-bold kt-font-' + e[t.Kasko].state + '">' + e[t.Kasko].title + "</span>"
                }
            }],
            translate: {
                records: {
                    processing: "Učitavanje...",
                    noRecords: "Nema rezultata"
                },
                toolbar: {
                    pagination: {
                        items: {
                            default: {
                                first: "Prva",
                                prev: "Prethodna",
                                next: "Sljedeća",
                                last: "Posljednja"                                
                            },
                            info: "Prikaz {{start}} - {{end}} od ukupno {{total}} rezultata"
                        }
                    }
                }
            }
        }), $("#kt_form_poslovnica").on("change", function() {
            t.search($(this).val().toLowerCase(), "VracanjePoslovnica")
        }), $("#kt_form_zavrsena").on("change", function() {
            t.search($(this).val().toLowerCase(), "Zavrsena")
        }), $("#kt_form_kasko").on("change", function() {
            t.search($(this).val().toLowerCase(), "Kasko")
        }), $("#kt_form_poslovnica,#kt_form_zavrsena,#kt_form_kasko").selectpicker()
    }
};


jQuery(document).ready(function() {
    KTDatatableHtmlTableDemo.init();
});