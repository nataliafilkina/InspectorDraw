using System;
using UnityEngine;

namespace InspectorDraw
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class SetConstantValueIfAttribute : PropertyAttribute
    {
        #region Fields
        public string comparedPropertyName { get; private set; }
        public object comparedValue { get; private set; }
        public object defaultValue { get; private set; }
        public bool readOnly { get; private set; }

        #endregion

        /// <summary>
        /// Sets a value in a field only if a condition is met. Supports int and string.
        /// </summary>
        /// <param name="comparedPropertyName">The name of the property that is being compared (case sensitive).</param>
        /// <param name="comparedValue">The value the property is being compared to.</param>
        /// <param name="defaultValue">The value to be set in the field.</param>
        /// <param name="readOnly">Field display type in case of successful installation. Defaulted to true.</param>
        public SetConstantValueIfAttribute(string comparedPropertyName, object comparedValue, object defaultValue, bool readOnly = true)
        {
            this.comparedPropertyName = comparedPropertyName;
            this.comparedValue = comparedValue;
            this.readOnly = readOnly;
            this.defaultValue = defaultValue;
        }
    }
}
