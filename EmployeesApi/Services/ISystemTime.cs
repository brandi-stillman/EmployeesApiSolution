using System;

namespace EmployeesApi.Services
{
    public interface ISystemTime
    {
        DateTime GetCreated();
        DateTime GetCurrent();
        DateTime GetDevelopmentDay();
    }
}