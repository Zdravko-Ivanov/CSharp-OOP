using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    internal class PilotRepository : IRepository<IPilot>
    {
        private List<IPilot> _models = new List<IPilot>();
        public IReadOnlyCollection<IPilot> Models
        {
            get
            {
                return _models;
            }

        }

        public void Add(IPilot model)
        {
            this._models.Add(model);
        }

        public IPilot FindByName(string name)
        {
            return this._models.FirstOrDefault(n => n.FullName == name);
        }

        public bool Remove(IPilot model)
        {
            return this._models.Remove(model);
        }
    }
}
