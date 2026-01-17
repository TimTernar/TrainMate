using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    public class DayCell
    {
        public DateTime Date { get; set; }
        public string DayNumber { get; set; } = "";
        public bool IsCurrentMonth { get; set; }
        public double Opacity { get; set; } = 1.0;
    }
}
