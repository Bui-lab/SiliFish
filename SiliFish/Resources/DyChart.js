var g__CHART_INDEX__ = new Dygraph(
    document.getElementById("div" + __CHART_INDEX__),
    __CHART_DATA__,
    {
        labelsDiv: document.getElementById("legend" + __CHART_INDEX__),
        labelsSeparateLines: true,
        __SCATTER_PLOT__
        colors: [__CHART_COLORS__],
        title: __CHART_TITLE__,
        ylabel: __Y_LABEL__,
        xlabel: __X_LABEL__,
        valueRange: [__Y_MIN__, __Y_MAX__],
        dateWindow: [__X_MIN__, __X_MAX__],
        drawPoints: __DRAW_POINTS__,
        yRangePad: 3
    }
);
g__CHART_INDEX__.updateOptions({ axes: { x: { logscale: __LOG_SCALE__ } } });

gs.push(g__CHART_INDEX__);


