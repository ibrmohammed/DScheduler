using System;

namespace DScheduler.Framework
{
    /// <summary>
    /// Defines additional formatting options for strings.
    /// </summary>
    /// <remarks>
    /// The following table lists the format specifiers supported by this
    /// <see cref="StringFormatInfo"/>. In-line format specifiers take precedence
    /// over related properties.
    /// <list type="table">
    ///		<listheader>
    ///			<term>Format Character</term>
    ///			<description>Description and Associated Properties</description>
    ///		</listheader>
    ///		<item>
    ///			<term>G</term>
    ///			<description>Converts input string to upper case.</description>
    ///		</item>
    ///		<item>
    ///			<term>g</term>
    ///			<description>Converts input string to lower case.</description>
    ///		</item>
    /// </list>
    /// </remarks>
    public sealed class StringFormatInfo : IFormatProvider, ICustomFormatter
    {
        #region IFormatProvider Members

        /// <summary>
        /// Gets an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to get.</param>
        /// <returns>The current instance, if formatType is the same type as the current instance;
        /// otherwise, a null reference (Nothing in Visual Basic).</returns>
        public object GetFormat(Type formatType)
        {
            return typeof(ICustomFormatter) == formatType ? this : null;
        }

        #endregion IFormatProvider Members

        #region ICustomFormatter Members

        /// <summary>
        /// Converts the value of a specified object to an equivalent string
        /// representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> object that supplies
        /// format information about the current instance.</param>
        /// <returns>
        /// If the format specifier is G, the string is returned converted to uppercase.
        /// If the format specifier is g, the string is returned converted to lowercase.
        /// </returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                throw new ArgumentNullException("arg");

            if (format != null && arg is string)
            {
                var temp = (string)arg;
                if (format.StartsWith("G"))
                    return temp.ToUpper();
                if (format.StartsWith("g"))
                    return temp.ToLower();
            }

            var formattable = arg as IFormattable;
            if (formattable != null)
                return formattable.ToString(format, formatProvider);
            return arg.ToString();
        }

        #endregion ICustomFormatter Members
    }
}