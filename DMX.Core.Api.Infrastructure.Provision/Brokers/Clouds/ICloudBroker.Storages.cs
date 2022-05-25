﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;

namespace DMX.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    internal partial interface ICloudBroker
    {
        ValueTask<ISqlServer> CreateSqlServerAsync(
            string sqlServerName,
            IResourceGroup resourceGroup);
    }
}
