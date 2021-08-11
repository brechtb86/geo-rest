# geo-service.rest

## What does it do?

With this REST service you can query countries, states and cities, with very detailed information.

## Warning!!!

This service is stil under development, this service should in no way be used in production!

## Endpoints

### v1

https://geo-service.rest/api/v1/countries

https://geo-service.rest/api/v1/countries/{countryId}

https://geo-service.rest/api/v1/countries/{twoLetterIsoCode}

https://geo-service.rest/api/v1/countries/{countryId}/states

https://geo-service.rest/api/v1/countries/{twoLetterIsoCode}/states

https://geo-service.rest/api/v1/countries/{countryId}/cities

https://geo-service.rest/api/v1/countries/{twoLetterIsoCode}/cities

## Docs & tryout

For documentation and tryout go to the [Swagger page](https://geo-service.rest/swagger/index.html).

## A big thanks!

This service is using the data from [dr5hn](https://github.com/dr5hn/countries-states-cities-database), shoutout to him for making and maintaining such a great database!

## Export

It is possible to export the database via https://geo-service.rest/export?databaseName=Geo&fileType=mssql

To change the database name from the default "Geo" just change the "databaseName" querystring parameter.
To change the output file type from the default "mssql" just change the "fileType" querystring parameter. For now only "mssql" is supported.

## Contact

I have set up a mailbox at [brecht@geo-service.rest](mailto:brecht@geo-service.rest)
