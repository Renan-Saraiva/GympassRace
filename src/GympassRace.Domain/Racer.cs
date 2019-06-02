using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GympassRace.Domain
{
    public class Racer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Lap> Laps { get; set; }
        public Lap BestLap
        {
            get
            {
                return Laps.OrderBy(lap => lap.LapTime).First();
            }
        }
        public bool EndedRace
        {
            get
            {
                return Laps.Count > 3;
            }
        }
        public TimeSpan RaceTime
        {
            get
            {
                return new TimeSpan(Laps.Sum(lap => lap.LapTime.Ticks));
            }
        }
        public float RaceAVGSpeed
        {
            get
            {
                return Laps.Average(lap => lap.LapAVGSpeed);
            }
        }

        public Racer(string id, string name)
        {
            Id = id;
            Name = name;
            Laps = new List<Lap>();
        }

        public void AddLap(Lap lap)
        {
            Laps.Add(lap);
        }
    }
}
