using AutoMapper;
using Geo.Rest.Business.Services.Interfaces;
using Geo.Rest.Data.Contexts;
using Geo.Rest.Data.Extensions;
using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;
using Geo.Rest.Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Services
{
    public class GeoService : IGeoService
    {
        private readonly GeoContext _geoContext;

        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _environment;

        public GeoService(GeoContext geoContext, IMapper mapper, IWebHostEnvironment environment)
        {
            this._geoContext = geoContext;
            this._mapper = mapper;
            this._environment = environment;
        }

        public async Task<WrappedCollection<Country>> GetCountriesAsync(CountryCollectionQueryParameters parameters)
        {
            var countryEntities = await this._geoContext.Countries
                .Include(country => country.CountryTimeZones)
                .Include(country => country.CountryNameTranslations)
                .ToWrappedCollectionAsync(parameters.Page, parameters.PageSize);

            return this._mapper.Map<WrappedCollection<Country>>(countryEntities, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });
        }

        public async Task<Country> GetCountryByIdAsync(int countryId, CountryItemQueryParameters parameters)
        {
            var countryEntity = await this._geoContext.Countries
                 .Include(country => country.CountryTimeZones)
                 .Include(country => country.CountryNameTranslations)
                 .FirstOrDefaultAsync(country => country.Id == countryId);

            if (countryEntity == null)
            {
                return null;
            }

            return this._mapper.Map<Country>(countryEntity, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });
        }

        public async Task<Country> GetCountryByTwoLetterIsoCodeAsync(string twoLetterIsoCode, CountryItemQueryParameters parameters)
        {
            var countryEntity = await this._geoContext.Countries
                .Include(country => country.CountryTimeZones)
                .Include(country => country.CountryNameTranslations)
                .FirstOrDefaultAsync(country => country.TwoLetterIsoCode.ToLower() == twoLetterIsoCode.ToLower());

            if (countryEntity == null)
            {
                return null;
            }

            return this._mapper.Map<Country>(countryEntity, opts =>
            {
                opts.Items.Add("language", parameters.Language);
            });
        }

        public string GenerateExportScript(string databaseName = "Geo")
        {
            databaseName = databaseName.Trim().Replace(" ", "_");

            var exportPath = $"{this._environment.WebRootPath}\\export";

            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }

            var exportDirectory = new DirectoryInfo(exportPath);

            foreach (var file in exportDirectory.GetFiles())
            {
                file.Delete();
            }

            foreach (var directory in exportDirectory.GetDirectories())
            {
                directory.Delete(true);
            }

            var fileName = $"{exportPath}\\{databaseName.ToLowerInvariant()}_export_{DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")}.sql";

            var exportScriptStringBuilder = new StringBuilder();

            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Exported by https://geolocation.rest/export?datbaseName={databaseName}");
            exportScriptStringBuilder.AppendLine($"-- Created by Brecht Baekelandt - brecht@geolocation.rest");
            exportScriptStringBuilder.AppendLine($"-- For more info visit https://geolocation.rest/");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')");
            exportScriptStringBuilder.AppendLine($"BEGIN");
            exportScriptStringBuilder.AppendLine($"CREATE DATABASE [{databaseName}] COLLATE LATIN1_GENERAL_100_CI_AS_SC_UTF8");
            exportScriptStringBuilder.AppendLine($"END");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"USE [{databaseName}]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET ANSI_NULLS ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET QUOTED_IDENTIFIER ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Table structure for table [dbo].[Cities]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"DROP TABLE IF EXISTS [dbo].[Cities]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"CREATE TABLE [dbo].[Cities] ([Id] int IDENTITY(1,1) NOT NULL, [Name] nvarchar(255) NOT NULL, [StateId] int NOT NULL, [StateCode] nvarchar(255) NOT NULL, [CountryId] int NOT NULL, [CountryCode] char(2) NOT NULL, [Latitude] decimal(11,8) DEFAULT NULL, [Longitude] decimal(11,8) DEFAULT NULL, [CreatedAt] date NOT NULL DEFAULT N'2014-01-01 01:01:01', [UpdatedAt] date NOT NULL DEFAULT N'2014-01-01 01:01:01', [Flag] smallint NOT NULL DEFAULT 1, [WikiDataId] nvarchar(255) DEFAULT NULL, CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Dumping data for table [dbo].[Cities]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[Cities] ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[Cities] ([Id], [Name], [StateId], [StateCode], [CountryId], [CountryCode], [Latitude], [Longitude], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");

            var citiesCounter = 1;
            var citiesTotalCount = this._geoContext.Cities.Count();

            foreach (var city in this._geoContext.Cities.OrderBy(city => city.Id))
            {
                if (citiesCounter % 100 != 0 && citiesCounter < citiesTotalCount)
                {
                    exportScriptStringBuilder.AppendLine($"({city.Id}, N'{city.Name}', {city.StateId}, N'{city.StateCode}', {city.CountryId}, N'{city.CountryCode}', {city.Latitude?.ToString().Replace(",", ".") ?? "NULL"} , {city.Longitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{city.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {city.Flag}, N'{city.WikiDataId}'),");
                }
                else
                {
                    exportScriptStringBuilder.AppendLine($"({city.Id}, N'{city.Name}', {city.StateId}, N'{city.StateCode}', {city.CountryId}, N'{city.CountryCode}', {city.Latitude?.ToString().Replace(",", ".") ?? "NULL"} , {city.Longitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{city.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {city.Flag}, N'{city.WikiDataId}');");

                    if (citiesCounter < citiesTotalCount)
                    {
                        exportScriptStringBuilder.AppendLine($"GO");
                        exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[Cities] ([Id], [Name], [StateId], [StateCode], [CountryId], [CountryCode], [Latitude], [Longitude], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");
                    }
                }

                citiesCounter++;
            }

            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[Cities] OFF");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Table structure for table [dbo].[States]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"DROP TABLE IF EXISTS [dbo].[States]");
            exportScriptStringBuilder.AppendLine($"CREATE TABLE [dbo].[States] ([Id] int IDENTITY(1,1) NOT NULL, [Name] nvarchar(255) NOT NULL, [CountryId] int NOT NULL, [CountryCode] char(2) NOT NULL, [FipsCode] nvarchar(255) DEFAULT NULL, [TwoLetterIsoCode] nvarchar(255) DEFAULT NULL, [Latitude] decimal(11,8) DEFAULT NULL, [Longitude] decimal(11,8) DEFAULT NULL, [CreatedAt] date NULL DEFAULT NULL, [UpdatedAt] date NOT NULL, [Flag] smallint NOT NULL DEFAULT 1, [WikiDataId] nvarchar(255) DEFAULT NULL, CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Dumping data for table [dbo].[States]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[States] ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[States] ([Id], [Name], [CountryId], [CountryCode], [FipsCode], [TwoLetterIsoCode], [Latitude], [Longitude], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");

            var statesCounter = 1;
            var statesTotalCount = this._geoContext.States.Count();

            foreach (var state in this._geoContext.States.OrderBy(state => state.Id))
            {
                if (statesCounter % 100 != 0 && statesCounter < statesTotalCount)
                {
                    exportScriptStringBuilder.AppendLine($"({state.Id}, N'{state.Name}', {state.CountryId}, N'{state.CountryCode}', N'{state.FipsCode}', N'{state.TwoLetterIsoCode}', {state.Longitude?.ToString().Replace(",", ".") ?? "NULL" ?? "NULL"}, {state.Latitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{state.CreatedAt?.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {state.Flag}, N'{state.WikiDataId}'),");
                }
                else
                {
                    exportScriptStringBuilder.AppendLine($"({state.Id}, N'{state.Name}', {state.CountryId}, N'{state.CountryCode}', N'{state.FipsCode}', N'{state.TwoLetterIsoCode}',{state.Longitude?.ToString().Replace(",", ".") ?? "NULL"}, {state.Latitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{state.CreatedAt?.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {state.Flag}, N'{state.WikiDataId}');");

                    if (statesCounter < statesTotalCount)
                    {
                        exportScriptStringBuilder.AppendLine($"GO");
                        exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[States] ([Id], [Name], [CountryId], [CountryCode], [FipsCode], [TwoLetterIsoCode], [Latitude], [Longitude], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");
                    }
                }

                statesCounter++;
            }

            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[States] OFF");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Table structure for table [dbo].[CountryTimeZones]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"DROP TABLE IF EXISTS [dbo].[CountryTimeZones]");
            exportScriptStringBuilder.AppendLine($"CREATE TABLE [dbo].[CountryTimeZones] ([Id] int IDENTITY(1,1) NOT NULL, [CountryId] int NOT NULL, [Name] nvarchar(100) DEFAULT NULL, [GmtOffset] int DEFAULT NULL, [GmtOffsetName] nvarchar(100) DEFAULT NULL, [Abbreviation] nvarchar(10) DEFAULT NULL, [TimeZoneName] nvarchar(100) DEFAULT NULL, CONSTRAINT [PK_CountryTimeZones] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Dumping data for table [dbo].[CountryTimeZones]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[CountryTimeZones] ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[CountryTimeZones] ([Id], [CountryId], [Name], [GmtOffset], [GmtOffsetName], [Abbreviation], [TimeZoneName]) VALUES");

            var timeZonesCounter = 1;
            var timeZonesTotalCount = this._geoContext.CountryTimeZones.Count();

            foreach (var timeZone in this._geoContext.CountryTimeZones.OrderBy(countryTimeZone => countryTimeZone.Name))
            {
                if (timeZonesCounter % 100 != 0 && timeZonesCounter < timeZonesTotalCount)
                {
                    exportScriptStringBuilder.AppendLine($"({timeZone.Id}, {timeZone.CountryId}, N'{timeZone.Name}', {timeZone.GmtOffset}, N'{timeZone.GmtOffsetName}', N'{timeZone.Abbreviation}', N'{timeZone.TimeZoneName}'),");
                }
                else
                {
                    exportScriptStringBuilder.AppendLine($"({timeZone.Id}, {timeZone.CountryId}, N'{timeZone.Name}', {timeZone.GmtOffset}, N'{timeZone.GmtOffsetName}', N'{timeZone.Abbreviation}', N'{timeZone.TimeZoneName}');");

                    if (timeZonesCounter < timeZonesTotalCount)
                    {
                        exportScriptStringBuilder.AppendLine($"GO");
                        exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[CountryTimeZones] ([Id], [CountryId], [Name], [GmtOffset], [GmtOffsetName], [Abbreviation], [TimeZoneName]) VALUES");
                    }
                }

                timeZonesCounter++;
            }

            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[CountryTimeZones] OFF");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Table structure for table [dbo].[CountryNameTranslations]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"DROP TABLE IF EXISTS [dbo].[CountryNameTranslations]");
            exportScriptStringBuilder.AppendLine($"CREATE TABLE [dbo].[CountryNameTranslations] ([Id] int IDENTITY(1,1) NOT NULL, [CountryId] int NOT NULL, [Language] nvarchar(2) DEFAULT NULL, [Value] nvarchar(100) DEFAULT NULL, CONSTRAINT [PK_CountryNameTranslations] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Dumping data for table [dbo].[CountryNameTranslations]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[CountryNameTranslations] ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[CountryNameTranslations] ([Id], [CountryId], [Language], [Value]) VALUES");

            var translationsCounter = 1;
            var translationsTotalCount = this._geoContext.CountryNameTranslations.Count();

            foreach (var translation in this._geoContext.CountryNameTranslations.OrderBy(countryNameTranslation => countryNameTranslation.Language))
            {
                if (translationsCounter % 100 != 0 && translationsCounter < translationsTotalCount)
                {
                    exportScriptStringBuilder.AppendLine($"({translation.Id}, {translation.CountryId}, N'{translation.Language}', N'{translation.Value}'),");
                }
                else
                {
                    exportScriptStringBuilder.AppendLine($"({translation.Id}, {translation.CountryId}, N'{translation.Language}', N'{translation.Value}');");

                    if (translationsCounter < translationsTotalCount)
                    {
                        exportScriptStringBuilder.AppendLine($"GO");
                        exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[CountryNameTranslations] ([Id], [CountryId], [Language], [Value]) VALUES");
                    }
                }

                translationsCounter++;
            }

            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[CountryNameTranslations] OFF");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Table structure for table [dbo].[Countries]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"DROP TABLE IF EXISTS [dbo].[Countries]");
            exportScriptStringBuilder.AppendLine($"CREATE TABLE [dbo].[Countries] ([Id] int IDENTITY(1,1) NOT NULL, [Name] nvarchar(100) NOT NULL, [ThreeLetterIsoCode] nvarchar(3) DEFAULT NULL, [NumericCode] nvarchar(3) DEFAULT NULL, [TwoLetterIsoCode] nvarchar(2) DEFAULT NULL, [PhoneCode] nvarchar(255) DEFAULT NULL, [Capital] nvarchar(255) DEFAULT NULL, [Currency] nvarchar(255) DEFAULT NULL, [CurrencySymbol] nvarchar(255) DEFAULT NULL, [TLD] nvarchar(255) DEFAULT NULL, [Native] nvarchar(255) DEFAULT NULL, [Region] nvarchar(255) DEFAULT NULL, [SubRegion] nvarchar(255) DEFAULT NULL, [Latitude] decimal(11,8) DEFAULT NULL, [Longitude] decimal(11,8) DEFAULT NULL, [Emoji] nvarchar(191) DEFAULT NULL, [EmojiUnicode] nvarchar(191) DEFAULT NULL, [CreatedAt] date NULL DEFAULT NULL, [UpdatedAt] date NOT NULL, [Flag] smallint NOT NULL DEFAULT 1, [WikiDataId] nvarchar(255) DEFAULT NULL, CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Id] ASC)) ON [PRIMARY]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"-- Dumping data for table [dbo].[Countries]");
            exportScriptStringBuilder.AppendLine($"--");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[Countries] ON");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[Countries] ([Id], [Name], [ThreeLetterIsoCode], [NumericCode], [TwoLetterIsoCode], [PhoneCode], [Capital], [Currency], [CurrencySymbol], [TLD], [Native], [Region], [SubRegion], [Latitude], [Longitude], [Emoji], [EmojiUnicode], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");

            var countriesCounter = 1;
            var countriesTotalCount = this._geoContext.Countries.Count();

            foreach (var country in this._geoContext.Countries.OrderBy(country => country.Id))
            {
                if (countriesCounter % 100 != 0 && countriesCounter < countriesTotalCount)
                {
                    exportScriptStringBuilder.AppendLine($"({country.Id}, N'{country.Name}', N'{country.ThreeLetterIsoCode}', N'{country.NumericCode}', N'{country.TwoLetterIsoCode}', N'{country.PhoneCode}', N'{country.Capital}', N'{country.Currency}', N'{country.CurrencySymbol}', N'{country.Tld}', N'{country.Native}', N'{country.Region}', N'{country.SubRegion}', {country.Latitude?.ToString().Replace(",", ".") ?? "NULL"} , {country.Longitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{country.Emoji}', N'{country.EmojiUnicode}', N'{country.CreatedAt?.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {country.Flag}, N'{country.WikiDataId}'),");
                }
                else
                {
                    exportScriptStringBuilder.AppendLine($"({country.Id}, N'{country.Name}', N'{country.ThreeLetterIsoCode}', N'{country.NumericCode}', N'{country.TwoLetterIsoCode}', N'{country.PhoneCode}', N'{country.Capital}', N'{country.Currency}', N'{country.CurrencySymbol}', N'{country.Tld}', N'{country.Native}', N'{country.Region}', N'{country.SubRegion}', {country.Latitude?.ToString().Replace(",", ".") ?? "NULL"} , {country.Longitude?.ToString().Replace(",", ".") ?? "NULL"}, N'{country.Emoji}', N'{country.EmojiUnicode}', N'{country.CreatedAt?.ToString("yyyy-MM-dd hh:mm:ss")}', N'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}', {country.Flag}, N'{country.WikiDataId}');");

                    if (countriesCounter < countriesTotalCount)
                    {
                        exportScriptStringBuilder.AppendLine($"GO");
                        exportScriptStringBuilder.AppendLine($"INSERT INTO [dbo].[Countries] ([Id], [Name], [ThreeLetterIsoCode], [NumericCode], [TwoLetterIsoCode], [PhoneCode], [Capital], [Currency], [CurrencySymbol], [TLD], [Native], [Region], [SubRegion], [Latitude], [Longitude], [Emoji], [EmojiUnicode], [CreatedAt], [UpdatedAt], [Flag], [WikiDataId]) VALUES");

                    }
                }

                countriesCounter++;
            }

            exportScriptStringBuilder.AppendLine($"SET IDENTITY_INSERT [dbo].[Countries] OFF");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[Cities] WITH CHECK ADD CONSTRAINT [FK_Cities_Countries] FOREIGN KEY([CountryId])");
            exportScriptStringBuilder.AppendLine($"REFERENCES [dbo].[Countries] ([Id])");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Countries]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[Cities] WITH CHECK ADD CONSTRAINT [FK_Cities_States] FOREIGN KEY([StateId])");
            exportScriptStringBuilder.AppendLine($"REFERENCES [dbo].[States] ([Id])");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_States]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[CountryTimeZones] WITH CHECK ADD CONSTRAINT [FK_CountryTimeZones_Countries] FOREIGN KEY([CountryId])");
            exportScriptStringBuilder.AppendLine($"REFERENCES [dbo].[Countries] ([Id])");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[CountryTimeZones] CHECK CONSTRAINT [FK_CountryTimeZones_Countries]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[CountryNameTranslations] WITH CHECK ADD CONSTRAINT [FK_CountryNameTranslations_Countries] FOREIGN KEY([CountryId])");
            exportScriptStringBuilder.AppendLine($"REFERENCES [dbo].[Countries] ([Id])");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[CountryNameTranslations] CHECK CONSTRAINT [FK_CountryNameTranslations_Countries]");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_States_Countries] FOREIGN KEY([CountryId])");
            exportScriptStringBuilder.AppendLine($"REFERENCES [dbo].[Countries] ([Id])");
            exportScriptStringBuilder.AppendLine($"GO");
            exportScriptStringBuilder.AppendLine($"ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_States_Countries]");
            exportScriptStringBuilder.AppendLine($"GO");

            File.WriteAllText(fileName, exportScriptStringBuilder.ToString());

            return fileName;
        }
    }
}
