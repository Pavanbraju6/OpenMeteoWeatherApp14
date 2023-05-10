using OpenMeteoWeatherApp.ApplicationLayer;

[TestFixture]
public class WeatherExportTests
{
    [Test]
    public void TestExportWeatherData()
    {
        // Arrange
        int daysToGet = 3;
        double latitude = 37.7749;
        double longitude = -122.4194;
        IApplicationLayer appLayer = new OpenMeteoWeatherApp1();
        var weatherData = appLayer.GetWeatherData(daysToGet, latitude, longitude);
        string expectedFileName = $"weatherExport_{DateTime.Today.ToString("yyyyMMdd")}.json";
        string expectedFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", expectedFileName);


        // Assert
        Assert.That(File.Exists(expectedFilePath), Is.True);
        string json = File.ReadAllText(expectedFilePath);
        Assert.That(json, Is.Not.Null.Or.Empty);
    }
}
