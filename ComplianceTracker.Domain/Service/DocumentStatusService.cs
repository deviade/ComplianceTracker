using ComplianceTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComplianceTracker.Domain.Service
{
    public class DocumentStatusService
    {
        public DocumentStatus CalculateStatus(DateTime expiryDate)
        {
            var today = DateTime.Today;

            if (expiryDate < today)
                return DocumentStatus.Expired;

            if (expiryDate <= today.AddDays(30))
                return DocumentStatus.ExpiringSoon;

            return DocumentStatus.Valid;
        }
    }
}
