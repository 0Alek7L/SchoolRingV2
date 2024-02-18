using System;
using System.Collections.Generic;

namespace SchoolRing.Repository
{
    internal interface IVDRepo<T>
    {
        IReadOnlyCollection<T> GetModels();
        void AddModel(T model);
        void RemoveModel(T model);
        T GetModel(DateTime start, DateTime end);
        T GetModel(DateTime start, DateTime end, string name);
        bool IsThereAModel(DateTime start, DateTime end);
        bool IsThereAModel(DateTime start, DateTime end, DateTime oldStart, DateTime oldEnd, string name);
        bool IsTodayVacation();
        bool IsThisDayVacation(DateTime dateTime);
    }
}
