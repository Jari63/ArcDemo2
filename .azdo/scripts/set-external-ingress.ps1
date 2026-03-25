# Get the resource group and container app name
$resourceGroup = azd env get-value AZURE_RESOURCE_GROUP
$webAppName = "web"

Write-Host "Updating Container App ingress to external..."
Write-Host "Resource Group: $resourceGroup"
Write-Host "Container App: $webAppName"

# Update the ingress to external
az containerapp ingress update `
  --name $webAppName `
  --resource-group $resourceGroup `
  --type external `
  --allow-insecure false `
  --target-port 8080

Write-Host "✅ Ingress updated to external"