using GympassRace.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GympassRace.Utils
{
    internal class ProcessRaceFieldLength
    {
        public static int Hour => 12;
        public static int RacerCode => 4;
        public static int LapNumber => 1;
        public static int LapTime => 8;
    }

    public class ProcessRace
    {
        private readonly Race Race;

        public ProcessRace(Race race)
        {
            Race = race;
        }

        public void Process()
        {
            if (Race != null && Race.UnprocessedLaps != null)
            {
                foreach (string UnprocessedLap in Race.UnprocessedLaps)
                {
                    string ul = UnprocessedLap;

                    ul = PartString(ul, ProcessRaceFieldLength.Hour, out string hour);
                    ul = PartString(ul, ProcessRaceFieldLength.RacerCode, out string racerCode);
                    ul = PartString(ul, GetFirtNumberOccurrenceIndex(ul), out string racerName);
                    ul = PartString(ul, ProcessRaceFieldLength.LapNumber, out string lapNumber);
                    ul = PartString(ul, ProcessRaceFieldLength.LapTime, out string lapTime);
                    string lapAVGSpeed = ul;

                    int racerCodeNumber = int.Parse(racerCode.Substring(0, 3));

                    if (!ContainsRacer(racerCodeNumber))
                        Race.Racers.Add(CreateRacer(racerCodeNumber, racerName));



                }
            }
        }

        private bool ContainsRacer(int id)
        {
            return Race.Racers.Any(racer => racer.Id.Equals(id));
        }

        private Racer CreateRacer(int id, string name)
        {
            return new Racer(id, name);
        }

        private string PartString(string value, int length, out string stringParted)
        {
            stringParted = value.Substring(0, length);

            return value.Substring(length);
        }

        private int GetFirtNumberOccurrenceIndex(string value)
        {
            Regex re = new Regex(@"\d+");
            Match m = re.Match(value);

            if (m.Success)
                return m.Index;

            throw new Exception("Invalid file format");
        }
    }
}