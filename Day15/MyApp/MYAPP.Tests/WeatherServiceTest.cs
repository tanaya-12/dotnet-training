using Xunit;
using Moq;
using MyApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Immutable;

namespace MyApp.Tests
{
    public class WeatherServiceTests
    {
        [Fact]
        public void GetWeather_ReturnsExpectedResult()
        {
            // Arrange
            var mockWeatherService = new Mock<IWeatherService>();

            mockWeatherService
                .Setup(x => x.GetTemperature(It.IsAny<string>()))
                .Returns(new List<double> { 30, 32, 28, 31, 29 });

            var weatherService = mockWeatherService.Object;
            var expectedCount = 5;

            // Act
            var result = weatherService.GetTemperature("New York");
            var actualCount = result.Count();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void GetWeather_ThrowsException()
        {
            // Arrange
            var mockWeatherService = new Mock<IWeatherService>();

            mockWeatherService
                .Setup(x => x.GetTemperature(It.IsAny<string>()))
                .Throws(new Exception("City Not Found"));

            var weatherService = mockWeatherService.Object;

            // Assert
            Assert.Throws<Exception>(() =>
            weatherService
            .GetTemperature("New York")
            .ToList());
        }
    }
}