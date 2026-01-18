using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    public class Workout
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatedAt { get; set; }

        public Dictionary<string, WorkoutExercise> Exercises { get; set; }

    }
}
