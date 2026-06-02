using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class TipoDePasoRepository : Repository<TipoDePaso>, ITipoDePasoRepository
{
    public TipoDePasoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
