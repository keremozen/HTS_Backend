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

        public enum PatientTreatmentStatusEnum
        {
            NewRecord = 1,
            HospitalAsked = 2,
            AssessmentWaiting = 3,
            QuotationWaiting = 4,
            Discharged = 5
        }
    }
}
