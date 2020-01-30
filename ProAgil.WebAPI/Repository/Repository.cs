using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Interfaces;
using ProAgil.WebAPI.Models;

namespace ProAgil.WebAPI.Repository
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            this._context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluiPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

                if (incluiPalestrantes)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetEventosTemaAsync(string tema, bool incluiPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

                if (incluiPalestrantes)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento)
                    .Where(t => t.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluiPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

                if (incluiPalestrantes)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento)
                    .Where(t => t.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsync(bool incluiEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrante
                .Include(x => x.RedesSociais);
                if (incluiEvento)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Evento);
                }
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteByNameAsync(string name, bool incluiEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrante
                .Include(x => x.RedesSociais);
                if (incluiEvento)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Evento);
                }
            return await query.Where(x => x.Nome.ToLower().Contains(name.ToLower())).ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluiEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrante
                .Include(x => x.RedesSociais);
                if (incluiEvento)
                {
                    query = query.Include(p => p.PalestrantesEvento)
                    .ThenInclude(p => p.Evento);
                }
            return await query.FirstOrDefaultAsync();
        }


    }
}