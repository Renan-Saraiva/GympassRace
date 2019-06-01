using System;
using System.Collections.Generic;

namespace GympassRace.Data
{
    public class Race
    {
        public List<string> UnprocessedLaps;
        public List<Lap> Laps;
        public List<Racer> Racers;

        public Race()
        {
            UnprocessedLaps = new List<string>();
            Laps = new List<Lap>();
            Racers = new List<Racer>();
        }

        public void AddLap(string unprocessedLap)
        {
            if (unprocessedLap != null)
                UnprocessedLaps.Add(unprocessedLap);
        }
    }
}
