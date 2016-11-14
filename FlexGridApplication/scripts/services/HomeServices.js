//TODO: call Api service
(function () {
    'use strict';

    var app = angular.module('app');
    app.service('dataService', function () {

        this.price = function () {
            return Math.random() * 1000;
        }

        this.getCols = function () {
            var cols = [];

            cols.push(['id', 'Id']);
            cols.push(['country', 'Country']);
            cols.push(['sales', 'Sales']);
            cols.push(['expenses', 'Expenses']);
            cols.push(['price', 'Price']);
            cols.push(['downloads', 'Downloads']);

            return cols;
        }

        this.getDatas = function () {
            var countries = 'US,Germany,UK,Japan,Italy,Greece'.split(','),
            data = [];
            for (var i = 0; i < countries.length; i++) {
                data.push({
                    id: i,
                    country: countries[i],
                    downloads: Math.round(Math.random() * 20000),
                    sales: Math.random() * 10000,
                    expenses: Math.random() * 5000,
                    price: this.price(),
                });
            }

            return data;
        }

        this.updateRow = function (dataRow) {

        }
    });
})();