# Obter o caminho do diretório atual
$caminhoAtual = Get-Location
######## Barrier System ################

$caminhoProjeto = Join-Path $caminhoAtual "barrier-system-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd barrier-system-frontend; ng serve --port 4201 --configuration=localhost5002`"" -WindowStyle Maximized

$caminhoProjeto = Join-Path $caminhoAtual "barrier-system-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd barrier-system-frontend; ng serve --port 4203 --configuration=localhost6002`"" -WindowStyle Maximized


######## Display System ################

$caminhoProjeto = Join-Path $caminhoAtual "display-system-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd display-system-frontend; ng serve --port 4202 --configuration=localhost5003`"" -WindowStyle Maximized

$caminhoProjeto = Join-Path $caminhoAtual "display-system-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd display-system-frontend; ng serve --port 4204 --configuration=localhost6003`"" -WindowStyle Maximized

######## MobileApp System ################

$caminhoProjeto = Join-Path $caminhoAtual "park20-mobileapp-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd park20-mobileapp-frontend; ng serve --port 4200`"" -WindowStyle Maximized

######## BackOffice UI System ################

$caminhoProjeto = Join-Path $caminhoAtual "backoffice-system-frontend\backoffice-system-frontend"
Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd backoffice-system-frontend; ng serve --port 4300`"" -WindowStyle Maximized


######## Display System Api ################
$caminhoProjeto = Join-Path $caminhoAtual "Park20.DisplaySystem.Api\Park20.DisplaySystem.Api"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run --urls `"http://localhost:5003`"`"" -WindowStyle Maximized
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run --urls `"http://localhost:6003`"`"" -WindowStyle Maximized

######## Barrier System Api ################

$caminhoProjeto = Join-Path $caminhoAtual "Park20.BarrierSystem.Api"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run --urls `"http://localhost:5002`"`"" -WindowStyle Maximized
Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run --urls `"http://localhost:6002`"`"" -WindowStyle Maximized

######## BackOffice ################
$caminhoProjeto = Join-Path $caminhoAtual "Park20.Backoffice.Api\Park20.Backoffice.Api"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run`"" -WindowStyle Maximized

########## Payment Simulation ################
$caminhoProjeto = Join-Path $caminhoAtual "PaymentSimulation\PaymentSimulation"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run`"" -WindowStyle Maximized

########## License Plate Reader Entry ################
$caminhoProjeto = Join-Path $caminhoAtual "Park20.LicensePlateReaderEntry.Api\Park20.LicensePlateReaderEntry.Api"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run`"" -WindowStyle Maximized

########## License Plate Reader Exit ################
$caminhoProjeto = Join-Path $caminhoAtual "Park20.LicensePlateReaderExit.Api\Park20.LicensePlateReaderExit.Api"

Write-Host "Abrindo nova janela do PowerShell para $caminhoProjeto"

Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -Command `"cd '$caminhoProjeto'; dotnet run`"" -WindowStyle Maximized

