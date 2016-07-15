using Bj.Essentials.Entities;
using Entiat.CustomSettings.Models;
using Entiat.CustomSettings.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System.Collections.Generic;

namespace Entiat.CustomSettings.Drivers
{
    public class EntiatSiteSettingsPartDriver : ContentPartDriver<EntiatSiteSettingsPart>
    {
        protected override string Prefix
        {
            get { return "EntiatSetting"; }
        }

        private readonly ISettingsService _service;
        public EntiatSiteSettingsPartDriver(ISettingsService service)
        {
            _service = service;
        }

        protected override DriverResult Editor(EntiatSiteSettingsPart part, dynamic shapeHelper)
        {
            var companies = _service.GetAllCompanies();
            part.companyList = companies;
            if (part.CompanyCode != "" && part.CompanyCode != null)
            {
                part.channelList = _service.GetAllChannels(part.CompanyCode);
            }
            else {
                part.channelList = new List<Channel>();
            }
            return ContentShape("Parts_EntiatSiteSettings_Edit", () => shapeHelper
                .EditorTemplate(TemplateName: "Parts/EntiatSiteSettings", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(EntiatSiteSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}