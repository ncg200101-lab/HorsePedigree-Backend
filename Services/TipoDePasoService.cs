using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class TipoDePasoService : ServiceBase<TipoDePaso>, ITipoDePasoService
{
    public TipoDePasoService(ITipoDePasoRepository repository) : base(repository)
    {
    }
}
