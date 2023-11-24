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
    public class InvitationLetterDocumentPdf : IDocument
    {
        private Proforma _proforma;SalesMethodAndCompanionInfo _salesMethodAndCompanionInfo;
        public InvitationLetterDocumentPdf(Proforma proforma,SalesMethodAndCompanionInfo salesMethodAndCompanionInfo )
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
                    text.Span("Patient's Name:: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.Name).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Patient's Birthdate: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.BirthDate.ToString()).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Patient's Passport Number:: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.PassportNumber).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Companion Name: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_salesMethodAndCompanionInfo.CompanionNameSurname).Style(textStyle).FontSize(8);
                });

                column.Item().Text(text =>
                {
                    text.Span("Companion's Passport Number: ").Style(boldTextStyle).FontSize(8);
                    text.Span(_salesMethodAndCompanionInfo.CompanionPassportNumber).Style(textStyle).FontSize(8);
                });

            });
        }
        
        private void ComposeHeader(IContainer container)
        {
            var patientName =
                $"{_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.Name} {_salesMethodAndCompanionInfo.PatientTreatmentProcess.Patient.Surname}";
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
                        column.Item().Text("TO WHOM IT MAY CONCERN;").Style(textStyle).FontSize(10).Bold();
                        column.Item().Text(text =>
                        {
                            text.Span(today.ToString("dd.MM.yyyy HH:mm")).Style(textStyle);
                        });
                       
                    });
                });
                
                column.Item().Text("Sağlık Bakanlığı Bilkent Yerleşkesi Üniversiteler Mah. Dumlupınar Bul.").Style(textStyle);
                column.Item().Text($"We humbly request the assistance of {patientName}’s," +
                    $" visa application in order to allow his/her and his/her companion." +
                    $"{patientName} is/are sent by " +
                    $"{_salesMethodAndCompanionInfo.ContractedInstitution?.Name} (please see annex) to visit Türkiye for medical purposes."+
                    $"{_proforma.Operation.Hospital?.Name} examined the first medical review of the relevant patient and the doctors of the hospital set"+
                    $"the effective treatment plan by giving the second medical review."+
                     "Given below the information of the relevant patient and his/her companion.").Style(textStyle);
              
            });
        }
    }
}
