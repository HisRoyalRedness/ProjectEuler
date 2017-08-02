using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/*
    Helper funtions for generating documentation with pandoc
    https://pandoc.org/

    Keith Fletcher
    Aug 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    internal static class PandocExtensions
    {
        static readonly string PANDOC_EXE = "pandoc.exe";
        static readonly string PANDOC_TEMPLATE = @"pandoc_html.template";
        static readonly string PANDOC_ARGS = $"--mathml -s --template={PANDOC_TEMPLATE}";
        //static readonly string PANDOC_ARGS = $"--mathml -s -t html5";

        public static string GenerateSummaryText(this string rawSummary, int problemNumber, string title, string solution)
        {
            var header = $"Problem [{problemNumber}](https://projecteuler.net/problem={problemNumber}) Summary";
            return 
                $"% Problem {problemNumber} Summary\n% Keith Fletcher\n" + 
                $"{header.MarkdownHeading()}\n\n" +
                (string.IsNullOrEmpty(title) ? "" :  $"{title.MarkdownHeading()}\n\n") +
                $"{rawSummary}\n\n" + 
                $"{MarkdownHeading("Solution", 2)}{solution}";
        }

        public static string GenerateAnalysisText(this string rawAnalysis, int problemNumber)
        {
            var header = $"Problem [{problemNumber}](https://projecteuler.net/problem={problemNumber}) Analysis";
            return $"% {header}\n% Keith Fletcher\n" +
                $"{header.MarkdownHeading()}\n" +
                $"{rawAnalysis}\n";
        }

        public static string ConvertToHtml(this string rawText)
        {
            try
            {
                var psi = new ProcessStartInfo(PANDOC_EXE)
                {
                    Arguments = PANDOC_ARGS,
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var proc = Process.Start(psi);

                using (proc.StandardInput)
                    proc.StandardInput.Write(rawText);

                return proc.StandardOutput.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public static async Task<string> ConvertToHtmlAsync(this string rawText)
        {
            try
            {
                var psi = new ProcessStartInfo(PANDOC_EXE)
                {
                    Arguments = PANDOC_ARGS,
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var proc = Process.Start(psi);

                using (proc.StandardInput)
                    await proc.StandardInput.WriteAsync(rawText);

                return await proc.StandardOutput.ReadToEndAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        static string MarkdownHeading(this string text, int level = 1)
            => $"{text}\n{new string((level == 1 ? '=' : '-'), text.Length)}\n";

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
