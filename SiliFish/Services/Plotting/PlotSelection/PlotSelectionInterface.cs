using System.Text.Json.Serialization;

namespace SiliFish.Services.Plotting.PlotSelection
{
    [JsonDerivedType(typeof(PlotSelectionMultiCells), typeDiscriminator: "multicell")]
    [JsonDerivedType(typeof(PlotSelectionSomite), typeDiscriminator: "somite")]
    [JsonDerivedType(typeof(PlotSelectionUnits), typeDiscriminator: "units")]

    public interface PlotSelectionInterface
    {
    }

}
