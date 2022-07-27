﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using DMX.Core.Api.Models.Foundations.Labs;
using DMX.Core.Api.Models.Foundations.Labs.Exceptions;
using DMX.Core.Api.Models.Orchestrations.Labs.Exceptions;
using DMX.Core.Api.Services.Orchestrations;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace DMX.Core.Api.Controllers
{
#if RELEASE
    [Authorize]
#endif
    [ApiController]
    [Route("api/[controller]")]
    public class LabsController : RESTFulController
    {
        private readonly ILabOrchestrationService labOrchestrationService;

        public LabsController(ILabOrchestrationService labOrchestrationService) =>
            this.labOrchestrationService = labOrchestrationService;

        [HttpPost]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:PostLab")]
#endif
        public async ValueTask<ActionResult<Lab>> PostLabAsync(Lab lab)
        {
            try
            {
                Lab addedLab =
                    await this.labOrchestrationService.AddLabAsync(lab);

                return Created(addedLab);
            }
            catch (LabValidationException labValidationException)
            {
                return BadRequest(labValidationException.InnerException);
            }
            catch (LabDependencyException labDependencyException)
            {
                return InternalServerError(labDependencyException);
            }
            catch (LabDependencyValidationException labDependencyValidationException)
                when (labDependencyValidationException.InnerException is AlreadyExistsLabException)
            {
                return Conflict(labDependencyValidationException.InnerException);
            }
            catch (LabDependencyValidationException labDependencyValidationException)
            {
                return BadRequest(labDependencyValidationException.InnerException);
            }
            catch (LabServiceException labServiceException)
            {
                return InternalServerError(labServiceException);
            }
        }

        [HttpGet]
#if RELEASE
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:GetAllLabs")]
#endif
        public async ValueTask<ActionResult<List<Lab>>> GetAllLabsAsync()
        {
            try
            {
                List<Lab> allLabs =
                    await this.labOrchestrationService.RetrieveAllLabsAsync();

                return Ok(allLabs);
            }
            catch (LabOrchestrationDependencyException labOrchestrationDependencyException)
            {
                return InternalServerError(labOrchestrationDependencyException);
            }
            catch (LabOrchestrationServiceException labOrchestrationServiceException)
            {
                return InternalServerError(labOrchestrationServiceException);
            }
        }
    }
}
