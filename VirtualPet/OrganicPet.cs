﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualPet
{
    public class OrganicPet : Pet
    {
        const int InitialHungerValue = 50;
        const int InitialHealthValue = 30;
        const int InitialHydrationValue = 30;
        const int InitialIritableValue = 25;
        const int InitialEnergyValue = 90;

        public int hungerThresholdMIN = 10;
        public int hungerThresholdMAX = 90;
        public int healthThresholdMIN = 10;
        public int healthThresholdMAX = 90;
        public int irritabaleThresholdMIN = 10;
        public int irritabaleThresholdMAX = 90;
        public int hydrationThresholdMIN = 10;
        public int hydrationThresholdMAX = 90;
        public int energyThresholdMIN = 10;
        public int energyThresholdMAX = 90;

        public int Hunger { get; set; }
        public int Health { get; set; }
        public int Hydration { get; set; }
        public int Energy { get; set; }
        public int Irritated { get; set; }

        public OrganicPet()
        {
            base.SetInitialPetValues();
            SetInitialPetValues();
        }

        public OrganicPet(string name)
        {
            base.SetName(name);
            base.SetInitialPetValues();
            SetInitialPetValues();
        }

        public OrganicPet(string name, string species)
        {
            base.SetName(name);
            base.SetSpecies(species);
            base.SetInitialPetValues();
            SetInitialPetValues();
        }

        public override void SetInitialPetValues()
        {
            Hunger = InitialHungerValue;
            Health = InitialHealthValue;
            Hydration = InitialHydrationValue;
            Energy = InitialEnergyValue;
            Irritated = InitialIritableValue;
        }

        public int GetHunger()
        {
            return Hunger;
        }

        public int GetHealth()
        {
            return Health;
        }

        public int GetHyrdation()
        {
            return Hydration;
        }

        public int GetEnergy()
        {
            return Energy;
        }

        public int GetIrritable()
        {
            return Irritated;
        }

        public void MinimizeIrritation()
        {
            Irritated = irritabaleThresholdMIN;
        }

        public void MaximizeIrritation()
        {
            Irritated = irritabaleThresholdMAX;
        }

        public void MinimzeHunger()
        {
            Hunger = hungerThresholdMIN;
        }

        public void MaximizeHunger()
        {
            Hunger = hungerThresholdMAX;
        }

        public void MinimizeHydration()
        {
            Hydration = hydrationThresholdMIN;
        }

        public void MaximizeHydration()
        {
            Hydration = hydrationThresholdMAX;
        }

        public void MinimizeEnergy()
        {
            Energy = energyThresholdMIN;
        }

        public void MaximizeEnergy()
        {
            Energy = energyThresholdMAX;
        }

        public void MinimizeHealth()
        {
            Health = healthThresholdMIN;
        }

        public void MaximizeHealth()
        {
            Health = healthThresholdMAX;
        }

        public bool IsPetHungry()
        {
            if (Hunger >= hungerThresholdMAX)
                return true;
            else return false;
        }

        public bool IsPetFullOfFood()
        {
            if (Hunger <= hungerThresholdMIN)
                return true;
            else return false;
        }

        public bool IsPetThirsty()
        {
            if (Hydration <= hydrationThresholdMIN)
                return true;
            else return false;
        }

        public bool IsPetFullOfWater()
        {
            if (Hydration >= hydrationThresholdMAX)
                return true;
            else return false;
        }

        public bool IsPetIrritated()
        {
            if (Irritated >= irritabaleThresholdMAX)
                return true;
            else return false;
        }

        public bool IsPetContent()
        {
            if (Irritated <= irritabaleThresholdMIN)
                return true;
            else return false;
        }

        public bool IsPetSick()
        {
            if (Health <= healthThresholdMIN)
                return true;
            else return false;
        }

        public bool IsPetHealthy()
        {
            if (Health >= healthThresholdMAX)
                return true;
            else return false;
        }

        public bool IsPetEnergized()
        {
            if (Energy >= energyThresholdMAX)
                return true;
            else return false;
        }

        public bool IsPetTired()
        {
            if (Energy <= energyThresholdMIN)
                return true;
            else return false;
        }

        public void Drink()
        {
            Hydration = Hydration + 40;
        }

        public void Relieve()
        {
            Irritated = irritabaleThresholdMIN;
        }

        public void SeeDoctor()
        {
            Health = Health + 30;
        }

        public override void ShowPetStatus()
        {
            base.ShowPetStatus();
            Console.WriteLine($"HEALTH factor is {Health}.");
            Console.WriteLine($"HUNGER factor is {Hunger}.");
            Console.WriteLine($"THIRST factor is {Hydration}.");
            Console.WriteLine($"ENERGY factor is {Energy}");
            Console.WriteLine($"IRRITATED factor is {Irritated}.");
        }

        public override void Play()
        {
            //base.Play();
            Boredom = Boredom - 40;
            Hunger = Hunger + 10;
            Health = Health + 10;
            Hydration = Hydration - 20;
            Irritated = Irritated - 10;
            Energy = Energy - 50;
        }

        public override void Sleep()
        {
            base.Sleep();
            Energy = Energy + 40;
            Hunger = Hunger + 20;
            Hydration = Hydration - 10;
            Health = Health + 5;
            Irritated = Irritated + 30;
        }

        public override void Ignore()
        {
            base.Ignore();
            Irritated = Irritated + 10;
            Energy = Energy - 10;
        }

        public override void Tick()
        {
            base.Tick();
            Hunger = Hunger + 5;
            Health = Health - 5;
            Hydration = Hydration - 5;
            Irritated = Irritated + 5;
            Energy = Energy - 5;
        }

        private  void Feed()
        {
            Hunger = Hunger - 40;
        }

        public string FeedPet()
        {
            string message = "";
            if (Hunger > hungerThresholdMIN)
            {
                //somePet.Feed();
                Feed();
                message = $"You fed {Name}.";
            }
            else
            {
                message = $"{Name} isn't hungry right now, maybe try something else.";
            }
            return message;
        }

        public string GivePetWater()
        {
            string message = "";
            if (Hydration < hydrationThresholdMAX)
            {
                Drink();
                message = $"You gave {Name} some water to drink.";
            }
            else
            {
                message = $"{Name} is fully hydrated.  Act quick before {Name} has an accident!";
            }
            return message;
        }

        public string TakePetToVet()
        {
            string message = "";
            if (Health < healthThresholdMAX)
            {
                SeeDoctor();
                message = $"You took {Name} to the vet and all is well.";
            }
            else
            {
                message = $"{Name} is as healthy as can be.  Save your money.";
            }
            return message;
        }

        public string LetPetOutside()
        {
            string message = "";
            if (Irritated > irritabaleThresholdMIN)
            {
                Relieve();
                message = $"You let {Name} relieve themself.";
            }
            else
            {
                message = $"{Name} is being stubborn and won't go outside right now.";
            }
            return message;
        }

        public string LetPetSleep()
        {
            string message = "";
            if (IsPetTired())
            {
                Sleep();
                message = $"{Name} is sleeping soundly.";
            }
            else if (IsPetEnergized())
            {
                message = $"{Name} is full of energy right now and refuses to sleep.";
            }
            else
            {
                message = $"{Name} is sleeping.";
                Sleep();
            }
            return message;
        }

    }
}
