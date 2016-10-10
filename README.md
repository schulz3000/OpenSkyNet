# OpenSkyNet
============

dotnet lib for the opensky-network.org api

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

//Get all states from specific time. Time must be given as Unix Timestamp.
var result = await client.GetStatesAsync(timestamp:12345679);

//Get all states for specific ICAO24 transponder address. ICAO24 must be given in hex representation.
var result = await client.GetStatesAsync(icao24: new[] { "abc9f3", "3e1bf9" });
```

##GetMyStates
Works only when credentials are given
```csharp
//Get all current states
var result = await client.GetMyStatesAsync();

//Get all states from specific time. Time must be given as Unix Timestamp.
var result = await client.GetMyStatesAsync(timestamp:12345679);

//Get all states for specific ICAO24 transponder address. ICAO24 must be given in hex representation.
var result = await client.GetMyStatesAsync(icao24: new[] { "abc9f3", "3e1bf9" });

//Get all states for subset of your receivers.
var result = await client.GetMyStatesAsync(serials: new[] { 1, 2, 3 });
```
##Result

//TODO: Table for Result Propertys


RestApi Reference: (https://opensky-network.org/apidoc)