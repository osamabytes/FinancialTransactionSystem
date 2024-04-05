using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTrackingSystemMain.Model.Tenant
{
    public class TenantSetting
    {
        public int TenantId { get; set; }
        public VelocityLimit? VelocityLimits { get; set; }
        public Threshold? Thresholds { get; set; }
        public Sanction? CountrySanctions { get; set; }
    }
}
