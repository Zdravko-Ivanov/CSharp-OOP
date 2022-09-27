using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Formula1.Models
{
    internal class Race : IRace
    {
        private string _raceName;
        private int _numberOfLaps;
        private bool _tookPlace;
        private readonly List<IPilot> _pilots = new List<IPilot>();
        public string RaceName
        {
            get
            {
                return _raceName;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.InvalidRaceName, value);
                _raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get
            {
                return _numberOfLaps;
            }
            private set
            {
                if (value < 1)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                _numberOfLaps = value;
            }
        }

        public bool TookPlace
        {
            get
            {
                return _tookPlace;
            }
            set
            {
                _tookPlace = value;

            }
        }
        public ICollection<IPilot> Pilots
        {
            get
            {
                return _pilots;
            }
        }

        private Race()
        {
            this.TookPlace = false;
        }
        public Race(string raceName, int numberOfLaps) :
            this()
        {
            this.RaceName = raceName;
            this.NumberOfLaps = numberOfLaps;
        }

        public void AddPilot(IPilot pilot)
        {
            this.Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            string trueOrFalse;
            if (this.TookPlace)
                trueOrFalse = "Yes";
            else
                trueOrFalse = "No";
            
            return $"The {this.RaceName} race has:" + Environment.NewLine +
                   $"Participants: {this.Pilots.Count}" + Environment.NewLine +
                   $"Number of laps: {this.NumberOfLaps}" + Environment.NewLine +
                   $"Took place: {trueOrFalse}";
        }
    }
}
