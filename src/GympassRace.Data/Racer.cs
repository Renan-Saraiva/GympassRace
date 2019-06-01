using System;
using System.Collections.Generic;
using System.Text;

namespace GympassRace.Data
{
    public class Racer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Racer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
