dotnet pack lgp/lgp.csproj /p:PackageVersion=1.0.2 --configuration release --include-source --output ../build
nuget push build/lgp.1.0.2.nupkg -Source https://www.nuget.org/api/v2/package