using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Options;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class ValueObjectServiceExtension
    {
        public static IServiceCollection AddCustomValueObjects(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection configurationSection = configuration.GetSection(nameof(ImageOptions));

            services.Configure<ImageOptions>(configurationSection);

            ImageOptions imageOptions = configurationSection.Get<ImageOptions>() ?? throw new NullValueServerException(14, nameof(ImageOptions),
                "Image options are null or empty");

            ImageName.Configure(imageOptions);

            return services;
        }
    }
}
