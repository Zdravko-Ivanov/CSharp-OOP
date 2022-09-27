using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    internal class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {

        private readonly List<IFormulaOneCar> _models = new List<IFormulaOneCar>();
        public IReadOnlyCollection<IFormulaOneCar> Models
        {
            get
            {
                return _models;
            }

        }

        public void Add(IFormulaOneCar model)
        {
            this._models.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return this._models.FirstOrDefault(n => n.Model == name);
        }

        public bool Remove(IFormulaOneCar model)
        {
            return this._models.Remove(model);
        }
    }
}
