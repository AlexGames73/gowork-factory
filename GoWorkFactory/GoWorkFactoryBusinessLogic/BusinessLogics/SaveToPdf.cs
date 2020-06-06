using GoWorkFactoryBusinessLogic.HelperModels;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    //for plane parts
    public static class SaveToPdf
    {
        [System.Obsolete]
        public static Stream CreateDocProducts(ProductsPdfInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph = section.AddParagraph($"С {info.From.ToShortDateString()} до {info.To.ToShortDateString()}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "3cm", "3cm", "3cm", "2cm", "3cm", "3cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Дата поставки", "Номер заказа", "Продукт", "Кол-во", "Стоимость", "Статус" },
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var product in info.Products.OrderBy(x => x.DeliveryDate).ThenBy(x => x.OrderId))
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> {
                        product.DeliveryDate.ToShortDateString(), 
                        product.OrderId.ToString("000000"),
                        product.ProductName,
                        product.Count.ToString(),
                        product.TotalPrice.ToString(),
                        product.Status.ToString()
                    },
                    Style = "NormalTitle",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            MemoryStream memoryStream = new MemoryStream();
            renderer.PdfDocument.Save(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        [System.Obsolete]
        public static Stream CreateDoc(PdfInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Document document = new Document();
            document.DefaultPageSetup.PageHeight = new Unit(7, UnitType.Centimeter);
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "6cm", "6cm", "3cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Plane", "Part", "Count" },
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            MemoryStream memoryStream = new MemoryStream();
            renderer.PdfDocument.Save(memoryStream);
            return memoryStream;
        }


        [System.Obsolete]
        public static Stream CreateDocRequestOrder(RequestOrderPdfInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Document document = new Document();
            DefineStyles(document);
            Section section = document.AddSection();
            Paragraph paragraph = section.AddParagraph(info.Title);
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            paragraph = section.AddParagraph($"С {info.DateFrom.ToString()} по {info.DateTo.ToString()}");
            paragraph.Format.SpaceAfter = "1cm";
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Style = "NormalTitle";
            var table = document.LastSection.AddTable();
            List<string> columns = new List<string> { "5cm", "3cm", "4cm", "3cm", "3cm" };
            foreach (var elem in columns)
            {
                table.AddColumn(elem);
            }
            CreateRow(new PdfRowParameters
            {
                Table = table,
                Texts = new List<string> { "Дата", "Тип", "Материал", "Количество", "Сумма" },
                Style = "NormalTitle",
                ParagraphAlignment = ParagraphAlignment.Center
            });
            foreach (var pc in info.ReportRequestOrders)
            {
                CreateRow(new PdfRowParameters
                {
                    Table = table,
                    Texts = new List<string> { pc.Date.ToString(), pc.Type, pc.NameMaterial.ElementAtOrDefault(0), pc.Count.ElementAtOrDefault(0).ToString(), pc.Price.ElementAtOrDefault(0).ToString() },
                    Style = "Normal",
                    ParagraphAlignment = ParagraphAlignment.Center
                });
                for (int i = 1; i < pc.NameMaterial.Count; i++)
                {
                    CreateRow(new PdfRowParameters
                    {
                        Table = table,
                        Texts = new List<string> { "", "", 
                            pc.NameMaterial.ElementAtOrDefault(i), pc.Count.ElementAtOrDefault(i).ToString(), pc.Type == "Заказ" ? "" : pc.Price.ElementAtOrDefault(i).ToString() },
                        Style = "Normal",
                        ParagraphAlignment = ParagraphAlignment.Center
                    });
                }
            }
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always)
            {
                Document = document
            };
            renderer.RenderDocument();
            MemoryStream memoryStream = new MemoryStream();
            renderer.PdfDocument.Save(memoryStream);
            return memoryStream;
        }
        /// <summary>
        /// Создание стилей для документа
        /// </summary>
        /// <param name="document"></param>
        private static void DefineStyles(Document document)
        {
            Style style = document.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = 14;
            style = document.Styles.AddStyle("NormalTitle", "Normal");
            style.Font.Bold = true;
        }
        /// <summary>
        /// Создание и заполнение строки
        /// </summary>
        /// <param name="rowParameters"></param>
        private static void CreateRow(PdfRowParameters rowParameters)
        {
            Row row = rowParameters.Table.AddRow();
            for (int i = 0; i < rowParameters.Texts.Count; ++i)
            {
                FillCell(new PdfCellParameters
                {
                    Cell = row.Cells[i],
                    Text = rowParameters.Texts[i],
                    Style = rowParameters.Style,
                    BorderWidth = 0.5,
                    ParagraphAlignment = rowParameters.ParagraphAlignment
                });
            }
        }
        /// <summary>
        /// Заполнение ячейки
        /// </summary>
        /// <param name="cellParameters"></param>
        private static void FillCell(PdfCellParameters cellParameters)
        {
            cellParameters.Cell.AddParagraph(cellParameters.Text);
            if (!string.IsNullOrEmpty(cellParameters.Style))
            {
                cellParameters.Cell.Style = cellParameters.Style;
            }
            cellParameters.Cell.Borders.Left.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Right.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Top.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Borders.Bottom.Width = cellParameters.BorderWidth;
            cellParameters.Cell.Format.Alignment = cellParameters.ParagraphAlignment;
            cellParameters.Cell.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
