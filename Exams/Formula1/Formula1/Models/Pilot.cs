using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    internal class Pilot : IPilot
    {
        private string _fullName;
        private int _numberOfWins;
        private bool _canRace;
        private IFormulaOneCar _car;
        public string FullName
        {
            get
            {
                return _fullName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                _fullName = value;
            }
        }


        public IFormulaOneCar Car
        {
            get
            {
                return _car;
            }
            private set
            {
                if (value == null)
                    throw new NullReferenceException(ExceptionMessages.InvalidCarForPilot);
                _car = value;
            }
        }

        public int NumberOfWins
        {
            get
            {
                return _numberOfWins;
            }
            private set
            {
                _numberOfWins = value;
            }
        }

        public bool CanRace
        {
            get
            {
                return _canRace;
            }
            private set
            {
                _canRace = value;
            }
        }

        private Pilot()
        {
            this.NumberOfWins = 0;
            this.CanRace = false;
        }
        public Pilot(string fullName)
            : base()
        {
            this.FullName = fullName;
        }

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }

        public override string ToString()
        {
            return $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
        }


    }
}
