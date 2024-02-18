using SchoolRing.Interfaces;
using SchoolRing.Repository;
using System.Collections.Generic;

namespace SchoolRing
{
    public class Controller : IController
    {
        internal readonly ISchoolClassRepository repository;
        public Controller()
        {
            repository = new Repository.Repository();
        }
        public ISchoolClass GetTheModel(string day, int num, bool isPurvaSmqna)
        {
            return repository.FirstModel(day, num, isPurvaSmqna);
        }
        public void AddNewClass(ISchoolClass model)
        {
            if (repository.IsThereAModel(model.Day, model.Num, model.IsPurvaSmqna))
            {
                repository.UpdateModel(model);
            }
            else
                repository.AddModel(model);
        }
        public IReadOnlyCollection<ISchoolClass> GetModel()
        {
            return repository.GetModels();
        }

        public void ClearTheSchedule()
        {
            repository.ClearTheSchedule();
        }


    }
}
