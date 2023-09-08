using HTS.Data.Entity;
using HTS.Dto.Proforma;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTS.PDFDocument
{
    public class InvoiceDocument : IDocument
    {
        private Payment _payment;
        string[] onesDigits = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
        string[] tensDigits = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" };
        string[] groups = { "", "Bin", "Milyon", "Milyar", "Trilyon", "Katrilyon" };

        public InvoiceDocument(Payment payment)
        {
            _payment = payment;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
             .Page(page =>
             {
                 page.Margin(50);

                 page.Header()
                 .PaddingBottom(10)
                 .Element(ComposeHeader);

                 page.Content().Element(ComposeContent);
             });
        }

        private void ComposeContent(IContainer container)
        {
            var textStyle = TextStyle.Default.FontFamily("Arial").FontSize(8);
            var boldTextStyle = TextStyle.Default.FontFamily("Arial").FontSize(8).Bold();


            container.PaddingTop(10).Column(column =>
            {
                column.Item().Text(text =>
                {
                    text.Span("Ödeme Yapan: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_payment.PayerNameSurname).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Ödeme Nedeni: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_payment.PaymentReason.Name).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Hasta Adı Soyadı: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_payment.PatientNameSurname).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Dosya Numarası: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_payment.FileNumber).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("İşlem Numarası: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_payment.ProcessingNumber).Style(textStyle).FontSize(8);
                });

                decimal totalPrice = 0;
                column.Spacing(10);
                column.Item().Table(table =>
                {
                    IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                    {
                        return container
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(backgroundColor)
                            .PaddingVertical(5)
                            .PaddingHorizontal(10)
                            .AlignCenter()
                            .AlignMiddle();
                    }

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();

                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Tahsilat Türü").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("POS Onay Kodu").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("Banka").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("Sorgu No").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("Para Birimi").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("Tutar").Style(textStyle);
                        header.Cell().Element(CellStyle).Text("Tutar TRY").Style(textStyle);
                    });


                    foreach (var item in _payment.PaymentItems)
                    {
                        var priceInTry = item.Price * item.ExchangeRate;
                        table.Cell().Element(CellStyle).Text(item.PaymentKind.Name).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(item.POSApproveCode).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(item.Bank).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(item.QueryNumber).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(item.Currency.Name).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(item.Price.ToString()).Style(textStyle);
                        table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(priceInTry.ToString("C", CultureInfo.CreateSpecificCulture("tr-TR"))).Style(textStyle);
                        totalPrice += priceInTry;
                    }

                    table.Cell().ColumnSpan(6).Element(CellStyle).ExtendHorizontal().AlignRight().Text("Genel Toplam").Style(boldTextStyle);
                    table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(totalPrice.ToString("C", CultureInfo.CreateSpecificCulture("tr-TR"))).Style(boldTextStyle);

                });

                column.Item().Text("Yalnız, " + this.ConvertDecimalToPieces(totalPrice) + " (Yazı ile) TL tahsil edilmiştir.").Style(textStyle);

                column.Item().Row(row =>
                {
                    row.RelativeColumn().Column(column =>
                    {
                        column.Item().Text("TESLİM EDEN;").Style(boldTextStyle);
                        column.Item().Text("Adı Soyadı: " + _payment.PayerNameSurname).Style(textStyle);
                        column.Item().Text("İmza:").Style(textStyle);
                    });

                    row.RelativeColumn().Column(column =>
                    {
                        column.Item().Text("TAHSİL EDEN;").Style(boldTextStyle);
                        column.Item().Text("Adı Soyadı: " + _payment.CollectorNameSurname).Style(textStyle);
                        column.Item().Text("İmza:").Style(textStyle);
                    });
                });

            });
        }

        private string ConvertDecimalToPieces(decimal number)
        {
            long lira = (long)Math.Floor(number);
            int kurus = (int)Math.Floor((number - lira) * 100);

            string liraAsText = ConvertNumberToText(lira);
            string kurusAsText = ConvertKurus(kurus);

            return liraAsText + " TL " + kurusAsText;
        }

        private string ConvertNumberToText(long totalPrice)
        {
            if (totalPrice == 0)
                return "Sıfır";

            string converted = "";

            int groupIndex = 0;
            while (totalPrice > 0)
            {
                int grupDegeri = (int)(totalPrice % 1000);
                if (grupDegeri > 0)
                {
                    string grupDegeriYaziyla = ConvertGroupValue(grupDegeri, groupIndex == 1);
                    converted = grupDegeriYaziyla + " " + groups[groupIndex] + " " + converted;
                }
                totalPrice /= 1000;
                groupIndex++;
            }

            return converted.Trim();

        }

        private string ConvertGroupValue(int number, bool thousandsGroup)
        {
            int hundredsDigit = number / 100;
            int tensDigit = (number % 100) / 10;
            int onesDigit = number % 10;

            string numberAsText = "";

            if (hundredsDigit > 0)
            {
                if (hundredsDigit == 1)
                    numberAsText += "Yüz ";
                else
                    numberAsText += onesDigits[hundredsDigit] + " Yüz ";
            }

            if (tensDigit > 0)
                numberAsText += tensDigits[tensDigit] + " ";

            if (onesDigit > 0)
            {
                if (thousandsGroup && onesDigit == 1)
                    numberAsText += "";
                else
                    numberAsText += onesDigits[onesDigit] + " ";
            }

            return numberAsText;
        }

        private string ConvertKurus(int kurus)
        {
            string kurusAsText = ConvertNumberToText(kurus);

            if (kurus == 0)
                return "";

            if (kurusAsText == "Bir")
                kurusAsText = "Bir Kuruş";
            else
                kurusAsText += " Kuruş";

            return kurusAsText;
        }

        private void ComposeHeader(IContainer container)
        {
            var textStyle = TextStyle.Default.FontFamily("Arial").FontSize(8);
            var boldTextStyle = TextStyle.Default.FontFamily("Arial").FontSize(8).Bold();
            var today = DateTime.Now;

            container.Column(column =>
            {
                column.Item().Row(row =>
                {
                    byte[] imageData = File.ReadAllBytes("./wwwroot/images/logo/logo-sb2.png");
                    row.ConstantItem(150).Height(40).AlignMiddle().Image(imageData);

                    row.RelativeItem().PaddingTop(8).AlignRight().Column(column =>
                    {
                        column.Item().Text("TAHSİLAT MAKBUZU").Style(textStyle).FontSize(10).Bold();
                        column.Item().Text(text =>
                        {
                            text.Span("Tarih: ").Style(textStyle).Bold();
                            text.Span(today.ToString("dd.MM.yyyy HH:mm")).Style(textStyle);
                        });
                        column.Item().Text(text =>
                        {
                            text.Span("Sıra No: ").Style(textStyle).Bold();
                            text.Span(_payment.GeneratedRowNumber).Style(textStyle);
                        });
                    });
                });

                column.Item().PaddingTop(10).Text("USHAŞ ULUSLARARASI SAĞLIK HİZMETLERİ A.Ş.").Style(boldTextStyle);
                column.Item().Text("Sağlık Bakanlığı Bilkent Yerleşkesi Üniversiteler Mah. Dumlupınar Bul.").Style(textStyle);
                column.Item().Text("6001. Cad. No:9 Kat:8 Çankaya 06800 ANKARA, TÜRKİYE").Style(textStyle);
                column.Item().Text(text =>
                {
                    text.Span("T:").Style(boldTextStyle);
                    text.Span("0312 585 12 20 | ").Style(textStyle);
                    text.Span("F:").Style(boldTextStyle);
                    text.Span("0312 585 25 67 | ").Style(textStyle);
                    text.Span("M:").Style(boldTextStyle);
                    text.Span("+90 546 806 02 51").Style(textStyle);
                });
                column.Item().Text("Ankara Kurumlar Vergi Dairesi: 8940697604   www.ushas.com.tr").Style(boldTextStyle);
            });
        }
    }
}
