﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

/*
    Data contract for transmitting problem objects from the server to the client

    Keith Fletcher
    Apr 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    [DebuggerDisplay("{ToString()}")]
    [DataContract]
    public class ProblemSummary : IProblem
    {
        public ProblemSummary(int problemNumber, string title, string solution, string analysis, string summary)
        {
            ProblemNumber = problemNumber;
            Title = title;
            Solution = solution;
            Analysis = analysis;
            Summary = summary;
        }

        [DataMember]
        public int ProblemNumber { get; private set; }
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public string Solution { get; private set; }
        [DataMember]
        public string Analysis { get; private set; }
        [DataMember]
        public string Summary { get; private set; }

        public int CompareTo(IProblem other)
            => ProblemComparer.Default.Compare(this, other);

        public override string ToString() => $"Problem {ProblemNumber:000}. {Title}. Solution: {Solution}";
    }

    [DataContract]
    public class SolutionResult : ISolutionResult
    {
        public SolutionResult(string solution, TimeSpan solveTime)
        {
            Solution = solution;
            SolveTime = solveTime;
        }

        [DataMember]
        public string Solution { get; private set; }
        [DataMember]
        public TimeSpan SolveTime { get; private set; }
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
