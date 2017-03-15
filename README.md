# sensorthings-net-sdk

[![NuGet Status](http://img.shields.io/nuget/v/Geodan.SensorThings.SDK.svg?style=flat)](https://www.nuget.org/packages/Geodan.SensorThings.SDK/)

The Geodan SensorThings .NET SDK makes it easy to add OGC SensorThings support to your .NET application.

This library is using .NET Standard 1.1

Implemented:

- HTTP GET methods

## Install

```
package-install Geodan.SensorThings.SDK
```

### Sample code

```
var server = "http://scratchpad.sensorup.com/OGCSensorThings/v1.0/";
var client = new SensorThingsClient(server);
var datastream = client.GetDatastream(760827);
var observations = datastream.GetObservations();
var nextpage_observations = observations.GetNextPage();
var foi = observations[0].GetFeatureOfInterest();
var observedProperty = datastream.GetObservedProperty();
var sensor = datastream.GetSensor();
var thing = datastream.GetThing();
var locations = thing.GetLocations();

```

### Sample applications

1] Console application

Description: Makes connection to SensorThings server and writes available datastreams to the console

Source: https://github.com/Geodan/sensorthings-net-sdk/tree/master/src/samples/ConsoleSampleApplication

2] MqttSampleApplication

Description: Consumes MQTT SensorThings messages

Source: https://github.com/Geodan/sensorthings-net-sdk/tree/master/src/samples/MqttSampleApplication

3] RealtimeOxyPlotSampleApplication

Description:Display a realtime OxyPlot graph based on MQTT SensorThings messages

Source: https://github.com/Geodan/sensorthings-net-sdk/tree/master/src/samples/RealtimeOxyPlotSampleApplication


### Dependencies

.NETStandard 1.1

System.Collections (>= 4.3.0)

System.Runtime.Extensions (>= 4.3.0)

System.Resources.ResourceManager (>= 4.3.0)

System.Runtime (>= 4.3.0)

Newtonsoft.Json (>=9.0.1)

System.Net.Http: (>=4.1.0)

### Roadmap:

- HTTP POST/PUT/DELETE

- OData query support
