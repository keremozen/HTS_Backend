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
using static Volo.Abp.Ui.LayoutHooks.LayoutHooks;

namespace HTS.PDFDocument
{
    public class InvitationLetterDocumentPdf : IDocument
    {
        private Proforma _proforma;
        private SalesMethodAndCompanionInfo _salesMethodAndCompanionInfo;
        public InvitationLetterDocumentPdf(Proforma proforma, SalesMethodAndCompanionInfo salesMethodAndCompanionInfo)
        {
            _proforma = proforma;
            _salesMethodAndCompanionInfo = salesMethodAndCompanionInfo;
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

                 page.Footer().Element(ComposeFooter);
             });
        }

        private void ComposeContent(IContainer container)
        {
            var textStyle = TextStyle.Default.FontFamily("Calibri").FontSize(11);
            var boldTextStyle = TextStyle.Default.FontFamily("Calibri").FontSize(11).Bold();
            var patientName =
                $"{_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.Name} {_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.Surname}";
            var hospitalName = _proforma.Operation.Hospital != null ? _proforma.Operation.Hospital.Name : _proforma.Operation.HospitalResponse.HospitalConsultation?.Hospital.Name;

            container.PaddingTop(10).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().PaddingTop(8).AlignRight().Column(column =>
                    {
                        column.Item().Text(text =>
                        {
                            text.Span(DateTime.Now.ToString("dd.MM.yyyy HH:mm")).Style(textStyle);
                        });
                    });
                });

                column.Item().Text("TO WHOM IT MAY CONCERN;").Style(boldTextStyle).LineHeight(2);

                column.Item().Text($"We humbly request the assistance of {patientName}’s," +
                    $" visa application in order to allow his/her and his/her companion. " +
                    $"{patientName} is/are sent by " +
                    $"{_salesMethodAndCompanionInfo.ContractedInstitution?.Name} (please see annex) to visit Türkiye for medical purposes.").Style(textStyle).LineHeight((float)1.1);

                column.Item().PaddingTop(5).Text($"{hospitalName} examined the first medical review of the relevant patient and the doctors of the hospital set" +
                    $"the effective treatment plan by giving the second medical review. ").Style(textStyle).LineHeight((float)1.1);

                column.Item().PaddingTop(5).Text($"Given below the information of the relevant patient and his/her companion.").Style(textStyle).Italic().LineHeight((float)1.1);

                column.Item().PaddingTop(20).Text(text =>
                {
                    text.Span("Patient's Name: ").Style(textStyle);
                    text.Span(patientName).Style(textStyle);
                });

                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("Patient's Birthdate: ").Style(textStyle);
                    text.Span(_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.BirthDate.Value.ToString("dd.MM.yyyy")).Style(textStyle);
                });

                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("Patient's Passport Number: ").Style(textStyle);
                    text.Span(_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.PassportNumber).Style(textStyle);
                });

                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("Companion Name: ").Style(textStyle);
                    text.Span(_salesMethodAndCompanionInfo.CompanionNameSurname).Style(textStyle);
                });

                column.Item().PaddingTop(10).Text(text =>
                {
                    text.Span("Companion's Passport Number: ").Style(textStyle);
                    text.Span(_salesMethodAndCompanionInfo.CompanionPassportNumber).Style(textStyle);
                });

                column.Item().PaddingTop(20).Text($"If required you may contact us for further information.").Style(textStyle).LineHeight((float)1.1);
                column.Item().PaddingTop(5).Text($"Your kind consideration to this request will be greatly appreciated.").Style(textStyle).LineHeight((float)1.1);
                column.Item().PaddingTop(5).Text($"Best regards,").Style(textStyle).LineHeight((float)1.1);
                column.Item().PaddingTop(5).Text($"UBEYDULLAH ŞAHİN").Style(textStyle).LineHeight((float)1.1);
                column.Item().PaddingTop(5).Text($"USHAŞ TMC Operations Manager").Style(textStyle).LineHeight((float)1.1);

            });
        }

        private void ComposeHeader(IContainer container)
        {

            var textStyle = TextStyle.Default.FontFamily("Calibri").FontSize(8);
            var boldTextStyle = TextStyle.Default.FontFamily("Calibri").FontSize(8).Bold();
            var today = DateTime.Now;

            container.Column(column =>
            {
                column.Item().AlignCenter().Row(row =>
                {
                    byte[] imageData = File.ReadAllBytes("./wwwroot/images/logo/logo-sb2.png");
                    row.ConstantItem(312).Height(75).Image(imageData);
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {

            var textStyle = TextStyle.Default.FontFamily("Calibri").FontSize(8);
            var boldTextStyle = TextStyle.Default.FontFamily("Calibri").FontSize(8).Bold();
            
            container.Column(column =>
            {
                column.Item().Text($"USHAŞ INTERNATIONAL HEALTH SERVICES").Style(boldTextStyle).FontSize(9).LineHeight((float)1.1);
                column.Item().Text(text =>
                {
                    text.Span("Address ").Style(boldTextStyle).FontSize(9);
                    text.Span("Bilkent Yerleşkesi Üniversiteler Mah. Dumlupınar Bulvarı 6001. Cad. No:9  Kat:8 Çankaya/Ankara 06800").Style(textStyle).FontSize(9);
                });
                column.Item().Text(text =>
                {
                    text.Span("Telephone: ").Style(boldTextStyle).FontSize(9);
                    text.Span("+90 312 9790020").Style(textStyle).FontSize(9);
                });
                column.Item().Text(text =>
                {
                    text.Span("E-mail: ").Style(boldTextStyle).FontSize(9);
                    text.Span("ushas@saglik.gov.tr").Style(textStyle).FontSize(9);
                });
            });
        }
    }
}
