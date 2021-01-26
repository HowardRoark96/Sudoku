angular.module('sudokuApp')
    .component('gameTable', {
        templateUrl: '../src/directives/game-table/game-table.html',
        controllerAs: 'ctrl',
        bindings: {
            selectedItem: '=?'
        },
        controller: function ($element) {
            const ctrl = this;

            ctrl.$onInit = () => {
                $element.on('click', cellSeleted);
                
            };

            function cellSeleted(item) {
               if (ctrl.selectedItem && ctrl.selectedItem !== 'clear' && $(item.target).hasClass('game-cell')) {
                $(item.target).find('.cell-value').text(ctrl.selectedItem);
               }
               else if (ctrl.selectedItem === 'clear') {
                $(item.target).find('.cell-value').text('');
               }
               if ($(item.target).hasClass('highlight-cell')) {
                   $(item.target).removeClass('highlight-cell');
                }
                else {
                    $(item.currentTarget).parent().find('.highlight-cell').each(function() {
                        $(this).removeClass('highlight-cell');
                    });
                    $(item.target).addClass('highlight-cell');
                }
            }
        }
    });