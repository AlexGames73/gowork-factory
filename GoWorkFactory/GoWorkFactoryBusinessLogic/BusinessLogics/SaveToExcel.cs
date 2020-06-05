using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GoWorkFactoryBusinessLogic.HelperModels;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    //for orders
    public static class SaveToExcel
    {
        public static Stream CreateDocOrdersProducts(OrdersProductsExcelInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                // Создаем книгу (в ней хранятся листы)
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                CreateStyles(workbookpart);
                // Получаем/создаем хранилище текстов для книги
                SharedStringTablePart shareStringPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0
                ? spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                : spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
                // Создаем SharedStringTable, если его нет
                if (shareStringPart.SharedStringTable == null)
                {
                    shareStringPart.SharedStringTable = new SharedStringTable();
                }
                // Создаем лист в книгу
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                // Добавляем лист в книгу
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Лист"
                };
                sheets.Append(sheet);
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "A",
                    RowIndex = 1,
                    Text = info.Title,
                    StyleIndex = 2U
                });
                MergeCells(new ExcelMergeParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    CellFromName = "A1",
                    CellToName = "D1"
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "A",
                    RowIndex = 2,
                    Text = "Заказ",
                    StyleIndex = 2U
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "B",
                    RowIndex = 2,
                    Text = "Продукт",
                    StyleIndex = 2U
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "C",
                    RowIndex = 2,
                    Text = "Количество",
                    StyleIndex = 2U
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "D",
                    RowIndex = 2,
                    Text = "Цена",
                    StyleIndex = 2U
                });
                uint row = 3;
                foreach (var group in info.OrdersProducts)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        Worksheet = worksheetPart.Worksheet,
                        ShareStringPart = shareStringPart,
                        ColumnName = "A",
                        RowIndex = row,
                        Text = group.Key,
                        StyleIndex = 2U
                    });
                    row++;
                    int total = 0;
                    foreach (var product in group)
                    {
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            Worksheet = worksheetPart.Worksheet,
                            ShareStringPart = shareStringPart,
                            ColumnName = "B",
                            RowIndex = row,
                            Text = product.ProductName,
                            StyleIndex = 2U
                        });
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            Worksheet = worksheetPart.Worksheet,
                            ShareStringPart = shareStringPart,
                            ColumnName = "C",
                            RowIndex = row,
                            Text = product.Count.ToString(),
                            StyleIndex = 2U
                        });
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            Worksheet = worksheetPart.Worksheet,
                            ShareStringPart = shareStringPart,
                            ColumnName = "D",
                            RowIndex = row,
                            Text = product.Price.ToString(),
                            StyleIndex = 2U
                        });
                        total += product.Price * product.Count;
                        row++;
                    }
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        Worksheet = worksheetPart.Worksheet,
                        ShareStringPart = shareStringPart,
                        ColumnName = "C",
                        RowIndex = row,
                        Text = "Итого",
                        StyleIndex = 2U
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        Worksheet = worksheetPart.Worksheet,
                        ShareStringPart = shareStringPart,
                        ColumnName = "D",
                        RowIndex = row,
                        Text = total.ToString(),
                        StyleIndex = 2U
                    });
                    row++;
                }
                workbookpart.Workbook.Save();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static Stream CreateDoc(ExcelInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                // Создаем книгу (в ней хранятся листы)
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                CreateStyles(workbookpart);
                // Получаем/создаем хранилище текстов для книги
                SharedStringTablePart shareStringPart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0
                ? spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                : spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
                // Создаем SharedStringTable, если его нет
                if (shareStringPart.SharedStringTable == null)
                {
                    shareStringPart.SharedStringTable = new SharedStringTable();
                }
                // Создаем лист в книгу
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                // Добавляем лист в книгу
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Лист"
                };
                sheets.Append(sheet);
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "A",
                    RowIndex = 1,
                    Text = info.Title,
                    StyleIndex = 2U
                });
                MergeCells(new ExcelMergeParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    CellFromName = "A1",
                    CellToName = "E1"
                });
                workbookpart.Workbook.Save();
            }
            return memoryStream;
        }

        public static Stream CreateDocRequestMaterials(RequestMaterialsInfo info)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            MemoryStream memoryStream = new MemoryStream();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                // Создаем книгу (в ней хранятся листы)
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                CreateStyles(workbookpart);
                // Получаем/создаем хранилище текстов для книги
                SharedStringTablePart shareStringPart =
               spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0
                ?
               spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
                :
               spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
                // Создаем SharedStringTable, если его нет
                if (shareStringPart.SharedStringTable == null)
                {
                    shareStringPart.SharedStringTable = new SharedStringTable();
                }
                // Создаем лист в книгу
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                // Добавляем лист в книгу
                Sheets sheets =
               spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Лист"
                };
                sheets.Append(sheet);
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "A",
                    RowIndex = 1,
                    Text = info.Title,
                    StyleIndex = 2U
                });
                MergeCells(new ExcelMergeParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    CellFromName = "A1",
                    CellToName = "C1"
                });
                uint rowIndex = 2;

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Название",
                    StyleIndex = 0U
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    Worksheet = worksheetPart.Worksheet,
                    ShareStringPart = shareStringPart,
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Количество",
                    StyleIndex = 0U
                });
                rowIndex++;

                foreach (var material in info.Materials)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        Worksheet = worksheetPart.Worksheet,
                        ShareStringPart = shareStringPart,
                        ColumnName = "A",
                        RowIndex = rowIndex,
                        Text = material.Item1,
                        StyleIndex = 0U
                    });
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        Worksheet = worksheetPart.Worksheet,
                        ShareStringPart = shareStringPart,
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = material.Item2.ToString(),
                        StyleIndex = 0U
                    });
                    rowIndex++;
                }
                workbookpart.Workbook.Save();
            }
            return memoryStream;
        }
        /// <summary>
        /// Настройка стилей для файла
        /// </summary>
        /// <param name="workbookpart"></param>
        private static void CreateStyles(WorkbookPart workbookpart)
        {
            WorkbookStylesPart sp = workbookpart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();
            Fonts fonts = new Fonts() { Count = 2U, KnownFonts = true };
            Font fontUsual = new Font();
            fontUsual.Append(new FontSize() { Val = 12D });
            fontUsual.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme = 1U
            });
            fontUsual.Append(new FontName() { Val = "Times New Roman" });
            fontUsual.Append(new FontFamilyNumbering() { Val = 2 });
            fontUsual.Append(new FontScheme() { Val = FontSchemeValues.Minor });
            Font fontTitle = new Font();
            fontTitle.Append(new Bold());
            fontTitle.Append(new FontSize() { Val = 14D });
            fontTitle.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme = 1U
            });
            fontTitle.Append(new FontName() { Val = "Times New Roman" });
            fontTitle.Append(new FontFamilyNumbering() { Val = 2 });
            fontTitle.Append(new FontScheme() { Val = FontSchemeValues.Minor });
            fonts.Append(fontUsual);
            fonts.Append(fontTitle);
            Fills fills = new Fills() { Count = 2U };
            Fill fill1 = new Fill();
            fill1.Append(new PatternFill() { PatternType = PatternValues.None });
            Fill fill2 = new Fill();
            fill2.Append(new PatternFill() { PatternType = PatternValues.Gray125 });
            fills.Append(fill1);
            fills.Append(fill2);
            Borders borders = new Borders() { Count = 2U };
            Border borderNoBorder = new Border();
            borderNoBorder.Append(new LeftBorder());
            borderNoBorder.Append(new RightBorder());
            borderNoBorder.Append(new TopBorder());
            borderNoBorder.Append(new BottomBorder());
            borderNoBorder.Append(new DiagonalBorder());
            Border borderThin = new Border();
            LeftBorder leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            leftBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            RightBorder rightBorder = new RightBorder()
            {
                Style = BorderStyleValues.Thin
            };
            rightBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            TopBorder topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            topBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            BottomBorder bottomBorder = new BottomBorder()
            {
                Style = BorderStyleValues.Thin
            };
            bottomBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });
            borderThin.Append(leftBorder);
            borderThin.Append(rightBorder);
            borderThin.Append(topBorder);
            borderThin.Append(bottomBorder);
            borderThin.Append(new DiagonalBorder());
            borders.Append(borderNoBorder);
            borders.Append(borderThin);
            CellStyleFormats cellStyleFormats = new CellStyleFormats()
            {
                Count = 1U
            };
            CellFormat cellFormatStyle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U
            };
            cellStyleFormats.Append(cellFormatStyle);
            CellFormats cellFormats = new CellFormats() { Count = 3U };
            CellFormat cellFormatFont = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };
            CellFormat cellFormatFontAndBorder = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 1U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };
            CellFormat cellFormatTitle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                Alignment = new Alignment()
                {
                    Vertical = VerticalAlignmentValues.Center,
                    WrapText = true,
                    Horizontal = HorizontalAlignmentValues.Center
                },
                ApplyFont = true
            };
            cellFormats.Append(cellFormatFont);
            cellFormats.Append(cellFormatFontAndBorder);
            cellFormats.Append(cellFormatTitle);
            CellStyles cellStyles = new CellStyles() { Count = 1U };
            cellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0U,
                BuiltinId = 0U
            });
            DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats differentialFormats = new DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats()
            {
                Count = 0U
            };

            TableStyles tableStyles = new TableStyles()
            {
                Count = 0U,
                DefaultTableStyle = "TableStyleMedium2",
                DefaultPivotStyle = "PivotStyleLight16"
            };
            StylesheetExtensionList stylesheetExtensionList = new StylesheetExtensionList();
            StylesheetExtension stylesheetExtension1 = new StylesheetExtension()
            {
                Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}"
            };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            stylesheetExtension1.Append(new SlicerStyles()
            {
                DefaultSlicerStyle = "SlicerStyleLight1"
            });
            StylesheetExtension stylesheetExtension2 = new StylesheetExtension()
            {
                Uri = "{9260A510-F301-46a8-8635-F512D64BE5F5}"
            };
            stylesheetExtension2.AddNamespaceDeclaration("x15", "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheetExtension2.Append(new TimelineStyles()
            {
                DefaultTimelineStyle = "TimeSlicerStyleLight1"
            });
            stylesheetExtensionList.Append(stylesheetExtension1);
            stylesheetExtensionList.Append(stylesheetExtension2);
            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cellStyleFormats);
            sp.Stylesheet.Append(cellFormats);
            sp.Stylesheet.Append(cellStyles);
            sp.Stylesheet.Append(differentialFormats);
            sp.Stylesheet.Append(tableStyles);
            sp.Stylesheet.Append(stylesheetExtensionList);
        }
        /// <summary>
        /// Добааляем новую ячейку в лист
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static void InsertCellInWorksheet(ExcelCellParameters cellParameters)
        {
            SheetData sheetData = cellParameters.Worksheet.GetFirstChild<SheetData>();
            // Ищем строку, либо добавляем ее
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == cellParameters.RowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == cellParameters.RowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = cellParameters.RowIndex };
                sheetData.Append(row);
            }
            // Ищем нужную ячейку
            Cell cell;
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == cellParameters.CellReference).Count() > 0)
            {
                cell = row.Elements<Cell>().Where(c => c.CellReference.Value == cellParameters.CellReference).First();
            }
            else
            {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell refCell = null;
                foreach (Cell rowCell in row.Elements<Cell>())
                {
                    if (string.Compare(rowCell.CellReference.Value, cellParameters.CellReference, true) > 0)
                    {
                        refCell = rowCell;
                        break;
                    }
                }
                Cell newCell = new Cell()
                {
                    CellReference = cellParameters.CellReference
                };
                row.InsertBefore(newCell, refCell);
                cell = newCell;
            }
            // вставляем новый текст
            cellParameters.ShareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new Text(cellParameters.Text)));
            cellParameters.ShareStringPart.SharedStringTable.Save();
            cell.CellValue = new CellValue((cellParameters.ShareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() - 1).ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = cellParameters.StyleIndex;
        }
        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="cell1Name"></param>
        /// <param name="cell2Name"></param>
        private static void MergeCells(ExcelMergeParameters mergeParameters)
        {
            MergeCells mergeCells;
            if (mergeParameters.Worksheet.Elements<MergeCells>().Count() > 0)
            {
                mergeCells = mergeParameters.Worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();
                if (mergeParameters.Worksheet.Elements<CustomSheetView>().Count() > 0)
                {
                    mergeParameters.Worksheet.InsertAfter(mergeCells, mergeParameters.Worksheet.Elements<CustomSheetView>().First());
                }
                else
                {
                    mergeParameters.Worksheet.InsertAfter(mergeCells, mergeParameters.Worksheet.Elements<SheetData>().First());
                }
            }
            MergeCell mergeCell = new MergeCell()
            {
                Reference = new StringValue(mergeParameters.Merge)
            };
            mergeCells.Append(mergeCell);
        }
    }
}
