``` powershell
dotnet pack Daeha.Shadrazor\Daeha.Shadrazor.csproj -c Release
```

``` powershell
dotnet nuget push nupkgs\Daeha.Shadrazor.*.nupkg `
  --api-key YOUR_API_KEY `
  --source https://api.nuget.org/v3/index.json
```
