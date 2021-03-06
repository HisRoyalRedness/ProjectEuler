﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Common extension methods used throughout the Euler solution

    Keith Fletcher
    Mar 2017

    This file is Unlicensed.
    See the foot of the file, or refer to <http://unlicense.org>
*/

namespace HisRoyalRedness.com
{
    internal static class TypeExtensions
    {
        internal static TAttribute[] GetAttributes<TAttribute>(this object obj)
            where TAttribute : Attribute
            => obj.GetType()
                .GetCustomAttributes(typeof(TAttribute), true)
                .Select(a => (TAttribute)a)
                .ToArray();
    }

    internal static class MiscExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        internal static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            return items.Select(i =>
            {
                action(i);
                return i;
            });
        }

        internal static IEnumerable<TOut> Do<TIn, TOut>(this IEnumerable<TIn> items, Func<TIn, TOut> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));            
            return items.Select(i => action(i));
        }

        internal static T Tap<T>(this T item, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            action(item);
            return item;
        }
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
