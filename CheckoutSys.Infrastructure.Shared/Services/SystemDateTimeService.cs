using CheckoutSys.Application.Interfaces.Shared;
using System;

namespace CheckoutSys.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}