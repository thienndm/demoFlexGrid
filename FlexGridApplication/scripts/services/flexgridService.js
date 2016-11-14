(function () {
    'use strict';

    var app = angular.module('app');

    app.factory('flexgridService', flexgridService);
    flexgridService.$inject = ['$q', 'baseService'];
    function flexgridService($q, baseService) {
        // constructor
        var flexgridService = function () {
        }
        flexgridService.prototype = new baseService('vehicles');
        flexgridService.prototype.constructor = flexgridService;

        // methods
        flexgridService.prototype.getColumns = function () {
            var url = String.format('{0}/getVehicleColumns', this.api);

            var q = $q.defer();
            this.getData(url).then(function (result) {
                q.resolve(result);
            }, function (error) {
                q.reject(error);
            })
            return q.promise;
        }

        flexgridService.prototype.getDatas = function () {
            var url = String.format('{0}/GetAllVehicles', this.api);

            var q = $q.defer();
            this.getData(url).then(function (result) {
                q.resolve(result);
            }, function (error) {
                q.reject(error);
            })
            return q.promise;
        }

        flexgridService.prototype.addData = function (data) {
            var url = String.format('{0}/PostVehicle', this.api);

            var q = $q.defer();
            this.add(url, data).then(function (result) {
                q.resolve(result);
            }, function (error) {
                q.reject(error);
            })
            return q.promise;
        }

        flexgridService.prototype.updateData = function (data) {
            var url = String.format('{0}/PutVehicle', this.api);

            var q = $q.defer();
            this.update(url, data).then(function (result) {
                q.resolve(result);
            }, function (error) {
                q.reject(error);
            })
            return q.promise;
        }

        flexgridService.prototype.deleteData = function (AccountId) {
            var url = String.format('{0}/delete/{1}', this.api, AccountId);

            var q = $q.defer();
            this.delete(url).then(function (result) {
                q.resolve(result);
            }, function (error) {
                q.reject(error);
            })
            return q.promise;
        }

        return new flexgridService;
    };
})();