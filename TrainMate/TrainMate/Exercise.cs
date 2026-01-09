using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MuscleGroup MuscleGroup { get; set; } = new MuscleGroup(); //ustvari prazen MuscleGroup ko je ustvarjen Exercise (če ni podan)

        public Exercise() { }

        public Exercise(int id, string name, MuscleGroup muscleGroup)
        {
            this.Id = id;
            this.Name = name;
            this.MuscleGroup = muscleGroup;
        }
    }
}
