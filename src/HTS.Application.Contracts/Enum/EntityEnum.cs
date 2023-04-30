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
            HospitalResponseWaiting = 1
        }
        
        public enum HospitalResponseTypeEnum
        {
            SuitableForTreatment = 1,
            NotSuitableForTreatment = 2 ,
            ExaminationsIsRequiredForDiagnosis = 3
        }
    }
}
