# Checkout PaymentGateway Assessment

## Project Information

This assessment contains 2 main parts: 
- Payment Gateway System
  Allow Merchants to create payments, submit payments and get payments list.
- Mocked Bank Payment Processor 
  The moked Bank Payment Processor is simulating credit card payment to the bank.


## Future Improvments

- Repositories & Services could be separated (read/write repo and service) so that these are also connected to different Databases. For ınstance, queries could read from Db like nosql, and we can keep Sql to write our data.
- A queue or background processing jobs for future-payment api method to trigger payment for non-realtime use could be useful.
- Constants could be used instead of hard coded error messages.


## Project Architecture : 
![Project_Diagram](https://user-images.githubusercontent.com/34062320/115612038-1d72a900-a2eb-11eb-9be6-99edba9c0797.png)

### Technologies
* .NET Core 3.1
* Entity Framework Core
* Mssql
* XUnit
* Docker

### Libraries

**EntityFrameworkCore**: It has been used as ORM framework.

Documentation: [https://docs.microsoft.com/en-us/ef/core/](https://docs.microsoft.com/en-us/ef/core/)

**MediatR**: It has been used for reduce dependencies between objects.

Documentation: [https://github.com/jbogard/MediatR/wiki](https://github.com/jbogard/MediatR/wiki)

**System.IdentityModel.Tokens.Jwt(JWT)**: It has been used for authentication.

Documentation: [https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt](https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.jwt)

**FluentValidation:** It has been used for validation of commands and queries.

Documentation: [https://github.com/FluentValidation/FluentValidation](https://github.com/FluentValidation/FluentValidation)

**AutoMapper**: It has been used for object-to-object mapping from entity to Dto or ViewModels.

Documentation: [https://docs.automapper.org/en/stable/](https://docs.automapper.org/en/stable/)

**Newtonsoft:** It has been used for json convert from enum.

Documentation: [https://www.newtonsoft.com/json/help/html/Introduction.htm](https://www.newtonsoft.com/json/help/html/Introduction.htm)

**Swagger:** It has been used for api documentation and structure.

Documentation: [https://swagger.io/docs/](https://swagger.io/docs/)

**FluentValidation:** It has been used for improve testing readability.

Documentation: [https://fluentassertions.com/introduction](https://github.com/fluentassertions/fluentassertions)**

**xUnit:** It has been used for unit tests.

Documentation: [https://xunit.net/docs/getting-started/netcore/cmdline](https://xunit.net/docs/getting-started/netcore/cmdline)**

**EntityFrameworkCore.InMemory**: It has been used to create mock database.

Documentation: [https://docs.microsoft.com/en-us/ef/core/providers/in-memory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory)

**Moq**: It has been used to create mock objects.

Documentation: [https://documentation.help/Moq/](https://documentation.help/Moq/)
