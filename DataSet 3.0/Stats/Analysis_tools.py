# pylint: disable=E1101, C0116, C0103
#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Created on July 24 2020

Updated from code written by Yann Roussel and Tuan Bui (2024)
"""

import numpy as np
from scipy import signal


def autocorr(x):
    """
    Function to calculate auto-correlation of time series x between time 0 and T_max
    where T_max is the ending time of x.
    :param x: list or 1-D numpy array
    :return: 1-D numpy array
    """
    result = np.correlate(x, x, mode="full")
    result = result[int(result.size / 2) :]  # remove negative time delays
    max_result = np.max(result)
    if max_result == 0 | np.isnan(max_result):
        return result
    return result / max_result


def Xcorr(x, y):
    """
    Function to calculate auto-correlation of time series x and y between time -T_max and T_max
    where T_max is the ending time of x.
    :param x: list or 1-D numpy array
    :param y: list or 1-D numpy array
    :return: 1-D numpy array
    """
    result = np.correlate(x, y, mode="full")
    max_result = np.max(result)
    if max_result == 0 | np.isnan(max_result):
        return result
    return result / max_result


def compute_tmax(trace_1, trace_2, t_max_1, limit_t, delta_t, positive_lag=False):
    """
    Function to compute the cross correlation between trace 1 and trace 2,
    with a limit time delay of limit_t ms
    """
    Xc_res = Xcorr(trace_1, trace_2)
    N = int(len(Xc_res) / 2)
    if positive_lag:
        Xc_res = Xc_res[N : int(N + limit_t / delta_t)]
        time_delay = np.arange(0, limit_t, delta_t)
    else:
        Xc_res = Xc_res[int(N - limit_t / delta_t) : int(N + limit_t / delta_t)]
        time_delay = np.arange(-limit_t, limit_t, delta_t)

    idx = np.where(Xc_res == np.max(Xc_res))[0][0]
    Xc_max = round(np.max(Xc_res), 4)  # value of peak cross correlation
    t_max = round(time_delay[idx], 2)  # time at which the peak occurs

    # check whether the first peak correlation is not the max correlation
    peaks, _ = signal.find_peaks(Xc_res, distance=10, height=0.5)
    Xc_first = round(Xc_res[peaks[0]], 4)  # value of peak cross correlation
    t_first = round(time_delay[peaks[0]], 2)  # time at which the peak occurs

    # if the secondary lag is smaller and the correlation is within acceptable range
    if (t_first < t_max) & (Xc_first > Xc_max * 0.9):
        Xc_max = Xc_first
        t_max = t_first

    t_max_rad = round(2 * np.pi * t_max / t_max_1, 4)
    # phase delay in radians between trace 1 and trace 2

    return t_max_rad, t_max, Xc_max
