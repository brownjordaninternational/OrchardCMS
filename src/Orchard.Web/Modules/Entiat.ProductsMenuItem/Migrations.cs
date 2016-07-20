using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace Entiat.ProductsMenuItem
{
    public class ProductsMenuItemDataMigration : DataMigrationImpl {

        public int Create() {
            ContentDefinitionManager.AlterTypeDefinition("NavigationProductsMenuItem",
                cfg => cfg
                        .WithPart("MenuPart")
                        .WithPart("CommonPart")
                        .DisplayedAs("Products Menu Item")
                        .WithSetting("Description", "Injects categories as menu item")
                        .WithSetting("Stereotype", "MenuItem")
                );

            return 1;
        }
    }
}