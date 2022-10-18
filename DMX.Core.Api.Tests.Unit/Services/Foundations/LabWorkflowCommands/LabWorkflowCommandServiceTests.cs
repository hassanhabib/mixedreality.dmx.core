﻿// ---------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using DMX.Core.Api.Brokers.Loggings;
using DMX.Core.Api.Brokers.Storages;
using DMX.Core.Api.Models.Foundations.LabWorkflowCommands;
using DMX.Core.Api.Services.Foundations.LabWorkflowCommands;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace DMX.Core.Api.Tests.Unit.Services.Foundations.LabWorkflowCommands
{
    public partial class LabWorkflowCommandServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILabWorkflowCommandService labWorkflowCommandService;

        public LabWorkflowCommandServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.labWorkflowCommandService = new LabWorkflowCommandService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }
        
        private static LabWorkflowCommand CreateRandomLabWorkflowCommand() =>
            CreateLabWorkflowCommandFiller(GetRandomDateTimeOffset()).Create();

        private static LabWorkflowCommand CreateRandomLabWorkflowCommand(DateTimeOffset date) =>
            CreateLabWorkflowCommandFiller(date).Create();

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetLocalRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber))
            {
                randomNumber = GetLocalRandomNumber();
            }

            return (T)(object)randomNumber;

            static int GetLocalRandomNumber() =>
                new IntRange(min: int.MinValue, max: int.MaxValue).GetValue();
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static SqlException GetSqlException() =>
           (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        public static TheoryData InvalidSeconds()
        {
            int secondsInPast =
                GetRandomNumberInRange(
                    minValue: 60,
                    maxValue: int.MaxValue) * -1;

            int secondsInFuture =
                GetRandomNumberInRange(
                    minValue: 0,
                    maxValue: int.MaxValue);

            return new TheoryData<int>
            {
                secondsInPast,
                secondsInFuture
            };

            static int GetRandomNumberInRange(int minValue, int maxValue) =>
                new IntRange(minValue, maxValue).GetValue();
        }

        private static Filler<LabWorkflowCommand> CreateLabWorkflowCommandFiller(DateTimeOffset dateTimeOffset)
        {
            var filler = new Filler<LabWorkflowCommand>();

            filler.Setup()
                .OnType<DateTimeOffset>()
                    .Use(dateTimeOffset);

            return filler;
        }
    }
}
