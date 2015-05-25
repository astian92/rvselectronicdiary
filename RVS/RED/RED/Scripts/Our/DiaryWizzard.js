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
            $('.product-list-validation').removeClass('collapse');
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

                    tests.push({ Id: id, Units: units, Name: name, Key: key });
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
                var units = test.find('.units');
                units.attr('name', 'Products[' + i + '].ProductTests[' + j + '].Units');
            }
        }


        var form = $(this);

        // Disable validation on fields that are disabled.
        // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
        form.validate().settings.ignore = ":disabled";

        // Start validation; Prevent form submission if false
        return form.valid();
    },
    onFinished: function (event, currentIndex) {
        var form = $(this);

        // Submit form input
        form.submit();
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
    $('.product-list-validation').addClass('collapse');
    $('.current').removeClass('error');
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

//$('.add-test-btn').on('click', function () {
//    console.log(this);
//});