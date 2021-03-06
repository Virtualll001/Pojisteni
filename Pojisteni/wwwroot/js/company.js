var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "language": {
            "emptyTable": "Tabulka neobsahuje žádná data",
            "info": "Zobrazuji _START_ až _END_ z celkem _TOTAL_ záznamů",
            "infoEmpty": "Zobrazuji 0 až 0 z 0 záznamů",
            "infoFiltered": "(filtrováno z celkem _MAX_ záznamů)",
            "loadingRecords": "Načítám...",
            "zeroRecords": "Žádné záznamy nebyly nalezeny",
            "paginate": {
                "first": "První",
                "last": "Poslední",
                "next": "Další",
                "previous": "Předchozí"
            },
            "searchBuilder": {
                "add": "Přidat podmínku",
                "clearAll": "Smazat vše",
                "condition": "Podmínka",
                "conditions": {
                    "date": {
                        "after": "po",
                        "before": "před",
                        "between": "mezi",
                        "empty": "prázdné",
                        "equals": "rovno",
                        "not": "není",
                        "notBetween": "není mezi",
                        "notEmpty": "není prázdné"
                    },
                    "number": {
                        "between": "mezi",
                        "empty": "prázdné",
                        "equals": "rovno",
                        "gt": "větší",
                        "gte": "rovno a větší",
                        "lt": "menší",
                        "lte": "rovno a menší",
                        "not": "není",
                        "notBetween": "není mezi",
                        "notEmpty": "není prázdné"
                    },
                    "string": {
                        "contains": "obsahuje",
                        "empty": "prázdné",
                        "endsWith": "končí na",
                        "equals": "rovno",
                        "not": "není",
                        "notEmpty": "není prázdné",
                        "startsWith": "začíná na"
                    },
                    "array": {
                        "equals": "rovno",
                        "empty": "prázdné",
                        "contains": "obsahuje",
                        "not": "není",
                        "notEmpty": "není prázdné",
                        "without": "neobsahuje"
                    }
                },
                "data": "Sloupec",
                "logicAnd": "A",
                "logicOr": "NEBO",
                "title": {
                    "0": "Rozšířený filtr",
                    "_": "Rozšířený filtr (%d)"
                },
                "value": "Hodnota",
                "button": {
                    "0": "Rozšířený filtr",
                    "_": "Rozšířený filtr (%d)"
                },
                "deleteTitle": "Smazat filtrovací pravidlo"
            },
            "autoFill": {
                "cancel": "Zrušit",
                "fill": "Vyplň všechny buňky textem <i>%d<i><\/i><\/i>",
                "fillHorizontal": "Vyplň všechny buňky horizontálně",
                "fillVertical": "Vyplň všechny buňky vertikálně"
            },
            "buttons": {
                "collection": "Kolekce <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "copy": "Kopírovat",
                "copySuccess": {
                    "1": "Skopírován 1 řádek do schránky",
                    "_": "SKopírováno %d řádků do schránky"
                },
                "copyTitle": "Kopírovat do schránky",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Zobrazit všechny řádky",
                    "_": "Zobrazit %d řádků"
                },
                "pdf": "PDF",
                "print": "Tisknout",
                "colvis": "Viditelnost sloupců",
                "colvisRestore": "Resetovat sloupce",
                "copyKeys": "Zmáčkněte ctrl or u2318 + C pro zkopírování dat.  Pro zrušení klikněte na tuto zprávu nebo zmáčkněte esc.."
            },
            "searchPanes": {
                "clearMessage": "Smazat vše",
                "collapse": {
                    "0": "Vyhledávací Panely",
                    "_": "Vyhledávací Panely (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown} ({total})",
                "emptyPanes": "Žádné Vyhledávací Panely",
                "loadMessage": "Načítám Vyhledávací Panely",
                "title": "Aktivních filtrů - %d"
            },
            "select": {
                "cells": {
                    "1": "Vybrán 1 záznam",
                    "_": "Vybráno %d záznamů"
                },
                "columns": {
                    "1": "Vybrán 1 sloupec",
                    "_": "Vybráno %d sloupců"
                }
            },
            "aria": {
                "sortAscending": "Aktivujte pro seřazení vzestupně",
                "sortDescending": "Aktivujte pro seřazení sestupně"
            },
            "lengthMenu": "Zobrazit _MENU_ výsledků",
            "processing": "Zpracovávání...",
            "search": "Vyhledávání:",
            "thousands": ",",
            "datetime": {
                "previous": "Předchozí",
                "next": "Další",
                "hours": "Hodiny",
                "minutes": "Minuty",
                "seconds": "Vteřiny",
                "unknown": "-",
                "amPm": [
                    "Dopoledne",
                    "Odpoledne"
                ]
            },
            "editor": {
                "close": "Zavřít",
                "create": {
                    "button": "Nový",
                    "title": "Nový záznam",
                    "submit": "Vytvořit"
                },
                "edit": {
                    "button": "Změnit",
                    "title": "Změnit záznam"
                }
            },
            "infoThousands": " "
        },


        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked/>`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/Company/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Určitě chcete tuto kategorii smazat?",
        text: "Data nebude možné obnovit!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
