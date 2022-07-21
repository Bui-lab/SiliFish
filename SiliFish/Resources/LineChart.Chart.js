
// Create chart
// https://www.amcharts.com/docs/v5/charts/xy-chart/
var chart__CHART_INDEX__ = container.children.push(am5xy.XYChart.new(root, {
    panX: true,
    panY: true,
    wheelX: "panX",
    wheelY: "zoomX",
    paddingTop: 20,
    maxTooltipDistance: 0
}));

// Add cursor
// https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
var cursor__CHART_INDEX__ = chart__CHART_INDEX__.set("cursor", am5xy.XYCursor.new(root, {
    behavior: "none"
}));
cursor__CHART_INDEX__.lineY.set("visible", false);

// Create axes
// https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
var xAxis__CHART_INDEX__ = chart__CHART_INDEX__.xAxes.push(am5xy.ValueAxis.new(root, {
    min: __X_START__,
    max: __X_END__,
    renderer: am5xy.AxisRendererX.new(root, {}),
    tooltip: am5.Tooltip.new(root, {})
}));

var yAxis__CHART_INDEX__ = chart__CHART_INDEX__.yAxes.push(am5xy.ValueAxis.new(root, {
    min: __Y_START__,
    max: __Y_END__,
    renderer: am5xy.AxisRendererY.new(root, {})
}));

__SERIES__

// Add scrollbar
// https://www.amcharts.com/docs/v5/charts/xy-chart/scrollbars/
var scrollbarX__CHART_INDEX__ = am5.Scrollbar.new(root, {
    min: __X_START__,
    max: __X_END__,
    orientation: "horizontal"
});

chart__CHART_INDEX__.set("scrollbarX", scrollbarX__CHART_INDEX__);
chart__CHART_INDEX__.bottomAxesContainer.children.push(scrollbarX__CHART_INDEX__);

chart__CHART_INDEX__.children.unshift(am5.Label.new(root, {
    text: "__CHART_TITLE__",
    fontSize: 15,
    fontWeight: "500",
    textAlign: "center",
    x: am5.percent(50),
    centerX: am5.percent(50),
    paddingTop: -20,
    paddingBottom: 0
}));



