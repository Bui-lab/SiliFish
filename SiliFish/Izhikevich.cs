using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiliFish.Extensions;
using SiliFish.ModelUnits;

namespace SiliFish
{
    public class MembraneDynamics
    {
        public double a, b, c, d;

        // vmax is the peak membrane potential of single action potentials
        public double vmax;
        // vr, vt are the resting and threshold membrane potential 
        public double vr, vt;
        // k is a coefficient of the quadratic polynomial 
        public double k;
        public double Cm; //the membrane capacitance

    }
    class Izhikevich_9P
    {
        //a, b, c, d, are the parameters for the membrane potential dynamics
        private double a;
        private double b;
        private double c;
        private double d;

        // vmax is the peak membrane potential of single action potentials
        double vmax;
        // vr, vt are the resting and threshold membrane potential 
        double vr, vt;
        // k is a coefficient of the quadratic polynomial 
        double k;
        double Cm; //the membrane capacitance

        double v = -65;//Keeps the current value of V 
        double u = 0;//Keeps the current value of u

        double init_v;
        double init_u;

        public Izhikevich_9P(MembraneDynamics dyn, double init_v, double init_u)
        {
            this.a = dyn?.a ?? 0;
            this.b = dyn?.b ?? 0;
            this.c = dyn?.c ?? 0;
            this.d = dyn?.d ?? 0;
            this.vmax = dyn?.vmax ?? 0;
            this.vr = dyn?.vr ?? 0;
            this.vt = dyn?.vt ?? 0;
            this.k = dyn?.k ?? 0;
            this.Cm = dyn?.Cm ?? 0;
            this.v = init_v;
            this.u = init_u;
            this.init_v = init_v;
            this.init_u = init_u;
        }

        public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new();

            paramDict.Add("Izhikevich_9P.a", a);
            paramDict.Add("Izhikevich_9P.b", b);
            paramDict.Add("Izhikevich_9P.c", c);
            paramDict.Add("Izhikevich_9P.d", d);
            paramDict.Add("Izhikevich_9P.vmax", vmax);
            paramDict.Add("Izhikevich_9P.vr", vr);
            paramDict.Add("Izhikevich_9P.vt", vt);
            paramDict.Add("Izhikevich_9P.k", k);
            paramDict.Add("Izhikevich_9P.Cm", Cm);
            paramDict.Add("Izhikevich_9P.v", v);
            paramDict.Add("Izhikevich_9P.u", u);
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            a = paramExternal.Read("Izhikevich_9P.a", a);
            b = paramExternal.Read("Izhikevich_9P.b", b);
            c = paramExternal.Read("Izhikevich_9P.c", c);
            d = paramExternal.Read("Izhikevich_9P.d", d);
            vmax = paramExternal.Read("Izhikevich_9P.vmax", vmax);
            vr = paramExternal.Read("Izhikevich_9P.vr", vr);
            vt = paramExternal.Read("Izhikevich_9P.vt", vt);
            k = paramExternal.Read("Izhikevich_9P.k", k);
            Cm = paramExternal.Read("Izhikevich_9P.Cm", Cm);
            init_v = v = paramExternal.Read("Izhikevich_9P.v", v);
            init_u = u = paramExternal.Read("Izhikevich_9P.u", u);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }
        public double GetNextVal(double Stim, ref bool spike)
        {
            double I = Stim;
            double vNew, uNew;
            spike = false;
            if (v < vmax)
            {
                // ODE eqs
                // Cdv refers to Capacitance * dV/dt as in Izhikevich model (Dynamical Systems in Neuroscience: page 273, Eq 8.5)
                double Cdv = k * (v - vr) * (v - vt) - u + I;
                vNew = v + (Cdv) * SwimmingModel.dt / Cm;
                double du = a * (b * (v - vr) - u);
                uNew = u + SwimmingModel.dt * du;
                v = vNew;
                u = uNew;
            }
            else
            {
                // Spike
                spike = true;
                vNew = c;
                uNew = u + d;
                v = vNew;
                u = uNew;
            }
            return v;
        }

