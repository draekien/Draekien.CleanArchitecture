using AutoMapper;

using WeatherForecast.Application.Common.Mappings;

using Xunit;

namespace WeatherForecast.Application.UnitTests.Common.Mappings
{
    public class MappingProfileTests
    {
        [Fact]
        public void GivenAllMappingConfigurationsFromApplicationAssembly_ThenMappingConfigurationsShouldBeValid()
        {
            // Arrange
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile(typeof(MappingProfile).Assembly));
            });

            // Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
