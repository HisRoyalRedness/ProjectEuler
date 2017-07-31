using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

/*
    Base class for problem descriptions
    
    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [DebuggerDisplay("{_debugDisplay}")]
    public abstract class ProblemBase : IExportedProblem
    {
        protected ProblemBase()
        {
            var name = this.GetType().Name;
            var match = _problemNumberRegex.Match(name);
            if (!match.Success)
                throw new ArgumentException($"Could not determine the problem number for '{name}'.");
            _problemNumber = int.Parse(match.Value);
            _title = this.GetAttributes<TitleAttribute>()?.FirstOrDefault()?.Title;
            _solution = this.GetAttributes<SolutionAttribute>()?.FirstOrDefault()?.Solution;
            _summary = (this.GetAttributes<SummaryAttribute>()?.FirstOrDefault()?.Summary ?? "No summary").GenerateSummaryText(_problemNumber, _title, _solution);
            _analysis = (this.GetAttributes<AnalysisAttribute>()?.FirstOrDefault()?.Analysis ?? "No analysis").GenerateAnalysisText(_problemNumber);
            _debugDisplay = $"Problem {_problemNumber:000}: {_title}";
        }

        public int ProblemNumber => _problemNumber;
        public string Title => _title;
        public string Solution => _solution;
        public string Analysis => _analysis;
        public string Summary => _summary;
        public string Solve() => InternalSolve();
        public int CompareTo(IProblem other) => ProblemComparer.Default.Compare(this, other);

        public override string ToString() => _debugDisplay;
        protected abstract string InternalSolve();

        readonly int _problemNumber = 0;
        readonly string _title = string.Empty;
        readonly string _solution = string.Empty;
        readonly string _analysis = string.Empty;
        readonly string _summary = string.Empty;
        readonly string _debugDisplay;
        readonly Regex _problemNumberRegex = new Regex(@"(?<=^problem)\d+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}

/*
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
*/
