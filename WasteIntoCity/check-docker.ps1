if (-not (Get-Command docker -ErrorAction SilentlyContinue)) {
    Write-Host "Docker не установлен." -ForegroundColor Red
    exit 1
}

if (-not (Get-Command docker-compose -ErrorAction SilentlyContinue)) {
    Write-Host "Docker Compose не установлен." -ForegroundColor Red
    exit 1
}

Write-Host "Docker и Docker Compose найдены." -ForegroundColor Green
exit 0