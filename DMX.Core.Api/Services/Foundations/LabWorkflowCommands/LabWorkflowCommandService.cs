﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using DMX.Core.Api.Brokers.DateTimes;
using DMX.Core.Api.Brokers.Loggings;
using DMX.Core.Api.Brokers.Storages;
using DMX.Core.Api.Models.Foundations.LabWorkflowCommands;
using System;
using System.Threading.Tasks;

namespace DMX.Core.Api.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandService : ILabWorkflowCommandService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public LabWorkflowCommandService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public ValueTask<LabWorkflowCommand> ModifyLabWorkflowCommand(LabWorkflowCommand labWorkflowCommand) =>
        TryCatch(async () =>
        {
            ValidateLabWorkflowCommandOnModify(labWorkflowCommand);

            LabWorkflowCommand maybeLabWorkflowCommand = await this.storageBroker.SelectLabWorkflowCommandByIdAsync(labWorkflowCommand.Id);

            ValidateLabWorkflowCommandAgainstStorageLabWorkflowCommand(
                labWorkflowCommand, maybeLabWorkflowCommand);

            return await this.storageBroker.UpdateLabWorkflowCommandAsync(labWorkflowCommand);
        });
    }
}