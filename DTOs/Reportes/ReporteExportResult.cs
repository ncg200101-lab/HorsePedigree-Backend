namespace HorsePedigree_2026.DTOs.Reportes;

public class ReporteExportResult
{
    public byte[] Content { get; set; } = [];
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}
