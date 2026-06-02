namespace HorsePedigree_2026.Constants;

public static class ResultadosCampeonato
{
    public const string Victoria = "Victoria";
    public const string Derrota = "Derrota";

    private static readonly HashSet<string> ValoresValidos =
        new(StringComparer.OrdinalIgnoreCase) { Victoria, Derrota };

    public static bool EsValido(string? valor) =>
        !string.IsNullOrWhiteSpace(valor) && ValoresValidos.Contains(valor);

    public static string Normalizar(string valor) =>
        string.Equals(valor, Derrota, StringComparison.OrdinalIgnoreCase) ? Derrota : Victoria;

    public static bool EsVictoria(string? valor) =>
        string.Equals(valor, Victoria, StringComparison.OrdinalIgnoreCase);

    public static bool EsDerrota(string? valor) =>
        string.Equals(valor, Derrota, StringComparison.OrdinalIgnoreCase);
}
