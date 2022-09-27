using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    internal abstract class FormulaOneCar :  IFormulaOneCar
    {
        private string _model;
        private int _horsepower;
        private double _engineDisplacement;

        public string Model
        {
            get { return _model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1CarModel, value));
                _model = value;
            }
        }


        public int Horsepower
        {
            get { return _horsepower; }
            private set
            {
                if (value < 900 && value > 1050)
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1EngineDisplacement, value));
                _horsepower = value;
            }
        }


        public double EngineDisplacement
        {
            get
            {
                return _engineDisplacement;
            }
            private set
            {
                if (value < 1.6 && value > 2)
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1EngineDisplacement, value));
                _engineDisplacement = value;
            }

        }


        public FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            this.Horsepower = horsepower;
            this.EngineDisplacement = engineDisplacement;
            this.Model = model;
        }

        public double RaceScoreCalculator(int laps)
        {
            return this.EngineDisplacement / this.Horsepower * laps;
        }
    }
}
