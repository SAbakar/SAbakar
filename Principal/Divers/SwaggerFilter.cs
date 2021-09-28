
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Principal.Divers
{
    public class SwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {            
            var filteredRoute = swaggerDoc.Paths
                .Where(x => x.Key.ToLower().Contains(@"/api/authenticate/register"))
                .ToList();
            filteredRoute.ForEach(x => 
                { 
                    swaggerDoc.Paths.Remove(x.Key);                     
                    //swaggerDoc.Components.Schemas.Remove(x.Key); 
                });
            
        }
    }
}

