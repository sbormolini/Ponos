@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location

@description('Timestamp for deployment name')
param timeStamp string = utcNow()

module store 'modules/cdb-serverless-rbac.bicep' = {
  name: 'cosmosDB-deployment-${timeStamp}'
  params: {
    accountName: 'cdb-bos-biceptest-chn-dev01'
    databaseName: 'cosmicworks'
    containerName: 'products'
    roleDefinitionId: '/subscriptions/2bce3d0d-b6bd-4306-9f9e-bc3336d91fbd/resourceGroups/arg-bos-biceptest-chn-dev01/providers/Microsoft.DocumentDB/databaseAccounts/cdb-bos-biceptest-chn-dev01/sqlRoleDefinitions/00000000-0000-0000-0000-000000000002'
    principalId: 'd50b258a-5a36-4ac0-8e40-de524f3032dd'
    location: location
  }
}

module container 'modules/container-instance.bicep' = {
  name: 'container-deployment-${timeStamp}'
  params: {
    name: 'aci-bos-biceptest-chn-dev01'
    image: 'sbormolini/azfuncdockersample:latest'
    location: location
  }
}
