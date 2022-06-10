﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using DMX.Core.Api.Models.Externals.ExternalLabs;

namespace DMX.Core.Api.Brokers.LabApis
{
    public partial class LabApiBroker
    {
        public async ValueTask<ExternalLabCollection> GetAvailableLabsAsync(
            ExternalLabServiceInformation externalLabServiceInformation)
        {
            const string RelativeUrl = "api/GetAvailableDevicesAsync";

            return await this.PostAync<ExternalLabServiceInformation, ExternalLabCollection>(
                relativeUrl: $"{RelativeUrl}?code={this.accessKey}",
                content: externalLabServiceInformation);
        }
    }
}