namespace HorsePedigree_2026.Exceptions;

public class NotFoundException : Exception
{
    public string EntityName { get; }
    public long EntityId { get; }

    public NotFoundException(string entityName, long entityId)
        : base($"No se encontró {entityName} con id {entityId}.")
    {
        EntityName = entityName;
        EntityId = entityId;
    }
}
