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
    }
});

$('.product-list-table').on('click', '.clickable-row', function (event) {
    if (window.event.ctrlKey) {
        $(this).addClass('active');
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

function loadTestMethods(dropDown) {
    var value = $(dropDown).val();
    var testId = value.substr(4, value.length);
    $.ajax({
        type: "GET",
        url: '/Diary/GetTestMethods?testId=' + testId,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            var methods = JSON.parse(data);
            var optionsAsString = "";
            for (var i = 0; i < methods.length; i++) {
                optionsAsString += "<option value='" + methods[i].Id + "'>" + methods[i].Method + "</option>";
            }

            var testMethodsDd = $(dropDown).parent().parent().find('.testMethods');
            $(testMethodsDd).empty().append(optionsAsString);
        }
    });
}

function loadTestMethodValue(dropDown) {
    var value = $(dropDown).val();
    var testId = value.substr(4, value.length);
    $.ajax({
        type: "GET",
        url: '/Diary/GetMethodValueForTest?testId=' + testId,
        contentType: "application/json; charset=utf-8",
        success: function (methodValue) {
            $(dropDown).parent().parent().find('.methodValueBox').val(methodValue);
        }
    });
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
    alert('YES');
}