using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

namespace Formula1.Core
{
    internal class Controller : IController
    {
        private FormulaOneCarRepository formulaOneCarRepository = new FormulaOneCarRepository();
        private PilotRepository pilotRepository = new PilotRepository();
        private RaceRepository raceRepository = new RaceRepository();
        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = pilotRepository.FindByName(pilotName);
            var car = formulaOneCarRepository.FindByName(carModel);

            if (pilot == null || pilot.CanRace)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            if (car == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));

            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);

        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var pilot = pilotRepository.FindByName(pilotFullName);
            var race = raceRepository.FindByName(raceName);

            if (pilot == null || !pilot.CanRace || race.Pilots.Contains(pilot))
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            if (race == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);

        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (formulaOneCarRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type == "Ferrari")
            {
                var car = new Ferrari(model, horsepower, engineDisplacement);

                formulaOneCarRepository.Add(car);
                return String.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }
            else if (type == "Williams")
            {
                var car = new Williams(model, horsepower, engineDisplacement);

                formulaOneCarRepository.Add(car);
                return String.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

        }

        public string CreatePilot(string fullName)
        {
            if (pilotRepository.FindByName(fullName) != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            else
            {
                pilotRepository.Add(new Pilot(fullName));
                return String.Format(OutputMessages.SuccessfullyCreatePilot, fullName);

            }
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.FindByName(raceName) != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));

            var race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return String.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            var pilots = pilotRepository.Models.OrderByDescending(p => p.NumberOfWins);

            foreach (var pilot in pilots)
            {
                sb.AppendLine(pilot.ToString());
            }
            return sb.ToString();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            var races = raceRepository.Models.Where(r => r.TookPlace == true);

            foreach (var race in races)
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString();
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.FindByName(raceName);

            if (race == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            if (race.Pilots.Count < 3)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            if (race.TookPlace)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));

            var winners = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).ToList();

            race.TookPlace = true;
            winners[0].WinRace();
            return $"Pilot {winners[0].FullName} wins the {raceName} race." + Environment.NewLine +
                   $"Pilot {winners[1].FullName} is second in the {raceName} race." + Environment.NewLine +
                   $"Pilot {winners[2].FullName} is third in the {raceName} race.";
        }
    }
}
