# pylint: disable=C0301, C0103, C0114, C0115, C0116, E1101, W0702

from math import isnan
from matplotlib.figure import Figure
import matplotlib.pyplot as plt
import numpy as np
from pylab import mean
from Const import (
    SWIM_MODE,
    SWIM_MODE_COLOR,
    CELL_POOL_COLOR,
    CELL_GROUP_COLOR,
    TIME_MIN_SIF,
    TIME_MAX_SIF,
    IPSILATERAL_INS,
    CONTRALATERAL_INS,
)

from Analysis_tools import autocorr, compute_tmax


class PhaseDelayGenerator:

    def __init__(self) -> None:
        self.fig = Figure()
        self.ax = None
        self.phase_delay_ipsi_coll = {}
        self.phase_delay_contra_coll = {}
        self.side = "Both"
        self.swim_mode = ""
        self.lim_min = 50
        self.lim_max = 200
        self.dt = 0.1

        self.fig_size = (10, 8)
        self.marker_size = 15
        self.label_size = 20

        self.source_pools = []
        self.target_pools = []

        self.cell_pool_list = list(CELL_POOL_COLOR.keys())
        self.cell_group_list = list(CELL_GROUP_COLOR.keys())

    def __set_plot_axes__(self, ax_plot):
        ax_plot.set_rmin(-1)
        ax_plot.set_rmax(1)
        ax_plot.set_rticks([0.25, 0.5, 0.75, 1])  # radial ticks
        ax_plot.set_rlabel_position(-30)  # get radial labels away from plotted line
        ax_plot.yaxis.set_tick_params(labelcolor="gray", labelsize = 6)

        angles = [0, np.pi/4, np.pi/2, 3*np.pi/4, np.pi, 5*np.pi/4, 3 * np.pi / 2, 7*np.pi/4]  # Radian values
        labels = ['0', '', r'$\pi/2$', '', r'$\pi$', '', r'$-\pi/2$', '']  # LaTeX formatting for labels
        ax_plot.set_xticks(angles)
        ax_plot.set_xticklabels(labels)

        ax_plot.grid(True, color = 'lightgray')
        ax_plot.set_ylim(0, 1)

    def __generate_plots__(self, num_cols, num_plots, two_modes):
        num_rows = int(np.ceil(num_plots / num_cols))
        multiplier = 2 if two_modes else 1
        self.fig, self.ax = plt.subplots(num_rows * multiplier, num_cols,
            subplot_kw = {"projection": "polar"},
            figsize = self.fig_size)
        if num_rows * multiplier == 1:
            self.ax = np.atleast_2d(self.ax)
        elif num_cols == 1:
            self.ax = np.atleast_2d(self.ax).T

        if two_modes:
            self.fig.text(0.02, 0.75, "Ipsilateral", va="center", rotation="vertical",
                            fontsize = self.label_size)
            # Add the second label covering the bottom 2 rows
            self.fig.text(0.02, 0.25, "Contralateral", va="center", rotation="vertical",
                fontsize = self.label_size)

        plot_counter = 0
        for row_index in range(num_rows):
            for col_index in range(num_cols):
                ax_plot = self.ax[row_index, col_index]
                plot_counter += 1
                if plot_counter > num_plots:
                    ax_plot.cla()  # Clear the subplot
                    ax_plot.set_axis_off()
                else:
                    self.__set_plot_axes__(ax_plot)
                if multiplier > 1:
                    ax_plot = self.ax[num_rows + row_index, col_index]
                    if plot_counter > num_plots:
                        ax_plot.cla()  # Clear the subplot
                        ax_plot.set_axis_off()
                    else:
                        self.__set_plot_axes__(ax_plot)


    def plot_polar_phase_plots(self, file_name = "", num_cols=5,
                               auto_corr=False, overplot=False, hold=False,
                               different_somites=False, legend=False):
        row_index = 0
        col_index = 0
        num_rows = int(np.ceil(len(self.source_pools) / num_cols))
        ipsi_row = 0
        contra_row = num_rows
        if self.side.startswith("Ipsi"):
            contra_row = None
        elif self.side.startswith("Contra"):
            ipsi_row = None
            contra_row = 0

        if (not overplot) | (self.ax is None):
            self.__generate_plots__(num_cols, len(self.source_pools), self.side == "Both")
            if legend:
                handles = [plt.Line2D([0], [0], marker='o', color='w', 
                                      markerfacecolor=CELL_POOL_COLOR.get(pool, CELL_GROUP_COLOR.get(pool, 'k')), 
                                      markersize=self.marker_size) for pool in self.target_pools]
                labels = self.target_pools
                self.fig.legend(handles, labels, loc="center right", bbox_to_anchor=(0.1, 0.5), fontsize=15)

            if not overplot:
                caption = SWIM_MODE[self.swim_mode]
                if self.side.startswith("Ipsi"):
                    caption = caption + " Ipsilateral"
                elif self.side.startswith("Contra"):
                    caption = caption + " Contralateral"
                self.fig.suptitle(caption, fontsize=30)

        # cycle through cell types and plot phase delay between ipsilateral and contralateral cells
        for lbl_1 in self.source_pools:
            phase_delay_ipsi = self.phase_delay_ipsi_coll.get(lbl_1)
            phase_delay_contra = self.phase_delay_contra_coll.get(lbl_1)
            if phase_delay_ipsi is None and phase_delay_contra is None:
                continue
            source_color = CELL_POOL_COLOR.get(lbl_1)
            if source_color is None:
                source_color = CELL_GROUP_COLOR.get(lbl_1)
            cell_pools2 = self.target_pools
            if auto_corr:
                if lbl_1 not in self.target_pools:
                    continue
                else:
                    cell_pools2 = [lbl_1]
            for lbl_2 in cell_pools2:
                if auto_corr & overplot:  # use the swim mode color
                    target_color = SWIM_MODE_COLOR.get(self.swim_mode)
                else:
                    target_color = CELL_POOL_COLOR.get(lbl_2)
                    if target_color is None:
                        target_color = CELL_GROUP_COLOR.get(lbl_2)
                res_ipsi = (None if phase_delay_ipsi is None
                            else phase_delay_ipsi.get(lbl_2))
                res_contra = (None if phase_delay_contra is None
                              else phase_delay_contra.get(lbl_2))
                if (different_somites or not lbl_1 == lbl_2) and not res_ipsi is None:
                    self.ax[row_index + ipsi_row, col_index].plot(res_ipsi[0], res_ipsi[2],
                                                                  "o", c = target_color,  
                                                                  markersize= self.marker_size, clip_on=False)
                if not res_contra is None:
                    self.ax[row_index + contra_row, col_index].plot(res_contra[0], res_contra[2],
                                                                    "o", c = target_color, 
                                                                    markersize= self.marker_size, clip_on=False)

            if not ipsi_row is None:
                self.ax[row_index + ipsi_row, col_index].set_title(
                    lbl_1, color=source_color, fontsize=self.label_size)
                self.ax[row_index + ipsi_row, col_index].tick_params(
                    axis="x", labelsize=self.label_size)
            if not contra_row is None:
                self.ax[row_index + contra_row, col_index].set_title(
                    lbl_1, color=source_color, fontsize=self.label_size)
                self.ax[row_index + contra_row, col_index].tick_params(
                   axis="x", labelsize=self.label_size)

            col_index = col_index + 1
            if col_index == num_cols:
                row_index = row_index + 1
                col_index = 0

        if not hold:
            plt.tight_layout()
            if file_name > " ":
                plt.savefig(file_name, dpi=1000)
            plt.show()

    def generate_polar_phase_data(self, data, source_pools, target_pools,
        somite_idx, somite_idx2, speed_mode, side="Both", time_range=(0, 0), auto_corr=False):
        # compute the phase delay of interests in radians

        self.phase_delay_ipsi_coll = {}
        self.phase_delay_contra_coll = {}
        self.side = side
        self.source_pools = source_pools
        self.target_pools = target_pools
        self.swim_mode = speed_mode

        time_min_idx = int(time_range[0] / self.dt)
        time_max_idx = len(data) if time_range[1] == 0 else int(time_range[1] / self.dt)

        for lbl_1 in source_pools:
            # get individual traces
            data1 = data[f"L_{lbl_1}_{somite_idx}_1"].iloc[time_min_idx:time_max_idx]
            if data1.empty:
                continue
            # baseline adjust the trace
            data1 = data1 - mean(data1)
            # calculate autocorrelation of trace 1 to set the cycle period
            X1_res = autocorr(data1)
            X1_res = X1_res[int(self.lim_min / self.dt) : int(self.lim_max / self.dt)]
            if isnan(np.max(X1_res)):
                continue
            time_delay_1 = np.arange(self.lim_min, self.lim_max, self.dt)
            idx_1 = np.where(X1_res == np.max(X1_res))[0][0]
            tmax_1 = round(time_delay_1[idx_1], 2)

            phase_delay_ipsi = {}
            phase_delay_contra = {}

            for lbl_2 in target_pools:
                if auto_corr & (lbl_1 != lbl_2):
                    continue
                if (side == "Both") | side.startswith("Ipsi"):
                    # skip if ploting the Ipsi part of Both - and the IN does not belong to the group with ipsilateral projections
                    if (side != "Both") or (lbl_2 in IPSILATERAL_INS):
                        # get ipsilateral traces
                        data2 = data[f"L_{lbl_2}_{somite_idx2}_1"].iloc[time_min_idx:time_max_idx]
                        # baseline adjust the trace
                        data2 = data2 - mean(data2)
                        res_ipsi = []
                        ipsi_done = True
                        try:
                            # compute phase delays
                            res_ipsi = compute_tmax(
                                data2.values, data1.values, tmax_1, self.lim_max, self.dt
                            )
                        except:
                            ipsi_done = False
                        if ipsi_done:
                            phase_delay_ipsi[lbl_2] = np.asarray(res_ipsi)

                if (side == "Both") | side.startswith("Contra"):
                    # skip if ploting the Contra part of Both - and the IN does not belong to the group with contralateral projections
                    if (side != "Both") or (lbl_2 in CONTRALATERAL_INS):
                        # get contralateral traces
                        data3 = data[f"R_{lbl_2}_{somite_idx2}_1"].iloc[
                            time_min_idx:time_max_idx
                        ]
                        # baseline adjust the trace
                        data3 = data3 - mean(data3)
                        res_contra = []
                        contra_done = True
                        try:
                            res_contra = compute_tmax(
                                data3.values, data1.values, tmax_1, self.lim_max, self.dt, 
                            )
                        except:
                            contra_done = False
                        if contra_done:
                            phase_delay_contra[lbl_2] = np.asarray(res_contra)
            if len(phase_delay_ipsi) > 0:
                self.phase_delay_ipsi_coll[lbl_1] = phase_delay_ipsi
            if len(phase_delay_contra) > 0:
                self.phase_delay_contra_coll[lbl_1] = phase_delay_contra

    def auto_cross_across_speeds(self, raw_data, somite, target_pools):
        # auto correlation

        for run_mode in range(1, 6, 2):
            time_range = (TIME_MIN_SIF[run_mode], TIME_MAX_SIF[run_mode])
            self.generate_polar_phase_data(raw_data, target_pools, target_pools,
                somite, somite, run_mode, side="Contra",
                time_range=time_range)
            self.plot_polar_phase_plots(
                "",
                num_cols=len(target_pools),
                auto_corr=True,
                overplot=True, hold=run_mode < 5)
