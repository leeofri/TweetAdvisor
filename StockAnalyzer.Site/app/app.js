'use strict';



$(document).on({
    ajaxStart: function () { $("#Loading").show(); $("#userOptionsWrapper").hide(); },
    ajaxStop: function () { $("#Loading").hide(); }
});

$(function () {
    $("#Loading").hide();
});


var gettingStockValuesJson = function () {

    var numberOfDays = document.getElementById("numberOfDays").value;
    var numberOfStocks = document.getElementById("numberOfStocks").value;
    var numberOfClusters = document.getElementById("numberOfClusters").value;
    var checkboxes = {};
    checkboxes.open = $("#openCheckBox")[0].checked;
    checkboxes.close = $("#closeCheckBox")[0].checked;
    checkboxes.high = $("#highCheckBox")[0].checked;
    checkboxes.low = $("#lowCheckBox")[0].checked;




    var UserOptions = {
        StocksNumber: numberOfStocks,
        DaysNumber: numberOfDays,
        ClusterNumber: numberOfClusters,
        Open: checkboxes.open,
        Close: checkboxes.close,
        High: checkboxes.high,
        Low: checkboxes.low,
    };


    $.getJSON({
        dataType: "json",
        url: "http://localhost:63486/api/stock",
        data: UserOptions,
        error: function (data) {
            console.log(data);
        },
        success: function (data) {

            data.forEach(function (cluster, index, array) {
               
                $("#stocksContainer").append(MakePanel("Cluster : " + index, index));

                cluster.Stocks.forEach(function (element, index, array) {


                    var openArray = [];
                    var closeArray = [];
                    var highArray = [];
                    var lowArray = [];

                    element.Days.forEach(function (element, index, array) {
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



                    $('#' + element.Name + '-open').sparkline(element.openArray);
                    $('#' + element.Name + '-close').sparkline(element.closeArray);
                    $('#' + element.Name + '-high').sparkline(element.highArray);
                    $('#' + element.Name + '-low').sparkline(element.lowArray);

                });
            });
        }
    });


}

var MakeStock = function (element) {
    return '<div>' +
                '<h4>' + element.Name + '</h4>' +
                '<span>open</span>' + '<span   id="' + element.Name + '-open' + '">' + '</span>' +
                '<span>close</span>' + '<span  id="' + element.Name + '-close' + '">' + '</span>' +
                '<span>high</span>' + '<span  id="' + element.Name + '-high' + '">' + '</span>' +
                '<span>low</span>' + '<span  id="' + element.Name + '-low' + '">' + '</span>' +
            '</div>'



}

var MakePanel = function (panelContent, index) {
    return '<div class="panel panel-default col-lg-12" id="' + index + '">' +
                '<div class="panel-body">' +
                     panelContent +
                '</div>' +
            '</div>';
}
