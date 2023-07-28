//import graph
// chartjs chart
var Exportlabels = ImportExportGraphList.map(item => item.month_Name);

var importMonthlyChemicals = ImportExportGraphList.map(item => item.importMonthlyChemicals);
var importMonthlyPetroChemical = ImportExportGraphList.map(item => item.importMonthlyPetroChemical);

Chart.defaults.global = {
    animation: true,
    animationSteps: 60,
    animationEasing: "easeOutIn",
    showScale: true,
    scaleOverride: false,
    scaleSteps: null,
    scaleStepWidth: null,
    scaleStartValue: null,
    scaleLineColor: "#eeeeee",
    scaleLineWidth: 1,
    scaleShowLabels: true,
    scaleLabel: "<%=value%>",
    scaleIntegersOnly: true,
    scaleBeginAtZero: false,
    scaleFontSize: 12,
    scaleFontStyle: "normal",
    scaleFontColor: "#717171",
    responsive: true,
    maintainAspectRatio: true,
    showTooltips: true,
    multiTooltipTemplate: "<%= value %>",
    tooltipFillColor: "#333333",
    tooltipEvents: ["mousemove", "touchstart", "touchmove"],
    tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value %>",
    tooltipFontSize: 14,
    tooltipFontStyle: "normal",
    tooltipFontColor: "#fff",
    tooltipTitleFontSize: 16,
    TitleFontStyle: "Raleway",
    tooltipTitleFontStyle: "bold",
    tooltipTitleFontColor: "#ffffff",
    tooltipYPadding: 10,
    tooltipXPadding: 10,
    tooltipCaretSize: 8,
    tooltipCornerRadius: 6,
    tooltipXOffset: 5,
    onAnimationProgress: function () { },
    onAnimationComplete: function () { }
};
var lineGraphData = {
    labels: Exportlabels,
    datasets: [{
        label: "My third dataset",
        fillColor: "transparent",
        strokeColor: "lightgreen",
        pointColor: "lightgreen",
        pointStrokeColor: "#fff",
        pointHighlightFill: "#000",
        pointHighlightStroke: "rgba(30, 166, 236, 1)",
        data: importMonthlyChemicals
    },
    {
        label: "My fouth dataset",
        fillColor: "transparent",
        strokeColor: "darkgreen",
        pointColor: "darkgreen",
        pointStrokeColor: "#fff",
        pointHighlightFill: "#000",
        pointHighlightStroke: "rgba(30, 166, 236, 1)",
        data: importMonthlyPetroChemical
    }]
};
var lineGraphOptions = {
    scaleShowGridLines: true,
    scaleGridLineColor: "rgba(0,0,0,.05)",
    scaleGridLineWidth: 1,
    scaleShowHorizontalLines: true,
    scaleShowVerticalLines: true,
    bezierCurve: true,
    bezierCurveTension: 0.4,
    pointDot: true,
    pointDotRadius: 4,
    pointDotStrokeWidth: 1,
    pointHitDetectionRadius: 20,
    datasetStroke: true,
    datasetStrokeWidth: 2,
    datasetFill: true,
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
};
var lineCtx = document.getElementById("myGraphImport").getContext("2d");
var myLineCharts = new Chart(lineCtx).Line(lineGraphData, lineGraphOptions);
// chartjs chart
var Exportlabels = ImportExportGraphList.map(item => item.month_Name);
var exportMonthlyChemicals = ImportExportGraphList.map(item => item.exportMonthlyChemicals);

var exportMonthlyPetroChemical = ImportExportGraphList.map(item => item.exportMonthlyPetroChemical);


