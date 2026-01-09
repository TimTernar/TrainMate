using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainMate
{
    //Kaj je cilj uporabnika
    //To ima malo večjo vlogo pri receptih, kjer mu predlaga glede na cilj recepte
    public enum Goal
    {
        WeightLoss,
        MuscleGain,
        Maintanance,
        Endurance
    }

    //Kolkiko izkušenj ima uporabnik
    public enum Expirience
    {
        Beginner,
        Intermidiate,
        Advanced,
    }

    //Kraj kjer uporabnik trenira. Glede na to se mu mogoče (če bomo imeli čas :) ) določi katere vaje so mu na voljo
    public enum Access
    {
        Gym,
        Home,
        FullGym
    }

    //kako pogosto trenira. To se mi zdi boljše kot pa da bi samo dal integer, saj več kot 1 na dan je nesmiselno
    public enum TrainingFrequency
    {
        OneToTwo,
        ThreeToFour,
        FivePlus,
    }
    class Uporabnik
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public Goal goal { get; set; }

        public Expirience expirience {  get; set; }

        public Access access { get; set; }

        public TrainingFrequency frequency { get; set; }

        //za vsak slučaj
        public Uporabnik() { }

        public Uporabnik(int id, string email, Goal goal, Expirience expirience, Access access, TrainingFrequency frequency)
        {
            Id = id;
            Email = email;
            this.goal = goal;
            this.expirience = expirience;
            this.access = access;
            this.frequency = frequency;
        }
    }
}
