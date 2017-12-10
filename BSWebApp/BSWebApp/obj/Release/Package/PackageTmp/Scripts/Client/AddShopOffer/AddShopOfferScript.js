var mydata = [];

function onLoadBS(chck) {
    if (chck.checked) {
        $("#divProductGrid").css("visibility", "visible");
        loadProductGrid();

    } else {
        $("#divProductGrid").css("visibility", "hidden");
    }
}

function loadProductGrid() {
    //if (! $.fn.dataTable.isDataTable("#ProductGrid")) {
    //    $("#ProductGrid").DataTable({
    //        "processing": true,
    //        "serverSide": true,
    //        ajax: {
    //            url: "/shopoffers/getproductlist/",
    //            "dataSrc": function (json) {
    //                var x = JSON.parse(json.data);
    //                return x;
    //            }
    //        },
    //        'columnDefs': [
    //     {
    //         'targets': 0,
    //         'checkboxes': {
    //             'selectRow': true
    //         }
    //     }
    //        ],
    //        'select': {
    //            'style': 'multi'
    //        },
    //        order: [[ 1, 'asc' ]],
    //        columns: [
    //          //  { data: "IsSelected" },
    //            { data: "ProductID" },
    //            { data: "ProductName" },
    //            { data: "ProductBrand" },
    //            { data: "ProductCategoryName" },
    //            { data: "ProductTypeName" },
    //            { data: "ProductSubTypeName" }
    //        ]
    //    });
    //}


    $('#Product_grid').jqGrid({
        url: '/shopoffers/getproductlist/',
        datatype: "json",
        colNames: ['Selected', 'Product ID', 'Product Name', 'Brand', 'Category', 'Type', 'SubType'],
        postData: { brand: "lawda" },
        colModel: [
            {
                name: 'IsSelected',
                index: 'IsSelected',
                formatter: cboxFormatter,
                //formatoptions: {disabled : false},
                //edittype:"checkbox",
                editable: true,
                hidden: false,
                width: 20
            },
            {
                name: 'ProductID',
                index: 'ProductID',
                editable: false,
                width: 200
            },
            {
                name: 'ProductName',
                editable: false,
                index: 'ProductName',
                width: 200
            },
            {
                name: 'ProductBrand',
                editable: false,
                index: 'ProductBrand',
                width: 200
            },
            {
                name: 'ProductCategoryName',
                editable: false,
                index: 'ProductCategoryName',
                width: 200
            },
            {
                name: 'ProductTypeName',
                editable: false,
                index: 'ProductTypeName',
                width: 200
            },
            {
                name: 'ProductSubTypeName',
                editable: false,
                index: 'ProductSubTypeName',
                width: 200
            }
        ],
        jsonReader: {
            root: function (data) {
              
                return JSON.parse(data.Data);
            },
            page: 'Page',
            total: 'PageSize' 
        },
        pager: $('#gridPager'),
        // iconSet: "fontAwesome",
        autoencode: true,
        viewrecords: false,
        rowNum: 5,
        idPrefix: "1",
        altRows: true,
        altclass: "myAltRowClass",
        rowList: [5, 10, 20, "10000:All"],
        caption: "Product List",
        navOptions: {
            add: false,
            del: false,
            search: true,
            refresh: true,
            edit: false
        },
        searching: {
            closeAfterSearch: true,
            closeAfterReset: true,
            closeOnEscape: true,
            searchOnEnter: true,
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        }
    }).jqGrid("navGrid");

    

    $.ajax({
        url: '/shopoffers/getproductlist1/',
        success: function (result) {
            
        }

    });

   

    $('#SelectedProduct_grid').jqGrid({
        datatype: "local",
        colNames: ['Selected', 'Product ID', 'Product Name'],
        colModel: [
            {
                name: 'IsSelected',
                index: 'IsSelected',
                formatter: "checkbox",
                formatoptions: { disabled: false },
                edittype: "checkbox",
                editable: true,
                hidden: false,
                width: 20
            },
            {
                name: 'ProductID',
                index: 'ProductID',
                editable: false,
                width: 200
            },
            {
                name: 'ProductName',
                editable: false,
                index: 'ProductName',
                width: 200
            }
        ],
        jsonReader: {
            root: function (data) {

                return JSON.parse(data.Data);
            },
            page: 'Page',
            total: 'PageSize'
        },
        pager: $('#SelectedgridPager'),
        // iconSet: "fontAwesome",
        autoencode: true,
        viewrecords: false,
        rowNum: 5,
        idPrefix: "1",
        altRows: true,
        altclass: "myAltRowClass",
        rowList: [5, 10, 20, "10000:All"],
        caption: "Selected Product List",
        navOptions: {
            add: false,
            del: false,
            search: true,
            refresh: true,
            edit: false
        },
        searching: {
            closeAfterSearch: true,
            closeAfterReset: true,
            closeOnEscape: true,
            searchOnEnter: true,
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        }
    }).jqGrid("navGrid");


    bindAfterLoad($("#Product_grid"));
    bindAfterLoad($("#SelectedProduct_grid"));

}

