using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Entiat.CustomSettings.Models;
using Orchard.Localization;


namespace Entiat.CustomSettings.Handlers
{
    public class EntiatSiteSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }

        public EntiatSiteSettingsPartHandler()
        {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<EntiatSiteSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<EntiatSiteSettingsPart>("Parts_EntiatSiteSettings_Edit", "Parts/EntiatSiteSettings", "Modules"));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Modules")));
        }
    }

}