﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

using EnvDTE;

using Moq;

namespace EnvDTE80
{
    internal static class DteFactory
    {
        public static DTE Create()
        {
            var mock = new Mock<DTE2>();

            return mock.As<DTE>().Object;
        }

        public static DTE2 ImplementSolution(Func<Solution> action)
        {
            var mock = new Mock<DTE2>();
            mock.As<DTE>();

            mock.SetupGet(m => m.Solution)
                .Returns(action);

            return mock.Object;
        }
    }
}
