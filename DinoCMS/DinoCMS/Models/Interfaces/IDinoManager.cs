using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinoCMS.Models.Interfaces
{
    public interface IDinoManager
    {

        Task CreateDinosaur(Dinosaur dinosaur);

        Task UpdateDinosaur(int id, Dinosaur dinosaur);

        Task<Dinosaur> DeleteDinosaur(int id);

        Task DeleteDinosaurFR(int id);


        Task<Dinosaur> GetDinosaur(int id);

        Task<IEnumerable<Dinosaur>> GetDinosaurs();

        bool DinosaurExists(int id);
    }
}
