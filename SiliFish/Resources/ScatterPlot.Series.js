// Add series
// https://www.amcharts.com/docs/v5/charts/xy-chart/series/

let series__SERIES_INDEX__ = chart__CHART_INDEX__.series.push(am5xy.LineSeries.new(root, {
    name: "__SERIES_NAME__",
    xAxis: xAxis__CHART_INDEX__,
    yAxis: yAxis__CHART_INDEX__,
    valueYField: "y",
    valueXField: "x",
    calculateAggregates: true,
    stroke: am5.color("__SERIES_COLOR__"),
    fill: am5.color("__SERIES_COLOR__"),
    fillOpacity: __SERIES_OPACITY__,
    tooltip: am5.Tooltip.new(root, {})
}));

series__SERIES_INDEX__.get("tooltip").label.set("text", "[bold]{name}[/]: {valueY}")
series__SERIES_INDEX__.data.setAll(__SERIES_DATA__);

series__SERIES_INDEX__.strokes.template.set("strokeOpacity", 0); //to prevent the line connecting the points

// Add bullet
// https://www.amcharts.com/docs/v5/charts/xy-chart/series/#Bullets
series__SERIES_INDEX__.bullets.push(function () {
    var graphics = am5.Circle.new(root, {
        fill: series0.get("fill"),
        radius: 5
    });
    return am5.Bullet.new(root, {
        sprite: graphics
    });
});




