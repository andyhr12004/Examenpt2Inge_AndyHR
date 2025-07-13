using backend_examen.Domain;
using backend_examen.Repository;
using System.Collections.Generic;

namespace backend_examen.Application
{
    public class GetBeveragesQuery
    {
        private readonly VendingMachineService _repo;

        public GetBeveragesQuery(VendingMachineService repo)
        {
            _repo = repo;
        }

        public List<DrinksModel> Execute() =>
            _repo.GetAvailableDrinks();
    }
}
