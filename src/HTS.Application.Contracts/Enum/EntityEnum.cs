﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HTS.Enum
{
    public static class EntityEnum
    {
        public enum GenderEnum
        {
            Men = 1,
            Women = 2         
        }

        public enum CurrencyEnum
        {
            TL = 1,
            USD =2
        }

        public enum PatientNoteStatusEnum
        {
            NewRecord = 1,
            Revoked = 2
        }
        
        public enum PatientDocumentStatusEnum
        {
            NewRecord = 1,
            Revoked = 2
        }

        public enum PatientTreatmentStatusEnum
        {
            /// <summary>
            /// Yeni Kayıt
            /// </summary>
            NewRecord = 1,
            /// <summary>
            /// Hastanelere Danışıldı - Cevap Bekleniyor
            /// </summary>
            HospitalAskedWaitingResponse = 2,
            /// <summary>
            /// Hastanelere Danışıldı - Değerlendirme Bekliyor
            /// </summary>
            HospitalAskedWaitingAssessment = 3,
            /// <summary>
            /// Operasyon Onaylandı - Fiyatlandırma Bekliyor
            /// </summary>
            OperationApprovedWaitingPricing = 4,
            /// <summary>
            /// Proforma Oluşturuldu - MFB Onayı Bekliyor
            /// </summary>
            ProformaCreatedWaitingForMFBApproval = 5,
            /// <summary>
            /// MFB Reddetti - Fiyatlandırma Bekliyor
            /// </summary>
            MFBRejectedPriceExpecting = 6,
            /// <summary>
            /// Proforma Onaylandı - Hastaya İletilecek
            /// </summary>
            ProformaApprovedWillBeTransferredToPatient = 7,
            /// <summary>
            /// Proforma İletildi - Hasta Onayı Bekleniyor
            /// </summary>
            ProformaTransferredWaitingForPatientApproval = 8,
            /// <summary>
            ///  Hasta Proformayı Reddetti
            /// </summary>
            PatientRejectedProforma = 9,
            /// <summary>
            /// Proforma Onaylandı - Tahsilat Bekleniyor
            /// </summary>
            ProformaApprovedWaitingForPayment = 10,
            /// <summary>
            /// Proforma Tahsil Edildi - Tedavi Aşaması
            /// </summary>
            PaymentCompletedTreatmentProcess = 11
        }

        public enum HospitalConsultationStatusEnum
        {
            /// <summary>
            /// Hastane Cevabı Bekleniyor
            /// </summary>
            HospitalResponseWaiting = 1,
            /// <summary>
            /// Tedaviye Uygun
            /// </summary>
            SuitableForTreatment = 2,
            /// <summary>
            /// Tedaviye Uygun Değildir
            /// </summary>
            NotSuitableForTreatment = 3,
            /// <summary>
            /// Tanı İçin Muayene Gerekli
            /// </summary>
            ExaminationsIsRequiredForDiagnosis = 4,
            /// <summary>
            /// Operasyon onaylandı
            /// </summary>
            OperationApproved = 5,
            /// <summary>
            /// Operasyon reddedildi
            /// </summary>
            OperationRejected = 6
        }
        
        public enum HospitalResponseTypeEnum
        {
            /// <summary>
            /// Tedaviye Uygundur
            /// </summary>
            SuitableForTreatment = 1,
            /// <summary>
            /// Tedaviye Uygun Değildir
            /// </summary>
            NotSuitableForTreatment = 2 ,
            /// <summary>
            /// Tanı İçin Muayene Gerekli
            /// </summary>
            ExaminationsIsRequiredForDiagnosis = 3
        }

        public enum HospitalizationTypeEnum
        {
            /// <summary>
            /// Medikal Tedavi
            /// </summary>
            MedicalTreatment = 1,
            /// <summary>
            /// Yatış
            /// </summary>
            Hospitalization = 2,
            /// <summary>
            /// Cerrahi Yatış
            /// </summary>
            SurgicalHospitalization = 3
        }

        public enum AdditionalServiceEnum
        {
            /// <summary>
            /// Transfer hizmeti
            /// </summary>
            TransferService = 1,
            /// <summary>
            /// Medikal İkinci Muayene
            /// </summary>
            MedicalSecondExamination = 2,
            /// <summary>
            /// Tercümanlık
            /// </summary>
            Interpreting = 3,  
            /// <summary>
            /// Koordinasyon Hizmeti
            /// </summary>
            CoordinationService = 4,
            /// <summary>
            /// Servis Yatışı
            /// </summary>
            ServiceAdmission = 5,  
            /// <summary>
            /// Yoğun bakım
            /// </summary>
            IntensiveCare = 6,  
            /// <summary>
            /// Konaklama
            /// </summary>
            Accomodation = 7,
            /// <summary>
            /// Seyahat
            /// </summary>
            Trip = 8,
            /// <summary>
            /// Fiziki Muayene
            /// </summary>
            PhysicalExamination = 9
        }

        public enum OperationTypeEnum
        {
            /// <summary>
            /// Hastane danışma
            /// </summary>
            HospitalConsultation = 1,
            /// <summary>
            /// Elle giriş
            /// </summary>
            Manual = 2
        }
        
        public enum OperationStatusEnum
        {
            /// <summary>
            /// Fiyatlandırma Bekleniyor
            /// </summary>
            PriceExpecting = 1,
            /// <summary>
            /// Proforma Oluşturuldu - MFB Onayı Bekliyor
            /// </summary>
            ProformaCreatedWaitingForMFBApproval = 2,
            /// <summary>
            /// MFB Reddetti - Fiyatlandırma Bekliyor
            /// </summary>
            MFBRejectedPriceExpecting = 3,
            /// <summary>
            /// Proforma Onaylandı - Hastaya İletilecek
            /// </summary>
            ProformaApprovedWillBeTransferredToPatient = 4,
            /// <summary>
            /// Proforma İletildi - Hasta Onayı Bekleniyor
            /// </summary>
            ProformaTransferredWaitingForPatientApproval = 5,
            /// <summary>
            ///  Hasta Proformayı Reddetti
            /// </summary>
            PatientRejectedProforma = 6,
            /// <summary>
            /// Proforma Onaylandı - Tahsilat Bekleniyor
            /// </summary>
            ProformaApprovedWaitingForPayment = 7,
            /// <summary>
            /// Proforma Tahsil Edildi - Tedavi Aşaması
            /// </summary>
            PaymentCompletedTreatmentProcess = 8
        }

        public enum ProcessTypeEnum
        {
            /// <summary>
            /// Sutcodes
            /// </summary>
            SutCode = 1,
            /// <summary>
            /// Materials
            /// </summary>
            Material = 2
        }

        public enum ProformaStatusEnum
        {
            /// <summary>
            /// MFB Onay Bekliyor
            /// </summary>
            MFBWaitingApproval = 1,
            /// <summary>
            /// MFB Reddetti
            /// </summary>
            MFBRejected = 2,
            /// <summary>
            /// Hastaya İletilecek
            /// </summary>
            WillBeTransferedToPatient = 3,
            /// <summary>
            /// Hasta Onayı Bekliyor
            /// </summary>
            WaitingForPatientApproval = 4,
            /// <summary>
            /// Hasta Proformayı Reddetti
            /// </summary>
            PatientRejected = 5,
            /// <summary>
            /// Tahsilat Bekleniyor
            /// </summary>
            WaitingForPayment = 6,
            /// <summary>
            /// Tahsil Edildi
            /// </summary>
            PaymentCompleted = 7
        }
    }
}
