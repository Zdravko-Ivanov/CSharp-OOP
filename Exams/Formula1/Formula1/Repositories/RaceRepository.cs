using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    internal class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> _models = new List<IRace>();
        public IReadOnlyCollection<IRace> Models
        {
            get
            {
                return _models;
            }
        }

        public void Add(IRace model)
        {
            this._models.Add(model);
        }

        public IRace FindByName(string name)
        {
            return this._models.FirstOrDefault(n => n.RaceName == name);
        }

        public bool Remove(IRace model)
        {
            return this._models.Remove(model);
        }
    }
}
