using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{
    public class ENabizProcess : FullAuditedEntityWithUser<int, IdentityUser>
    {
        public string TreatmentCode { get; set; }
        public string SysTrackingNumber { get; set; }
        public int? ProcessId { get; set; }
        public bool IsCancelled { get; set; }
        public string? GERCEKLESME_ZAMANI { get; set; }
        public string? ISLEM_TURU { get; set; }
        public string? ISLEM_KODU { get; set; }
        public string? ISLEM_ADI { get; set; }
        public string? ISLEM_ZAMANI { get; set; }
        public string? ADET { get; set; }
        public string? HASTA_TUTARI { get; set; }
        public string? KURUM_TUTARI { get; set; }
        public string? RANDEVU_ZAMANI { get; set; }
        public string? KULLANICI_KIMLIK_NUMARASI { get; set; }
        public string? CIHAZ_NUMARASI { get; set; }
        public string? ISLEM_REFERANS_NUMARASI { get; set; }
        public string? GIRISIMSEL_ISLEM_KODU { get; set; }
        public string? KLINIK_KODU { get; set; }
        public string? ISLEM_PUAN_BILGISI { get; set; }
        [ForeignKey("ProcessId")]
        public Process? Process { get; set; }

    }
}
