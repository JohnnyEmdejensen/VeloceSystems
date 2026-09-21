using System;
using System.Collections.Generic;
using System.Text;

namespace VeloceCRM.Client.Internals
{
    
    public class EventHelper
    {
        public delegate void DefaultHandler(object sender, EventArgs e);
        public event DefaultHandler? CountryCollectionChanged;
        public event DefaultHandler? PostalzoneCollectionChanged;
        public event DefaultHandler? LocationCollectionChanged;
        public event DefaultHandler? UserCollectionChanged;
        public event DefaultHandler? CompanyCollectionChanged;
        public event DefaultHandler? PersonCollectionChanged;
        public event DefaultHandler? ActiveCompanyChanged;
        public event DefaultHandler? CompanyChanged;
        public event DefaultHandler? CountryChanged;
        public event DefaultHandler? PostalzoneChanged;
        public event DefaultHandler? LocationChanged;
        public event DefaultHandler? ActivePersonChanged;
        public event DefaultHandler? PersonChanged;
        public event DefaultHandler? TitleCollectionChanged;
        public event DefaultHandler? TitleChanged;
        public event DefaultHandler? FollowuptypeCollectionChanged;
        public event DefaultHandler? FollowuptypeChanged;
        public event DefaultHandler? ActivityCollectionChanged;
        public event DefaultHandler? ActivityChanged;
        public event DefaultHandler? CanvasCaseParticipantRoleCollectionChanged;
        public event DefaultHandler? CanvasCaseParticipantCollectionChanged;
        public event DefaultHandler? CanvasCaseCollectionChanged;
        public event DefaultHandler? CanvasCaseChanged;
        public event DefaultHandler? CanvasCaseParticipantChanged;
        public event DefaultHandler? CanvasCaseParticipantRoleChanged;
        public event DefaultHandler? CanvasCaseCompanyLinkCollectionChanged;
        public event DefaultHandler? DocumentCollectionChanged;

        public void RaiseDocumentCollectionChangedEvent()
        {
            DocumentCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseCompanyLinkCollectionChangedEvent()
        {
            CanvasCaseCompanyLinkCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseChangedEvent()
        {
            CanvasCaseChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseParticipantChangedEvent()
        {
            CanvasCaseParticipantChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseParticipantRoleChangedEvent()
        {
            CanvasCaseParticipantRoleChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseCollectionChangedEvent()
        {
            CanvasCaseCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseParticipantCollectionChangedEvent()
        {
            CanvasCaseParticipantCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCanvasCaseParticipantRoleCollectionChangedEvent()
        {
            CanvasCaseParticipantRoleCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActivityChangedEvent()
        {
            ActivityChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActivityCollectionChangedEvent()
        {
            ActivityCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseFollowuptypeChangedEvent()
        {
            FollowuptypeChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseFollowuptypeCollectionChangedEvent()
        {
            FollowuptypeCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseTitleChangedEvent()
        {
            TitleChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseTitleCollectionChangedEvent()
        {
            TitleCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePersonChangedEvent()
        {
            PersonChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActivePersonChangedEvent()
        {
            ActivePersonChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseLocationChangedEvent()
        {
            LocationChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePostalzoneChangedEvent()
        {
            PostalzoneChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryChangedEvent()
        {
            CountryChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCompanyChangedEvent()
        {
            CompanyChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseActiveCompanyChangedEvent()
        {
            ActiveCompanyChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePersonCollectionChangedEvent()
        {
            PersonCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCompanyCollectionChangedEvent()
        {
            CompanyCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseUserCollectionChangedEvent()
        {
            UserCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseLocationCollectionChangedEvent()
        {
            LocationCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaisePostalzoneCollectionChangedEvent()
        {
            PostalzoneCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseCountryCollectionChangedEvent()
        {
            CountryCollectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
