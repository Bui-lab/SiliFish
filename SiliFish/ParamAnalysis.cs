using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebrafish.Helpers;

namespace Zebrafish
{
    class ParamAnalysis
    {
        private void GetIzhikevichData(MembraneDynamics dyn, string filename, bool shortmode = false)
        {
            Izhikevich_9P core = new Izhikevich_9P(dyn, init_v: dyn.vr, init_u: 0);
            int tmax = shortmode? 1000 : 5000;
            double incr = 0.01;
            SwimmingModel.dt = incr;
            int count = (int)(tmax / incr);
            double[] I = new double[count];
            double[] T = Enumerable.Range(0, count).Select(index => index * incr).ToArray();
            double[] V;
            double[] u;
            for (int t = 0; t < count; t++)
            {
                T[t] = t * incr;
                if (shortmode)
                {
                    if (t > 0.1 * count && t < 0.5 * count)
                        I[t] = 70; 
                }
                else if (t > 0.1 * count && (t < 0.2 * count || t > 0.6 * count))
                    I[t] = 10; // rand.NextDouble();
            }
            (V, u) = core.SolveODE(I);
            Dictionary<string, double[]> dict = new Dictionary<string, double[]> {
                { "I", I}, {"V", V }, {"u", u}};
            Util.SaveToCSV(filename, T, dict);
        }

        private void GetLeakyIntegratorData(double R, double C, double init_v, string filename)
        {
            Leaky_Integrator mc = new Leaky_Integrator(R, C, init_v);
            int tmax = 5000;
            double incr = 0.01;
            SwimmingModel.dt = incr;
            int count = (int)(tmax / incr);
            double[] I = new double[count];
            double[] T = Enumerable.Range(0, count).Select(index => index * incr).ToArray();
            double[] V;
            double[] u;
            for (int t = 0; t < count; t++)
            {
                T[t] = t * incr;
                if (t > 0.1 * count && (t < 0.2 * count || t > 0.6 * count))
                    I[t] = 10; // rand.NextDouble();
            }
            V = mc.SolveODE(I);
            Dictionary<string, double[]> dict = new Dictionary<string, double[]> {
                { "I", I}, {"V", V }};
            Util.SaveToCSV(filename, T, dict);
        }
        public void TestSingleCoil()
        {
            MembraneDynamics ic_dyn = new() { a = 0.0005, b = 0.5, c = -30, d = 5, vmax = 0, vr = -60, vt = -45, k = 0.05, Cm = 50 };
            GetIzhikevichData(ic_dyn, "SingleCoilICStep.csv");

            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 0.2, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            GetIzhikevichData(mn_dyn, "SingleCoilMNStep.csv");

            MembraneDynamics v0d_dyn = new() { a = 0.5, b = 0.01, c = -50, d = 0.2, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            GetIzhikevichData(v0d_dyn, "SingleCoilV0dStep.csv");

            GetLeakyIntegratorData(R: 25, C: 10.0, init_v: 0, filename: "SingleCoilMuscleStep.csv");
        }

        public void TestDoubleCoil()
        {
            MembraneDynamics ic_dyn = new() { a = 0.0002, b = 0.5, c = -40, d = 5, vmax = 0, vr = -60, vt = -45, k = 0.3, Cm = 50 };
            GetIzhikevichData(ic_dyn, "DoubleCoilICStep.csv");

            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 100, vmax = 10, vr = -60, vt = -50, k = 0.05, Cm = 20 };
            GetIzhikevichData(mn_dyn, "DoubleCoilMNStep.csv");

            MembraneDynamics v0d_dyn = new() { a = 0.02, b = 0.1, c = -30, d = 3.75, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            GetIzhikevichData(v0d_dyn, "DoubleCoilV0dStep.csv");

            MembraneDynamics v0v_dyn = new() { a = 0.02, b = 0.1, c = -30, d = 11.6, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            GetIzhikevichData(v0v_dyn, "DoubleCoilV0vStep.csv");

            MembraneDynamics v2a_dyn = new() { a = 0.5, b = 0.1, c = -50, d = 100, vmax = 10, vr = -60, vt = -45, k = 0.05, Cm = 20 };
            GetIzhikevichData(v2a_dyn, "DoubleCoilV2aStep.csv");

            GetLeakyIntegratorData(R: 50, C: 5.0, init_v: 0, filename: "DoubleCoilMuscleStep.csv");
        }

        public void TestBeatAndGlide()
        {
            MembraneDynamics mn_dyn = new() { a = 0.5, b = 0.01, c = -55, d = 100, vmax = 10, vr = -65, vt = -58, k = 0.5, Cm = 20 };
            GetIzhikevichData(mn_dyn, "BeatAndGlideMNStep.csv");

            MembraneDynamics dI6_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            GetIzhikevichData(dI6_dyn, "BeatAndGlidedI6Step.csv");

            MembraneDynamics v0v_dyn = new() { a = 0.01, b = 0.002, c = -55, d = 2, vmax = 8, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            GetIzhikevichData(v0v_dyn, "BeatAndGlideV0vStep.csv");

            MembraneDynamics v2a_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            GetIzhikevichData(v2a_dyn, "BeatAndGlideV2aStep.csv");

            MembraneDynamics v1_dyn = new() { a = 0.1, b = 0.002, c = -55, d = 4, vmax = 10, vr = -60, vt = -54, k = 0.3, Cm = 10 };
            GetIzhikevichData(v1_dyn, "BeatAndGlideV1Step.csv");

            GetLeakyIntegratorData(R: 1, C: 3.0, init_v: 0, filename: "BeatAndGlideMuscleStep.csv");
        }

        public void IzhikevichAnalysis()
        {
            //a: recovery time constant
            //b=0 "quadratic integrate and fire neuron with adaptation"
            //b<0 "quadratic integrate and fire neuron with a passive dendritic compartment" - amplifying
            //b>0 spiking - resonant
            //c: voltage reset - behavior after spike
            //d: outward-inward current after a spike - behavior after spike
            //u: recovery current
            //k and b depends on neuron's rheobase and input resistance
            //b: 
            double[] a_list = new double[] { 1 }; //0.03, 1, 10 }; // 0.1, 1, 10 };
            double[] b_list = new double[] { -2 }; //, 0, 20 };
            double[] k_list = new double[] { 0.1, 0.3, 0.7 }; //, 1, 10 };
            foreach (double a in a_list)
                foreach (double b in b_list)
                    foreach (double k in k_list)
                    {
                        MembraneDynamics dyn = new() { a = a, b = b, c = -50, d = 100, vmax = 35, vr = -60, vt = -40, k = k, Cm = 100 };
                        string filename = string.Format("Izhikevich_a_{0:0.#}_b_{1:0.#}_k_{2:0.#}Step2.csv", a, b, k);
                        GetIzhikevichData(dyn, filename, shortmode: true);
                    }

        }

    }
}
