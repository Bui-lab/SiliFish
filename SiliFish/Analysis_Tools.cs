using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zebrafish
{
    class Analysis_Tools
    {


            // 
            //     This function calculates the angle of the musculoskeletal model based on the VRMuscle and VLMuscle data.
            //     It also produces an animated plot of the musculoskeletal model
            //     :param Time: list or 1-D numpy array
            //     :param nMuscle: int, number of segments in the musculoskeletal model
            //     :param nmax: int, lenght of muscles time series
            //     :param VRMuscle: list or 1-D numpy array
            //     :param VLMuscle: list or 1-D numpy array
            //     :param dt: float
            //     :param title: string
            //     :return: animated plot
            //     
            public static object angles_(
                double[] Time,
                int nMuscle,
                int nmax,
                double[] VRMuscle,
                double[] VLMuscle,
                double dt,
                string title = "")
            {
                // Allocating arrays for velocity and position
                var vel = np.zeros((nMuscle, nmax));
                var pos = np.zeros((nMuscle, nmax));
                // Setting constants and initial values for vel. and pos.
                var khi = 3.0;
                var w0 = 2.5;
                var vel0 = 0.0;
                var pos0 = 0.0;
                var Wd = w0;
                foreach (var k in Enumerable.Range(0, nMuscle - 0))
                {
                    vel[k, 0] = vel0;
                    pos[k, 0] = pos0;
                    pos[nMuscle - 1, 0] = 0.0;
                    foreach (var i in Enumerable.Range(1, nmax - 1))
                    {
                        vel[k, i] = -Math.Pow(w0, 2) * pos[k, i - 1] * dt + vel[k, i - 1] * (1 - 2 * dt * khi * w0) + 0.1 * VRMuscle[k, i - 1] * dt - 0.1 * VLMuscle[k, i - 1] * dt;
                        pos[k, i] = dt * vel[k, i - 1] + pos[k, i - 1];
                    }
                }
                //## DYNAMIC PLOTING
                var x = np.zeros((nMuscle, nmax));
                var y = np.zeros((nMuscle, nmax));
                foreach (var i in Enumerable.Range(0, nmax - 0))
                {
                    x[0, i] = 0;
                    y[0, i] = 0;
                    pos[0, i] = 0;
                    foreach (var k in Enumerable.Range(1, nMuscle - 1))
                    {
                        pos[k, i] = pos[k - 1, i] + pos[k, i];
                        x[k, i] = x[k - 1, i] + np.sin(pos[k, i]);
                        y[k, i] = y[k - 1, i] - np.cos(pos[k, i]);
                    }
                }
                var fig = plt.figure();
                var ax = fig.add_subplot(111, autoscale_on: false, xlim: (-10, 10), ylim: (-nMuscle - 5, 5));
                ax.grid();
                ax.set_title(title);
                var _tup_1 = ax.plot(new List<object>(), new List<object>(), "o-", lw: 5, label: "Muscle");
                var line = _tup_1.Item1;
                var time_template = "time = %.1fms";
                var time_text = ax.text(0.05, 0.9, "", transform: ax.transAxes);
                ax.legend();
                Func<object> init = () => {
                    line.set_data(new List<object>(), new List<object>());
                    time_text.set_text("");
                    return Tuple.Create(line, time_text);
                };
                //This function will animate the musculoskeletal model based on updates at every time point i
                Func<object, object> animate = i => {
                    var thisx = (from k in Enumerable.Range(0, nMuscle)
                                 select x[k, i]).ToList();
                    var thisy = (from k in Enumerable.Range(0, nMuscle)
                                 select y[k, i]).ToList();
                    line.set_data(thisx, thisy);
                    time_text.set_text(time_template % Time[i]);
                    return Tuple.Create(line, time_text);
                };
                var ani = animation.FuncAnimation(fig, animate, np.arange(1, Time.Count, 100), interval: 100, blit: false, init_func: init);
                //ani.save('CPG_ani_BnG.mpeg', writer="ffmpeg")
                //ani.save("./results_test/" + name + ".mp4")#, fps=30)#, extra_args=['-vcodec', 'libx264'])
                //from matplotlib.animation import FFMpegWriter. The two lines below may be useful if you are having trouble with the animation
                //writer = FFMpegWriter(fps=1000, metadata=dict(artist='Me'), bitrate=1800)
                //ani.save("Single coiling.mp4", writer=writer)
                plt.show();
                return ani;
            }
        
        /*

            // 
            //     This function calculates the heatmap of the body angles as calculated by VLMuscle and VRMuscle.
            //     :param VRMuscle: list or 1-D numpy array
            //     :param VLMuscle: list or 1-D numpy array
            //     :param dt: float, default=0.1
            //     :param vmin_: float, default=-0.5
            //     :param vmax_: float, default=0.5
            //     :param ymin: float, default=0
            //     :param ymax: float, default=1000
            //     :return: body angle matrix of dimension (nmax, nMuscle) and FFT of body angle matrix
            //     
            public static object Heatmap(
                object VLMuscle,
                object VRMuscle,
                object dt = 0.1,
                object vmin_ = -0.5,
                object vmax_ = 0.5,
                object ymin = 0,
                object ymax = 1000)
            {
                var nmax = VLMuscle[0, ":"].Count;
                var nMuscle = VLMuscle[":", 0].Count;
                // Allocating arrays for velocity and position
                var vel = np.zeros((nMuscle, nmax));
                var pos = np.zeros((nMuscle, nmax));
                // Setting constants and initial values for vel. and pos.
                var khi = 3.0;
                var w0 = 2.5;
                var vel0 = 0.0;
                var pos0 = 0.0;
                var Wd = w0;
                foreach (var k in Enumerable.Range(0, nMuscle - 0))
                {
                    vel[k, 0] = vel0;
                    pos[k, 0] = pos0;
                    pos[nMuscle - 1, 0] = 0.0;
                    foreach (var i in Enumerable.Range(1, nmax - 1))
                    {
                        vel[k, i] = -Math.Pow(w0, 2) * pos[k, i - 1] * dt + vel[k, i - 1] * (1 - 2 * dt * khi * w0) + 0.1 * VRMuscle[k, i - 1] * dt - 0.1 * VLMuscle[k, i - 1] * dt;
                        pos[k, i] = dt * vel[k, i - 1] + pos[k, i - 1];
                    }
                }
                var pos2 = pos.transpose();
                var FFT = zeros((nmax, nMuscle - 1));
                var FFT2 = zeros((Convert.ToInt32(nmax / 2), nMuscle - 1));
                figure();
                plt.pcolormesh(np.arange(nMuscle) / nMuscle, np.arange(nmax) * dt, pos2, cmap: plt.cm.bwr, vmin: vmin_, vmax: vmax_);
                plt.title("Local Body Angle", fontsize: 14);
                plt.ylabel("Time (ms)", fontsize: 14);
                //plt.ylim(0,1000)
                plt.ylim(ymin, ymax);
                plt.xlabel("Body position", fontsize: 14);
                plt.colorbar();
                plt.show();
                //#Fourier
                var Fs = 10000.0;
                // sampling rate
                var Ts = 1.0 / Fs;
                // sampling interval
                var t = np.arange(0, 1, Ts);
                var n = nmax;
                var k = np.arange(n);
                var T = n / Fs;
                var frq = k / T;
                frq = frq[Enumerable.Range(0, Convert.ToInt32(n / 2))];
                var freqs = np.fft.fftfreq(Convert.ToInt32(nMuscle / 2), Ts);
                var idx = np.argsort(freqs);
                foreach (var k in Enumerable.Range(0, nMuscle - 1 - 0))
                {
                    FFT[":", k] = np.fft.fft(pos2[":", k]) / n;
                    FFT2[":", k] = FFT[Enumerable.Range(0, Convert.ToInt32(n / 2)), k];
                    FFT2[":", k] = sqrt(FFT2[":", k] * FFT2[":", k].conjugate());
                }
                figure();
                plt.pcolormesh(np.arange(nMuscle) / nMuscle, frq, FFT2, cmap: plt.cm.bwr);
                plt.title("FFT Magnitude");
                plt.ylabel("Frequency (Hz)");
                plt.ylim(0, 100);
                plt.xlabel("Body position");
                plt.colorbar();
                plt.show();
                return (pos2, FFT2);
            }

            // 
            //     This function smoothes y time serie by convolving using a box.
            //     :param y: list or 1-D numpy array
            //     :param box_pts: int, lenght of the box for convolution
            //     :return: 1-D numpy array
            //     
            public static object smooth(object y, object box_pts)
            {
                var box = np.ones(box_pts) / box_pts;
                var y_smooth = np.convolve(y, box, mode: "same");
                return y_smooth;
            }

            // 
            //     This function calculates the start, end and duration of swimming episode, as defined by a threshold.
            //      :param VLMuscle: list or 1-D numpy array
            //      :param VRMuscle: list or 1-D numpy array
            //      :param threshold: float
            //      :return: three 1-D numpy array with starting times, ending times and durations of the detected events.
            //      
            public static object detect_event(object VLMuscle, object VRMuscle, object threshold)
            {
                object duration;
                object start;
                object end;
                var X = np.sum(VLMuscle, axis: 0) + np.sum(VRMuscle, axis: 0);
                X = smooth(X, 500);
                var Xt = Time[np.where(X > threshold)];
                plt.plot(Time, X);
                plt.axhline(y: threshold, ls: "--", c: "r");
                plt.xlabel("Time (ms)");
                plt.ylabel("Integrated motor output (arbitrary units)");
                plt.rcParams.update(new Dictionary<object, object> {
                {
                    "font.size",
                    22}});
                if (!any(Xt))
                {
                    end = new List<object>();
                    start = new List<object>();
                    duration = new List<object>();
                }
                else
                {
                    end = Xt[(from i in Enumerable.Range(0, Xt.Count - 1)
                              select (Xt[i + 1] - Xt[i] > 0.2)).ToList() + new List<object> {
                    true
                }];
                    start = Xt[new List<object> {
                    true
                } + (from i in Enumerable.Range(0, Xt.Count - 1)
                     select (Xt[i + 1] - Xt[i] > 0.2)).ToList()];
                    duration = end - start;
                }
                return Tuple.Create(start, end, duration);
            }

            // 
            //    This function calculates the start, end and duration of swimming episode, as defined by a threshold.
            //     :param VLMuscle: list or 1-D numpy array
            //     :param VRMuscle: list or 1-D numpy array
            //     :param Time: list or 1-D numpy array
            //     :param threshold: float
            //     :return: three 1-D numpy array with starting times, ending times and durations of the detected events.
            //     
            public static object detect_event(object VLMuscle, object VRMuscle, object Time, object Threshold)
            {
                object duration;
                object start;
                object end;
                var X = np.sum(VLMuscle, axis: 0) + np.sum(VRMuscle, axis: 0);
                X = smooth(X, 500);
                var Xt = Time[np.where(X > Threshold)];
                plt.plot(Time, X);
                plt.axhline(y: Threshold, ls: "--", c: "r");
                if (!any(Xt))
                {
                    end = new List<object>();
                    start = new List<object>();
                    duration = new List<object>();
                }
                else
                {
                    end = Xt[(from i in Enumerable.Range(0, Xt.Count - 1)
                              select (Xt[i + 1] - Xt[i] > 0.2)).ToList() + new List<object> {
                    true
                }];
                    start = Xt[new List<object> {
                    true
                } + (from i in Enumerable.Range(0, Xt.Count - 1)
                     select (Xt[i + 1] - Xt[i] > 0.2)).ToList()];
                    duration = end - start;
                }
                return Tuple.Create(start, end, duration);
            }

            // 
            //     This function calculates the start, end and duration of swimming episode, as defined by a threshold. 
            //     Does not plot the result
            //     :param VLMuscle: list or 1-D numpy array
            //     :param VRMuscle: list or 1-D numpy array
            //     :param Time: list or 1-D numpy array
            //     :param threshold: float
            //     :return: three 1-D numpy array with starting times, ending times and durations of the detected events.
            //     
            public static object detect_event_no_plot(object VLMuscle, object VRMuscle, object Time, object Threshold)
            {
                object duration;
                object start;
                object end;
                var X = np.sum(VLMuscle, axis: 0) + np.sum(VRMuscle, axis: 0);
                X = smooth(X, 500);
                var Xt = Time[np.where(X > Threshold)];
                if (!any(Xt))
                {
                    end = new List<object>();
                    start = new List<object>();
                    duration = new List<object>();
                }
                else
                {
                    end = Xt[(from i in Enumerable.Range(0, Xt.Count - 1)
                              select (Xt[i + 1] - Xt[i] > 0.2)).ToList() + new List<object> {
                    true
                }];
                    start = Xt[new List<object> {
                    true
                } + (from i in Enumerable.Range(0, Xt.Count - 1)
                     select (Xt[i + 1] - Xt[i] > 0.2)).ToList()];
                    duration = end - start;
                }
                return Tuple.Create(start, end, duration);
            }

            // 
            //     This function takes the VLMN and VRMN output and calculates the muscle output based upon R, C and weight of MN to Muscle connection 
            //     :param VLMN: list or 1-D numpy array
            //     :param VRMN: list or 1-D numpy array
            //     :param Time: list or 1-D numpy array
            //     :param dt: float, time step
            //     :param nMN: int, number of motor neuron for the hemicord.
            //     :param nMuscle: int, number of muscle segments.
            //     :param R: float, resistance of muscle cells
            //     :param C: float, capacitance of muscle cells
            //     :param weight_MN_Muscle: int, number of muscle segments.
            //     :return: Two numpy arrays of dimension (nMuscle, len(Time)) for Left and Right muscle cells
            //     
            public static object recalc_muscle_ouptut(
                object VLMN,
                object VRMN,
                object Time,
                object dt,
                object nMN,
                object nMuscle,
                object R,
                object C,
                object weight_MN_Muscle)
            {
                var nmax = Time.Count;
                var L_MN = (from i in Enumerable.Range(0, nMN)
                            select Izhikevich_9P(a: 0.5, b: 0.01, c: -55, d: 100, vmax: 10, vr: -65, vt: -58, k: 0.5, Cm: 20, dt: dt, x: 5.0 + 1.6 * i, y: -1)).ToList();
                var R_MN = (from i in Enumerable.Range(0, nMN)
                            select Izhikevich_9P(a: 0.5, b: 0.01, c: -55, d: 100, vmax: 10, vr: -65, vt: -58, k: 0.5, Cm: 20, dt: dt, x: 5.0 + 1.6 * i, y: 1)).ToList();
                var L_Muscle = (from i in Enumerable.Range(0, nMuscle)
                                select Leaky_Integrator(R, C, dt, 5.0 + 1.6 * i, -1)).ToList();
                var R_Muscle = (from i in Enumerable.Range(0, nMuscle)
                                select Leaky_Integrator(R, C, dt, 5.0 + 1.6 * i, -1)).ToList();
                //# Declare Synapses
                var L_glusyn_MN_Muscle = (from i in Enumerable.Range(0, nMN * nMuscle)
                                          select TwoExp_syn(0.5, 1.0, -15, dt, 120)).ToList();
                var R_glusyn_MN_Muscle = (from i in Enumerable.Range(0, nMN * nMuscle)
                                          select TwoExp_syn(0.5, 1.0, -15, dt, 120)).ToList();
                var VLMuscle = zeros((nMuscle, nmax));
                var VRMuscle = zeros((nMuscle, nmax));
                //Ach
                var LSyn_MN_Muscle = zeros((nMN * nMuscle, 3));
                var RSyn_MN_Muscle = zeros((nMN * nMuscle, 3));
                //Ach
                var LW_MN_Muscle = zeros((nMN, nMuscle));
                var RW_MN_Muscle = zeros((nMN, nMuscle));
                // residuals
                var resLMuscle = zeros((nMuscle, 2));
                var resRMuscle = zeros((nMuscle, 2));
                // Calculating MN_muscle weights
                var MN_Muscle_syn_weight = weight_MN_Muscle;
                foreach (var k in Enumerable.Range(0, nMN - 0))
                {
                    foreach (var l in Enumerable.Range(0, nMuscle - 0))
                    {
                        if (L_Muscle[l].x - 1 < L_MN[k].x < L_Muscle[l].x + 1)
                        {
                            //this connection is segmental
                            LW_MN_Muscle[k, l] = MN_Muscle_syn_weight;
                        }
                        else
                        {
                            LW_MN_Muscle[k, l] = 0.0;
                        }
                    }
                }
                foreach (var k in Enumerable.Range(0, nMN - 0))
                {
                    foreach (var l in Enumerable.Range(0, nMuscle - 0))
                    {
                        if (R_Muscle[l].x - 1 < R_MN[k].x < R_Muscle[l].x + 1)
                        {
                            //it is segmental
                            RW_MN_Muscle[k, l] = MN_Muscle_syn_weight;
                        }
                        else
                        {
                            RW_MN_Muscle[k, l] = 0.0;
                        }
                    }
                }
                foreach (var k in Enumerable.Range(0, nMuscle - 0))
                {
                    resLMuscle[k, ":"] = L_Muscle[k].getNextVal(0, 0);
                    VLMuscle[k, 0] = resLMuscle[k, 0];
                    resRMuscle[k, ":"] = R_Muscle[k].getNextVal(0, 0);
                    VRMuscle[k, 0] = resRMuscle[k, 0];
                }
                foreach (var t in Enumerable.Range(0, nmax - 0))
                {
                    Time[t] = dt * t;
                    foreach (var k in Enumerable.Range(0, nMN - 0))
                    {
                        foreach (var l in Enumerable.Range(0, nMuscle - 0))
                        {
                            LSyn_MN_Muscle[nMuscle * k + l, ":"] = L_glusyn_MN_Muscle[nMuscle * k + l].getNextVal(VLMN[k, t - 10], VLMuscle[l, t - 1], LSyn_MN_Muscle[nMuscle * k + l, 1], LSyn_MN_Muscle[nMuscle * k + l, 2]);
                            RSyn_MN_Muscle[nMuscle * k + l, ":"] = R_glusyn_MN_Muscle[nMuscle * k + l].getNextVal(VRMN[k, t - 10], VRMuscle[l, t - 1], RSyn_MN_Muscle[nMuscle * k + l, 1], RSyn_MN_Muscle[nMuscle * k + l, 2]);
                        }
                    }
                    //# Calculate membrane potentials
                    foreach (var k in Enumerable.Range(0, nMuscle - 0))
                    {
                        var IsynL = (from l in Enumerable.Range(0, nMN - 0)
                                     select (LSyn_MN_Muscle[nMuscle * l + k, 0] * LW_MN_Muscle[l, k])).Sum();
                        var IsynR = (from l in Enumerable.Range(0, nMN - 0)
                                     select (RSyn_MN_Muscle[nMuscle * l + k, 0] * RW_MN_Muscle[l, k])).Sum();
                        resLMuscle[k, ":"] = L_Muscle[k].getNextVal(resLMuscle[k, 0], IsynL);
                        VLMuscle[k, t] = resLMuscle[k, 0];
                        resRMuscle[k, ":"] = R_Muscle[k].getNextVal(resRMuscle[k, 0], IsynR);
                        VRMuscle[k, t] = resRMuscle[k, 0];
                    }
                }
                return Tuple.Create(VLMuscle, VRMuscle);
            }

            // 
            //     This function tells if each element of a list are above or below some bounds
            //     :param lower_bound: int, bound used to discriminate swimming tail beats from noise
            //     :param upper_bound: int, bound used to discriminate swimming tail beats from noise
            //     :param alist: list or 1-D numpy array
            //     :return: boolean
            //     
            public static object check_all_in(object the_lower_bound, object the_upper_bound, object alist)
            {
                var in_list = true;
                foreach (var i in Enumerable.Range(0, alist.Count - 0))
                {
                    if (alist[i] < the_lower_bound || alist[i] > the_upper_bound)
                    {
                        in_list = false;
                    }
                }
                return in_list;
            }

            // 
            //     This function calculates tail beat frequency based upon crossings of y = 0 as calculated from the body angles calculated
            //     by VRMuscle and VLMuscle
            //     :param VRMuscle: list or 1-D numpy array
            //     :param VLMuscle: list or 1-D numpy array
            //     :param nmax: int, length of Time array
            //     :param dt: float, time step
            //     :param lower_bound: int, bound used to discriminate swimming tail beats from noise
            //     :param upper_bound: int, bound used to discriminate swimming tail beats from noise
            //     :param delay: float, defines the time window during wich we consider tail beats
            //     :return: Four 1-D numpy arrays for number of tail beats, interbeat time intervals, start times and beat times
            //     
            public static object calc_tail_beat_freq(
                object VRMuscle,
                object VLMuscle,
                object nmax,
                object dt,
                object lower_bound,
                object upper_bound,
                object delay)
            {
                object First;
                //## Calculate angles
                var vmin_ = -0.1;
                var vmax_ = 0.1;
                var ymin = 0;
                var ymax = 5000;
                var nMuscle = VRMuscle.Count;
                // Allocating arrays for velocity and position
                var vel = np.zeros((nMuscle, nmax));
                var pos = np.zeros((nMuscle, nmax));
                // Setting constants and initial values for vel. and pos.
                var khi = 3.0;
                var w0 = 2.5;
                var vel0 = 0.0;
                var pos0 = 0.0;
                var Wd = w0;
                foreach (var k in Enumerable.Range(0, nMuscle - 0))
                {
                    vel[k, 0] = vel0;
                    pos[k, 0] = pos0;
                    pos[nMuscle - 1, 0] = 0.0;
                    foreach (var i in Enumerable.Range(1, nmax - 1))
                    {
                        vel[k, i] = -Math.Pow(w0, 2) * pos[k, i - 1] * dt + vel[k, i - 1] * (1 - 2 * dt * khi * w0) + 0.1 * VRMuscle[k, i - 1] * dt - 0.1 * VLMuscle[k, i - 1] * dt;
                        pos[k, i] = dt * vel[k, i - 1] + pos[k, i - 1];
                    }
                }
                var pos2 = pos.transpose();
                var angle = pos2;
                //## Measure x and y coordinates based on angles at each segment
                var x = np.zeros((nMuscle, nmax));
                var y = np.zeros((nMuscle, nmax));
                foreach (var i in Enumerable.Range(0, nmax - 0))
                {
                    x[0, i] = 0;
                    y[0, i] = 0;
                    pos[0, i] = 0;
                    foreach (var k in Enumerable.Range(1, nMuscle - 1))
                    {
                        pos[k, i] = pos[k - 1, i] + pos[k, i];
                        x[k, i] = x[k - 1, i] + np.sin(pos[k, i]);
                        y[k, i] = y[k - 1, i] - np.cos(pos[k, i]);
                    }
                }
                // We will only use the tip of the tail to determine tail beats (if the x coordinate of the tip is smaller (or more negative)
                // than the lower bound or if the x coordinate of the tip is greater than the upper bound, then detect as a tail beat
                var tail_tip_x = x[nMuscle - 1, ":"];
                var Between_episodes = 1;
                var num_tail_beats = new List<object>();
                var interbeat_interval = new List<object>();
                var start = new List<object>();
                var beat_times = new List<object>();
                var side = 0;
                var LEFT = -1;
                var RIGHT = 1;
                var cross = 0;
                foreach (var i in Enumerable.Range(0, tail_tip_x.Count - 10 - 0))
                {
                    if (check_all_in(lower_bound, upper_bound, tail_tip_x[i::(i + delay)]))
                    {
                        Between_episodes = 1;
                        if (side == LEFT || side == RIGHT)
                        {
                            // if we are coming out of an episode
                            num_tail_beats.append(cross);
                        }
                        side = 0;
                        cross = 0;
                    }
                    if (Between_episodes == 1 && tail_tip_x[i] < lower_bound)
                    {
                        // beginning an episode on the left
                        cross += 1;
                        side = LEFT;
                        Between_episodes = 0;
                        start.append(i * 0.1);
                        beat_times.append(i * 0.1);
                        interbeat_interval.append(math.nan);
                        First = true;
                    }
                    else if (Between_episodes == 1 && tail_tip_x[i] > upper_bound)
                    {
                        // beginning an episode on the right
                        cross += 1;
                        side = RIGHT;
                        Between_episodes = 0;
                        start.append(i * 0.1);
                        beat_times.append(i * 0.1);
                        interbeat_interval.append(math.nan);
                        First = true;
                    }
                    // During an episode
                    if (tail_tip_x[i] < lower_bound && side == RIGHT)
                    {
                        cross += 1;
                        side = LEFT;
                        interbeat_interval.append(i * 0.1 - beat_times[-1]);
                        beat_times.append(i * 0.1);
                    }
                    else if (tail_tip_x[i] > upper_bound && side == LEFT)
                    {
                        cross += 1;
                        side = RIGHT;
                        interbeat_interval.append(i * 0.1 - beat_times[-1]);
                        beat_times.append(i * 0.1);
                    }
                }
                return Tuple.Create(num_tail_beats, interbeat_interval, start, beat_times);
            }
        */
}
}
