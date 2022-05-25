﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Core.Api.Models.External.ExternalLabs;

namespace DMX.Core.Api.Brokers.ReverbApis
{
    public partial class ReverbApiBroker
    {
        public async ValueTask<ExternalLabsCollection> GetAvailableDevicesAsync(
            ExternalLabsServiceInformation externalLabsServiceInformation)
        {
            const string RelativeUrl = "api/GetAvailableDevicesAsync";

            return await this.PostAync<ExternalLabsServiceInformation, ExternalLabsCollection>(
                relativeUrl: $"{RelativeUrl}?code={this.accessKey}",
                content: externalLabsServiceInformation);
        }
    }
}