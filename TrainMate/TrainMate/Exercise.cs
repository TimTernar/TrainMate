using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //prazavprav je to za posebno mišico ki jo trenira, in je list ker ji je lahk več na enkrat
        public List<MuscleGroup> MuscleGroup { get; set; }

        public Exercise() { }

        public Exercise(int id, string name, List<MuscleGroup> muscleGroup)
        {
            this.Id = id;
            this.Name = name;
            this.MuscleGroup = muscleGroup;
        }
    }
}
