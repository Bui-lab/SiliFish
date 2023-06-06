using SiliFish.DynamicUnits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SiliFish.Services.Plotting
{
    [JsonDerivedType(typeof(PlotSelectionMultiCells), typeDiscriminator: "multicell")]
    [JsonDerivedType(typeof(PlotSelectionJunction), typeDiscriminator: "junction")]

    public interface PlotSelectionInterface
    {
    }


}
