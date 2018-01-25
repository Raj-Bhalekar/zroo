

var ShopOfferImageUpload = function (file) {
    this.file = file;
};

ShopOfferImageUpload.prototype.getType = function () {
    return this.file.type;
};
ShopOfferImageUpload.prototype.getSize = function () {
    return this.file.size;
};
ShopOfferImageUpload.prototype.getName = function () {
    return this.file.name;
};
ShopOfferImageUpload.prototype.doUpload = function (frm) {
    var that = this;
    var formData = new FormData();
    var token = $.getAntiForgeryToken();
    $('#progress-wrp').show();

    var form = $(offerfileImageCntrl)[0].files[0];
    var imgOffercntl = '#' + "Offerid" + frm.ProductID + "img";

    var dataString = new FormData();
    dataString.append('file', form);
    dataString.append('imgId', $(imgOffercntl).attr("name"));
    dataString.append('OfferDetails', JSON.stringify(frm));
    $.ajax({
        type: "POST",
        url: "/ShopOffers/EditOffer",
        xhr: function () {
            var myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) {
                myXhr.upload.addEventListener('progress', that.progressHandling, false);
            }
            return myXhr;
        },
        success: function (data) {
            $("#progress-wrp").hide(2000);

        },
        error: function (error) {
            $("#progress-wrp").hide(2000);

        },
        //async: true,
        data: dataString,
        cache: false,
        contentType: false,
        processData: false,
        timeout: 60000
    });
};

ShopOfferImageUpload.prototype.progressHandling = function (event) {
    var percent = 0;
    var position = event.loaded || event.position;
    var total = event.total;
    var progress_bar_id = "#progress-wrp";

    if (event.lengthComputable) {
        percent = Math.ceil(position / total * 100);
    }

    $(progress_bar_id + " .progress-bar").css("width", +percent + "%");
    $(progress_bar_id + " .status").text(percent + "%");
};

//<script type="text/javascript">
var offerfileImageCntrl = "";
function fileCheck(obj, offerIdImg, row_id, subGridId) {
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if ($.inArray($(obj).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
    }
    if (typeof FileReader !== "undefined") {
        var size = document.getElementById(row_id + '_OfferImageData').files[0].size;
        // check file size
        if (size > 500000) {
            alert("File size can not exceed 500kb");
            $(obj).val('');
        } else {

            readURL(obj, offerIdImg);
        }
    }

}

function readURL(input, prodIdImg) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(prodIdImg)
                .attr('src', e.target.result)
                .width(200)
                .height(200);
        };
        reader.readAsDataURL(input.files[0]);

    }
}

function saveOfferDetails(editedrowid, subGridId) {
    $("#progress-wrp" + " .progress-bar").css("width", +0 + "%");
    $("#progress-wrp" + " .status").text(0 + "%");
    $('#ShopOffers_grid').jqGrid("editCell", 0, 0, false);
    var dataFromTheRow = jQuery('#ShopOffers_grid').jqGrid('getRowData', editedrowid);
    var otherjsongridid = '#' + editedrowid + 'ProdDtlschldgrid' + subGridId;
    dataFromTheRow['ProductsJsonDetails'] = JSON.stringify($(otherjsongridid).jqGrid('getRowData'));
    fileImageCntrl = '#' + editedrowid + '_OfferImageData';
    var file = $(fileImageCntrl)[0].files[0];
    var upload = new Upload(file);
    upload.doUpload((dataFromTheRow)
   );
}


$(document).ready(function () {
    $("#vwofferStartDate").datepicker();
    $('#viewOfferbtnSearch').click(function () { SearchOffers(); });
    loadOffersGrid();
});

function SearchOffers() {
    $('#ShopOffers_grid').GridUnload();
    loadOffersGrid();
}

