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
                    field: "Poruka ID",
                    type: "number"
                },
                {
                field: "Sadržaj",
                type: "number"
            }, {
                field: "Akcija",
                title: "Akcija",
                autoHide: !1                
            }, {
                field: "Datumporuke",
                type: "date",
                format: "YYYY-MM-DD"
            }, {
                
                field: "Statusporuke",
                title: "Statusporuke",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0: {
                            title: "Nije pročitano",
                            class: " kt-badge--danger"
                        },
                        1: {
                            title: "Pročitano",
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
                    return '<span class="kt-badge ' + e[t.Statusporuke].class + ' kt-badge--inline kt-badge--pill">' + e[t.Statusporuke].title + "</span>"
                }
            }, {
                
                field: "Odgovoreno",
                title: "Odgovoreno",
                autoHide: !1,
                template: function(t) {
                    var e = {
                        0: {
                            title: "Nije odgovoreno",
                            class: " kt-badge--danger"
                        },
                        1: {
                            title: "Odgovoreno",
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
                    return '<span class="kt-badge ' + e[t.Odgovoreno].class + ' kt-badge--inline kt-badge--pill">' + e[t.Odgovoreno].title + "</span>"
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
            t.search($(this).val().toLowerCase(), "Statusporuke")
        }), $("#kt_form_type").on("change", function() {
            t.search($(this).val().toLowerCase(), "Odgovoreno")
        }), $("#kt_form_status,#kt_form_type").selectpicker()
    }
};


jQuery(document).ready(function() {
    KTDatatableHtmlTableDemo.init();
});