dotnet sonarscanner begin /k:"Algebra" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_ab5778cb5974b7794a0d454569880d5136e5a4fd" /d:sonar.cs.xunit.reportsPaths=coverage.xml
dotnet build --no-incremental
dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
dotnet sonarscanner end /d:sonar.token="sqp_ab5778cb5974b7794a0d454569880d5136e5a4fd"