function loadOffersGrid() {
    
    $('#ShopOffers_grid').jqGrid({
        url: '/ShopOffers/GetOffersList/',
        datatype: "json",
        colNames: [
            // 'Select',
            "OfferID",
            "ShopID",
            "Offer Short Text",
            "Offer Start Date",
            "Offer End Date",
            "Offer On Brand",
            "On Products",
            "Active",
            "Offer Detail Text",
            "Create Date",
            "Created By",
            "Update Date",
            "UpdatedBy"
        ],
        postData: {
            offerShortDetails: $('#vwofferShortDetails').val(),
            offerStartDate: $('#vwofferStartDate').val(),
            offerEndDate: $('#vwofferEndDate').val(),
            offerOnBrand: $('#vwofferOnBrand').val(),
            isOfferOnProduct: $('#vwShopOfferIsOfferonBrand').val(),
            isActive: $('#vwShopOfferIsactive').val(),
            bsGnd: window.BSGnd
        },
        colModel: [
            {
                name: 'OfferID',
                index: 'OfferID',
                editable: false,
                hidden: false,
                width: 50,
                stype: 'int'
            },
            {
                name: 'ShopID',
                editable: true,
                index: 'ShopID',
                hidden: true,
                width: 40,
                stype: 'int'
            },
            {
                name: 'OfferShortText',
                editable: true,
                index: 'OfferShortText',
                width: 120,
                cellEdit: true
               
            },
            {
                name: 'OfferStartDate',
                index: 'OfferStartDate',
                editable: true, edittype: 'text',
                width : 110,
                editoptions: {
                    size: 10,
                    maxlengh: 10,
                    dataInit: function (el) {
                        $(el).datepicker({ dateFormat: 'yy-mm-dd' });
                    }
                }

            },
            {
                name: 'OfferEndDate',
                index: 'OfferEndDate',
                editable: true, edittype: 'text',
                width: 110,
                editoptions: {
                    size: 10,
                    maxlengh: 10,
                    dataInit: function (element) {
                        $(element).datepicker({ dateFormat: 'yy.mm.dd' });
                    }
                }
            },
            {
                name: 'OfferOnBrand',
                editable: true,
                index: 'OfferOnBrand',
                width: 110,
                sortable: true,
                align: 'center',
                cellEdit: true,
                edittype: 'select',
                formatter: 'select',
                editoptions: { value: getddlforVwOfferOptions("vwofferOnBrand") }
            },
            {
                name: 'IsOfferOnProducts',
                editable: true,
                edittype: 'checkbox',
                editoptions: { value: "true:false", defaultValue: "true" },
                index: 'IsOfferOnProducts',
                stype: 'boolean',
                //   formatter: cboxFormatter,
                align: 'center',
                width: 60
            },
            {
                name: 'IsActive',
                editable: true,
                edittype: 'checkbox',
                editoptions: { value: "true:false", defaultValue: "true" },
                index: 'IsActive',
                stype: 'boolean',
                align: 'center',
                //formatter: cboxFormatter,
                width: 60
            },
            {
                name: 'OfferDetailText',
                editable: true,
                index: 'OfferDetailText',
                width: 120,
                sortable: true,
                align: 'center',
                cellEdit: true
            
             
            },
            {
                name: 'CreateDate',
                index: 'CreateDate',
                editable: true, edittype: 'text',
                width: 80,
                editoptions: {
                    size: 10,
                    maxlengh: 10,
                    dataInit: function (element) {
                        $(element).datepicker({ dateFormat: 'yy.mm.dd' });
                    }
                }
            },
               {
                   name: 'CreatedBy',
                   editable: true,
                   index: 'CreatedBy',
                   width: 80,
                   align: 'center'
                   //stype: string
               },
            {
                name: 'UpdateDate',
                index: 'UpdateDate',
                editable: true, edittype: 'text',
                width: 80,
                editoptions: {
                    size: 10,
                    maxlengh: 10,
                    dataInit: function (element) {
                        $(element).datepicker({ dateFormat: 'yy.mm.dd' });
                    }
                }
            },
            {
                name: 'UpdatedBy',
                editable: true,
                width: 80,
                index: 'UpdatedBy'
            }
        ],
        jsonReader: {
            root: function (data) {

                return JSON.parse(data.Data);
            },
            page: 'Page',
            total: 'PageSize'
        },
        pager: $('#offergridPager'),
        'cellEdit': true,
        // iconSet: "fontAwesome",
        autoencode: true,
        viewrecords: false,
        rowNum: 5,
        idPrefix: "1",
        altRows: true,
        altclass: "myAltRowClass",
        rowList: [5, 10, 20, "10000:All"],
        caption: "Offers List",
        navOptions: {
            add: false,
            del: false,
            search: true,
            refresh: true,
            edit: true
        },
        searching: {
            closeAfterSearch: true,
            closeAfterReset: true,
            closeOnEscape: true,
            searchOnEnter: true,
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        },
        subGrid: true,
        multiselect: false,
        grouping: false,
        groupField: [''],
        subGridBeforeExpand: function (rowid, selected) {
            var rowIds = $("#ShopOffers_grid").getDataIDs();
            $.each(rowIds,
                function (index, rowId) {
                    $("#ShopOffers_grid").collapseSubGridRow(rowId);
                });
        },
        subGridRowExpanded: function (subgrid_id, row_id) {
            callAjaxImg(subgrid_id, row_id);
        }
        //Edit Options. save key parameter will keybind the Enter key to submit.

    });//.jqGrid('navGrid');
    $('#ShopOffers_grid').jqGrid('navGrid', '#offergridPager',
        {
            edit: true, edittitle: "Edit Post", width: 500,
            add: false, addtitle: "Add Post", width: 500,
            del: false,
            search: true,
            refresh: true,
            view: true
        },
        //Edit Options. save key parameter will keybind the Enter key to submit.
        {
            editCaption: "Edit Offer",
            edittext: "Edit",
            closeOnEscape: true,
            closeAfterEdit: true,
            savekey: [true, 13],
            //errorTextFormat: commonError,
            width: "500",
            reloadAfterSubmit: true,
            bottominfo: "Fields marked with (*) are required",
            onclickSubmit: function (response, postdata) {
                EditPost(postdata);
            }
        });
}

