namespace CleanArchitectureExample.Application.Weather.Contracts;

public class WeatherResponse
{
    public Coord Coord { get; set; } = null!;
    public Weather[] Weather { get; set; } = null!;
    public string Base { get; set; } = null!;
    public Main Main { get; set; } = null!;
    public long Visibility { get; set; }
    public Wind Wind { get; set; } = null!;
    public Clouds Clouds { get; set; } = null!;
    public long Dt { get; set; }
    public Sys Sys { get; set; } = null!;
    public long Timezone { get; set; }
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long Cod { get; set; }
}

public class Clouds
{
    public long All { get; set; }
}

public class Coord
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public long Pressure { get; set; }
    public long Humidity { get; set; }
}

public class Sys
{
    public long Type { get; set; }
    public long Id { get; set; }
    public double Message { get; set; }
    public string Country { get; set; } = null!;
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
}

public class Weather
{
    public long Id { get; set; }
    public string Main { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Icon { get; set; } = null!;
}

public class Wind
{
    public double Speed { get; set; }
    public long Deg { get; set; }
}
