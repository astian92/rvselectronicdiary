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

$('#Quantity').keydown(function (e) {
    if (e.which == 13) {
        e.preventDefault();
        e.stopPropagation();
        $('.add-product-btn').click();
        return false;
    }
})

$('#Products').keydown(function (e) {
    if (e.which == 13) {
        e.preventDefault();
        e.stopPropagation();
        $('.add-product-btn').click();
        return false;
    }
})

$('.add-product-btn').click(function () {
    if (validateProductInfo() == false) {
        return false;
    }

    var rowCount = $('.product-list-table .product').length;

    var content = '<tr class="clickable-row product" key="' + guid() + '">' +
        '<td><span>' + rowCount + '</span></td>' +
        '<td colspan="2" class="issue-info">' +
            '<div ondblclick="updateVal(this)">' + $('#Products').val() + '</div>' +
            '<input class="productName" type="hidden" value="' + $('#Products').val() + '" name="Products[].Name" />' +
        '</td>' +
        '<td colspan="2">' +
            '<div ondblclick="updateVal(this)">' + $('#Quantity').val() + '</div>' +
            '<input class="productQuantity" type="hidden" value="' + $('#Quantity').val() + '" name="Products[].Quantity" />' +
        '</td>' +
        '<td class="text-right">' +
            '<a class="delete-product" onclick="deleteProduct(this)"><h3 style="margin: 0px">x</h3></a>' +
        '</td></tr>';

    $('.product-list-table tbody').append(content);

    $(content).find('.product').data('tests', {});

    $('#Products').val('');
    $('#Products').focus();
    $('#Quantity').val('');
    $('.product-list-table tbody .error-msg').remove();
    $('.table-error').html('');
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

$("#form").submit(function (e) {
    e.preventDefault();

    //Get Products and tests and name them correctly
    var dataIsValid = true;
    var products = $('.product');

    if (products.length < 1) {
        dataIsValid = false;
        $('.table-error').html('<div class="error-msg"><span style="color: red">Необходимо е да въведете поне един продукт!!</span></div>');
        return false;
    }
    for (var i = 0; i < products.length; i++) {
        var product = $(products[i]);

        product.find('.productName').attr('name', 'Products[' + i + '].Name');
        product.find('.productQuantity').attr('name', 'Products[' + i + '].Quantity');

        var productKey = product.attr('key');
        var tests = $('.test[for="' + productKey + '"]');
        if (tests.length < 1) {
            dataIsValid = false;
            $('.table-error').html('<div class="error-msg"><span style="color: red">Необходимо е да въведете поне по едно изследване на продукт!!</span></div>');
            return false;
        }

        for (var j = 0; j < tests.length; j++) {
            var test = $(tests[j]);

            var testId = test.find('.testId');
            testId.attr('name', 'Products[' + i + '].ProductTests[' + j + '].TestId');
            var testMethodId = test.find('.testMethodId');
            testMethodId.attr('name', 'Products[' + i + '].ProductTests[' + j + '].TestMethodId');
            var methodValue = test.find('.methodValue');
            methodValue.attr('name', 'Products[' + i + '].ProductTests[' + j + '].MethodValue');
            var remark = test.find('.remark');
            remark.attr('name', 'Products[' + i + '].ProductTests[' + j + '].Remark');
        }
    }

    var form = this;
    form.submit(); // submit bypassing the jQuery bound event
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
    var product = $(e).parent().parent();

    var productKey = product.attr('key');
    $('.test[for="' + productKey + '"]').remove();
    product.remove();

    if ($('.clickable-row.active').length == 0) {
        $('#loadTestViewBtn').attr('disabled', 'disabled');
    }
}

function deleteTest(e) { //this function is here so it wont be loaded every time in the partial view
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
    $('.product.active').each(function (index, item) {
        var testId = guid();
        var productId = $(item).attr('key');
        var content = createTestRow(productId, testId);

        if ($(item).next('.test').length == 0) {
            $(item).after(content);
        } else {
            $('.test[for="' + productId + '"]').last().after(content);
        }
    });

    $('.btn-close').click();
}

function createTestRow(productId, testId) {
    var testDd = $('#Tests option:selected');
    var selectedTestValue = testDd.val();
    var selectedTestText = testDd.text();
    var array = selectedTestValue.split('_');
    var selectedTestType = array[0];
    var selectedTestId = array[1];

    var testMethod = $('#TestMethodId').val();
    var methodValue = $('.methodValueBox').val();
    var remark = $('.remarkBox').val();

    var content = '<tr class="test" key="' + testId + '" for="' + productId + '">'+
                    '<td><input type="hidden" class="testId" value="' + selectedTestId + '"></td>' +
                    '<td><span class="label label-default">' + selectedTestType + '</span><input type="hidden" class="type" value="' + selectedTestType + '"></td>' +
                    '<td>' +
                        '<div>' + selectedTestText + '</div>' +
                        '<input type="hidden" class="name" value="' + selectedTestText + '">' +
                    '</td>' +
                    '<td>' +
                        '<div>' + methodValue + '</div>' +
                        '<input type="hidden" class="testMethodId" value="' + testMethod + '">' +
                        '<input type="hidden" class="methodValue" value="' + methodValue + '">' +
                    '</td>' +
                    '<td>' +
                        '<div>' + remark + '</div>' +
                        '<input type="hidden" class="remark" value="' + remark + '">' +
                    '</td>' +
                    '<td class="text-right">' +
                        '<a class="delete-test" onclick="deleteTest(this)">' +
                            '<h3 style="margin: 0px">x</h3>' +
                        '</a>' +
                    '</td></tr>';

    return content;
}