using System.Linq;
using System.Collections.Generic;

namespace GympassRace.Domain
{
    public class Race
    {
        public List<string> UnprocessedLaps;
        public List<Racer> Racers;
        public Racer Winner
        {
            get
            {
                return Racers.Where(racer => racer.EndedRace).OrderBy(racer => racer.RaceTime).First();
            }
        }
        public Lap BestLap
        {
            get
            {
                return Racers.Select(racer => racer.BestLap).OrderBy(lap => lap.LapTime).First();
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
    }
}
