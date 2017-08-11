$('#data_1 .input-group.date').datepicker({
    format: "dd.m.yyyy",
    todayBtn: "linked",
    keyboardNavigation: false,
    forceParse: false,
    calendarWeeks: true,
    autoclose: true
});

$('#acceptance-date .input-group.date').datepicker({
    format: "dd.m.yyyy",
    todayBtn: "linked",
    keyboardNavigation: false,
    forceParse: false,
    calendarWeeks: true,
    autoclose: true
});

$('.input-group.time').clockpicker();

$('#Quantity').keyup(function (e) {
    if (e.which == 13) {
        $('.add-product-btn').click();
    }
})

$('#Products').keyup(function (e) {
    if (e.which == 13) {
        $('.add-product-btn').click();
    }
})

$('.add-product-btn').click(function () {
    if (validateProductInfo() == false) {
        return false;
    }

    var rowCount = $('.product-list-table tr').length;

    var content = '<tr class="clickable-row"><td class="col-md-2"><span>' + rowCount + '</span></td>' +
        '<td class="issue-info product" key="' + guid() + '">' +
            '<div ondblclick="updateVal(this)">' + $('#Products').val() + '</div>' +
            '<input class="productName" type="hidden" value="' + $('#Products').val() + '" name="Products[].Name" />' +
        '</td><td class="col-md-2">' +
            '<div ondblclick="updateVal(this)">' + $('#Quantity').val() + '</div>' +
            '<input class="productQuantity" type="hidden" value="' + $('#Quantity').val() + '" name="Products[].Quantity" />' +
        '</td><td class="text-right">' +
            '<a class="delete-product" onclick="deleteProduct(this)"><h3 style="margin: 0px">x</h3></a>' +
        '</td></tr>';

    $('.product-list-table tbody').append(content);

    $(content).find('.product').data('tests', {});

    $('#Products').val('');
    $('#Products').focus();
    $('#Quantity').val('');
    $('.product-list-table tbody .error-msg').remove();
});

$('.product-list-table').on('click', '.clickable-row', function (event) {
    if (!window.event.ctrlKey) {
        $(this).addClass('active').siblings().removeClass('active');
        $('#loadTestViewBtn').removeAttr('disabled');
    }
});

$('.product-list-table').on('click', '.clickable-row', function (event) {
    if (window.event.ctrlKey) {
        $(this).addClass('active');
        $('#loadTestViewBtn').removeAttr('disabled');
    }
});

function validateProductInfo() {
    if ($('#Products').val() == '') {
        $('.product-name-validation').removeClass('collapse');

        if ($('#Quantity').val() == '') {
            $('.product-quantity-validation').removeClass('collapse');
        }

        return false;
    }

    if ($('#Quantity').val() == '') {
        $('.product-quantity-validation').removeClass('collapse');
        return false;
    }
}

function deleteProduct(e, number) {
    $(e).parent().parent().remove();
    if ($('.clickable-row').length == 0) {
        $('#loadTestViewBtn').attr('disabled', 'disabled');
    }
}

function deleteTest(e) { //this function is here so it wont be loaded every time in the partial view
    var testKey = $(e).attr('key');
    $('#' + testKey).remove();

    $(e).parent().parent().remove();
}

function hideNameValidation() {
    if ($('#Products').val() != '') {
        $('.product-name-validation').addClass('collapse');
    }
}

function hideQuantityValidation() {
    if ($('#Quantity').val() != '') {
        $('.product-quantity-validation').addClass('collapse');
    }
}

function updateVal(currentEle) {
    var value = $(currentEle).html();

    $(currentEle).html('');
    $(currentEle).append('<input class="thVal form-control input-sm" type="text" value="" />');

    $(".thVal").focus();
    $(".thVal").val(value);
    $(".thVal").keyup(function (event) {
        if (event.keyCode == 13) {
            var inputValue = $(".thVal").val().trim();
            $(currentEle).html(inputValue);
            $(currentEle).parent().find('input').val(inputValue);
        }
    });

    $(document).click(function () {
        var inputValue = $(".thVal").val().trim();
        $(currentEle).html(inputValue);
        $(currentEle).parent().find('input').val(inputValue);
    });
}

function loadTestView() {
    var url = '/Diary/AddTests';
    $.ajax({
        type: "GET",
        url: url,
        success: function (view) {
            $('.modal-content').html(view);
        }
    });
}

function addTest() {
    var testKey = guid();
    var testDd = $('#Tests option:selected');
    var selectedTestValue = testDd.val();
    var selectedTestText = testDd.text();
    var array = selectedTestValue.split('_');
    var selectedTestType = array[0];
    var selectedTestId = array[1];

    var testMethod = $('#TestMethodId').val();
    var methodValue = $('.methodValueBox').val();
    var remark = $('.remarkBox').val();

    var content = '<tr class="test-row">' +
                '<td><span class="label label-primary">' + selectedTestType + '</span></td>' +
                '<td colspan="2">' + 
                    '<div class="col-md-11 test">' + selectedTestText + '</div>' +
                    '<div class="col-md-6"><p style="margin-top:10px;">' + methodValue + '</p></div>' +
                    '<div class="col-md-6"><p style="margin-top:10px;">' + remark + '</p></div>' +
                '</td>' +
                '<td class="text-right">' +
                    '<a class="delete-product" key="' + testKey + '" onclick="deleteTest(this)">' +
                        '<h3 style="margin: 0px">x</h3>' +
                    '</a>' +
                '</td></tr>';

    $.each($('.product-list-table tbody .clickable-row.active'), function (index, item) {
        if ($(item).siblings('.test-row').length == 0) {
            $(item).after(content);
        } else {
            $(item).siblings('.test-row').after(content);
        }

    });

    $('.btn-close').click();
}