function getddlforVwOfferOptions(getFrom) {
    var categoryList = [];
    var ddlFrom = "#" + getFrom + " option";
    $(ddlFrom).each(function () {
        categoryList[$(this).val()] = $(this).text();
    });
    return categoryList;
}


function callAjaxImg(subgrid_id, row_id) {
    var rowDatapid = $('#Product_grid').getRowData(row_id);
    var colDatapid = rowDatapid['ProductID'];
    var resultdata = '';
    $.ajax({
        url: "/ShopOffers/GetOfferImage?id=" + colDatapid + "&bsGnd=" + BSGnd,
        type: 'GET',

        success: function (data) {

            jQuery('#' + subgrid_id)
                .append("<div class='row'><div class='col-sm-6'>" + data
                    + "</div><div class='col-sm-6'> <table id='" + row_id + "ProductDtlschldgrid" + subgrid_id
                    + "' class='display' cellspacing='0' width='100%'></table><div id='"
                    + row_id + "pager2chldProductDtls'></div><div id='" + row_id + "footerchldOffergrid'></div> " + "</div></div>"
                    + "<div class='row'><div style='align:right' class='col-sm-6'>"
                    + "<input type='file' name='ImageData' onchange='fileCheck(this, \"#Prodid" + colDatapid + "img\", \"" + row_id + "\", \"" + subgrid_id + "\");' id='" + row_id
                    + "_ImageData' accept='.jpg,.jpeg,.png' />"
                    + " <div id='progress-wrp'><div class='progress-bar'></div><div class='status'>0%</div></div></div>"
                    + "<div style='align:right' class='col-sm-6'>"
                    + "<input type='button' class='btn btn-sm btn-primary' value = 'Save' onClick=\"saveProductDetails('" + row_id + "', '" + subgrid_id + "')\" />" + "</div></div>"
                    );
            resultdata = data;
            showOtherDetails(subgrid_id, row_id);
            var childPager = '#' + row_id + "pager2chldOtherDtls";
            var childgrid = '#' + row_id + "OtherDtlschldgrid" + subgrid_id;

            $(childgrid).jqGrid('navGrid', childPager).focusout(function () {
                // need stop row edit mode
            });
        },
        error: function (request, error) {
            jQuery('#' + subgrid_id)
                .append("<div class='row'><div class='col-sm-6'>" + "" +
                    "</div><div class='col-sm-6'> <table id='" + row_id + "OtherDtlschldgrid" + subgrid_id
                    + "' class='display' cellspacing='0' width='100%'></table><div id='"
                    + row_id + "pager2chldOtherDtls'></div><div id='" + row_id + "footerchldgrid'></div> </div></div>");
            resultdata = null;
            showOtherDetails(subgrid_id, row_id);
        }
    });

    return resultdata;
}

