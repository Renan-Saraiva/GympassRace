using System;
using System.Collections.Generic;
using System.Text;

namespace GympassRace.Domain
{
    public class Lap
    {
        public TimeSpan Hour { get; set; }
        public int LapNumber { get; set; }
        public TimeSpan LapTime { get; set; }
        public float LapAVGSpeed { get; set; }

        public Lap(TimeSpan hour, int lapNumber, TimeSpan lapTime, float lapAVGSpeed)
        {
            Hour = hour;
            LapNumber = lapNumber;
            LapTime = lapTime;
            LapAVGSpeed = lapAVGSpeed;
        }
    }
}
