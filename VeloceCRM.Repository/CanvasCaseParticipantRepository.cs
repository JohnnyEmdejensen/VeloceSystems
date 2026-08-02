using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CanvasCaseParticipantRepository : BaseRepository
    {
        public CanvasCaseParticipantRepository(string licenseKey, ApiContext apiContext) 
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.CanvasCaseParticipant? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseParticipants.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.CanvasCaseParticipant>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseParticipants.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public Entity.CanvasCaseParticipant? Create(Entity.CanvasCaseParticipant? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.CanvasCaseParticipants.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.CanvasCaseParticipant? Update(Entity.CanvasCaseParticipant? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.CanvasCaseId = Source.CanvasCaseId;
            record.PersonId = Source.PersonId;
            record.RoleId = Source.RoleId;
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.CanvasCaseParticipant? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
