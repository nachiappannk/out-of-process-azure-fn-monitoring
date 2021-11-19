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
 
 
 