Chart.defaults.global = {
    animation: true,
    animationSteps: 60,
    animationEasing: "easeOutIn",
    showScale: true,
    scaleOverride: false,
    scaleSteps: null,
    scaleStepWidth: null,
    scaleStartValue: null,
    scaleLineColor: "#eeeeee",
    scaleLineWidth: 1,
    scaleShowLabels: true,
    scaleLabel: "<%=value%>",
    scaleIntegersOnly: true,
    scaleBeginAtZero: false,
    scaleFontSize: 12,
    scaleFontStyle: "normal",
    scaleFontColor: "#717171",
    responsive: true,
    maintainAspectRatio: true,
    showTooltips: true,
    multiTooltipTemplate: "<%= value %>",
    tooltipFillColor: "#333333",
    tooltipEvents: ["mousemove", "touchstart", "touchmove"],
    tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value %>",
    tooltipFontSize: 14,
    tooltipFontStyle: "normal",
    tooltipFontColor: "#fff",
    tooltipTitleFontSize: 16,
    TitleFontStyle : "Raleway",
    tooltipTitleFontStyle: "bold",
    tooltipTitleFontColor: "#ffffff",
    tooltipYPadding: 10,
    tooltipXPadding: 10,
    tooltipCaretSize: 8,
    tooltipCornerRadius: 6,
    tooltipXOffset: 5,
    onAnimationProgress: function() {},
    onAnimationComplete: function() {}
};
var lineGraphData = {
    labels: Exportlabels,
    datasets: [{
        label: "My First dataset",
        fillColor: "transparent",
        strokeColor: "mediumvioletred",
        pointColor: "mediumvioletred",
        pointStrokeColor: "#fff",
        pointHighlightFill: "#fff",
        pointHighlightStroke: "#000",
        data: exportMonthlyChemicals
    }, {
        label: "My Second dataset",
        fillColor: "transparent",
        strokeColor: "darkred",
        pointColor: "darkred",
        pointStrokeColor: "#fff",
        pointHighlightFill: "#000",
        pointHighlightStroke: "rgba(30, 166, 236, 1)",
        data: exportMonthlyPetroChemical
    }]
};
var lineGraphOptions = {
    scaleShowGridLines: true,
    scaleGridLineColor: "rgba(0,0,0,.05)",
    scaleGridLineWidth: 1,
    scaleShowHorizontalLines: true,
    scaleShowVerticalLines: true,
    bezierCurve: true,
    bezierCurveTension: 0.4,
    pointDot: true,
    pointDotRadius: 4,
    pointDotStrokeWidth: 1,
    pointHitDetectionRadius: 20,
    datasetStroke: true,
    datasetStrokeWidth: 2,
    datasetFill: true,
    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].strokeColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>"
};
var lineCtx = document.getElementById("myGraphExport").getContext("2d");
var myLineCharts = new Chart(lineCtx).Line(lineGraphData, lineGraphOptions);


//chartist chart
new Chartist.Line('.ct-4', {
    labels: [1, 2, 3, 4, 5, 6, 7],
    series: [
        [3, 4, 3, 5, 4, 3, 5]
    ]
}, {
    low: 0,
    offset: 0,
    fullWidth: !0,
    showArea: !0,
    chartPadding: {
        right: 0,
        left: 0,
        bottom: 0
    },
    axisY: {
        low: 0,
        showGrid: false,
        showLabel: false,
        offset: 0
    },
    axisX: {
        showGrid: false,
        showLabel: false,
        offset: 0
    }
});
//
var labels = capacityGraphList.map(item => item.fyear_Name);
var series = [
    capacityGraphList.map(item => item.petroChemicalInstalledCapacity),
    capacityGraphList.map(item => item.petroChemicalProductionQuantity)
];
//market-chart
new Chartist.Bar('.market-chart', {
    labels: labels,
    series: series
},
    {
        seriesBarDistance: 2,
        chartPadding: {
            left: 0,
            right: 0,
            bottom: 0,
        },
        axisX: {
            showGrid: false,
            labelInterpolationFnc: function (value) {
                return value[0];
            }
        }
    }, [
    ['screen and (min-width: 300px)', {
        seriesBarDistance: 15,
        axisX: {
            labelInterpolationFnc: function (value) {
                return value.slice(0, 3);
            }
        }
    }],
    ['screen and (min-width: 600px)', {
        seriesBarDistance: 12,
        axisX: {
            labelInterpolationFnc: Chartist.noop
        }
    }]
]).on('draw', function (ctx) {
    if (ctx.type === 'bar') {
        ctx.element.attr({
            x1: ctx.x1 + 0.05,
            style: 'stroke-linecap: round'
        });
    }
});

//

