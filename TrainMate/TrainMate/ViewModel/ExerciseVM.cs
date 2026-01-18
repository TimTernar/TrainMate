using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainMate.ViewModel;

namespace TrainMate.ViewModel
{
    internal class ExerciseVM
    {
        public string Key { get; set; }

        private readonly CreateTelovadbaViewModel _root;

        public string Name { get; set; } = "";

        public ObservableCollection<SetVM> Sets { get; } = new();

        public ICommand AddSetCommand { get; }

        public ExerciseVM(CreateTelovadbaViewModel root)
        {
            _root = root;

            AddSetCommand = new Command(() =>
            {
                AddSet(previous: "");
            });
        }

        public void AddSet(string previous)
        {
            var next = Sets.Count + 1;
            Sets.Add(new SetVM(this, next, previous));
        }

        public void RenumberSets()
        {
            for (int i = 0; i < Sets.Count; i++)
                Sets[i].SetNumber = i + 1;
        }
    }
}
