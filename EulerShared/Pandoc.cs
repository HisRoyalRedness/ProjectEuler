using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    internal static class PandocExtensions
    {
        static readonly string PANDOC_EXE = "pandoc.exe";
        static readonly string PANDOC_TEMPLATE = @"pandoc_html.template";

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

        public static string ConvertToHtmlAsync(this string rawText)
        {
            string inFile = null;
            string outFile = null;
            string content = null;
            try
            {
                inFile = Path.GetTempFileName();
                outFile = Path.GetTempFileName();
                File.WriteAllText(inFile, rawText);

                var psi = new ProcessStartInfo(PANDOC_EXE)
                {
                    Arguments = $"--mathml -s --template={PANDOC_TEMPLATE} -o \"{outFile}\" \"{inFile}\"",
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    CreateNoWindow = true,
                };


                var p = Process.Start(psi);
                p.WaitForExit(10000);
                content = File.ReadAllText(outFile);
            }
            finally
            {
                if (inFile != null && File.Exists(inFile))
                    File.Delete(inFile);
                if (outFile != null && File.Exists(outFile))
                    File.Delete(outFile);
            }
            return content;
        }

        static string MarkdownHeading(this string text, int level = 1)
            => $"{text}\n{new string((level == 1 ? '=' : '-'), text.Length)}\n";

    }
}
