using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GoWorkFactoryBusinessLogic.HelperModels;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    public static class SaveToWord
    {
        public static Stream CreateDocOrdersProducts(OrdersProductsWordInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<WordText> {
                        info.Title
                    },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                var table = new Table();
                var props = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                    ));
                table.AppendChild(props);
                var tr = new TableRow();
                var tc1 = new TableCell();
                var tc2 = new TableCell();
                var tc3 = new TableCell();
                var tc4 = new TableCell();
                tc1.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<WordText> { "Заказ" },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                tc2.AppendChild(CreateParagraph(new WordParagraph()
                {
                    Texts = new List<WordText> { "Продукт" },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                tc3.AppendChild(CreateParagraph(new WordParagraph()
                {
                    Texts = new List<WordText> { "Количество" },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                tc4.AppendChild(CreateParagraph(new WordParagraph()
                {
                    Texts = new List<WordText> { "Цена" },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                tr.AppendChild(tc1);
                tr.AppendChild(tc2);
                tr.AppendChild(tc3);
                tr.AppendChild(tc4);
                table.AppendChild(tr);
                foreach (var order in info.OrdersProducts)
                {
                    tr = new TableRow();
                    tc1 = new TableCell();
                    tc2 = new TableCell();
                    tc3 = new TableCell();
                    tc4 = new TableCell();
                    tc1.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<WordText> { order.Key },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc2.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { "" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc3.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { "" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc4.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { "" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tr.AppendChild(tc1);
                    tr.AppendChild(tc2);
                    tr.AppendChild(tc3);
                    tr.AppendChild(tc4);
                    table.AppendChild(tr);
                    int total = 0;
                    foreach (var product in order)
                    {
                        tr = new TableRow();
                        tc1 = new TableCell();
                        tc2 = new TableCell();
                        tc3 = new TableCell();
                        tc4 = new TableCell();
                        tc1.AppendChild(CreateParagraph(new WordParagraph
                        {
                            Texts = new List<WordText> { "" },
                            Properties = new WordParagraphProperties
                            {
                                Bold = true,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        }));
                        tc2.AppendChild(CreateParagraph(new WordParagraph()
                        {
                            Texts = new List<WordText> { product.ProductName },
                            Properties = new WordParagraphProperties
                            {
                                Bold = true,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        }));
                        tc3.AppendChild(CreateParagraph(new WordParagraph()
                        {
                            Texts = new List<WordText> { product.Count.ToString() },
                            Properties = new WordParagraphProperties
                            {
                                Bold = true,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        }));
                        tc4.AppendChild(CreateParagraph(new WordParagraph()
                        {
                            Texts = new List<WordText> { product.Price.ToString() },
                            Properties = new WordParagraphProperties
                            {
                                Bold = true,
                                Size = "24",
                                JustificationValues = JustificationValues.Center
                            }
                        }));
                        tr.AppendChild(tc1);
                        tr.AppendChild(tc2);
                        tr.AppendChild(tc3);
                        tr.AppendChild(tc4);
                        table.AppendChild(tr);

                        total += product.Price * product.Count;
                    }
                    tr = new TableRow();
                    tc1 = new TableCell();
                    tc2 = new TableCell();
                    tc3 = new TableCell();
                    tc4 = new TableCell();
                    tc1.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<WordText> { "" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc2.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { "" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc3.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { "Итого" },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tc4.AppendChild(CreateParagraph(new WordParagraph()
                    {
                        Texts = new List<WordText> { total.ToString() },
                        Properties = new WordParagraphProperties
                        {
                            Bold = true,
                            Size = "24",
                            JustificationValues = JustificationValues.Center
                        }
                    }));
                    tr.AppendChild(tc1);
                    tr.AppendChild(tc2);
                    tr.AppendChild(tc3);
                    tr.AppendChild(tc4);
                    table.AppendChild(tr);
                }
                docBody.AppendChild(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="info"></param>
        public static Stream CreateDoc(WordInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<WordText> { 
                        info.Title
                    },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
            return memoryStream;
        }

        public static Stream CreateDocRequestComponents(RequestComponentsInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<WordText> {
                        info.Title
                    },
                    Properties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
            return memoryStream;
        }
        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.Properties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize
                    {
                        Val = string.IsNullOrEmpty(run.Properties?.Size) ? paragraph.Properties.Size : run.Properties.Size
                    });
                    if (paragraph.Properties.Bold || (run.Properties?.Bold ?? false))
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Text,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
