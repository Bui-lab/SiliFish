    gs.push(
        new Dygraph(
            document.getElementById("div" + __CHART_INDEX__),
            __CHART_DATA__ ,
            {
                labelsDiv: document.getElementById("legend" + __CHART_INDEX__),
                labelsSeparateLines: true,
                __SCATTER_PLOT__
                colors: [__CHART_COLORS__],
                title: __CHART_TITLE__,
                ylabel: __Y_LABEL__,
                xlabel: __X_LABEL__,
                valueRange: [__Y_MIN__,__Y_MAX__]
            }
        )
    );


