using SiliFish.DataTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiliFish.Database
{
    public class EpisodeRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SimulationId { get; set; } 
        public int EpisodeId { get; set; } 
        public double EpisodeStart { get; set; } 
        public double EpisodeEnd { get; set; } 
        public double TBF { get; set; } 
        public double MeanAmplitude { get; set; } 
        public double MedianAmplitude { get; set; } 
        public double MaxAmplitude { get; set; }

        public EpisodeRecord() { }//required for SQLite
        public EpisodeRecord(int episodeID, int simulationID, SwimmingEpisode episode)
        {
            SimulationId = simulationID;
            EpisodeId = episodeID;
            EpisodeStart = episode.Start;
            EpisodeEnd = episode.End;
            TBF = episode.BeatFrequency;
            MeanAmplitude = episode.MeanAmplitude;
            MedianAmplitude = episode.MedianAmplitude;
            MaxAmplitude = episode.MaxAmplitude;
        }
    }
}
