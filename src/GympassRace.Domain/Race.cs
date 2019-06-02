using System.Linq;
using System.Collections.Generic;

namespace GympassRace.Domain
{
    public class Race
    {
        public List<string> UnprocessedLaps { get; set; }
        public List<Racer> Racers { get; set; }
        public bool HasWinner
        {
            get
            {
                return Racers.Any(racer => racer.EndedRace);
            }            
        }
        public Racer Winner
        {
            get
            {
                return Racers.Where(racer => racer.EndedRace).OrderBy(racer => racer.RaceTime).First();
            }
        }
        public List<Racer> FinalClassification
        {
            get
            {
                return Racers.OrderByDescending(racer => racer.Laps.Count).ThenBy(racer => racer.RaceTime).ToList();
            }
        }
        public List<Racer> BestLapClassification
        {
            get
            {
                return Racers.OrderBy(racer => racer.BestLap.LapTime).ToList();
            }
        }

        public Race()
        {
            UnprocessedLaps = new List<string>();
            Racers = new List<Racer>();
        }

        public void AddLap(string unprocessedLap)
        {
            if (unprocessedLap != null)
                UnprocessedLaps.Add(unprocessedLap);
        }

        public void AddRacer(Racer racer)
        {
            if (racer != null)
                Racers.Add(racer);
        }
    }
}
