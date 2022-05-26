﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Core.Api.Infrastructure.Provision.Brokers.Clouds;
using DMX.Core.Api.Infrastructure.Provision.Brokers.Loggings;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace DMX.Core.Api.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();

            this.loggingBroker.LogActivity(
                message: $"Starting provisioning {resourceGroupName} ...");

            IResourceGroup resourceGroup =
                await this.cloudBroker.CreateResourceGroupAsync(
                    resourceGroupName);

            this.loggingBroker.LogActivity(
                message: $"Provisioning {resourceGroupName} Completed.");

            return resourceGroup;
        }
    }
}
