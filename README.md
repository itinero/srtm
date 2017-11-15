# SRTM

A simple library to load SRTM data and return heights in _meter_ for a given lat/lon.

## Usage

```csharp
// create a new srtm data instance.
// it accepts a folder to download and cache data into.
var srtmData = new SRTMData(@"/path/to/data/cache");

// get elevations for some locations
int? elevation = srtmData.GetElevation(47.267222, 11.392778);
Console.WriteLine("Elevation of Innsbruck: {0}m", elevation);

elevation = srtmData.GetElevation(-16.5, -68.15);
Console.WriteLine("Elevation of La Paz: {0}m", elevation);

elevation = srtmData.GetElevation(27.702983735525862f, 85.2978515625f);
Console.WriteLine("Elevation of Kathmandu {0}m", elevation);

elevation = srtmData.GetElevation(21.030673628606102f, 105.853271484375f);
Console.WriteLine("Elevation of Ha Noi {0}m", elevation);
```