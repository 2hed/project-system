﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

using Xunit;

namespace Microsoft.VisualStudio.ProjectSystem.References
{
    [Trait("UnitTest", "ProjectSystem")]
    public class AlwaysAllowValidProjectReferenceCheckerTests
    {

        [Fact]
        public void CanAddProjectReferenceAsync_NullAsReferencedProject_ThrowsArgumentNull()
        {
            var checker = CreateInstance();

            Assert.Throws<ArgumentNullException>("referencedProject", () =>
            {

                checker.CanAddProjectReferenceAsync((object)null);
            });

        }

        [Fact]
        public void CanAddProjectReferencesAsync_NullAsReferencedProjects_ThrowsArgumentNull()
        {
            var checker = CreateInstance();

            Assert.Throws<ArgumentNullException>("referencedProjects", () =>
            {

                checker.CanAddProjectReferencesAsync((IImmutableSet<object>)null);
            });
        }

        [Fact]
        public void CanAddProjectReferencesAsync_EmptyAsReferencedProjects_ThrowsArgument()
        {
            var checker = CreateInstance();

            var referencedProjects = ImmutableHashSet<object>.Empty;

            Assert.Throws<ArgumentException>("referencedProjects", () =>
            {

                checker.CanAddProjectReferencesAsync(ImmutableHashSet<object>.Empty);
            });
        }

        [Fact]
        public void CanBeReferencedAsync_NullAsReferencingProject_ThrowsArgumentNull()
        {
            var checker = CreateInstance();

            Assert.Throws<ArgumentNullException>("referencingProject", () =>
            {

                checker.CanBeReferencedAsync((object)null);
            });
        }

        [Fact]
        public async Task CanAddProjectReferenceAsync_ReturnsSupported()
        {
            var project = new object();
            var checker = CreateInstance();

            var result = await checker.CanAddProjectReferenceAsync(project);

            Assert.Equal(SupportedCheckResult.Supported, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task CanAddProjectReferencesAsync_ReturnsErrorMessageSetToNull(int count)
        {
            var checker = CreateInstance();
            var referencedProjects = CreateSet(count);

            var result = await checker.CanAddProjectReferencesAsync(referencedProjects);

            Assert.Null(result.ErrorMessage);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task CanAddProjectReferencesAsync_ReturnsAsManyIndivualResultsAsProjects(int count)
        {
            var checker = CreateInstance();
            var referencedProjects = CreateSet(count);

            var result = await checker.CanAddProjectReferencesAsync(referencedProjects);

            Assert.Equal(result.IndividualResults.Keys, referencedProjects);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task CanAddProjectReferencesAsync_ReturnsAllResultsSetToSupported(int count)
        {
            var checker = CreateInstance();
            var referencedProjects = CreateSet(count);

            var result = await checker.CanAddProjectReferencesAsync(referencedProjects);

            Assert.All(result.IndividualResults.Values, r => Assert.Equal(SupportedCheckResult.Supported, r));
        }

        [Fact]
        public async Task CanBeReferencedAsync_ReturnsSupported()
        {
            var project = new object();
            var checker = CreateInstance();

            var result = await checker.CanBeReferencedAsync(project);

            Assert.Equal(SupportedCheckResult.Supported, result);
        }

        private IImmutableSet<object> CreateSet(int count)
        {
            var builder = ImmutableHashSet.CreateBuilder<object>();

            for (int i = 0; i < count; i++)
            {
                builder.Add(new object());
            }

            return builder.ToImmutableHashSet();
        }

        private AlwaysAllowValidProjectReferenceChecker CreateInstance()
        {
            return new AlwaysAllowValidProjectReferenceChecker();
        }
    }
}
