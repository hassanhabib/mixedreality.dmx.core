﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace DMX.Core.Api.Models.Foundations.LabCommands.Exceptions
{
    public class LabCommandValidationException : Xeption
    {
        public LabCommandValidationException(Xeption exception)
            : base(message: "Lab command validation error occured.", exception)
        { }
    }
}
