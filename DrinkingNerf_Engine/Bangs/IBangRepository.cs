using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkingNerf_Engine.Bangs
{
    public interface IBangRepository
    {
        void Add(BangOutcome bangOutcome);
        void Delete(BangOutcome bang);
        IEnumerable<BangOutcome> GetBangs();
    }
}
