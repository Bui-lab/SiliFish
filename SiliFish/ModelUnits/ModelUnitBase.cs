using SiliFish.DataTypes;
using SiliFish.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SiliFish.ModelUnits
{
    public abstract class ModelUnitBase : IComparable<ModelUnitBase>
    {
        private string _Name;
        public int DbId = 0;
        public string PlotType;
        public virtual string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_Name))
                    _Name = GeneratedName();
                return _Name;
            }
            set { _Name = value; }
        }

        [JsonIgnore]
        public virtual string ID { get; }
        [JsonIgnore]
        public virtual string Tooltip { get { return ID; } }
        private bool _Active = true;
        public virtual bool Active { get => _Active; set => _Active = value; }
        public TimeLine TimeLine_ms { get; set; } = new TimeLine();
        [JsonIgnore]
        public TimeLine TimeLine_ind { get; set; } = null;

        public virtual List<Difference> DiffersFrom(ModelUnitBase other)
        {
            return null;
        }

        public static List<Difference> ListDiffersFrom(List<ModelUnitBase> firstList, List<ModelUnitBase> secondList)
        {
            List<Difference> differences = [];
            foreach (ModelUnitBase c1 in firstList)
            {
                ModelUnitBase c2 = secondList.FirstOrDefault(cp => cp.ID == c1.ID);
                if (c2 is null)
                    differences.Add(new Difference($"New", c1.GetType().Name, c1.ID, null));
                else
                {
                    List<Difference> diff = c1.DiffersFrom(c2);
                    if (diff != null)
                        differences.AddRange(diff);
                }
            }
            foreach (ModelUnitBase c3 in secondList)
            {
                ModelUnitBase c4 = firstList.FirstOrDefault(cp => cp.ID == c3.ID);
                if (c4 is null)
                    differences.Add(new Difference("Deleted", c3.GetType().Name, null, c3.ID));
            }
            if (differences.Count != 0)
                return differences;
            return null;
        }
        public virtual int CompareTo(ModelUnitBase other)
        {
            return ID.CompareTo(other.ID);
        }

        public virtual bool CheckValues(ref List<string> errors, ref List<string> warnings)
        {
            errors ??= [];
            warnings ??= [];
            return true;
        }
        public virtual string GeneratedName()
        {
            return _Name;
        }

        public virtual ModelUnitBase CreateCopy()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }
        public virtual bool HasStimulus()
        {
            Exception exception = new NotImplementedException();
            ExceptionHandler.ExceptionHandling(System.Reflection.MethodBase.GetCurrentMethod().Name, exception);
            throw exception;
        }

        public virtual void InitForSimulation(double dt)
        {
            TimeLine_ind = new TimeLine(TimeLine_ms);
            if (dt > 0)
                TimeLine_ind.MultiplyBy(1 / dt);
        }
        public virtual bool IsActive(int timepoint)
        {
            return TimeLine_ind?.IsActive(timepoint) ?? true;
        }

    }
}
