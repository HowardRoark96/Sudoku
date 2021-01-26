angular.module('sudokuApp')
    .component('gameControlPanel', {
        templateUrl: '../src/directives/game-control-panel/game-control-panel.html',
        controllerAs: 'ctrl',
        bindings: {
            selectedItem: '=?'
        },
        controller: function ($element) {
            const ctrl = this;

            ctrl.itemSelected = itemSelected;
            ctrl.resetCell = resetCell;

            ctrl.$onInit = () => {               
            };

            function itemSelected(cell) {
                $(cell.currentTarget).parent().find('.game-control-cell-selected').each(function() {
                    $(this).removeClass('game-control-cell-selected');
                });

                if (ctrl.selectedItem === cell.currentTarget.outerText) {
                    $(cell.currentTarget).removeClass('game-control-cell-selected');
                    ctrl.selectedItem = '';
                }
                else {
                    $(cell.currentTarget).addClass('game-control-cell-selected');
                    ctrl.selectedItem = cell.currentTarget.outerText;
                }
            };

            function resetCell(cell) {
                $(cell.currentTarget).parent().find('.game-control-cell-selected').each(function() {
                    $(this).removeClass('game-control-cell-selected');
                });

                if (ctrl.selectedItem === 'clear') {
                    $(cell.currentTarget).removeClass('game-control-cell-selected');
                    ctrl.selectedItem = '';
                }
                else {
                    $(cell.currentTarget).addClass('game-control-cell-selected');
                    ctrl.selectedItem = 'clear';
                }               
            }
        }
    });