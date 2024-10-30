# ContactsApi - Rest api project for test task. Маленькое тестовое задание в одну компанию.

I wrote the project using .net 8. 
This is a RESTful project that contains basic CRUD operations with the Contact entity. And a list of paginated contacs.
I used PostgreSQL as the database and EF as the ORM for this project. Object in db, "Contacts" table was created by EF code based migration.
Due to small size of project, i didn't separate project to layers such Domain, Infrastructure, Application etc. and just wrote everything in one. 

There is a main entity "Contact "as the project domain and a dto's for communication and interaction between layers, which contains validation attributes. For convert  those i use AutoMapper lib. 
Layer of services responsible for business logics and database interaction using DbContext, i refused to implement repositories for it, cause EF already has abstraction over database.
Controller act as presentation layer, and responsible for request handling and interaction with services layer. 
I wrote a middleware component for global request exceptions handling.
I didn't use caching data, cause i think this data suppose to be changed often. But could use in memory cache or Redis external caching, 

For logging i use Serilog lib, that wrote logs in local file with ElasticSearch understandable format. 
For app behaviour monitoring i use OpenTelemetry lib metrics, which available in /metrics route.
Also, i added health check of app, which available in /healthz route
