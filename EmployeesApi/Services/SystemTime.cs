using System;

namespace EmployeesApi.Services
{
    public class SystemTime : ISystemTime
    {
        DateTime Created;

        public SystemTime()
        {
            Created = DateTime.Now;
        }

        public DateTime GetCreated()
        {
            return Created;
        }

        public DateTime GetCurrent()
        {
            return DateTime.Now;
        }

        public DateTime GetDevelopmentDay()
        {
            return new DateTime(2020, 7, 6, 15, 30, 0);
        }
    }
}
