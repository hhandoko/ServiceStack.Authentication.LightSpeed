ServiceStack.Authentication.LightSpeed is a [LightSpeed ORM](http://www.mindscapehq.com/products/lightspeed) provider for ServiceStack authentication.

## Prerequisites and Installation
The library does not ship with LightSpeed ORM binaries, and it expects the binaries to be available at the default installation path: `C:\Program Files (x86)\Mindscape\Bin\`.

It is currently built for:
  * .NET 4.5
  * Mindscape LightSpeed 5
  * ServiceStack 4

It is available from NuGet, either via the GUI or running the following command from the Package Manager console:

```
PM> Install-Package ServiceStack.Authentication.LightSpeed
```

**Dependencies:**

  * ServiceStack (>= 4.0)

## Configuration
The following is an example for ASP.NET web applications. Set the unit of work context and scope in `Global.asax.cs`,

```C#
authContext = new LightSpeedContext<DataModelUnitOfWork>;
authScope = new PerRequestUnitOfWorkScope<DataModelUnitOfWork>(authContext);
```

and initialise it as normal via `AppHost.Init()` in `AppHost.cs`.

```C#
container.Register<IUserAuthRepository>(c =>
    new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>())).ReusedWithin(ReuseScope.Request);
```

By default, it will use the standard `JsvStringSerializer` to save complex object (e.g. roles and permissions in UserAuth).

In order to use other `IStringSerializer`, define it within the custom `UserAuthModelUnitOfWorkFactory`. The following is an example found in the [unit test](/blob/master/tests/ServiceStack.Authentication.LightSpeedTests/LightSpeedAuthProviderReadCompatibilityTest.cs):

```C#
authContext =
    new LightSpeedContext<UserAuthModelUnitOfWork>
        {
            ConnectionString = dbConnStr,
            DataProvider = DataProvider.SQLite3,
            UnitOfWorkFactory = new UserAuthModelUnitOfWorkFactory(new JsvStringSerializer()),
            IdentityMethod = IdentityMethod.IdentityColumn
        };
```

Please read the following [ServiceStack release notes](https://github.com/ServiceStack/ServiceStack/blob/081842d11c5dcf304a89f65e4491a9e92718d038/release-notes.md#pluggable-complex-type-serializers) for further information on pluggable complex type serializers.

Also note the use of `IdentityMethod.IdentityColumn` for identity generation method. Use this option to preserve full compatibility with OrmLite, as by default, LightSpeed will use the `KeyTable` implementation. More information on various LightSpeed identity generation can be found on [this page](http://www.mindscapehq.com/documentation/lightspeed/Controlling-the-Database-Mapping/Identity-Generation).

## Usage
The library implements ServiceStack's `IUserAuthRepository` interface (including `IManageRoles` for role management). Thus, the usage shall be no different from the standard `OrmLiteAuthRepository`.

```C#
...
var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);
...
```

```C#
...

this.OrmLiteRepository.AssignRoles(ormLiteUser, roles: new Collection<string> { "SuperAdmin" });
this.LightSpeedRepository.AssignRoles(lightspeedUser, roles: new Collection<string> { "SuperAdmin" });
...
```

Please note that at this time, role addition and removal is only supported through the repository class. Running collection methods over the UserAuth's `Roles` and `Permissions` properties (e.g. `userAuth.Roles.Add(newRole)`) will not persist the changes.    

## Contributors
Please refer to the following [page](/blob/master/Contributors.md) for a complete list of all contributors.

## Contributing
Pull requests shall be made against `develop` branch, which will be reviewed for merging into the `master` branch.

## Copyright and License
  * Code and documentation copyright ServiceStack.Authentication.LightSpeed [contributors](/blob/master/Contributors.md).
  * Code released under [BSD3 license](/blob/master/License.txt).
  * Documentation released under [Creative Commons license](/blob/master/LicenseDocs.txt).

## TODO
The following tasks shall be completed for version 1.0 milestone:
  * Complete read and write compatibility unit tests
  * Create repository constructors based on IDbConnection to keep similar signatures with OrmLite 