using System;
using System.Collections.Generic;
using System.Text;
using VeloceCRM.Data;

namespace VeloceCRM.Repository
{
    public class CanvasCaseParticipantRoleRepository : BaseRepository
    {
        public CanvasCaseParticipantRoleRepository(string licenseKey, ApiContext apiContext) 
        {
            LicenseKey = licenseKey;
            ApiContext = apiContext;
        }

        public Entity.CanvasCaseParticipantRole? Get(long Id)
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseParticipantRoles.FirstOrDefault(x => x.Id == Id && x.LicenseKey == LicenseKey && !x.IsDeleted);
        }
        public List<Entity.CanvasCaseParticipantRole>? GetAll()
        {
            if (ApiContext == null) return null;
            return ApiContext.CanvasCaseParticipantRoles.Where(x => x.LicenseKey == LicenseKey && !x.IsDeleted).ToList();
        }
        public Entity.CanvasCaseParticipantRole? Create(Entity.CanvasCaseParticipantRole? Source)
        {
            if (ApiContext == null || Source == null) return null;
            Source.LicenseKey = LicenseKey;
            ApiContext.CanvasCaseParticipantRoles.Add(Source);
            ApiContext.SaveChanges();
            return Source;
        }
        public Entity.CanvasCaseParticipantRole? Update(Entity.CanvasCaseParticipantRole? Source)
        {
            if (ApiContext == null || Source == null) return null;
            var record = Get(Source.Id);
            if (record == null) return null;
            record.Key = Source.Key;
            record.Text = Source.Text;            
            ApiContext.SaveChanges();
            return record;
        }
        public Entity.CanvasCaseParticipantRole? Delete(long Id)
        {
            var record = Get(Id);
            if (ApiContext == null || record == null) return null;
            record.IsDeleted = true;
            ApiContext.SaveChanges();
            return record;
        }
    }
}
