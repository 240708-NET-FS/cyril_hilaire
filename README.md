# Project Title

This project is a sample of CRUD (Create, Read, Update, Delete) using C# and Entity Framework.


# Environment Variables

To run this project, you will need to add the following file 'appsettings.json' to './CSharpContactBook/ContactBook/ContactBook'. And add your connection string.

```bash
  {
    "ConnectionStrings": {
      "DefaultConnection": "ADD_YOUR_CONNECTION_STRING_HERE"
    }
}
```

## Run Locally

Clone the project

```bash
  git clone https://github.com/cmhilaire/CSharpContactBook.git
```

Go to the project directory

```bash
  cd CSharpContactBook/ContactBook
```

Install dependencies

```bash
  dotnet add package Microsoft.EntityFrameworkCore 
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer 
  dotnet add package Microsoft.EntityFrameworkCore.Tools 
  dotnet add package Microsoft.EntityFrameworkCore.Design 
  dotnet add package Microsoft.Extensions.Configuration 
  dotnet add package Microsoft.Extensions.Configuration.Json
```

Start the server

```bash
  dotnet run
```


## Running Tests

To run tests, run the following command

```bash
  dotnet test ./ContactBook.Tests
```


## Features

- Add contact
- View contact
- Search contact
- Edit contact
- Delete contact


## Authors

- [@cmhilaire](https://www.github.com/cmhilaire)