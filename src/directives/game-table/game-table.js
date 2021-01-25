const { element } = require("angular");

angular.module('sudokuApp')
    .component('gameTable', {
        templateUrl: '../src/directives/game-table/game-table.html',
        controllerAs: 'ctrl',
        controller: function ($element) {
            const ctrl = this;

            ctrl.$onInit = () => {
                $element.on('click', cellSeleted);
                
            };

            function cellSeleted(item) {
               angular.element(item.target).addClass('game-cell-selected');
               angular.element(item.target).unbind('mouseenter mouseleave');
            }
        }
    });