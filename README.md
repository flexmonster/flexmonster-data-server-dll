# Sample server with Flexmonster Data Server as a DLL
[![Flexmonster Pivot Table & Charts](https://cdn.flexmonster.com/landing.png)](http://flexmonster.com)
Website: www.flexmonster.com

## Flexmonster Pivot Table & Charts

Flexmonster Pivot is a powerful JavaScript tool for interactive web reporting. It allows you to visualize and analyze data from JSON, CSV, SQL, NoSQL, Elasticsearch, and OLAP data sources quickly and conveniently. Flexmonster is designed to integrate seamlessly with any client-side framework and can be easily embedded into your application.

This repository holds the source code for a custom data server with [Flexmonster Data Server](https://www.flexmonster.com/doc/getting-started-with-flexmonster-data-server/) as a DLL.

Flexmonster Data Server is a special server-side tool that is responsible for fetching data from a data source, processing, and aggregating it. Then the data is passed to Flexmonster Pivot in a ready-to-show format. The Data Server significantly reduces the time of data loading and allows analyzing large datasets.

The table of contents:

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)

## Prerequisites

To run a sample server with the Data Server as a DLL, you will need Microsoft .NET Core 3.0 or higher. [Get it here](https://dotnet.microsoft.com/download) if it's not already installed on your machine.

## Installation

1. Download the `.zip` archive with the sample project or clone it from GitHub with the following command:

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

## Usage

For details on usage, refer to the [Getting started with the Data Server as a DLL](https://www.flexmonster.com/doc/getting-started-with-data-server-dll/) guide.
