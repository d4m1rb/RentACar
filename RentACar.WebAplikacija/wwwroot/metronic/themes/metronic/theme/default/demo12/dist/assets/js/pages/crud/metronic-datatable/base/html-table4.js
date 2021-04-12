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
                    field: "Klijent ID",
                    type: "number",
                    autoHide: !1
                },
                {
                field: "Ime i prezime",
                autoHide: !1
                
            }, {
                field: "Grad",
                title: "Grad"               
            }, {
                field: "Dregistracije",
                type: "date",
                format: "DD-MM-YYYY",
                autoHide: !1
            }, {
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
                
                field: "Status",
                title: "Status",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0: {
                            title: "Neaktivan",
                            class: " kt-badge--danger"
                        },
                        1: {
                            title: "Aktivan",
                            class: " kt-badge--success"
                        },                        
                        2: {
                            title: "Delivered",
                            class: " kt-badge--danger"
                        },
                        3: {
                            title: "Canceled",
                            class: " kt-badge--primary"
                        },
                        4: {
                            title: "Pending",
                            class: "kt-badge--brand"
                        },                       
                        5: {
                            title: "Info",
                            class: " kt-badge--info"
                        },
                        6: {
                            title: "Danger",
                            class: " kt-badge--danger"
                        },
                        7: {
                            title: "Warning",
                            class: " kt-badge--warning"
                        }
                    };
                    return '<span class="kt-badge ' + e[t.Status].class + ' kt-badge--inline kt-badge--pill">' + e[t.Status].title + "</span>"
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
        }), $("#kt_form_status").on("change", function() {
            t.search($(this).val().toLowerCase(), "Status")
        }), $("#kt_form_type").on("change", function() {
            t.search($(this).val().toLowerCase(), "Odgovoreno")
        }), $("#kt_form_status,#kt_form_type").selectpicker()
    }
};


jQuery(document).ready(function() {
    KTDatatableHtmlTableDemo.init();
});