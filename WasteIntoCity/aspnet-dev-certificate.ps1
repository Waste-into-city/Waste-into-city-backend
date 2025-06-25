$certPath = "$env:USERPROFILE\.aspnet\https"
$pfxFile = "$certPath\aspnetapp.pfx"
$password = "YourSecurePassword"

if (-Not (Test-Path $certPath)) {
    New-Item -ItemType Directory -Path $certPath | Out-Null
}

Write-Host "Создание HTTPS-сертификата..."
dotnet dev-certs https --clean
dotnet dev-certs https -ep $pfxFile -p $password
dotnet dev-certs https --trust
