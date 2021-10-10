using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Demo.WebApi
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionProvider)
            => _apiVersionProvider = apiVersionProvider;

        public void Configure(string name, SwaggerGenOptions options)
            => Configure(options);

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _apiVersionProvider.ApiVersionDescriptions)
            {
                var descriptions = new List<string>
                {
                    "This API is a demo using TheMovieDatabase as the primary data source."
                };

                if (desc.IsDeprecated)
                {
                    descriptions.Add("This version is deprecated, please consider using a later version.");
                }

                var openApiInfo = new OpenApiInfo
                {
                    Title = "Demo Movies WebApi",
                    Version = desc.ApiVersion.ToString(),
                    Description = string.Join(' ', descriptions),
                };

                options.SwaggerDoc(desc.GroupName, openApiInfo);
            }
        }
    }
}
