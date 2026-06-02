namespace HorsePedigree_2026.Constants;

public static class ReporteFormatos
{
    public const string Pdf = "pdf";

    public static bool EsPdf(string? formato) =>
        string.Equals(formato, Pdf, StringComparison.OrdinalIgnoreCase);
}
