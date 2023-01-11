using SiliFish.DataTypes;
using SiliFish.Definitions;
using SiliFish.DynamicUnits;
using SiliFish.Extensions;
using SiliFish.ModelUnits;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace SiliFish.ModelUnits.Model
{
    /// <summary>
    /// This class is generated as a sample on how to create a model using the code
    /// </summary>
    public class SampleSwimmingModel : SwimmingModel //TODO not tested/incomplete
    {
        string NeuronCoreType = "Izhikevich_9P";
        string MuscleCoreType = "LeakyIntegrator";

        //Create any number of cellular groupings that will be relevant to your model.
        //Groups of supraspinal neurons, spinal neurons  and muscle cells are created as an example
        Dictionary<string, int> SupraSpinalNeuronCount;
        Dictionary<string, int> SpinalNeuronCount;
        Dictionary<string, int> MuscleCount;

        Dictionary<string, double> Sigma;

        public object Colors { get; private set; }

        public SampleSwimmingModel()
        {
            //This sample creates 3 brain neuronal pools, 5 neuronal pools, and 3 muscular pools.
            SupraSpinalNeuronCount = new Dictionary<string, int>();
            for (int i = 0; i < 3; i++)
                SupraSpinalNeuronCount.Add($"SupraSpinal{i + 1}", 5); //replace the neuronal cell group names and default numbers 

            SpinalNeuronCount = new Dictionary<string, int>();
            for (int i = 0; i < 5; i++)
                SpinalNeuronCount.Add($"Spinal{i + 1}", 5); //replace the neuronal cell group names and default numbers 

            MuscleCount = new Dictionary<string, int>();
            for (int i = 0; i < 2; i++)
                MuscleCount.Add($"Muscle{i + 1}", 5); //replace the muscular cell group names and default numbers

            Sigma = new Dictionary<string, double>
            {
                { $"X", 0.5 },
                { $"Y", 0.3 },
                { $"Z", 0.1 },
                { $"ConductionVelocity", 0.01 },

            };
        }
        /// <summary>
        /// Create the parameters that will be displayed on the UI and can be modified dynamically, rather than in the code
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> paramDict = base.GetParameters();
            CellCoreUnit NeuronCore = CellCoreUnit.CreateCore(NeuronCoreType, null);
            Dictionary<string, double> neuronParameters = NeuronCore.GetParameters();
            CellCoreUnit MuscleCore = CellCoreUnit.CreateCore(MuscleCoreType, null);
            Dictionary<string, double> muscleParameters = MuscleCore.GetParameters();

            foreach (string key in SpinalNeuronCount.Keys)
            {
                paramDict.Add($"Number.{key}", SpinalNeuronCount[key]);
                foreach (string neuronParam in neuronParameters.Keys)
                {
                    paramDict.Add($"CoreParam{key}.{neuronParam}", neuronParameters[neuronParam]);
                }
            }
            foreach (string key in SupraSpinalNeuronCount.Keys)
            {
                paramDict.Add($"Number.{key}", SupraSpinalNeuronCount[key]);
                foreach (string neuronParam in neuronParameters.Keys)
                {
                    paramDict.Add($"CoreParam{key}.{neuronParam}", neuronParameters[neuronParam]);
                }
            }
            foreach (string key in MuscleCount.Keys)
            {
                paramDict.Add($"Number.{key}", MuscleCount[key]);
                foreach (string muscleParam in muscleParameters.Keys)
                {
                    paramDict.Add($"CoreParam{key}.{muscleParam}", muscleParameters[muscleParam]);
                }
            }

            foreach (string key in Sigma.Keys)
                paramDict.Add($"Sigma.{key}", Sigma[key]);

            return paramDict;
        }

        /// <summary>
        /// Ensures all JSON files with missing parameters are brought up to date 
        /// if there are new parameters added to the code
        /// Update it as necessary (if you add a parameter after already creating models)
        /// </summary>
        /// <param name="paramDict"></param>
        protected override void FillMissingParameters(Dictionary<string, object> paramDict)
        {
            base.FillMissingParameters(paramDict);
        }

        public override void SetParameters(Dictionary<string, object> paramExternal)
        {
            if (paramExternal == null || paramExternal.Count == 0)
                return;
            base.SetParameters(paramExternal);

            foreach (string key in paramExternal.Keys)
            {
                if (key.StartsWith("Number."))
                {
                    string cellName = key[7..];//skip "Number."
                    if (SupraSpinalNeuronCount.ContainsKey(cellName))
                        SupraSpinalNeuronCount[cellName] = paramExternal.ReadInteger(key, SupraSpinalNeuronCount[cellName]);
                    else if (SpinalNeuronCount.ContainsKey(cellName))
                        SpinalNeuronCount[cellName] = paramExternal.ReadInteger(key, SpinalNeuronCount[cellName]);
                    else if (MuscleCount.ContainsKey(cellName))
                        MuscleCount[cellName] = paramExternal.ReadInteger(key, MuscleCount[cellName]);
                }
                else if (key.StartsWith("Sigma."))
                {
                    Sigma[key]= paramExternal.ReadDouble(key, Sigma[key]);
                }

            }
        }
        protected override void InitNeurons()
        {
            //https://stackoverflow.com/questions/4834659/loop-through-all-colors
            var colorProperties = Colors.GetType().GetProperties(BindingFlags.Static | BindingFlags.Public);
            Color[] colors = colorProperties.Select(prop => (Color)prop.GetValue(null, null)).ToArray();

            int colorIndex = 0;
            double cv = CurrentSettings.Settings.cv;
            //This sample assumes the model is somite based
            if (ModelDimensions.NumberOfSomites <= 0) return;
            NeuronPools = new List<CellPool>();
            //Spinal cells
            foreach (string key in SpinalNeuronCount.Keys)
            {
                if (colorIndex >= colors.Length)
                    colorIndex = 0;
                CellPool pool = new(this, CellType.Neuron, BodyLocation.SpinalCord, key, SagittalPlane.Both, 1, colors[colorIndex++]);
                //this sample uniformly distributes all of the cell pools across the spinal cord

                int neuronCount = SpinalNeuronCount[key];
                UniformDistribution xdist = new(ModelDimensions.SupraSpinalRostralCaudalDistance, ModelDimensions.SpinalRostralCaudalDistance + ModelDimensions.SupraSpinalRostralCaudalDistance, true, false);
                UniformDistribution ydist = new(0, ModelDimensions.SpinalMedialLateralDistance, true, false);
                UniformDistribution zdist = new(0, ModelDimensions.SpinalDorsalVentralDistance, true, false);

                for (int somite = 0; somite < ModelDimensions.NumberOfSomites; somite++)
                {
                    Coordinate[] coors = Coordinate.GenerateCoordinates(this, BodyLocation.SpinalCord, xdist, ydist, zdist, neuronCount, somite);
                    for (int i = 0; i < neuronCount; i++)
                    {
                        Neuron n = new Neuron(key, NeuronCoreType, somite + 1, i + 1, cv);
                        n.coordinate = coors[i];
                    }
                }
                NeuronPools.Add(pool);
            }
            //Musculoskeletal cells
            foreach (string key in MuscleCount.Keys)
            {
                if (colorIndex >= colors.Length)
                    colorIndex = 0;
                CellPool pool = new(this, CellType.MuscleCell, BodyLocation.MusculoSkeletal, key, SagittalPlane.Both, 1, colors[colorIndex++]);
                //this sample uniformly distributes all of the cell pools across the spinal cord

                int muscleCount = SpinalNeuronCount[key];
                UniformDistribution xdist = new(ModelDimensions.SupraSpinalRostralCaudalDistance, ModelDimensions.SpinalRostralCaudalDistance + ModelDimensions.SupraSpinalRostralCaudalDistance, true, false);
                UniformDistribution ydist = new(0, ModelDimensions.BodyMedialLateralDistance, true, false);
                UniformDistribution zdist = new(0, ModelDimensions.BodyDorsalVentralDistance, true, false);
                for (int somite = 0; somite < ModelDimensions.NumberOfSomites; somite++)
                {
                    Coordinate[] coors = Coordinate.GenerateCoordinates(this, BodyLocation.SpinalCord, xdist, ydist, zdist, muscleCount, somite);
                    for (int i = 0; i < muscleCount; i++)
                    {
                        Neuron n = new Neuron(key, NeuronCoreType, somite + 1, i + 1, cv);
                        n.coordinate = coors[i];
                    }
                }
                NeuronPools.Add(pool);
            }
            //SupraSpinal cells
            foreach (string key in SupraSpinalNeuronCount.Keys)
            {
                if (colorIndex >= colors.Length)
                    colorIndex = 0;
                CellPool pool = new(this, CellType.Neuron, BodyLocation.SupraSpinal, key, SagittalPlane.Both, 1, colors[colorIndex++]);
                //this sample uniformly distributes all of the cell pools across the brain region

                int neuronCount = SpinalNeuronCount[key];
                UniformDistribution xdist = new(ModelDimensions.SupraSpinalRostralCaudalDistance, ModelDimensions.SpinalRostralCaudalDistance + ModelDimensions.SupraSpinalRostralCaudalDistance, true, false);
                UniformDistribution ydist = new(0, ModelDimensions.SpinalMedialLateralDistance, true, false);
                UniformDistribution zdist = new(0, ModelDimensions.SpinalDorsalVentralDistance, true, false);
                Coordinate[] coors = Coordinate.GenerateCoordinates(this, BodyLocation.SpinalCord, xdist, ydist, zdist, neuronCount, -1);
                for (int i = 0; i < neuronCount; i++)
                {
                    Neuron n = new Neuron(key, NeuronCoreType, 0, i + 1, cv);
                    n.coordinate = coors[i];
                }

                NeuronPools.Add(pool);
            }
        }

        protected override void InitSynapsesAndGapJunctions()
        {
            //base.InitSynapsesAndGapJunctions();
        }
        protected override void InitStructures(int nmax)
        {
            Time = new double[nmax];
            neuronPools.Clear();
            musclePools.Clear();
            gapPoolConnections.Clear();
            chemPoolConnections.Clear();
            InitNeurons();
            InitSynapsesAndGapJunctions();
            InitDataVectors(nmax);
            initialized = true;
}
    }
}
