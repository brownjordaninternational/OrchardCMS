using Orchard.UI.Resources;

namespace Entiat.Products
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            builder.Add().DefineStyle("CustomCatalogStyle").SetUrl("collectionCatalog.css");
            builder.Add().DefineStyle("CatalogBlockStyle").SetUrl("blockCatalog.css");
        }
    }
}