function bindAfterLoad(grd) {
    grd.bind("jqGridAfterLoadComplete", function () {
        var $this = $(this), iCol, iRow, rows, row, cm, colWidth,
            $cells = $this.find(">tbody>tr>td"),
            $colHeaders = $(this.grid.hDiv).find(">.ui-jqgrid-hbox>.ui-jqgrid-htable>thead>.ui-jqgrid-labels>.ui-th-column>div"),
            colModel = $this.jqGrid("getGridParam", "colModel"),
            n = $.isArray(colModel) ? colModel.length : 0,
            idColHeadPrexif = "jqgh_" + this.id + "_";

        $cells.wrapInner("<span class='mywrapping'></span>");
        $colHeaders.wrapInner("<span class='mywrapping'></span>");

        for (iCol = 0; iCol < n; iCol++) {
            cm = colModel[iCol];
            colWidth = $("#" + idColHeadPrexif + $.jgrid.jqID(cm.name) + ">.mywrapping").outerWidth() + 25; // 25px for sorting icons
            for (iRow = 0, rows = this.rows; iRow < rows.length; iRow++) {
                row = rows[iRow];
                if ($(row).hasClass("jqgrow")) {
                    colWidth = Math.max(colWidth, $(row.cells[iCol]).find(".mywrapping").outerWidth());
                }
            }
            $this.jqGrid("setColWidth", iCol, colWidth);
        }
    });
}

function cboxFormatter(cellvalue, options, rowObject) {
    return "<input type='checkbox' onclick='AddSelected(this,\""
            + rowObject.ProductID + "\",\""+rowObject.ProductName+"\""
           + ")'/>";
}

function refreshGrid($grid, results) {
    $grid.jqGrid("clearGridData");
    $grid.jqGrid('setGridParam',
        {
            datatype: 'local',
            data: results
        })
    .trigger("reloadGrid");
}
function checkAvailability(arr, val) {
    return arr.some(arrVal => arrVal.ProductID===val);
}
function AddSelected(chk,pId,pName) {
    //debugger;
    var selectdCount= mydata.length;
    
    if (chk.checked === false) {
        mydata = $.grep(mydata, function (e) {
            return e.ProductID !== pId;
        });

    } else {
       // alert(checkAvailability(mydata, pId));
        if (!checkAvailability(mydata, pId)) {
            if (mydata != null && mydata.length > 0) {
                mydata[selectdCount] = {};
                mydata[selectdCount]["IsSelected"] = 1;
                mydata[selectdCount]["ProductID"] = pId;
                mydata[selectdCount]["ProductName"] = pName;
            } else {

                mydata[0] = {};
                mydata[0]["IsSelected"] = 1;
                mydata[0]["ProductID"] = pId;
                mydata[0]["ProductName"] = pName;
            }
        }
    }
   // debugger;
    refreshGrid($("#SelectedProduct_grid"), mydata);
    //  alert(mydata[2].ProductName);
}

function onSubmit_AddOffer() {
    $("#ShopOffer_hidselprodList").val(JSON.stringify(mydata));
}

$(document).ready(function () {

    onLoadBS($("#ShopOffer_chkIsOfferOnProducts"));

    $("#ShopOffer_chkIsOfferOnProducts").change(function () {
        onLoadBS(this);
    });

    $("#btnSumbit_AddOffer").bind("click",onSubmit_AddOffer);

});