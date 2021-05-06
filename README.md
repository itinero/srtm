# SRTM

[![build](https://github.com/itinero/srtm/actions/workflows/build.yml/badge.svg)](https://github.com/itinero/srtm/actions/workflows/build.yml)
[![Join the chat at https://gitter.im/Itinero/Lobby](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Itinero/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Visit our website](https://img.shields.io/badge/website-itinero.tech-020031.svg) ](http://www.itinero.tech/)
[![](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/itinero/srtm/blob/master/LICENSE.md)

- SRTM: [![NuGet Badge](https://buildstats.info/nuget/SRTM)](https://www.nuget.org/packages/SRTM/)

A simple library to load SRTM data and return heights in _meter_ for a given lat/lon. Based on [Alpinechough.Srtm
](https://github.com/alpinechough/Alpinechough.Srtm).

## Usage

```csharp
// create a new srtm data instance.
// it accepts a folder to download and cache data into in addition to the source you want to use for the data.
// USGS data is immediately available, but is of a lower resolution.
var srtmData = new SRTMData(@"/path/to/data/cache", new USGSSource());
// NASA data is of a higher resolution, but requires creating an account at https://urs.earthdata.nasa.gov/users/new/.
var credentials = new NetworkCredential("username", "password");
var srtmData = new SRTMData(@"/path/to/data/cache", new NASASource(credentials));

// get elevations for some locations
int? elevation = srtmData.GetElevation(47.267222, 11.392778);
Console.WriteLine("Elevation of Innsbruck: {0}m", elevation);

elevation = srtmData.GetElevation(-16.5, -68.15);
Console.WriteLine("Elevation of La Paz: {0}m", elevation);

elevation = srtmData.GetElevation(27.702983735525862f, 85.2978515625f);
Console.WriteLine("Elevation of Kathmandu {0}m", elevation);

elevation = srtmData.GetElevation(21.030673628606102f, 105.853271484375f);
Console.WriteLine("Elevation of Ha Noi {0}m", elevation);

// if a smoother result is preferred, it is possible to use bilinear interpolation at the cost of some accuracy
double? smoothElevation = srtmData.GetElevationBilinear(47.267222, 11.392778);
Console.WriteLine("Elevation of Innsbruck: {0}m", elevation);

smoothElevation = srtmData.GetElevationBilinear(-16.5, -68.15);
Console.WriteLine("Elevation of La Paz: {0}m", elevation);

smoothElevation = srtmData.GetElevationBilinear(27.702983735525862f, 85.2978515625f);
Console.WriteLine("Elevation of Kathmandu {0}m", elevation);

smoothElevation = srtmData.GetElevationBilinear(21.030673628606102f, 105.853271484375f);
Console.WriteLine("Elevation of Ha Noi {0}m", elevation);
```

## Data sources

We implemented two sources of data, the [USGS SRTM](https://dds.cr.usgs.gov/srtm/version2_1/SRTM3/) and [NASA SRTM](https://e4ftl01.cr.usgs.gov/MEASURES/SRTMGL1.003/). If you want to add an extra source, we're accepting pull requests, you just need to implement [something like this](https://github.com/itinero/srtm/blob/master/src/SRTM/Sources/USGS/USGSSource.cs).

## We need help!

If you think we need to add another source of data **let us know via the issues**, if you know more about SRTM or of another source of elevation, also **let us know**.

