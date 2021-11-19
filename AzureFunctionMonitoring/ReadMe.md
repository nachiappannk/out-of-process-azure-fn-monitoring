# Prerequisites
 * Download and instal Azure CLI. Refer [here](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
 * Download and install docker desktop
 * Login to Azure with command `az login`
 * Choose the right subscription with command `az account set -s 4ed70c4e-8b24-431b-875b-9d22e7785e77`
 * Verify the correct susciption is choosed by `az account show`
 
# Create registry
 * create Resource group `az group create --name nachiacrrg --location eastus`
 * create ACR `az acr create -n nachiacrdemo -g nachiacrrg --sku Standard`
 * Provide admin access `az acr update -n nachiacrdemo --admin-enabled true`
 * Get the container registry's password by `az acr credential show --name nachiacrdemo`
 
# Setting variable
 * `set DOCKER_FILE_DIR=AzureFunctionMonitoring`
 * `set FEATURE_NAME=nachiazurfnmon`
 * `set LOCATION=eastus`
 * `set RG_NAME=nachiFunctionTestRg`
 * `set STORAGE_NAME=%FEATURE_NAME%storage` 
 * `set PLAN_NAME=%FEATURE_NAME%FunctionPlan`
 * `set IMAGE_NAME=%FEATURE_NAME%:<version>`
 * `set CONTAINER_REG_PASSWORD=<password>` 

# Steps to publish the docker container
 * From the directory ~\AzureFunctionMonitoring\ the command `docker build -f .\%DOCKER_FILE_DIR%\Dockerfile -t nachiacrdemo.azurecr.io/%IMAGE_NAME% .`
 * Login to the container registry by `docker login nachiacrdemo.azurecr.io -u nachiacrdemo -p %CONTAINER_REG_PASSWORD%`
 * Push the container to registry by `docker push nachiacrdemo.azurecr.io/%IMAGE_NAME%`
 
# One time deployment steps
 * Create resource group by `az group create --name %RG_NAME% --location %LOCATION%`
 * Create storage account with `az storage account create --name %STORAGE_NAME% --location eastus --resource-group %RG_NAME% --sku Standard_LRS`
 * Create function plan `az functionapp plan create -g %RG_NAME% -n %PLAN_NAME% --sku EP1 --is-linux true`

# Updating code and deploying function
 * Deploy the image in registry `az functionapp create --resource-group %RG_NAME% -p %PLAN_NAME% --deployment-container-image-name nachiacrdemo.azurecr.io/%IMAGE_NAME% --docker-registry-server-password %CONTAINER_REG_PASSWORD% --docker-registry-server-user nachiacrdemo --runtime dotnet-isolated --runtime-version 5.0 --functions-version 3 --name %FEATURE_NAME% --storage-account %STORAGE_NAME%`
 
# Run the query
```
requests 
| project ['HttpPath'] = tostring(customDimensions['HttpPath']), ['HttpMethod']= tostring(customDimensions["HttpMethod"]),  resultCode
| summarize Count=count() by HttpMethod, HttpPath, resultCode
```