using Orchard.Logging;
using Orchard.Mvc.Routes;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Entiat.Products
{
    public class Routes : IRouteProvider
    {
     
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("collection/{collectionid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "Catalog"}, { "action", "GetProductsInCollection"},{"collectionid","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                    
                },
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("collection/{collectionid}/product/{channelproductid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "GetProductDetail"},{ "channelproductid","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                 new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("category/{categoryid}/product/{channelproductid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "GetProductDetail"},{ "channelproductid","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                 new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("product/{channelproductid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "GetProductDetail"}, { "channelproductid","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                 
            new RouteDescriptor
                {

                    Priority=5,
                    Route = new Route("category/{categoryid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "Catalog"}, { "action", "GetProductsInCategory"},{"categoryid","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("swatch/{swatchId}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "GetFullSizeSwatchImage"},{"swatchId","" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("AddPoolsideProduct/{channelproductid}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "AddPoolsideProduct" },{ "channelproductid", "" } },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("EmailMyPoolsideProduct/{address}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "EmailMyPoolsideProduct" },{ "address",  ""} },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority=5,
                    Route = new Route("allProductsEmail/{address}",
                    new RouteValueDictionary { {"area", "Entiat.Products"}, { "controller", "ProductDetail"}, { "action", "allProductsEmail" },{ "address",  ""} },
                    new RouteValueDictionary (),
                    new RouteValueDictionary { {"area", "Entiat.Products" } },
                    new MvcRouteHandler())
                }
        };
            
        }
    }
}