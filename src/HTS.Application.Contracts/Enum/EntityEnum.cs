using System;
using System.Collections.Generic;
using System.Text;

namespace HTS.Enum
{
    public static class EntityEnum
    {
        public enum GenderEnum
        {
            Women = 1,
            Men = 2            
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
            NewRecord = 1,
            HospitalAsked = 2,
            AssessmentWaiting = 3,
            QuotationWaiting = 4,
            Discharged = 5
        }

        public enum HospitalConsultationStatusEnum
        {
            HospitalResponseWaiting = 1,
            /// <summary>
            /// Operasyon onaylandı
            /// </summary>
            OperationApproved = 2,
            /// <summary>
            /// Operasyon reddedildi
            /// </summary>
            OperationRejected = 3
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
    }
}
