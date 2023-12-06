using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using Microsoft.Office.Interop.Excel;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace UIDESK.PDFs
{
    public class RemitoObraPDF : IDocument
    {
        public Documento _documento { get; }
        public List<DocumentoDetalle> _docudetalle { get; }

        public RemitoObraPDF( Documento documento, List<DocumentoDetalle> documentoDetalle)
        {
                _documento = documento;
            _docudetalle = documentoDetalle;
        }
       

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);


                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }

        //header . Aca va la cabecera del remito 
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Numero: #{_documento.NumDocumento}").Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span("Fecha: ").SemiBold();
                    text.Span($"{_documento.FechaRemito:d}");
                });
                column.Item().Text(text =>
                {
                    text.Span("Cliente:").NormalWeight();
                    text.Span($"{_documento.ClienteObra}");
                });
                column.Item().Text(text =>
                {
                    text.Span("CUI]T:").NormalWeight();
                    text.Span($"{_documento.CuitObra}");
                });
                column.Item().Text(text =>
                {
                    text.Span("Imputacion:").NormalWeight();
                    text.Span($"{_documento.Imputacion}");
                });
                column.Item().Text(text =>
                {
                    text.Span("Obra:").NormalWeight();
                    text.Span($"{_documento.NombreObra}");
                });
                column.Item().Text(text =>
                {
                    text.Span("Direccion:").NormalWeight();
                    text.Span($"{_documento.DirObra}");
                });

            });
           

                row.ConstantItem(100).Height(50).Placeholder();
            });

        }
        //contenedor del documento , aca iria la tabla del detalle del remito 
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);
                column.Item().Element(ComposeTable);
                if (!string.IsNullOrWhiteSpace(_documento.NotaRemito))
                {
                    column.Item().PaddingTop(25).Element(ComposeComments);
                }
            });
               
        }

        void ComposeTable(IContainer container) 
        {
            container.Table(table =>
            {
                //step 1 defines number and sizes of columns 
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                //step 2 
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Product");
                    header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                    header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                    header.Cell().Element(CellStyle).AlignRight().Text("Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                //step 3

                // step 3
                foreach (var item in _docudetalle)
                {
                    table.Cell().Element(CellStyle).Text(_docudetalle.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.Descripcion);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.PrecioItem}$");
                    table.Cell().Element(CellStyle).AlignRight().Text(item.CantidadItem);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.PrecioItem * item.CantidadItem}$");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
            
        }

        void ComposeComments(IContainer container) 
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14);
                column.Item().Text(_documento.NotaRemito);
            });
        }

    }
}
