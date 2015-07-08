
var Tables = (function () {
    var Tables = {}; //will be filled with mTables

    var tables = function () {
        return Tables;
    }

    var get = function (selector) {
        return Tables[selector];
    }

    //added my parameters as properties of settings!
    var CreateTable = function (selector, settings) {
        if (selector == undefined || selector == '') {
            throw "Incorrect selector, you need to assign a selector for the table on which to apply the DataTables mode!"
        }

        var table = new mTable(selector, settings, settings.ajaxData, settings.disableDefaultSearch, settings.toolBarSelector);
        Tables[selector] = table; //overriding if same selector is newly applied
    }

    return {
        tables: tables,
        get: get,
        CreateTable: CreateTable
    }
}())


var mTable = function (selector, settings, ajaxData, disableDefaultSearch, toolBarSelector) {
    var self = this;
    this.selectedRow = null;
    this.selectedRows = [];

    this.ajaxData = ajaxData || {};
    settings.ajax.data = function (d) {
        for (var prop in self.ajaxData) {
            d[prop] = self.ajaxData[prop];
        }
    };

    this.reload = function (data) {
        if (data != false) { //so sending false as parameter will not undo last parameters !
            self.ajaxData = data;
        }
        //remove previous selections
        self.selectedRow = null;
        self.selectedRows = [];
        //controlled through the ajaxData, thats why its also exposed
        self.dataTable.ajax.reload(null, false); //false keeps the paging of the table ! and the first argument is callback 
    }

    //needs to be before so it can change settings !
    if (toolBarSelector != undefined && toolBarSelector != '') {
        settings.dom = '<"toolbar">frtip'; //!This is the toolbar (with buttons)
    }

    this.dataTable = $(selector).DataTable(settings); //this works sync so ... 

    //again, because first time it was to change the settings to include the dom setting
    if (toolBarSelector != undefined && toolBarSelector != '') {
        var toolbar = $(toolBarSelector);
        toolbar.remove();
        $(selector + "_wrapper .toolbar").append(toolbar);

        var searchBox = $(selector + '_wrapper .dataTables_filter');
        toolbar.append(searchBox);
    }

    if (!disableDefaultSearch && settings.searching != false) {
        var sBox = $(selector + "_wrapper .dataTables_filter input[type='search']").addClass("text-box");
        var el1 = $(selector + '_wrapper .dataTables_filter label').get(0).firstChild.nodeValue = '';
        $(selector + "_wrapper .dataTables_filter label")
            .append('<button class="searchBtn" title="search">' +
                        '<span class="glyphicon glyphicon-search"></span>' +
                    '</button>');
    }

    this.singleClick = settings.singleClick;

    function selectRow() {

    }

    if (settings.selectable) {
        $(selector + ' tbody').on('click', 'tr', function (e) {
            if ($(selector).attr('disabled') != 'disabled') {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                    self.selectedRow = null;
                }
                else {
                    self.dataTable.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                    self.selectedRow = $(this);
                }
            }

            if (typeof self.singleClick == "function") {
                self.singleClick(this, e);
            }
        });
    }

    //multi selectable option !!
    if (settings.multiSelectable) {
        $(selector + ' tbody').on('click', 'tr', function (e) {
            if (e.ctrlKey) {
                if ($(this).hasClass('selected')) {
                    var index = self.selectedRows.indexOf(this);
                    if (index > -1) {
                        self.selectedRows.splice(index, 1);
                    }
                    $(this).removeClass('selected');
                }
                else {
                    self.selectedRows.push(this);
                    $(this).addClass('selected');
                }
            }
            else {
                var hadSelected = $(this).hasClass('selected');
                self.dataTable.$('tr.selected').removeClass('selected');
                self.selectedRows = [];

                if (!hadSelected) {
                    self.selectedRows.push(this);
                    $(this).addClass('selected');
                }
            }

            if (typeof self.singleClick == "function") {
                self.singleClick(this, e);
            }
        });
    }

    this.getRowId = function (row, idColumnIndex) {
        var rowIndex = this.dataTable.row(row).index();
        var idIndex = idColumnIndex || 0;

        var id = this.dataTable.cell(rowIndex, idIndex).data();
        return id;
    }

    this.doubleClick = settings.doubleClick;
    if (this.doubleClick) {
        $(selector + ' tbody').on('dblclick', 'tr', function (event) {
            self.dataTable.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            if (settings.multiSelectable) {
                self.selectedRows = [];
                self.selectedRows.push(this);
            } else {
                self.selectedRow = $(this);
            }
            if (typeof self.doubleClick == "function") {
                self.doubleClick(this, event);
            }
            self.selectedRow = null;
            self.selectedRows = [];
            $(this).parent().find('tr').removeClass('selected');
        });
    }

    this.GetRowDomFromDataValue = function(value, valueName) {
        var indexes = self.dataTable.rows();

        for (var i = 0; i < indexes[0].length; i++) {
            var index = indexes[0][i];
            var rowData = self.dataTable.row(index).data();

            if (valueName) {
                if (rowData[valueName] == value) {
                    return self.dataTable.row(index).node();
                }
            }
            else {
                if (rowData["Id"]) {
                    if (rowData["Id"] == value) {
                        return self.dataTable.row(index).node();
                    }
                }
            }
        }
    }

}