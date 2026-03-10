# Nuget Upload

## Mac/Linux
``` zsh
dotnet pack src/Daeha.RnUI/Daeha.RnUI.csproj -c Release
```

``` zsh
dotnet nuget push src/nupkgs/*.nupkg \
  --api-key YOUR_API_KEY \
  --source https://api.nuget.org/v3/index.json
```

## Windows
``` powershell
dotnet pack src\Daeha.RnUI\Daeha.RnUI.csproj -c Release
```

``` powershell
dotnet nuget push src\nupkgs\Daeha.RnUI.*.nupkg `
  --api-key YOUR_API_KEY `
  --source https://api.nuget.org/v3/index.json
```
