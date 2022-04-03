// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.Serialization;

namespace Microsoft.Extensions.Hosting
{
    [Serializable]
    public sealed class HostAbortedException : Exception
    {
        public HostAbortedException() : base() { }
        public HostAbortedException(string message) : base(message) { }
        public HostAbortedException(string message, Exception innerException) : base(message, innerException) { }
        private HostAbortedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
