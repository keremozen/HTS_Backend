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
    public class ProformaDocument : IDocument
    {
        private Proforma _proforma;
        public ProformaDocument(Proforma proforma)
        {
            _proforma = proforma;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(QuestPDF.Infrastructure.IDocumentContainer container)
        {
            container
             .Page(page =>
             {

                 page.Margin(50);

                 page.Header()
                 .BorderBottom(1)
                 .BorderColor(Colors.Grey.Darken1)
                 .PaddingBottom(10)
                 .Element(ComposeHeader);
                 page.Content().Element(ComposeContent);


                 page.Footer()
                 .BorderTop(1)
                 .BorderColor(Colors.Grey.Darken1)
                 .PaddingTop(10)
                 .AlignLeft()
                 .Text(text =>
                 {
                     text.Span("Thanks for working with ").FontSize(8).FontFamily("Arial");
                     text.Span("USHAŞ!").FontFamily("Arial").FontSize(8).Bold();
                 });
             });
        }

        private void ComposeContent(IContainer container)
        {
            var textStyle = TextStyle.Default.FontFamily("Arial").FontSize(8);


            container.Padding(10).Column(column =>
            {
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
                        columns.ConstantColumn(80);
                        columns.RelativeColumn();
                        columns.ConstantColumn(100);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("QUANTITY").FontSize(8).Bold();
                        header.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text("TREATMENT").FontSize(8).Bold();
                        header.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text("COST").FontSize(8).Bold();
                    });

                    decimal totalPrice = 0;
                    foreach (var process in _proforma.ProformaProcesses)
                    {
                        table.Cell().Element(CellStyle).Text(process.TreatmentCount.ToString()).FontSize(8);
                        table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(process.Process.EnglishName).FontSize(8);
                        table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(process.ProformaFinalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))).FontSize(8);
                        totalPrice += process.ProformaFinalPrice;
                    }

                    if (_proforma.TPDescription != null)
                    {
                        table.Cell().ColumnSpan(3).Element(CellStyle).ExtendHorizontal().AlignRight().Text(_proforma.TPDescription).FontFamily("Arial").FontSize(8);
                    }
                    table.Cell().ColumnSpan(2).Element(CellStyle).ExtendHorizontal().AlignLeft().Text("Total Amount").FontSize(8).Bold();
                    table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text(totalPrice.ToString("C", CultureInfo.CreateSpecificCulture("en-US"))).FontSize(8).Bold();

                });

                column.Item().Text(text =>
                {
                    text.Span("Selected Hospital: ").Style(textStyle).FontSize(8);

                    if (_proforma.Operation.Hospital != null)
                    {
                        text.Span(_proforma.Operation?.Hospital?.Name).Style(textStyle).FontSize(8).Bold();

                    }
                    else if (_proforma.Operation?.HospitalResponse?.HospitalConsultation?.Hospital != null)
                    {
                        text.Span(_proforma.Operation?.HospitalResponse?.HospitalConsultation?.Hospital?.Name).Style(textStyle).FontSize(8).Bold();
                    }
                });

                column.Item().Text("EXTRA SERVICES INCLUDED IN THE COST").Style(textStyle).FontSize(8);
                foreach (var service in _proforma.ProformaAdditionalServices)
                {
                    column.Item().Row(row =>
                    {
                        row.Spacing(5);
                        row.AutoItem().Text('\u2022' + " ").Style(textStyle).FontSize(8); // text or image
                        row.RelativeItem().Text(text =>
                        {
                            text.Span(service.AdditionalService.EnglishName).Style(textStyle).FontSize(8);
                            bool hasExtraInfo = service.AdditionalService.Day || service.AdditionalService.Companion || service.AdditionalService.Companion || service.AdditionalService.Piece;
                            if (hasExtraInfo)
                            {
                                text.Span(" (").Style(textStyle).FontSize(8);
                            }
                            string extraInfo = "";
                            if (service.AdditionalService.Day)
                            {
                                extraInfo += service.DayCount.ToString() + " Day - ";
                            }
                            if (service.AdditionalService.Companion)
                            {
                                extraInfo += service.CompanionCount.ToString() + " Companion - ";
                            }
                            if (service.AdditionalService.RoomType)
                            {
                                extraInfo += (service.RoomTypeId.Value == 1 ? "Standart" : "VIP") + " Room - ";
                            }
                            if (service.AdditionalService.Piece)
                            {
                                extraInfo += service.ItemCount.ToString() + " Count - ";
                            }
                            if (hasExtraInfo)
                            {
                                extraInfo = extraInfo.Substring(0, extraInfo.Length - 3);
                                text.Span(extraInfo + ")").Style(textStyle).FontSize(8);
                            }
                        });
                    });
                }

                column.Item().Text("SERVICES NOT INCLUDED").Style(textStyle).FontSize(8);
                foreach (var service in _proforma.ProformaNotIncludingServices)
                {
                    column.Item().Row(row =>
                    {
                        row.Spacing(5);
                        row.AutoItem().Text('\u2022' + " ").Style(textStyle).FontSize(8); // text or image
                        row.RelativeItem().Text(service.Description).Style(textStyle).FontSize(8);
                    });
                }

                column.Item()
                .BorderHorizontal(1)
                .BorderColor(Colors.Grey.Darken1).Row(row =>
                {
                    row.RelativeItem().PaddingTop(10).PaddingBottom(10).Text(_proforma.Description).Style(textStyle).FontSize(8);
                });

                column.Item().Text("BANK DETAILS").Style(textStyle).FontSize(8);

                column.Item().Text("USHAŞ Uluslararası Sağlık Hizmetleri A.Ş. Bank Account No:").Style(textStyle).FontSize(8).Bold();
                column.Item().Text(text =>
                {
                    text.Span("TL: ").FontSize(8).Bold();
                    text.Span("TR 4400 0100 2533 8997 3332 5030").FontSize(8);
                });
                column.Item().Text(text =>
                {
                    text.Span("USD: ").FontSize(8).Bold();
                    text.Span("TR 1700 0100 2533 8997 3332 5031").FontSize(8);
                });
                column.Item().Text(text =>
                {
                    text.Span("EUR: ").FontSize(8).Bold();
                    text.Span("TR 8700 0100 2533 8997 3332 5032").FontSize(8);
                });

                column.Item().Text("Name of the Bank: Ziraat Bankası A.Ş.").Style(textStyle).FontSize(8);
                column.Item().Text("Branch: Ankara Şehir Hastanesi Branch").Style(textStyle).FontSize(8);
                column.Item().Text("Branch Code: 2533").Style(textStyle).FontSize(8);
                column.Item().Text("SWIFT Code (BIC): TCZBTR2A").Style(textStyle).FontSize(8);

            });
        }

        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontFamily("Arial").FontSize(6);
            var today = DateTime.Now;

            container.Row(row =>
            {
                byte[] imageData = File.ReadAllBytes("./wwwroot/images/logo/logo-sb2.png");
                row.ConstantItem(150).Height(50).AlignMiddle().Image(imageData);

                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span("HQ Address: ").Style(titleStyle).Bold();
                        text.Span("USHAŞ International Health Services Inc. Ministry of Health").Style(titleStyle);
                    });
                    column.Item().Text("Campus, Üniversiteler Mahallesi Dumlupınar Bulvarı, 6001. Cadde No:09 Kat:8").Style(titleStyle);
                    column.Item().Text("06800 Çankaya, Ankara, Türkiye").Style(titleStyle);
                    column.Item().Text("Date: " + today.ToString("dd.MM.yyyy HH:mm")).Style(titleStyle).FontSize(8);
                    column.Item().Text(text =>
                    {
                        text.Span("Price Offer for ").Style(titleStyle).FontSize(10);
                        text.Span(_proforma.Operation?.PatientTreatmentProcess?.Patient?.Name + " " + _proforma.Operation?.PatientTreatmentProcess?.Patient?.Surname).Style(titleStyle).FontSize(10).Bold();
                    });
                });


            });
        }
    }
}
