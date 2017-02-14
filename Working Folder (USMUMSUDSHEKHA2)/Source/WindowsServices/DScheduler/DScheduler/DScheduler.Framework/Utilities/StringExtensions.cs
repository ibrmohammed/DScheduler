using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    public static class StringExtensions
    {
        public static string AsFriendlyName(this string code)
        {
            return Regex.Replace(
                Regex.Replace(
                    code,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                    ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
                );
        }
    }
}
