﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;

/*
    MEF composer to find an load all problems that implement IProblem

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    public class ProblemLoader
    {
        public ProblemLoader()
        {
            Compose();
            if (_problemsList != null)
                foreach (var problem in _problemsList)
                    _problems.Add(problem.ProblemNumber, (ISolvableProblem)problem);
        }

        public Dictionary<int, ISolvableProblem> Problems => _problems;

        void Compose()
        {
            try
            {
                AggregateCatalog catalog = null;
                CompositionContainer container = null;

                var dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                using (catalog = new AggregateCatalog(new DirectoryCatalog(dirName)))
                {
                    using (container = new CompositionContainer(catalog))
                        container.ComposeParts(this);
                    container = null;
                }
                catalog = null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [ImportMany(typeof(IExportedProblem))]
        List<IExportedProblem> _problemsList = null;

        readonly Dictionary<int, ISolvableProblem> _problems = new Dictionary<int, ISolvableProblem>();
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
