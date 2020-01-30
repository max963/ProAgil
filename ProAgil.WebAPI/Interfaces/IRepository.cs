using System.Collections.Generic;
using System.Threading.Tasks;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;

        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetAllEventosAsync(bool incluiPalestrantes = false);
        Task<Evento[]> GetEventosTemaAsync(string tema, bool incluiPalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool incluiPalestrantes = false);
        
        Task<Palestrante[]> GetAllPalestranteAsync(bool incluiEvento = false);
        Task<Palestrante[]> GetAllPalestranteByNameAsync(string name, bool incluiEvento = false);
        Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluiEvento = false);
    }
}