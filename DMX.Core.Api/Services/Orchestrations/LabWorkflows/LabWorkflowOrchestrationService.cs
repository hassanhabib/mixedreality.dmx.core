﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using DMX.Core.Api.Brokers.Loggings;
using DMX.Core.Api.Models.Foundations.LabWorkflows;
using DMX.Core.Api.Services.Foundations.LabWorkflowCommands;
using DMX.Core.Api.Services.Foundations.LabWorkflowEvents;
using DMX.Core.Api.Services.Foundations.LabWorkflows;

namespace DMX.Core.Api.Services.Orchestrations.LabWorkflows
{
    public partial class LabWorkflowOrchestrationService : ILabWorkflowOrchestrationService
    {
        private readonly ILabWorkflowService labWorkflowService;
        private readonly ILabWorkflowCommandService labWorkflowCommandService;
        private readonly ILabWorkflowEventService labWorkflowEventService;
        private readonly ILoggingBroker loggingBroker;

        public LabWorkflowOrchestrationService(
            ILabWorkflowService labWorkflowService,
            ILabWorkflowCommandService labWorkflowCommandService,
            ILabWorkflowEventService labWorkflowEventService,
            ILoggingBroker loggingBroker)
        {
            this.labWorkflowService = labWorkflowService;
            this.labWorkflowCommandService = labWorkflowCommandService;
            this.labWorkflowEventService = labWorkflowEventService;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<LabWorkflow> SubmitLabWorkflowAsync(LabWorkflow labWorkflow)
        {
            await this.labWorkflowService.AddLabWorkflowAsync(labWorkflow);

            labWorkflow.Commands.ForEach(async command =>
                await this.labWorkflowCommandService.AddLabWorkflowCommandAsync(command));

            return await this.labWorkflowEventService.AddLabWorkflowEventAsync(labWorkflow);
        } 


        public ValueTask<LabWorkflow> RetrieveLabWorkflowByIdAsync(Guid labWorkflowId) =>
        TryCatch(async () =>
        {
            ValidateLabWorkflowId(labWorkflowId);

            return await this.labWorkflowService.RetrieveLabWorkflowByIdAsync(labWorkflowId);
        });
    }
}
