# PeerBerry.API

PeerBerry.API is a strongly typed client library for accessing the PeerBerry API.

[![GitHub Actions Status](https://github.com/GetoXs/PeerBerry.API/workflows/Build%20&%20Test/badge.svg)](https://github.com/GetoXs/PeerBerry.API/actions)
[![NuGet](https://img.shields.io/nuget/dt/PeerBerry.API.svg)](https://www.nuget.org/packages/PeerBerry.API) 
[![NuGet](https://img.shields.io/nuget/vpre/PeerBerry.API.svg)](https://www.nuget.org/packages/PeerBerry.API)
[![license](https://img.shields.io/github/license/GetoXs/PeerBerry.API.svg)](https://github.com/GetoXs/PeerBerry.API/blob/master/LICENSE.txt)


## Features

* Response data is mapped to strongly typed models
* Support for most of the PeerBerry functionallity
* Possibility to execute custom API calls (in case of PeerBerry new functionallities)
* Automatic access token refresh mechanism
* Initialization via refresh token or username/password

## Supported Frameworks
The library is targeting both `.NET 8.0` for optimal compatibility

## Install the library

### Nuget (.NET CLI)
[![NuGet version](https://img.shields.io/nuget/v/PeerBerry.API.svg?style=for-the-badge)](https://www.nuget.org/packages/PeerBerry.API)  [![Nuget downloads](https://img.shields.io/nuget/dt/PeerBerry.API.svg?style=for-the-badge)](https://www.nuget.org/packages/PeerBerry.API)

	dotnet add package PeerBerry.API

### NuGet (Install-Package)

	Install-Package PeerBerry.API

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/GetoXs/PeerBerry.API?style=for-the-badge&label=GitHub)](https://github.com/GetoXs/PeerBerry.API/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/GetoXs/PeerBerry.API/releases).

## How to use

### Quick start

```csharp
// Get my balance
var client = new PeerBerryClient();
await client.InitializeUsingEmailAsync("email", "password");
// alternatively you can use client.InitializeUsingTokensAsync
var balance = await _client.GetBalanceMainAsync();
```

## Support the project
Any support is greatly appreciated.