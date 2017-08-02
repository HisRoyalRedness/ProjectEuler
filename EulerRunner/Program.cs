using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    static class Program
    {
        static readonly ProblemLoader _loader = new ProblemLoader();

        static void Main(string[] args)
        {
            SummaryCompiler();
            ProblemSolver(args);
        }

        static void ProblemSolver(string[] args)
        {
            int problemNumber = 65;

            foreach (var a in args)
                if (int.TryParse(a, out problemNumber))
                    break;

            var problem = _loader.Problems.ContainsKey(problemNumber) ? _loader.Problems[problemNumber] : null;

            Console.WriteLine(
                problem != null
                    ? $"Solution: {problem.Solve()}"
                    : $"Problem {problemNumber} not found...");            
        }

        static void SummaryCompiler()
        {
            const string DOCS_FOLDER = "Documents";

            if (!Directory.Exists(DOCS_FOLDER))
                Directory.CreateDirectory(DOCS_FOLDER);

            var assemblyTimes = new Dictionary<Assembly, DateTime>();

            _loader.Problems
                .Where(p => !string.IsNullOrEmpty(p.Value.Summary))
                .Select(p => p.Value)
                .AsParallel()
                .ForAll(prob =>
                {
                    var createSummary = true;
                    var createanalysis = true;

                    var summaryFile = $"{DOCS_FOLDER}\\Problem{prob.ProblemNumber:000}_Summary.html";
                    var analysisFile = $"{DOCS_FOLDER}\\Problem{prob.ProblemNumber:000}_Analysis.html";

                    if (File.Exists(summaryFile) && File.Exists(analysisFile))
                    {
                        var sourceFile = Directory
                            .GetFiles(@"C:\Users\KeithF\Source\Repos\ProjectEuler\EulerProblems", $"Problem{prob.ProblemNumber:000}.cs", SearchOption.AllDirectories)
                            .FirstOrDefault();

                        if (sourceFile != null)
                        {
                            var sourceFileTime = File.GetLastWriteTimeUtc(sourceFile);
                            createSummary = File.GetLastWriteTimeUtc(summaryFile) <= sourceFileTime;
                            createanalysis = File.GetLastWriteTimeUtc(analysisFile) <= sourceFileTime;
                        }
                    }

                    if (createSummary)
                    {
                        File.WriteAllText(summaryFile, PandocExtensions.ConvertToHtml(prob.Summary));
                        Console.WriteLine($"Compiled summary for problem {prob.ProblemNumber} to '{summaryFile}'.");
                    }
                    if (createanalysis)
                    {
                        File.WriteAllText(analysisFile, PandocExtensions.ConvertToHtml(prob.Analysis));
                        Console.WriteLine($"Compiled analysis for problem {prob.ProblemNumber} to '{analysisFile}'.");
                    }
                });
                
        }

        static DateTime GetLinkerTime(this Assembly assembly, TimeZoneInfo target = null)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                stream.Read(buffer, 0, 2048);

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }
    }
}
