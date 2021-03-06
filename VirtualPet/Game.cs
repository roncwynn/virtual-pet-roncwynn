﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace VirtualPet
{
    public class Game
    {
        public Game()
        {
        }

        public void BeginGame()
        {
            Shelter someShelter = new Shelter();
            AddInitalPetsToShelter(someShelter);
            PlayGame(someShelter);
        }

        private void PlayGame(Shelter someShelter)
        {
            Menu menu = new Menu();
            string playerFeedbackMessage = "";
            string selectedMenuOption;
            bool keepPlaying = true;
            while (keepPlaying)
            {
                menu.ShowGameMainMenu(someShelter);
                selectedMenuOption = menu.GetPlayerChoice();
                switch (selectedMenuOption)
                {
                    case "1":
                        ShowAllPets(someShelter);
                        break;
                    case "2":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                            { playerFeedbackMessage = FeedAllOrganicPets(someShelter); }
                        break;
                    case "3":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                            { playerFeedbackMessage = WaterAllOrganicPets(someShelter); }
                        break;
                    case "4":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                        { playerFeedbackMessage = OilAllRoboticPets(someShelter); }
                        break;
                    case "5":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                        { playerFeedbackMessage = PlayWithPets(someShelter); }
                        break;
                    case "6":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                        { PetOneOnOne(someShelter); }
                        break;
                    case "7":
                        AdmitPet(someShelter);
                        break;
                    case "8":
                        if (someShelter.IsShelterEmpty())
                        { ShowShelterEmptyMessage(); }
                        else
                        {
                            Pet selectedPet = SelectPet(someShelter);
                            AdoptPet(someShelter, selectedPet);
                        }
                        break;
                    case "9":
                        LeaveShelterMessage(someShelter);
                        keepPlaying = false;
                        break;
                    default:
                        Console.Clear();
                        playerFeedbackMessage = "\n\nInvalid Choice.  Please try again.";
                        break;
                }
                Console.WriteLine(playerFeedbackMessage);
                playerFeedbackMessage = "";
                Console.ResetColor();
            }
        }

        private void AddInitalPetsToShelter(Shelter someShelter)
        {
            OrganicPet someOrganicPet = new OrganicPet("Ron", "Lion");
            someShelter.AddPetToShelter(someOrganicPet);
            RoboticPet someRoboticPet = new RoboticPet("Alex", "Dog");
            someShelter.AddPetToShelter(someRoboticPet);
            someOrganicPet = new OrganicPet("Rachel", "Cat");
            someShelter.AddPetToShelter(someOrganicPet);
            someRoboticPet = new RoboticPet("Vanessa", "Fembot");
            someShelter.AddPetToShelter(someRoboticPet);
        }

        private void ShowShelterFullMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry, the Shelter is currently at capacity.");
            Console.WriteLine("\n\nPlease try again later.");
        }

        private void ShowShelterEmptyMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("We are very sorry but at this time the shetler is empty.");
            Console.WriteLine("\nIf you have pet that needs a new home, please feel free to donate them to the shelter.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nPlease press ENTER to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }
        
        private void ShowNoOrganicPetsInShelterMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("We are very sorry but at this time there are no Organic Pets in the shetler.");
            Console.ResetColor();
        }
        
        private void ShowNoRoboticPetsInShelterMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("We are very sorry but at this time there are no Robotic Pets in the shetler.");
            Console.ResetColor();
        }

        private void CheckForAdoption(Shelter someShelter, Pet somePet)
        {
            Console.WriteLine($"Thanks for your interaction with {somePet.GetName()}.");
            Console.WriteLine($"\nWould you like to Adopt {somePet.GetName()}?");
            Menu menu = new Menu();
            string playerChoice = menu.GetYesNoResponse();
            if (playerChoice == "y")
            { AdoptPet(someShelter, somePet); }
            else
            { Console.Clear(); }
        }

        private void PetOneOnOne(Shelter someShelter)
        {
            Pet selectedPet = SelectPet(someShelter);
            if (selectedPet != null)
            {
                Console.Clear();
                InteractWithSelectedPet(selectedPet);
                Console.Clear();
                CheckForAdoption(someShelter, selectedPet);
            }
        }

        private void CreatePetLists(Shelter someShelter, List<OrganicPet> organicPets, List<RoboticPet> roboticPets)
        {
            foreach (Pet somePet in someShelter.GetListOfPets())
            {
                if (somePet is OrganicPet)
                {
                    OrganicPet someOrganicPet = new OrganicPet();
                    someOrganicPet = (OrganicPet)somePet;
                    organicPets.Add(someOrganicPet);
                }
                else if (somePet is RoboticPet)
                {
                    RoboticPet someRoboticPet = new RoboticPet();
                    someRoboticPet = (RoboticPet)somePet;
                    roboticPets.Add(someRoboticPet);
                }
            }
        }

        private void ShowOrganicPets(List<OrganicPet> organicPets)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nORGANIC PETS");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Pet Name   Type      Boredom | Energy | Health | Hunger |  Hydration | Irritable");
            Console.ResetColor();

            if (organicPets.Count >= 1)
            {
                foreach (OrganicPet someOrganicPet in organicPets)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{someOrganicPet.GetName().PadRight(11, ' ')}");
                    Console.Write($"{someOrganicPet.GetSpecies().PadRight(12, ' ')}");
                    Console.ResetColor();
                    Console.Write($"{someOrganicPet.GetBoredom().ToString().PadRight(10, ' ')} ");
                    Console.Write($"{someOrganicPet.GetEnergy().ToString().PadRight(9, ' ')}");
                    Console.Write($"{someOrganicPet.GetHealth().ToString().PadRight(8, ' ')} ");
                    Console.Write($"{someOrganicPet.GetHunger().ToString().PadRight(8, ' ')} ");
                    Console.Write($"{someOrganicPet.GetHyrdation().ToString().PadRight(11, ' ')} ");
                    Console.WriteLine($"{someOrganicPet.GetIrritable().ToString()} ");
                }
            }
            else
            {
                ShowNoOrganicPetsInShelterMessage();
            }
        }

        private void ShowRoboticPets(List<RoboticPet> roboticPets)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nROBOTIC PETS");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Pet Name   Type      | Boredom | Oil | Performance");
            Console.ResetColor();
            Console.WriteLine();

            if (roboticPets.Count >= 1)
            {
                foreach (RoboticPet someRoboticPet in roboticPets)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{someRoboticPet.GetName().PadRight(11, ' ')}");
                    Console.Write($"{someRoboticPet.GetSpecies().PadRight(12, ' ')}");
                    Console.ResetColor();
                    Console.Write($"{someRoboticPet.GetBoredom().ToString().PadRight(10, ' ')} ");
                    Console.Write($"{someRoboticPet.GetOil().ToString().PadRight(8, ' ')} ");
                    Console.WriteLine($"{someRoboticPet.GetPerformance().ToString()}");
                }
            }
            else
            {
                ShowNoRoboticPetsInShelterMessage();
            }

        }

        private void ShowAllPets(Shelter someShelter)
        {
            List<OrganicPet> organicPets = new List<OrganicPet>();
            List<RoboticPet> roboticPets = new List<RoboticPet>();
            CreatePetLists(someShelter, organicPets, roboticPets);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Here are all the pets currently in the shelter:");
            ShowOrganicPets(organicPets);
            ShowRoboticPets(roboticPets);

            Console.WriteLine();
            Console.WriteLine("Press ENTER to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        private string FeedAllOrganicPets(Shelter someShelter)
        {
            Console.Clear();
            foreach (Pet somePet in someShelter.GetListOfPets())
            {
                if (somePet is OrganicPet)
                {
                    OrganicPet pet;
                    pet = (OrganicPet)somePet;
                    pet.FeedPet();
                    ProcessTime(somePet);
                }
            }
            return "Thanks for feeding the Organic Pets";
        }

        private string WaterAllOrganicPets(Shelter someShelter)
        {
            Console.Clear();
            foreach (Pet somePet in someShelter.GetListOfPets())
            {
                if (somePet is OrganicPet)
                {
                    OrganicPet pet;
                    pet = (OrganicPet)somePet;
                    pet.Drink();
                    ProcessTime(somePet);
                }
            }
            return "Thanks for giving the Organic Pets some water to drink.";
        }

        private string OilAllRoboticPets(Shelter someShelter)
        {
            Console.Clear();
            foreach (Pet somePet in someShelter.GetListOfPets())
            {
                if (somePet is RoboticPet)
                {
                    RoboticPet pet;
                    pet = (RoboticPet)somePet;
                    pet.AddOil();
                    ProcessTime(somePet);
                }
            }
            return "Thanks for Oiling the Robotic Pets.";
        }

        private string PlayWithPets(Shelter someShelter)
        {
            Console.Clear();
            foreach (Pet somePet in someShelter.GetListOfPets())
            {
                somePet.Play();
                ProcessTime(somePet);
            }
            return "Thanks for playing with the Pets.";
        }

        private void CreatePetListWithType(Shelter someShelter)
        {
            int index = 1;
            foreach (Pet pet in someShelter.GetListOfPets())
            {
                Console.Write($"{index}.  {pet.GetName()} is ");
                if (pet is OrganicPet)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Organic ");
                }
                else if (pet is RoboticPet)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Robotic ");
                }
                Console.ResetColor();
                Console.WriteLine($"{pet.GetSpecies()}.");
                index++;
            }
        }

        private Pet SelectPet(Shelter someShelter)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nPlease Select a Pet from the Shelter:\n");
            Console.ResetColor();
            CreatePetListWithType(someShelter);

            int petIndex;
            Pet selectedPet = null;
            while (selectedPet == null)
            {
                Menu menu = new Menu();
                string selectedMenuOption = menu.GetPlayerChoice();
                try
                {
                    petIndex = Convert.ToInt32(selectedMenuOption) - 1;
                    selectedPet = someShelter.GetPet(petIndex);
                    if (selectedPet == null)
                    { Console.WriteLine("\nInvalid Option.  Please try again."); }
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid Option.  Please try again.");
                }
            }
            return selectedPet;
        }

        private void AdoptPet(Shelter someShelter, Pet somePet)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Thanks for adopting {somePet.GetName()}.  We hope you give them a good home.");
            Console.ResetColor();
            someShelter.RemovePetFromShelter(somePet);
        }

        private void LeaveShelterMessage(Shelter someShelter)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nThanks for visiting {someShelter.GetShelterName()} Wildly Popular Pet Shelter.");
            Console.WriteLine("\n\nPlease come back again soon.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\nI NEED THE MONEY FOR MY DEFENSE FUND!!!");
            Console.ResetColor();
        }

        private string AskPlayerForPetName()
        {
            string name = "";
            while (name == "")
            {
                Console.WriteLine("\nWhat is the name of the pet?");
                name = Console.ReadLine();
            }
            return name;
        }

        private string AskPlayerForPetSpecies()
        {
            string species = "";
            while (species == "")
            {
                Console.WriteLine("\nWhat species of Pet would you like to add to the Shelter?");
                species = Console.ReadLine();
            }
            return species;
        }

        private void  AddPetToShelter(Shelter someShelter, Type petType)
        {
            string playerPetSpeciesEntry = AskPlayerForPetSpecies();
            string playerPetNameEntry = AskPlayerForPetName();

            if (petType == typeof(OrganicPet))
            { 
                OrganicPet somePet = new OrganicPet(playerPetNameEntry, playerPetSpeciesEntry);
                someShelter.AddPetToShelter(somePet);
            }
            else if (petType == typeof(RoboticPet))
            {
                RoboticPet somePet = new RoboticPet(playerPetNameEntry, playerPetSpeciesEntry);
                someShelter.AddPetToShelter(somePet);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{playerPetNameEntry} has been added to Shelter.\n");
            Console.ResetColor();
        }

        private void RoboticPetsAddLoop(Shelter someShelter)
        {
            bool keepAdding = true;
            do
            {
                AddPetToShelter(someShelter, typeof(RoboticPet));
                Console.WriteLine("\nWould you like to add another Robotic Pet?");
                Menu menu = new Menu();
                string playerChoice = menu.GetYesNoResponse();
                if (someShelter.IsShelterFull())
                {
                    ShowShelterFullMessage();
                    keepAdding = false;
                }
                else if (playerChoice == "n")
                { keepAdding = false; }
            } while (keepAdding);
        }

        private void AdmitPet(Shelter someShelter)
        {
            if (someShelter.IsShelterFull())
            {
                ShowShelterFullMessage();
            }
            else
            {
                string playerPetTypeEntry;
                Menu menu = new Menu();
                Console.Clear();
                do
                {
                    menu.ShowPetTypeMenu();
                    playerPetTypeEntry = menu.GetPlayerChoice();
                    Console.WriteLine();
                    switch (playerPetTypeEntry)
                    {
                        case "1":
                            AddPetToShelter(someShelter,typeof(OrganicPet));
                            break;
                        case "2":
                            RoboticPetsAddLoop(someShelter);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Invalid option.  Please try again.");
                            playerPetTypeEntry = "";
                            Console.ResetColor();
                            break;
                    }
                }
                while (playerPetTypeEntry == "");
            }
        }

        private void InteractWithSelectedPet(Pet somePet)
        {
            bool keepPlaying = true;
            while (keepPlaying)
            {
                somePet.ShowPetStatus();
                string gameFeedbackToPlayer="";
                string playerChoice;
                Menu menu = new Menu();
                if (somePet is OrganicPet)
                {
                    menu.ShowOrganicPetMenu(somePet.GetName());
                    playerChoice = menu.GetPlayerChoice();
                    gameFeedbackToPlayer = ProcessPlayerChoiceOrganic(playerChoice, (OrganicPet)somePet);
                }
                else if (somePet is RoboticPet)
                {
                    menu.ShowRoboticPetMenu(somePet.GetName());
                    playerChoice = menu.GetPlayerChoice();
                    gameFeedbackToPlayer = ProcessPlayerChoiceRobotic(playerChoice, (RoboticPet)somePet);
                }

                if (gameFeedbackToPlayer == "stop")
                { keepPlaying = false; }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine(gameFeedbackToPlayer);

                    string petFeedbacktoPlayer = ProcessTime(somePet);
                    Console.WriteLine(petFeedbacktoPlayer);
                }
            }
        }

        private string ProcessPlayerChoiceOrganic(string playerChoice, OrganicPet somePet)
        {
            string message;
            switch (playerChoice)
            {
                case "1": 
                    message = PlayWithPet(somePet);
                    return message;
                case "2": 
                    message = somePet.FeedPet();
                    return message;
                case "3":
                    message = somePet.GivePetWater();
                    return message;
                case "4":
                    message = somePet.TakePetToVet();
                    return message;
                case "5":
                    message = somePet.LetPetOutside();
                    return message;
                case "6":
                    message = somePet.LetPetSleep();
                    return message;
                case "7": 
                    message = LeavePetAlone(somePet);
                    return message;
                case "9": 
                    message = "stop";
                    return message;
                default:
                    message = "Invalid Choice.  Please try again.";
                    return message;
            }
        }

        private string ProcessPlayerChoiceRobotic(string playerChoice, RoboticPet somePet)
        {
            string message;
            switch (playerChoice)
            {
                case "1": 
                    message = PlayWithPet(somePet);
                    return message;
                case "2":
                    message = somePet.AddOil();
                    return message;
                case "3":
                    message = somePet.TakePetToMechanic();
                    return message;
                case "4":
                    message = LeavePetAlone(somePet);
                    return message;
                case "9":
                    message = "stop";
                    return message;
                default:
                    message = "Invalid Choice.  Please try again.";
                    return message;
            }

        }

        private string PlayWithPet(Pet somePet)
        {
            string message;
            Random rand = new Random();
            int petPlayFactor = rand.Next(1, 6);
            if (petPlayFactor == 4)
            {
                message = $"{somePet.GetName()} doesn't want to play right now.";
            }
            else
            {
                somePet.Play();
                message = $"You played with {somePet.GetName()}.";
            }
            return message;
        }

        private string LeavePetAlone(Pet somePet)
        {
            string message;
            somePet.Ignore();
            message = $"{somePet.GetName()} is doing their own thing.";

            if (somePet is OrganicPet)
            {
                OrganicPet pet = (OrganicPet)somePet;
                Random rand = new Random();
                int petFreedomFactor = rand.Next(1, 4);

                switch (petFreedomFactor)
                {
                    case 1:
                        pet.Sleep();
                        break;
                    case 2:
                        pet.Feed();
                        break;
                    case 3:
                        pet.Drink();
                        break;
                }
                pet.LivingPetProcess();
            }
            return message;
        }

        private string ProcessTime(Pet somePet)
        {
            somePet.Tick();
            return somePet.CheckPetLevels();
        }

    }
}
