'use strict';



$(document).on({
    ajaxStart: function () { $("#Loading").show(); $("#userOptionsWrapper").hide(); },
    ajaxStop: function () { $("#Loading").hide(); }
});

$(function () {
    $("#Loading").hide();
});


var gettingStockValuesJson = function()
{
    
    var numberOfDays = document.getElementById("numberOfDays").value;
    var numberOfStocks = document.getElementById("numberOfStocks").value;
    

   var data = {"numberOfDays":numberOfDays, "numberOfStocks" : numberOfStocks};
    

    $.ajax({
        dataType: "json",
        url: "http://localhost:63486/api/stock",
        data: data,
        error: function(data) {
            console.log(data);
        },
        success: function (data) {
            
            var stocks = data;
           
            data.forEach(function (element, index, array) {
                
              
                
                var openArray = [];
                var closeArray = [];
                var highArray = [];
                var lowArray = [];

                element.Days.forEach(function(element, index, array){
                    openArray[index] = element.Open.toFixed();
                    closeArray[index] = element.Close.toFixed();
                    highArray[index] = element.High.toFixed();
                    lowArray[index] = element.Low.toFixed();
                });

                element.openArray = openArray;
                element.closeArray = closeArray;
                element.highArray = highArray;
                element.lowArray = lowArray;
             

               // Making the panel element
               $("#stocksContainer").append(MakePanel(MakeStock(element), index));
                
           

               $('#'+ element.Name + '-open').sparkline(element.openArray);
               $('#'+ element.Name + '-close').sparkline(element.closeArray);
               $('#'+ element.Name + '-high').sparkline(element.highArray);
               $('#'+ element.Name + '-low').sparkline(element.lowArray);

            });
        }
    });

  
}

var MakeStock = function(element)
{
    return '<div>' +
                '<h4>'+element.Name+'</h4>'+
                '<span>open</span>' + '<span   id="' + element.Name + '-open' + '">' + '</span>' +
                '<span>close</span>' + '<span  id="' + element.Name + '-close' + '">' + '</span>' +
                '<span>high</span>' + '<span  id="' + element.Name + '-high' + '">' + '</span>' +
                '<span>low</span>' + '<span  id="' + element.Name + '-low' + '">' + '</span>' +
            '</div>'



}

var MakePanel = function (panelContent,  index) {
    return '<div class="panel panel-default id="'+ index + '>' +
                '<div class="panel-body">' +
                     panelContent +
                '</div>' +
            '</div>';

  

}
