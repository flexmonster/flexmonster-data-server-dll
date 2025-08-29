# Sample server with Flexmonster Data Server as a DLL
[![Flexmonster Pivot Table & Charts](https://cdn.flexmonster.com/landing.png)](https://www.flexmonster.com?r=github)
Website: [www.flexmonster.com](https://www.flexmonster.com?r=github)

## Flexmonster Pivot Table & Charts

Flexmonster Pivot Table & Charts is a powerful and fully customizable JavaScript component for web reporting. It is packed with all core features for data analysis and can easily become a part of your data visualization project. The tool supports popular frameworks like React, Vue, Angular, Blazor, and [more](https://www.flexmonster.com/doc/available-tutorials-integration?r=github). Also, Flexmonster connects to [any data source](https://www.flexmonster.com/doc/supported-data-sources?r=github), including SQL and NoSQL databases, JSON and CSV files, OLAP cubes, and Elasticsearch.

This repository contains the source code for a .NET Core application with [Flexmonster Data Server](https://www.flexmonster.com/doc/getting-started-with-data-server?r=github) as a DLL.

[Flexmonster Data Server](https://www.flexmonster.com/doc/intro-to-flexmonster-data-server?r=github) is an installable, server-side application. The idea of the Data Server is to reduce the time of data loading and allow analyzing large datasets by delegating all calculations to the server. The Data Server fetches your data from a data source, aggregates it, and then sends it to Flexmonster Pivot.

Table of contents:

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Related Flexmonster docs](#related-flexmonster-docs)
- [Support and feedback](#support-and-feedback)
- [Flexmonster licensing](#flexmonster-licensing)
- [Social media](#social-media)

## Prerequisites

- [Microsoft .NET 9.0](https://dotnet.microsoft.com/en-us/download)

## Installation

1. Download a `.zip` archive with the sample project or clone it from GitHub with the following commands:

```bash
git clone https://github.com/flexmonster/flexmonster-data-server-dll && cd flexmonster-data-server-dll
```
  
2. Run the sample server from the console:

```
cd DemoDataServerCore && dotnet restore && dotnet run
``` 

To see the result, open `http://localhost:5000/` in your browser.

## Related Flexmonster docs

- [Getting started with the Data Server as a DLL](https://www.flexmonster.com/doc/getting-started-with-data-server-dll?r=github) — learn how to use the Data Server as a DLL.
- [Referencing the Data Server as a DLL](https://www.flexmonster.com/doc/referencing-data-server-as-dll?r=github) — see details on embedding Flexmonster Data Server in a .NET Core application.

## Support and feedback

In case of any issues, visit our [Troubleshooting](https://www.flexmonster.com/doc/typical-errors?r=github) section or check the list of [Common issues](https://www.flexmonster.com/doc/common-issues-with-the-data-server?r=github) for possible solutions. You can also search among the [resolved cases](https://www.flexmonster.com/technical-support?r=github) for a solution to your issue.

To share your feedback or ask questions, contact our Tech team by raising a ticket on our [Help Center](https://www.flexmonster.com/help-center?r=github). You can also find a list of samples, technical specifications, and a user interface guide there.

## Flexmonster licensing

To learn about Flexmonster Pivot licenses, visit the [Flexmonster licensing page](https://www.flexmonster.com/pivot-table-editions-and-pricing?r=github). 
If you want to test our product, we provide a 30-day free trial.

If you need any help with your license, fill out our [Contact form](https://www.flexmonster.com/contact-our-team?r=github), and we will get in touch with you.

## Social media

Follow us on social media and stay updated on our development process!

[![LinkedIn](https://img.shields.io/badge/LinkedIn-blue?style=for-the-badge&logo=linkedin&logoColor=white)](https://linkedin.com/company/flexmonster) [![YouTube](https://img.shields.io/badge/YouTube-red?style=for-the-badge&logo=youtube&logoColor=white)](https://youtube.com/user/FlexMonsterPivot) [![Twitter](https://img.shields.io/badge/Twitter-blue?style=for-the-badge&logo=twitter&logoColor=white)](https://twitter.com/flexmonster)

