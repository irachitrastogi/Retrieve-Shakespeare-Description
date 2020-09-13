FROM microsoft/dotnet:2.2-sdk as build

COPY PokeSpeare.Api.csproj /build/

RUN dotnet restore ./build/PokeSpeare.Api.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
ARG source 	 
WORKDIR /app

COPY --from=build /build/out .
COPY ${source:-obj/Docker/publish} .

ENTRYPOINT ["dotnet", "PokeSpeare.Api.dll"]