        public (double[], double[]) SolveODE(double[] I)
        {
            int tmax = I.Length;
            double[] Vlist = new double[tmax];
            double[] ulist = new double[tmax];
            bool spike = false;
            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t], ref spike);
                Vlist[t] = this.v;
                ulist[t] = this.u;
            }
            return (Vlist, ulist);
        }

        public bool DoesSpike(double[] I, double init_v, double init_u, int warmup)
        {
            bool spike = false;
            int tmax = I.Length;
            v = init_v;
            u = init_u;
            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t], ref spike);
                if (t > warmup && spike) //ignore first little bit
                    break;
            }
            return spike;
        }


        public double CalculateRheoBase(double maxI, double sensitivity, int infinity, int warmup = 100)
        {
            infinity = (int)(infinity / SwimmingModel.dt);
            warmup = (int) (warmup/ SwimmingModel.dt);
            int tmax = infinity + warmup +10;
            double[] I = new double[tmax];
            double curI = maxI;
            double minI = 0;
            double rheobase = -1;
            
            while (curI >= minI + sensitivity)
            {
                foreach (int i in Enumerable.Range(warmup, infinity))
                    I[i] = curI;
                if (DoesSpike(I, init_v, init_u, warmup))
                {
                    rheobase = curI;
                    curI = (curI + minI) / 2;
                }
                else //increment
                {
                    minI = curI;
                    curI = (curI + (rheobase > 0 ? rheobase : maxI)) / 2;
                }
            }
            return rheobase;
        }
    }
    class Leaky_Integrator
    {
        public double R, C;
        double v; //keeps the current v value 
        public Leaky_Integrator(double R, double C, double init_v)
        {
            //Set Neuron constants.
            this.R = R;
            this.C = C;
            this.v = init_v;
        }
                public virtual Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = new();

            paramDict.Add("Leaky_Integrator.R", R);
            paramDict.Add("Leaky_Integrator.C", C);
            paramDict.Add("Leaky_Integrator.v", v);
            return paramDict;
        }

        public virtual void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            R = paramExternal.Read("Leaky_Integrator.R", R);
            C = paramExternal.Read("Leaky_Integrator.C", C);
            v = paramExternal.Read("Leaky_Integrator.v", v);
        }

        public virtual string GetInstanceParams()
        {
            return string.Join("\r\n", GetParameters().Select(kv => kv.Key + ": " + kv.Value.ToString()));
        }

        public double GetNextVal(double Stim)
        {
            double I = Stim;
            // ODE eqs
            double dv = (-1 / (R * C)) * v + I / C;
            double vNew = v + (dv) * SwimmingModel.dt;
            v = vNew;

            return v;
        }
        public double[] SolveODE(double[] I)
        {
            int tmax = I.Length;
            double[] Vlist = new double[tmax];

            for (int t = 0; t < tmax; t++)
            {
                GetNextVal(I[t]);
                Vlist[t] = this.v;
            }
            return Vlist;
        }
    }

    class Global
    {
        //Function to calculate the Euclidean distance between two neurons 
        static public double Distance(Cell cell1, Cell cell2, DistanceMode mode)
        {
            double x = Math.Abs(cell1.x - cell2.x);
            double y = Math.Abs(cell1.y - cell2.y);
            double z = Math.Abs(cell1.z - cell2.z);
            switch (mode)
            {
                case DistanceMode.Chebyshev:
                    return Math.Max(x, Math.Max(y, z));
                case DistanceMode.Manhattan:
                    return x + y + z;
                //FUTURE_IMPROVEMENT
                //case DistanceMode.Haversine:
                default: //Euclidean
                    return Math.Sqrt(x * x + y * y + z * z);
            }
        }

    }

    public class TwoExp_syn
    {
        readonly double taur;
        readonly double taud;
        readonly double vth;
        readonly double E_rev;
        public readonly double Conductance;
        public TwoExp_syn(SynapseParameters param, double conductance)
        {
            //Set synapse constants.
            this.taud = param.TauD;
            this.taur = param.TauR;
            this.vth = param.VTh;
            this.E_rev = param.E_rev;
            this.Conductance = conductance; //unitary conductance
        }

        public (double, double) GetNextVal(double v1, double v2, double IsynA, double IsynB)
        {
            double IsynANew, IsynBNew;
            if (v1 > vth)//pre-synaptic neuron spikes
            {
                // mEPSC
                IsynA += (E_rev - v2) * Conductance;
                IsynB += (E_rev - v2) * Conductance;
                double dIsynA = (-1 / taud) * IsynA;
                double dIsynB = (-1 / taur) * IsynB;
                IsynANew = IsynA + SwimmingModel.dt * (dIsynA);
                IsynBNew = IsynB + SwimmingModel.dt * (dIsynB);
            }
            else
            {
                // no synaptic event
                double dIsynA = (-1 / taud) * IsynA;
                double dIsynB = (-1 / taur) * IsynB;
                IsynANew = IsynA + SwimmingModel.dt * (dIsynA);
                IsynBNew = IsynB + SwimmingModel.dt * (dIsynB);
            }

            return (IsynANew, IsynBNew);
        }
    }


}
