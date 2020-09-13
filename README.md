
# Part 1

The response is under folder path :`Part 1/Part 1_Response.pdf`

# Part 2
# Pokemon Retrieve Shakespeare Description
A Pokemon API in a Shakespeare flavour

# Endpoint

`GET /pokemon/{name}` => 
Get pokemon description in a Shakespeare flavour

# Documentation

Please find endpoint documentation via swagger page (`localhost:5000/swagger`).

# Prerequisites

* `dotnet-core/2.2` runtime (download at https://dotnet.microsoft.com/download/dotnet-core/2.2) [hint: search for "v2.2.0", under SDK 2.2.100, choose your OS and load the corresponding installer]
* `docker desktop` (if run in container) at https://www.docker.com/products/docker-desktop
* `docker-compose` (if run in container) at https://docs.docker.com/compose/install/

# Run via current host

Clone the master branch of this repository into your local system

	`git clone https://github.com/irachitrastogi/Retrieve-Shakespeare-Description.git`

Browse to `./src/PokeSpeare.Api` folder then run commands

	`dotnet build`  
	`dotnet run`

Application should start listening on `localhost:5000`
Use a browser to see some pokemon description in Shakespeare style

`http://localhost:5000/pokemon/charizard`

`http://localhost:5000/pokemon/snorlax`

`http://localhost:5000/pokemon/ditto`

# Run via docker container

Browse to `./src/PokeSpeare.Api` folder then run command
 
	`docker-compose build`  
	`docker-compose up`

Application should start listening on `localhost:5000`

# Run tests

Browse to root folder then run 

`dotnet test`

Unit and Integration tests should run (and pass!)

# Underlying APIs

This project uses the external APIs:

* https://pokeapi.co/docs/v2.html/ (max 100 request/minute)
* https://funtranslations.com/api/shakespeare (max 5 request/hour - 60/day)



