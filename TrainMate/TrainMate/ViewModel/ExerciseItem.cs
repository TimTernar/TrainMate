using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate.ViewModel
{
    public class ExerciseItem
    {
        public string Key { get; set; } = "";  // bench_press
        public Exercise Exercise { get; set; } = new();
        public string Name => Exercise?.Name ?? "";

    }
}
