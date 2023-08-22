# Sample server with Flexmonster Data Server as a DLL
[![Flexmonster Pivot Table & Charts](https://cdn.flexmonster.com/landing.png)](https://www.flexmonster.com?r=github)
Website: [www.flexmonster.com](https://www.flexmonster.com?r=github)

## Flexmonster Pivot Table & Charts

Flexmonster Pivot is a powerful JavaScript tool for interactive web reporting. It allows you to visualize and analyze data from JSON, CSV, SQL, NoSQL, Elasticsearch, and OLAP data sources quickly and conveniently. Flexmonster is designed to integrate seamlessly with any client-side framework and can be easily embedded into your application.

This repository contains the source code for a .NET Core application with [Flexmonster Data Server](https://www.flexmonster.com/doc/getting-started-with-data-server?r=github) as a DLL.

Flexmonster Data Server is a server-side tool that is responsible for fetching data from a data source, processing, and aggregating it. Then the data is passed to Flexmonster Pivot in a ready-to-show format. The Data Server significantly reduces the time of data loading and allows analyzing large datasets.

Table of contents:

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Related Flexmonster docs](#related-flexmonster-docs)

## Prerequisites

- [Microsoft .NET Core 3.1 or later](https://dotnet.microsoft.com/en-us/download)

## Installation

1. Download a `.zip` archive with the sample project or clone it from GitHub with the following commands:

```bash
git clone https://github.com/flexmonster/flexmonster-data-server-dll
cd flexmonster-data-server-dll
```
  
2. Run the sample server from the console:

```
cd DemoDataServerCore
dotnet restore
dotnet run
``` 

To see the result, open `http://localhost:5000/` in your browser.

## Related Flexmonster docs

- [Getting started with the Data Server as a DLL](https://www.flexmonster.com/doc/getting-started-with-data-server-dll?r=github) — learn how to use the Data Server as a DLL.
