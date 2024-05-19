using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WordFinderNet8.WebApi.Extensions.Swagger
{
    public class ConfigureSwaggerOptions: IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "D3xtr0yed Technoloy Services",
                Description = "Word Finder API",
                Contact = new OpenApiContact
                {
                    Name = "Edson Martinez",
                    Email = "emz19.com@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/edsonmz/")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX"
                }
            };
            if (description.IsDeprecated)
            {
                info.Description += "Esta versión de la API ha quedado obsoleta.";
            }
            return info;
        }
    }
}
