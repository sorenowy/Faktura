using System;
using System.IO;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Faktura.Configuration;
using System.Windows;
using Faktura.Logs;

namespace Faktura.Output
{
    public class PDFPrinter
    {
        public void CreatePDF()
        {
            if (!Directory.Exists(LocalParameters.printPDFPath))
            {
                Directory.CreateDirectory(LocalParameters.printPDFPath);
            }
            if (!File.Exists(LocalParameters.pdfFile))
            {
                File.Create(LocalParameters.pdfFile).Close();
            }
            try
            {
                FileStream fs = new FileStream(LocalParameters.pdfFile, FileMode.Create);
                Document doc = new Document(PageSize.A4, 25f, 25f, 30f, 30f);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                Paragraph sum = new Paragraph("SUMA:");
                Paragraph sumValue = new Paragraph(Convert.ToString(LocalParameters.sumInvoice));
                Paragraph bossSignMark = new Paragraph("Podpis kierownika komórki organizacyjnej");
                Paragraph coordinatorSignMark = new Paragraph("Podpis koordynatora");
                Paragraph cellSpacer = new Paragraph("\n\n\n\n");
                Paragraph spacer = new Paragraph("");
                spacer.SpacingBefore = 100f;
                spacer.SpacingAfter = 10f;
                doc.AddAuthor("KWP Gorzów Wlkp.");
                doc.AddCreator("Test");
                doc.AddKeywords("PDF");
                doc.AddSubject("WYDRUK ZESTAWIENIA FAKTURY NA DANY ROK");
                doc.AddTitle("WYDRUK FAKTURY");

                doc.Open();
                PdfContentByte pcb = writer.DirectContent;
                Image _img = Image.GetInstance(LocalParameters.logoPath);
                _img.SetAbsolutePosition(35, 700);
                _img.ScaleAbsolute(50, 50);
                _img.ScalePercent(50f);
                doc.Add(_img);
                doc.Add(spacer);

                PdfPTable dataTable = new PdfPTable(2);
                dataTable.WidthPercentage = 70;
                dataTable.AddCell("DATA ZESTAWIENIA:");
                dataTable.AddCell(DateTime.Now.ToShortDateString());
                dataTable.AddCell("UTWORZYL PRACOWNIK:");
                dataTable.AddCell(LocalParameters.username);
                dataTable.AddCell("WYDZIAL:");
                dataTable.AddCell("WYDZIAL FINANSÓW KWP");
                dataTable.SpacingAfter = 5f;

                PdfPTable signTable = new PdfPTable(2);
                signTable.WidthPercentage = 100;
                bossSignMark.Alignment = PdfPCell.ALIGN_CENTER;
                coordinatorSignMark.Alignment = PdfPCell.ALIGN_CENTER;
                signTable.AddCell(bossSignMark);
                signTable.AddCell(coordinatorSignMark);
                signTable.AddCell(cellSpacer);
                signTable.AddCell(cellSpacer);

                PdfPTable sumTable = new PdfPTable(2);
                sumTable.WidthPercentage = 30;
                sumTable.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                sumTable.AddCell(sum);
                sumTable.AddCell(sumValue);
                PdfPTable invoiceTable = new PdfPTable(MainParameters.dataTable.Columns.Count);
                invoiceTable.WidthPercentage = 100;

                //Set columns names in the pdf file
                for (int i = 0; i < MainParameters.dataTable.Columns.Count; i++)
                {
                    PdfPCell invoiceCell = new PdfPCell(new Phrase(MainParameters.dataTable.Columns[i].ColumnName));

                    invoiceCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    invoiceCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    invoiceCell.BackgroundColor = new BaseColor(245, 240, 240);

                    invoiceTable.AddCell(invoiceCell);
                }

                //Add values of DataTable in pdf file
                for (int i = 0; i < MainParameters.dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < MainParameters.dataTable.Columns.Count; j++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(MainParameters.dataTable.Rows[i][j].ToString()));

                        //Align the cell in the center
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                        invoiceTable.AddCell(cell);
                    }
                }
                dataTable.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                doc.Add(dataTable);
                doc.Add(invoiceTable);
                doc.Add(Chunk.NEWLINE);
                doc.Add(sumTable);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(signTable);
                /*_coordinatorSign.Alignment = PdfPCell.ALIGN_RIGHT;
                _coordinatorSignMark.Alignment = PdfPCell.ALIGN_RIGHT;
                _bossSignMark.Alignment = PdfPCell.ALIGN_LEFT;
                _bossSign.Alignment = PdfPCell.ALIGN_LEFT;
                _doc.Add(_coordinatorSign);
                _doc.Add(_coordinatorSignMark);
                _doc.Add(_bossSign);
                _doc.Add(_bossSignMark); */
                doc.Close();
                writer.Close();
                fs.Close();
                LogWriter.LogWrite($"Wygenerowano pdf {LocalParameters.pdfFile}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udalo się wygenerować PDF."+ ex.Message);
                LogWriter.LogWrite("Bład wygenerowania PDF!");
            }
        }
        public void PrintPDF()
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = LocalParameters.pdfFile;
                p.StartInfo.Verb = "print";
                p.Start();
            }
        }
    }
}
