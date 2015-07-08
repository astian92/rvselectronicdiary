
var CustomPagination = function (Settings) {
    var self = this;
    this.settings = Settings;
    this.paginateElement;
    this.startPage = Settings.startPage || 1;

    this.ClickPrevious = function () {
        var pageField = self.paginateElement.find('.pagination-page-field');
        var page = pageField.val();

        if (page >= 2) {
            page = page - 1;
            //pageField.val(page);
        }

        return page;
    }

    this.ClickNext = function () {
        var pageField = self.paginateElement.find('.pagination-page-field');
        var page = pageField.val();

        page = Number(page) + 1;    

        //pageField.val(page);
        return page;
    }

    this.Create = function (selectorToAppendTo) {
        self.paginateElement = $('<div class="customPaginate">' +
                            '<ul>' +
                                '<li class="paginate_btn paginate_previous">' +
                                    '<a>Назад</a>' +
                                '</li>' +
                                '<li>' +
                                    '<input type="number" class="pagination-page-field" value="' + self.startPage + '" />' +
                                '</li>' +
                                '<li class="paginate_btn paginate_next">' +
                                    '<a>Напред</a>' +
                                '</li>' +
                            '</ul>' +
                        '</div>');

        var prevBtn = self.paginateElement.find('.paginate_previous');
        prevBtn.on("click", function () {
            var page = self.ClickPrevious();
            var pageField = self.paginateElement.find('.pagination-page-field');
            //var page = pageField.val();

            if (self.settings.clickBack) {
                self.settings.clickBack(self.settings.clickBackArguments, page);
            }
        });

        var nextBtn = self.paginateElement.find('.paginate_next');
        nextBtn.on("click", function () {
            var page = self.ClickNext();
            var pageField = self.paginateElement.find('.pagination-page-field');
            //var page = pageField.val();

            if (self.settings.clickNext) {
                self.settings.clickNext(self.settings.clickNextArguments, page);
            }
        });
        
        if (self.settings.onPageInputChange) {
            var pageField = self.paginateElement.find('.pagination-page-field');
            pageField.on('change', function () {
                var page = $(this).val();
                self.settings.onPageInputChange(self.settings.changePageFieldArguments, page);
            });
        }

        $(selectorToAppendTo).append(self.paginateElement);
    }

    

};