function showOtherDetails(subgrid_id, row_id) {

    var rowDatav = $('#Product_grid').getRowData(row_id);
    var colDatav = rowDatav['OtherJsonDetails'];

    var gndchildgrd = "#" + row_id + "OtherDtlschldgrid" + subgrid_id;

    var pager2chld = "#" + row_id + "pager2chldOtherDtls";
    $(gndchildgrd).jqGrid({
        data: JSON.parse(colDatav),
        datatype: "local",
        colNames: ['id', 'Key', 'Value'],
        colModel: [
            {
                name: 'id',
                index: 'id',
                editable: false,
                hidden: true,
                width: 20,
                sorttype: "int"
            },
            {
                name: 'Key',
                index: 'Key',
                editable: true,

                width: 200,
                sorttype: "date"
            },
            {
                name: 'Value',
                editable: true,
                index: 'Value',
                width: 200
            }
        ],
        pager: $(pager2chld),
        'cellEdit': true,
        autoencode: true,
        viewrecords: false,
        rowNum: 10,
        idPrefix: "1",
        altRows: true,
        altclass: "myAltRowClass",
        rowList: [5, 10, 20, "10000:All"],
        caption: "Product Other Details",
        navOptions: {
            add: true,
            del: true,
            search: false,
            refresh: false,
            edit: true
        },
        //afterEditCell: function () {
        //    alert('le');
        //    jQuery(gndchildgrd).jqGrid('restoreRow', lastSelectRoot);
        //},
        searching: {
            closeAfterSearch: true,
            closeAfterReset: true,
            closeOnEscape: true,
            searchOnEnter: true,
            multipleSearch: true,
            multipleGroup: true,
            showQuery: true
        }
    });
    $(gndchildgrd).jqGrid('navGrid', pager2chld, { edit: false, add: true, del: true });
}

function EditPost(params) {
    //var token = $('input[name="__RequestVerificationToken"]').val();
    //var headers = {};
    //headers['__RequestVerificationToken'] = token;

    $.postAntiForgery("/Product/EditProduct",
        params,
        null,
        "POST"
    );
    //$.ajax({
    //    type: "POST",
    //    url: "/Product/EditProduct",
    //    headers: headers,
    //    data: $(params).serialize(),
    //    dataType: "json",
    //    success: function (result) {
    //       //$(targetProgress).hide();
    //       // $('#btnSignUpubmit').removeClass('btn-disable').addClass('btn-primary');
    //       // $('#btnLoginSubmit').prop('disabled', false);
    //       // $('#btnSignUpubmit').prop('disabled', false);
    //       // $('#btnLoginSubmit').removeClass('btn-disable').addClass('btn-primary');

    //        if (result.Status === "FailedFromServer") {
    //            var container = $('span[data-valmsg-for]');
    //            container.html('');
    //            container.addClass("field-validation-valid").removeClass("field-validation-error");
    //            alert(result.Errors.ServerError);

    //        }
    //        if (result.Status === "ServerSuccess") {

    //        }
    //        else {

    //        }
    //    },
    //    error: function (xhr, textStatus, errorThrown) {
    //        //$(targetProgress).hide();
    //        alert("Sorry your details failed to save!");
    //    }
    //});
}

function cboxFormatter(cellvalue, options, rowObject) {
    return "<input type='checkbox' onclick='AddSelected(this,\"" +
        rowObject.ProductID +
        "\",\"" +
        rowObject.ProductName +
        "\"" + ")'/>";
}


