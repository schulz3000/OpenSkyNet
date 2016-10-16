# OpenSkyNet
============

dotnet lib for the opensky-network.org api

##Builds
[![Build status](https://ci.appveyor.com/api/projects/status/gwga69d3bnlmis7k?svg=true)](https://ci.appveyor.com/project/schulz3000/openskynet)

##Usage

```csharp
var client = new OpenSkyApi();

//Alternative with credentials
var  client = new OpenSkyApi("Username", "Password");

// Close Connection to opensky-network.org endpoint
client.Dispose();
```

##GetStates
```csharp
//Get all current states
var result = await client.GetStatesAsync();

//Get all states from specific time.
var result = await client.GetStatesAsync(time:DateTime.UtcNow);

//Get all states for specific ICAO24 transponder address. ICAO24 must be given in hex representation.
var result = await client.GetStatesAsync(icao24: new[] { "abc9f3", "3e1bf9" });
```

##GetMyStates
Works only when credentials are given
```csharp
//Get all current states
var result = await client.GetMyStatesAsync();

//Get all states from specific time.
var result = await client.GetMyStatesAsync(time:DateTime.UtcNow);

//Get all states for specific ICAO24 transponder address. ICAO24 must be given in hex representation.
var result = await client.GetMyStatesAsync(icao24: new[] { "abc9f3", "3e1bf9" });

//Get all states for subset of your receivers.
var result = await client.GetMyStatesAsync(serials: new[] { 1, 2, 3 });
```
##Result

//TODO: Table for Result Propertys


RestApi Reference: (https://opensky-network.org/apidoc)