using System;

namespace SchoolRing.Interfaces
{
    public interface IVacationalDays
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Argument { get; set; }
    }
}
