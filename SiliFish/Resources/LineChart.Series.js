// Add series
// https://www.amcharts.com/docs/v5/charts/xy-chart/series/

let series__SERIES_INDEX__ = chart__CHART_INDEX__.series.push(am5xy.LineSeries.new(root, {
    name: "__SERIES_NAME__",
    xAxis: xAxis__CHART_INDEX__,
    yAxis: yAxis__CHART_INDEX__,
    valueYField: "v",
    valueXField: "t",
    calculateAggregates: false,
    stroke: am5.color("__SERIES_COLOR__"),
    fill: am5.color("__SERIES_COLOR__"),
    fillOpacity: __SERIES_OPACITY__,
    tooltip: am5.Tooltip.new(root, {})
}));

series__SERIES_INDEX__.get("tooltip").label.set("text", "[bold]{name}[/]: {valueY}")
series__SERIES_INDEX__.data.setAll(__SERIES_DATA__);



