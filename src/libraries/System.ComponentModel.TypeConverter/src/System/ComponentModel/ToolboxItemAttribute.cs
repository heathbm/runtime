// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel
{
    /// <summary>
    /// Specifies attributes for a toolbox item.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ToolboxItemAttribute : Attribute
    {
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        private Type? _toolboxItemType;
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        private readonly string? _toolboxItemTypeName;

        private const string DefaultToolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

        /// <summary>
        /// Initializes a new instance of ToolboxItemAttribute and sets the type to
        /// </summary>
        public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute(DefaultToolboxItemTypeName);

        /// <summary>
        /// Initializes a new instance of ToolboxItemAttribute and sets the type to
        /// <see langword='null'/>.
        /// </summary>
        public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);

        /// <summary>
        /// Gets whether the attribute is the default attribute.
        /// </summary>
        public override bool IsDefaultAttribute() => _toolboxItemTypeName == DefaultToolboxItemTypeName;

        /// <summary>
        /// Initializes a new instance of ToolboxItemAttribute and specifies if default values should be used.
        /// </summary>
        public ToolboxItemAttribute(bool defaultType)
        {
            if (defaultType)
            {
                if (!IDesignerHost.IsSupported)
                {
                    throw new NotSupportedException(SR.IDesignerHostNotSupported);
                }

                _toolboxItemTypeName = DefaultToolboxItemTypeName;
            }
        }

        /// <summary>
        /// Initializes a new instance of ToolboxItemAttribute and specifies the name of the type.
        /// </summary>
        public ToolboxItemAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] string toolboxItemTypeName)
        {
            ArgumentNullException.ThrowIfNull(toolboxItemTypeName);

            _toolboxItemTypeName = toolboxItemTypeName;
        }

        /// <summary>
        /// Initializes a new instance of ToolboxItemAttribute and specifies the type of the toolbox item.
        /// </summary>
        public ToolboxItemAttribute([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type toolboxItemType)
        {
            ArgumentNullException.ThrowIfNull(toolboxItemType);

            _toolboxItemType = toolboxItemType;
            _toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
        }

        /// <summary>
        /// Gets the toolbox item's type.
        /// </summary>
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        public Type? ToolboxItemType
        {
            get
            {
                if (_toolboxItemType == null)
                {
                    if (_toolboxItemTypeName != null)
                    {
                        try
                        {
                            _toolboxItemType = Type.GetType(_toolboxItemTypeName, true);
                        }
                        catch (Exception ex)
                        {
                            throw new ArgumentException(SR.Format(SR.ToolboxItemAttributeFailedGetType, _toolboxItemTypeName), ex);
                        }
                    }
                }
                return _toolboxItemType;
            }
        }

        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
        public string ToolboxItemTypeName => _toolboxItemTypeName ?? string.Empty;

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == this)
            {
                return true;
            }

            return (obj is ToolboxItemAttribute other) && (other.ToolboxItemTypeName == ToolboxItemTypeName);
        }

        public override int GetHashCode()
        {
            if (_toolboxItemTypeName != null)
            {
                return _toolboxItemTypeName.GetHashCode();
            }
            return base.GetHashCode();
        }
    }
}
