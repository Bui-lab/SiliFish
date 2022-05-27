using System;
using System.Drawing;
using System.Linq;
using SiliFish.PredefinedModels;

namespace SiliFish
{
    class Program
    {
        static int TIME_END = 10000;
        static int SKIP = 200;


        static void RunSingleCoil()
        {
            int nIC = 5;
            int nMN, nV0d, nMuscle;
            nMN = nV0d = nMuscle = 10;
            SingleCoilModel testnewsc = new ();
            testnewsc.SetConstants(stim0: 50, sigma_range: 0, E_glu: 0, E_gly: -45, cv: 4);
            testnewsc.SetNumberOfCells(nIC, nMN, nV0d, nMuscle);
            testnewsc.SetStimulusMode(StimulusMode.Step);
            testnewsc.MainLoop(seed: 0, tmax: TIME_END, tskip: SKIP);

        }
        static void RunDoubleCoil() 
        {
            int nIC = 10;
            int nMN, nV0d, nV0v, nV2a, nMuscle;
            nMN = nV0d = nV0v = nV2a = nMuscle = 10;
            DoubleCoilModel testnewdc = new ();
            testnewdc.SetConstants(stim0: 35, sigma_range: 0, E_glu: 0, E_gly: -58, cv: 1);
            testnewdc.SetNumberOfCells(nIC, nMN, nV0d, nV0v, nV2a,  nMuscle);
            testnewdc.SetStimulusMode(StimulusMode.Step);
            testnewdc.MainLoop(seed: 0, tmax: TIME_END, tskip: SKIP);
        }

        static void RunBeatAndGlide()
        {
            BeatAndGlideModel testbg = new ();
            testbg.SetConstants(stim0: 2.89, sigma_range: 0, E_glu: 0, E_gly: -70, cv: 0.8);
            int nMN, ndI6, nV0v, nV2a, nV1, nMuscle;
            nMN = ndI6 = nV0v = nV2a = nV1 = nMuscle = 15;

            testbg.SetNumberOfCells(nMN, ndI6, nV0v, nV2a, nV1, nMuscle);
            testbg.SetStimulusMode(StimulusMode.Step);
            testbg.MainLoop(seed: 0, tmax: TIME_END, tskip: SKIP);

        }

        static void Main(string[] args)
        {
            
            DateTime dtStart = DateTime.Now;
            //RunSingleCoil();
            //RunDoubleCoil();
            RunBeatAndGlide();
            DateTime dtEnd = DateTime.Now;
            System.TimeSpan ts = dtEnd.Subtract(dtStart);
            Console.WriteLine(string.Format("{0} minutes, {1} seconds", ts.Hours * 60 + ts.Minutes, ts.Seconds));
        }
    }
}
