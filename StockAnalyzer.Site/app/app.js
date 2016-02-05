'use strict';


$(function () {

  
    /* Sparklines can also take their values from the first argument 
    passed to the sparkline() function */
    // Getting all the values
    gettingStockValuesJson();
   

});

var gettingStockValuesJson = function()
{
    alert();
    $.ajax({
        dataType: "json",
        url: "http://localhost:63486/api/stock",
        success: function (data) {
            var stocks = data;
           
            data.forEach(function (element, index, array) {
                
                $("#stocksContainer").append(MakePanel(element.Name));
            });

           
        }
    });
}

var MakePanel = function (panelContent) {
    return '<div class="panel panel-default">' +
                '<div class="panel-body">' +
                     panelContent +
                '</div>' +
            '</div>';
}