var labels = capacityGraphList.map(item => item.fyear_Name);
var series = [
    capacityGraphList.map(item => item.chemicalInstalledCapacity),
    capacityGraphList.map(item => item.chemicalProductionQuantity)
];
//sales-purchase chart
new Chartist.Bar('.sales-chart', {
    labels: labels,
    series: series
    },
    {
        seriesBarDistance: 2,
        chartPadding: {
            left: 0,
            right: 0,
            bottom: 0,
        },
        axisX: {
            showGrid: false,
            labelInterpolationFnc: function(value) {
                return value[0];
            }
        }
    }, [
        ['screen and (min-width: 300px)', {
            seriesBarDistance: 15,
            axisX: {
                labelInterpolationFnc: function(value) {
                    return value.slice(0, 3);
                }
            }
        }],
        ['screen and (min-width: 600px)', {
            seriesBarDistance: 12,
            axisX: {
                labelInterpolationFnc: Chartist.noop
            }
        }]
    ]);

// sales & purchase return
var myLineChart = {
    labels: ["","10", "20", "30", "40", "50", "60", "70", "80"],
    datasets: [{
        fillColor: "transparent",
        strokeColor: "#00baf2",
        pointColor: "#00baf2",
        data: [20, 40, 20, 50, 20, 60, 10, 40, 20]
    }, {
        fillColor: "transparent",
        strokeColor: "#314da7",
        pointColor: "#314da7",
        data: [60, 10, 40, 30, 80, 30, 20, 90, 0]
    }]
}
var ctx = document.getElementById("myLineCharts").getContext("2d");
var LineChartDemo = new Chart(ctx).Line(myLineChart, {
    pointDotRadius: 2,
    pointDotStrokeWidth: 5,
    pointDotStrokeColor: "#ffffff",
    bezierCurve: false,
    scaleShowVerticalLines: false,
    scaleGridLineColor: "#eeeeee"
});

// expense
google.charts.load('current', {packages: ['corechart', 'bar']});
google.charts.load('current', {'packages':['line']});
google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawBasic);
function drawBasic() {
    if ($("#area-chart1").length > 0) {
        var data = google.visualization.arrayToDataTable([
            ['Year', 'Sales', 'Expenses'],
            ['2013',  1000,      400],
            ['2014',  1170,      460],
            ['2015',  660,       1120],
            ['2016',  1030,      540]
        ]);
        var options = {
            title: 'Company Performance',
            hAxis: {title: 'Year',  titleTextStyle: {color: '#333'}},
            vAxis: {minValue: 0},
            width:'100%',
            colors: ["#00baf2", "#314da7"]
        };
        var chart = new google.visualization.AreaChart(document.getElementById('area-chart1'));
        chart.draw(data, options);
    }
}
// expense
google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.load('current', { 'packages': ['line'] });
google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawBasic);
function drawBasic() {
    if ($("#area-chart1").length > 0) {
        var data = google.visualization.arrayToDataTable(salesexpensearr);

        var options = {
            title: 'Company Performance',
            hAxis: { title: 'Year', titleTextStyle: { color: '#333' } },
            vAxis: { minValue: 0 },
            width: '100%',
            colors: ["#00baf2", "#314da7"]
        };

        var chart = new google.visualization.AreaChart(document.getElementById('area-chart1'));
        chart.draw(data, options);
    }

    ////monthly chmical graph
    var labels = MonthChemGraphList.map(item => item.month_Name);
    var series = [
        MonthChemGraphList.map(item => item.monthlyChemicals),
        MonthChemGraphList.map(item => item.monthlyPetroChemical)
    ];
    //
   
        new Chartist.Bar('.monthchemgraph', {
            labels: labels,
            series: series
        },
            {
                seriesBarDistance: 2,
                chartPadding: {
                    left: 0,
                    right: 0,
                    bottom: 0,
                },
                axisX: {
                    showGrid: false,
                    labelInterpolationFnc: function (value) {
                        return value[0];
                    }
                }
            }, [
            ['screen and (min-width: 300px)', {
                seriesBarDistance: 15,
                axisX: {
                    labelInterpolationFnc: function (value) {
                        return value.slice(0, 3);
                    }
                }
            }],
            ['screen and (min-width: 600px)', {
                seriesBarDistance: 12,
                axisX: {
                    labelInterpolationFnc: Chartist.noop
                }
            }]
        ]);

}
