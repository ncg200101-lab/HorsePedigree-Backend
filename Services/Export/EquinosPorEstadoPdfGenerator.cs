using HorsePedigree_2026.DTOs.Reportes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HorsePedigree_2026.Services.Export;

public interface IEquinosPorEstadoPdfGenerator
{
    byte[] Generar(IReadOnlyList<EquinosPorEstadoItemResponse> datos, DateTime generadoEn);
}

public class EquinosPorEstadoPdfGenerator : IEquinosPorEstadoPdfGenerator
{
    public byte[] Generar(IReadOnlyList<EquinosPorEstadoItemResponse> datos, DateTime generadoEn)
    {
        var totalEquinos = datos.Sum(d => d.TotalEquinos);

        return Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(text => text.FontSize(10));

                page.Header().Column(column =>
                {
                    column.Item().Text("Horse Pedigree")
                        .FontSize(11)
                        .FontColor(Colors.Grey.Darken2);

                    column.Item().PaddingTop(8).Text("Reporte: Equinos por estado")
                        .FontSize(18)
                        .SemiBold();

                    column.Item().PaddingTop(4).Text($"Generado: {generadoEn:dd/MM/yyyy HH:mm} UTC")
                        .FontSize(9)
                        .FontColor(Colors.Grey.Darken1);
                });

                page.Content().PaddingVertical(20).Column(column =>
                {
                    column.Item().Text($"Total de equinos: {totalEquinos}")
                        .SemiBold()
                        .FontSize(11);

                    column.Item().PaddingTop(16).Element(container => ComposeTabla(container, datos));
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ");
                    text.CurrentPageNumber();
                    text.Span(" de ");
                    text.TotalPages();
                });
            });
        }).GeneratePdf();
    }

    private static void ComposeTabla(IContainer container, IReadOnlyList<EquinosPorEstadoItemResponse> datos)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(80);
                columns.RelativeColumn(3);
                columns.ConstantColumn(100);
            });

            table.Header(header =>
            {
                header.Cell().Element(CeldaEncabezado).Text("Estado ID");
                header.Cell().Element(CeldaEncabezado).Text("Estado");
                header.Cell().Element(CeldaEncabezado).AlignRight().Text("Total equinos");
            });

            if (datos.Count == 0)
            {
                table.Cell().ColumnSpan(3).Element(CeldaCuerpo).Text("No hay equinos registrados.");
                return;
            }

            foreach (var item in datos)
            {
                table.Cell().Element(CeldaCuerpo).Text(item.EstadoId?.ToString() ?? "—");
                table.Cell().Element(CeldaCuerpo).Text(item.EstadoDescripcion);
                table.Cell().Element(CeldaCuerpo).AlignRight().Text(item.TotalEquinos.ToString());
            }
        });
    }

    private static IContainer CeldaEncabezado(IContainer container) =>
        container
            .Background(Colors.Grey.Lighten3)
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten1)
            .PaddingVertical(6)
            .PaddingHorizontal(8)
            .DefaultTextStyle(style => style.SemiBold());

    private static IContainer CeldaCuerpo(IContainer container) =>
        container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .PaddingVertical(6)
            .PaddingHorizontal(8);
}
