﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace DMX.Core.Api.Models.Foundations.LabWorkflowEvent.Exceptions
{
    public class LabWorkflowEventValidationException : Xeption
    {
        public LabWorkflowEventValidationException(Xeption innerException)
            : base(message: "Lab workflow event validation error occured. Please fix and try again.",
                  innerException)
        { }
    }
}
