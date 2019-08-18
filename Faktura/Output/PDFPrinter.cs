﻿using System;
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
                Document _doc = new Document(PageSize.A4, 25f, 25f, 30f, 30f);
                PdfWriter _writer = PdfWriter.GetInstance(_doc, fs);

                Paragraph _bossSignMark = new Paragraph("Podpis kierownika komórki organizacyjnej");
                Paragraph _coordinatorSignMark = new Paragraph("Podpis koordynatora");
                Paragraph _cellSpacer = new Paragraph("\n\n\n\n");
                Paragraph _spacer = new Paragraph("");
                _spacer.SpacingBefore = 100f;
                _spacer.SpacingAfter = 10f;
                _doc.AddAuthor("KWP Gorzów Wlkp.");
                _doc.AddCreator("Test");
                _doc.AddKeywords("PDF");
                _doc.AddSubject("WYDRUK ZESTAWIENIA FAKTURY NA DANY ROK");
                _doc.AddTitle("WYDRUK FAKTURY");

                _doc.Open();
                PdfContentByte pcb = _writer.DirectContent;
                Image _img = Image.GetInstance(LocalParameters.logoPath);
                _img.SetAbsolutePosition(35, 700);
                _img.ScaleAbsolute(50, 50);
                _img.ScalePercent(50f);
                _doc.Add(_img);
                _doc.Add(_spacer);

                PdfPTable _dataTable = new PdfPTable(2);
                _dataTable.WidthPercentage = 70;
                _dataTable.AddCell("DATA ZESTAWIENIA:");
                _dataTable.AddCell(DateTime.Now.ToShortDateString());
                _dataTable.AddCell("UTWORZYŁ PRACOWNIK:");
                _dataTable.AddCell(LocalParameters.username);
                _dataTable.AddCell("WYDZIAL:");
                _dataTable.AddCell("WYDZIAL FINANSÓW KWP");
                _dataTable.SpacingAfter = 5f;

                PdfPTable _signTable = new PdfPTable(2);
                _signTable.WidthPercentage = 100;
                _bossSignMark.Alignment = PdfPCell.ALIGN_CENTER;
                _coordinatorSignMark.Alignment = PdfPCell.ALIGN_CENTER;
                _signTable.AddCell(_bossSignMark);
                _signTable.AddCell(_coordinatorSignMark);
                _signTable.AddCell(_cellSpacer);
                _signTable.AddCell(_cellSpacer);
                PdfPTable _invoiceTable = new PdfPTable(MainParameters.dataTable.Columns.Count);
                _invoiceTable.WidthPercentage = 100;

                //Set columns names in the pdf file
                for (int i = 0; i < MainParameters.dataTable.Columns.Count; i++)
                {
                    PdfPCell _invoiceCell = new PdfPCell(new Phrase(MainParameters.dataTable.Columns[i].ColumnName));

                    _invoiceCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    _invoiceCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    _invoiceCell.BackgroundColor = new BaseColor(245, 240, 240);

                    _invoiceTable.AddCell(_invoiceCell);
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

                        _invoiceTable.AddCell(cell);
                    }
                }
                _dataTable.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                _doc.Add(_dataTable);
                _doc.Add(_invoiceTable);
                _doc.Add(Chunk.NEWLINE);
                Paragraph p = new Paragraph($"SUMA: {Convert.ToString(LocalParameters.sumInvoice)} PLN");
                p.Alignment = PdfPCell.ALIGN_RIGHT;
                _doc.Add(p);
                _doc.Add(Chunk.NEWLINE);
                _doc.Add(Chunk.NEWLINE);
                _doc.Add(Chunk.NEWLINE);
                _doc.Add(Chunk.NEWLINE);
                _doc.Add(_signTable);
                /*_coordinatorSign.Alignment = PdfPCell.ALIGN_RIGHT;
                _coordinatorSignMark.Alignment = PdfPCell.ALIGN_RIGHT;
                _bossSignMark.Alignment = PdfPCell.ALIGN_LEFT;
                _bossSign.Alignment = PdfPCell.ALIGN_LEFT;
                _doc.Add(_coordinatorSign);
                _doc.Add(_coordinatorSignMark);
                _doc.Add(_bossSign);
                _doc.Add(_bossSignMark); */
                _doc.Close();
                _writer.Close();
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
