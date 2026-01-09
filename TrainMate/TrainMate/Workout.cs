using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    class Workout
    {
        public int Id { get; set; }

        public DateTime date { get; set; }

        public Dictionary<string, Exercise> exercises { get; set; }

        public Workout() { }

        public Workout(int id, DateTime date, Dictionary<string, Exercise> exercises)
        {
            Id = id;
            this.date = date;
            this.exercises = exercises;
        }
    }
}
