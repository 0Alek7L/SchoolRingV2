using Newtonsoft.Json;
using SchoolRing.Interfaces;
using System;

namespace SchoolRing
{
    [Serializable]
    internal class VacationalDays : IVacationalDays
    {
        [JsonProperty("startDate")]
        private DateTime startDate;
        [JsonProperty("endDate")]
        private DateTime endDate;
        [JsonProperty("argument")]
        private string argument;

        public VacationalDays(string argument, DateTime startDate, DateTime endDate)
        {
            Argument = argument;
            EndDate = endDate;
            StartDate = startDate;
        }

        public string Argument
        {
            get { return argument; }
            set { argument = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

    }
}
