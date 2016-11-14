(function () {
    'use strict';

    // define app, include Wijmo 5 directives
    var app = angular.module('app');
    // controller
    app.controller('appCtrl', function ($scope, $q, dataService, flexgridService) {

        // expose data as a CollectionView to get events
        $scope.collectionView = [];
        var colsBinding = [];
        var flexGrid;

        // use the "initialized" event to initialize the second grid
        $scope.init = function (s, e) {
            flexGrid = s;
            initializeColumns(s);
        }

        // initialize the columns on a FlexGrid    
        function initializeColumns(flex) {
            let promises = [flexgridService.getColumns(), flexgridService.getDatas()];

            $q.all(promises).then(function(values){
                var cols = values[0];
                var datas = values[1];

                //Define Columns
                angular.forEach(cols, function (value, key) {
                    var col = new wijmo.grid.Column();
                    col.binding = value.Name;
                    col.header = value.Text;
                    col.isReadOnly = value.IsReadOnly;
                    //var col = { binding: value.Name, header: value.Text, isReadOnly: value.IsReadOnly };
                    colsBinding.push(col);
                    flex.columns.push(col);
                });

                //Bind Datas
                $scope.collectionView = new wijmo.collections.CollectionView(datas);
                $scope.collectionView.trackChanges = true;

                flex.initialize({
                    //columns: colsBinding,
                    itemsSource: $scope.collectionView,
                    selectionMode: 'Row', //Values: None, Cell, CellRange, Row, RowRange, ListBox
                });
            });

        }

        function getDatas() {
            flexgridService.getDatas().then(function (result) {
                var hostElement = document.getElementById('mainFlexGrid');
                var flexGrid = wijmo.Control.getControl(hostElement);

                initializeColumns(flexGrid);
            }, function (error) {
            })
        };

        // Tell scope when current item changes
        // TODO: add validation for cell editing
        $scope.onCellEditEnding = function (s, e) {
            var flex = s;

            var oldVal = flex.getCellData(e.row, e.col);
            var newVal = flex.activeEditor.value;

            //e.cancel = true; // Cancel event
        };

        $scope.onCellEditEnded = function (s, e) {
            //alert('onCellEditEnded');

        };

        // Forbid changing the current record when the 4th one is current.
        var stopCurrentIn4th = function (sender, e) {
            // When the current record is the 4th record, stop moving.
            if (sender.currentPosition === 3) {
                e.cancel = true;
            }
        };

        $scope.stopCurrent = function () {
            $scope.collectionView.currentChanging.addHandler(stopCurrentIn4th);
        };

        // Restore the ability to change the current record.
        $scope.reset = function () {
            $scope.collectionView.currentChanging.removeHandler(stopCurrentIn4th);
        };

        $scope.updateEditedRows = function () {
            var editedRows = $scope.collectionView.itemsEdited;
            var createdRows = $scope.collectionView.itemsAdded;
            var deletedRows = $scope.collectionView.itemsRemoved;

            flexgridService.updateData(editedRows[0]).then(function () {
                //Success
                alert("Success");
            }, function (error) {
                //Error
            });
        };

        $scope.addNewRow = function () {
            var newRows = $scope.collectionView.itemsAdded;
            angular.forEach(newRows, function (value, key) {
                flexgridService.addData(value).then(function (result) {
                    window.location.reload();
                });
            });
        };

    });

})();