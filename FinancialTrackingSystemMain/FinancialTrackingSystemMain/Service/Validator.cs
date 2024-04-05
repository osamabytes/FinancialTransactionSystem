using FinancialTrackingSystemMain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTrackingSystemMain.Service
{
    public class Validator
    {
        public bool Validate(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException();
            return false;
        }
    }
}
