# Logicea cards

## How to run the project

### Macbook & linux
The docker-compose file created is suitable to be run on Macbook and linux and might not work as expected on windows.

Navigate to the root of the project and run i.e Cards folder and run the following command:

```docker-compose up -d```

This will spin up the containers including the database and seq

You can then access the application via:

```http://localhost:8080/swagger/index.html```

### Windows

Requires ```.net8 sdk``` installed

This project is best suited for MSSQL database because it has a stored procedure that is best translates by mssql.

To run this, open ```AppSettings.Development.json``` and replace the value ``DATABASE_CONNECTION`` with your mssql connection string.

#### Example
From : 
```"DATABASE_CONNECTION": "Server=host.docker.internal,1433;Database=Cards;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;",```

To:
```"DATABASE_CONNECTION": "Server=localhost,1433;Database=Cards;TrustServerCertificate=True;",```

After making the above change, open your terminal and navigate to `Cards/Cards.Api/` and run the following command:

`dotnet run`

Access the APIs documentation by navigating to: ```https://localhost:7200/swagger/index.html```

## Test users

The system by default creates 4 users: 1 admin user and 3 member users all with password:

```Password@123```:


### Admin Users

```
1. admin@cards.dev
```

### Member Users

```
1. user@cards.dev
2. user.test@cards.dev
3. test.user@cards.dev
```

🥂