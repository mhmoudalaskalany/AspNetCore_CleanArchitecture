# .Net 9 Api Clean Architecture
Clean Architecture For .Net 9 Web Api

# Layers
![layers](https://github.com/mhmoudalaskalany/Images/raw/main/clean_architecture_images/CleanArchitecture.png)
- Web Api Layer 
- Application Layer (Business Logic)
- Common Layer  (Abstraction Layer Between (High Level Layer) Application Layer And (Low Level Layer) Infrastructure)
- Domain Layers (Domain Models)
- Infrastructure Layer (Data Access Layer)
- Integration Layer (External Third Parties Integration)

# Features

- Generic crud operations
- Auto Mapper
- SOLID Principles Applied
- Generic Repository And Unit Of Work Pattern
- User Management Module (managing user role and permission per page level)
- Redis Caching
- Audit Trails
- Logging Using Serilog To Sql Server Database
- Fluent Scheduler (For Background Tasks)
- Swagger Documentation
- JWT Authentication
- Policy Based Authorization
- Form Based (Username And Password) Authentication And LDAP Authentication
- Api Versioning

# Road Map
- Add Dashboard For Audit Trails Logs
- Add Logging Dashboard
- Add Prometheus And Grafana Dashboard For Monitoring And Health Checks
- Add AMQP (Rabbit MQ And Masstransit)
- Add External Providers Authentication (Google , Azure AD , Facebook)
- Divide Entities To Schemas in Database
- Use File-Scoped Namespaces to make refactor easier
- Run Unit Tests using Iteration Count like (1000) case for the test till it fails 
- Move Logging Mechanism To Different Nuget SDK
- Move Date Layer Access To Separate Nuget SDK  
- Use ClaimPrincipalExtensions to add better extension methods to get claim values from token
- Refactor BaseService to remove the inputs to the service
- Add https://www.flagsmith.com/
- Use https://youtube.com/shorts/NLopFJ-EtKA?feature=share
- Token Management With Identity Provider with OAuth 2
# Installation

- clone the repository
- run Update-Database through PMC(Package Manager Console)
- try login using Username: Admin ; Password:123456

# Releases
- current release version is 1.0.0
