namespace HorsePedigree_2026.Helpers;

public static class EquinoEstadoHelper
{
    public static bool EsEstadoFallecido(string? descripcionEstado)
    {
        if (string.IsNullOrWhiteSpace(descripcionEstado))
        {
            return false;
        }

        return descripcionEstado.Contains("fallec", StringComparison.OrdinalIgnoreCase);
    }
}
