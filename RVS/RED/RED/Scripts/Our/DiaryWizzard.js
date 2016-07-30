//var count = 0;
//var products = [];

$("#wizard").steps();
$("#form").steps({
    bodyTag: "fieldset",
    onStepChanging: function (event, currentIndex, newIndex) {
        // Always allow going backward even if the current step contains invalid fields!
        if (currentIndex > newIndex) {
            return true;
        }

        if (currentIndex == 1 && $('.product').length == 0) {
            //$('.product-list-validation').removeClass('collapse');
            $('.product-list-table tbody').append('<tr class="error-msg"><td><span style="color: red">Необходимо е да въведете поне един продукт!</span></td></tr>');
            return false;
        }

        if (newIndex == 2) {
            var products = [];

            $('.product').each(function () {
                var productName = $(this).find('.productName').val();
                var productKey = $(this).attr('key');
                var testSpans = $(this).find('.test');
                var tests = [];
                $(testSpans).each(function () {
                    var key = $(this).attr('id');
                    var id = $(this).find('.testId').val();
                    var units = $(this).find('.units').val();
                    var name = $(this).find('.name').val();
                    var type = $(this).find('.type').val();
                    var methodValue = $(this).find('.methodValue').val();
                    var remark = $(this).find('.remark').val();

                    tests.push({ Id: id, Units: units, Name: name, Key: key, MethodValue: methodValue, Remark: remark, Type: type });
                });
                products.push({ Name: productName, Key: productKey, Tests: tests });
            });

            var url = '/Diary/ProductsTests';
            $.ajax({
                type: "POST",
                url: url,
                data: { products: products },
                success: function (view) {
                    $('.products-tests').html(view);
                }
            });
        }

        var form = $(this);

        // Clean up if user went backward before
        if (currentIndex < newIndex) {
            // To remove error styles
            $(".body:eq(" + newIndex + ") label.error", form).remove();
            $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
        }

        // Disable validation on fields that are disabled or hidden.
        form.validate().settings.ignore = ":disabled,:hidden";

        // Start validation; Prevent going forward if false
        return form.valid();
    },
    onStepChanged: function (event, currentIndex, priorIndex) {

    },
    onFinishing: function (event, currentIndex) {

        //Get Products and tests and name them correctly
        var products = $('.product');
        
        for (var i = 0; i < products.length; i++) {
            var product = $(products[i]);

            product.find('.productName').attr('name', 'Products[' + i + '].Name');
            product.parent().find('.productQuantity').attr('name', 'Products[' + i + '].Quantity');

            var tests = product.find('.test');
            for (var j = 0; j < tests.length; j++) {
                var test = $(tests[j]);

                var testId = test.find('.testId');
                testId.attr('name', 'Products[' + i + '].ProductTests[' + j + '].TestId');
                var testMethodId = test.find('.testMethodId');
                testMethodId.attr('name', 'Products[' + i + '].ProductTests[' + j + '].TestMethodId');
                var units = test.find('.units');
                units.attr('name', 'Products[' + i + '].ProductTests[' + j + '].Units');
                var methodValue = test.find('.methodValue');
                methodValue.attr('name', 'Products[' + i + '].ProductTests[' + j + '].MethodValue');
                var remark = test.find('.remark');
                remark.attr('name', 'Products[' + i + '].ProductTests[' + j + '].Remark');
            }
        }

        var dataIsValid = true;
        for (var k = 0; k < products.length; k++) {
            product = $(products[k]);
            var tests = product.find('.test');
            if (tests.length < 1) {
                dataIsValid = false;
            }
        }

        if (dataIsValid == false) {
            $('.current').addClass('error');
            
            var testListTables = $('.test-list-table tbody');

            for (var i = 0; i < testListTables.length; i++) {
                var tableBody = $(testListTables[i]);
                var bodyChildren = tableBody.children();
                var count = bodyChildren.length;
                
                if (count == 0) {
                    $(tableBody).append('<tr class="error-msg"><td colspan="2"><span style="color: red">Необходимо е да въведете поне по едно изследване на продукт!!</span></td></tr>');
                }
            }
        }

        var form = $(this);
        // Disable validation on fields that are disabled.
        // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
        form.validate().settings.ignore = ":disabled";

        return dataIsValid;
    },
    onFinished: function (event, currentIndex) {
        var form = $(this);

        // Submit form input
        form.submit();
    },
    onCanceled: function () { 
        window.location = "/Diary/Index";
    },
    labels: {
        cancel: "Откажи",
        finish: "Готово",
        next: "Напред",
        previous: "Назад"
    }
})
//.validate({
//    errorPlacement: function (error, element) {
//        element.before(error);
//    }
//});

$('#data_1 .input-group.date').datepicker({
    format: "dd.m.yyyy",
    todayBtn: "linked",
    keyboardNavigation: false,
    forceParse: false,
    calendarWeeks: true,
    autoclose: true
});

$('.add-product-btn').click(function () {
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

    //var html = $('.product-list').html();

    //html += '<li>';
    //html += '<span>' + $('#Products').val() + '</span>';
    //html += '<input type="hidden" value="' + $('#Products').val() + '" name="Products[' + count + '].Name">';
    //html += '<input type="hidden" value="' + $('#Quantity').val() + '" name="Products[' + count + '].Quantity">';
    //html += '<input type="hidden" value="' + $('#QuantityLabel').val() + '" name="Products[' + count + '].QuantityLabel">';
    //html += '<input type="hidden" value="' + $('.chosen-select').val() + '" name="Products[' + count + '].Test.Id">';
    //html += '</li>';
    //$('.product-list').html(html);

    var content = '<tr><td class="col-md-2"><span class="label label-primary">Добавен</span></td>' +
        '<td class="issue-info product" key="' + guid() + '">' +
            $('#Products').val() +
            '<input class="productName" type="hidden" value="' + $('#Products').val() + '" name="Products[].Name"></td><td class="col-md-2">' +
            $('#Quantity').val() +
            '<input class="productQuantity" type="hidden" value="' + $('#Quantity').val() + '" name="Products[].Quantity"></td><td class="text-right">' +
            '<a class="delete-product" onclick="deleteProduct(this)"><h3 style="margin: 0px">x</h3></a></td></tr>';

    $('.product-list-table tbody').append(content);

    $(content).find('.product').data('tests', {});

    //products.push($('#Products').val());
    //count++;

    $('#Products').val('');
    $('#Quantity').val('');
    //$('.product-list-validation').addClass('collapse');
    $('.product-list-table tbody .error-msg').remove();
    $('.current').removeClass('error');
    $('#Products').focus();
});

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
