using GympassRace.Domain;
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
                    ul = PartString(ul, GetFirstNumberOccurrenceIndex(ul), out string racerName);
                    ul = PartString(ul, ProcessRaceFieldLength.LapNumber, out string lapNumber);
                    ul = PartString(ul, ProcessRaceFieldLength.LapTime, out string lapTime);
                    string lapAVGSpeed = ul;

                    if (racerCode.Length > 2)
                        racerCode = racerCode.Substring(0, 3);

                    if (!TryGetRacer(racerCode, out Racer racer))
                    {
                        racer = CreateRacer(racerCode, racerName);
                        Race.Racers.Add(racer);
                    }

                    Lap lap = CreateLap(hour, lapNumber, lapTime, lapAVGSpeed);
                    
                    if (lap.LapNumber <= 4 || racer.Laps.Count <= 4)
                        racer.AddLap(CreateLap(hour, lapNumber, lapTime, lapAVGSpeed));
                }
            }
        }

        private bool TryGetRacer(string id, out Racer racer)
        {
            racer = Race.Racers.FirstOrDefault(r => r.Id.Equals(id));
            return racer != null;
        }        

        private string PartString(string value, int length, out string stringParted)
        {
            stringParted = value.Substring(0, length);
            return value.Substring(length);
        }

        private int GetFirstNumberOccurrenceIndex(string value)
        {
            Regex re = new Regex(@"\d+");
            Match m = re.Match(value);

            if (m.Success)
                return m.Index;

            throw new Exception("Formato de arquivo inválido");
        }

        #region Builders

        private Racer CreateRacer(string id, string name)
        {
            return new Racer(id, name);
        }

        private Lap CreateLap(string hour, string lapNumber, string lapTime, string lapAVGSpeed)
        {
            if (!TimeSpan.TryParse(hour, out TimeSpan hourTP))
                throw new Exception("Formato da hora da volta inválido");

            if (!int.TryParse(lapNumber, out int lapNumberInt))
                throw new Exception("Formato do número da volta inválido");

            if (!TimeSpan.TryParse(string.Concat("00:0", lapTime), out TimeSpan lapTimeTP))
                throw new Exception("Formato do tempo da volta inválido");

            if (!float.TryParse(lapAVGSpeed, out float lapAVGSpeedFloat))
                throw new Exception("Formato da velocidade média da volta inválido");

            return new Lap(hourTP, lapNumberInt, lapTimeTP, lapAVGSpeedFloat);
        }

        #endregion
    }
}