using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Numerics;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace fletcher.org
{
    [Export(typeof(IProblem))]
    [Solution("107359")]
    class Problem059Generator : ProblemBase
    {
        /// <summary>
        /// http://projecteuler.net/index.php?section=problems&id=59
        /// 
        /// Each character on a computer is assigned a unique code and 
        /// the preferred standard is ASCII (American Standard Code for 
        /// Information Interchange). For example, uppercase A = 65, 
        /// asterisk (*) = 42, and lowercase k = 107.
        /// 
        /// A modern encryption method is to take a text file, convert 
        /// the bytes to ASCII, then XOR each byte with a given value, 
        /// taken from a secret key. The advantage with the XOR function 
        /// is that using the same encryption key on the cipher text, 
        /// restores the plain text; for example, 65 XOR 42 = 107, 
        /// then 107 XOR 42 = 65.
        /// 
        /// For unbreakable encryption, the key is the same length as 
        /// the plain text message, and the key is made up of random 
        /// bytes. The user would keep the encrypted message and the 
        /// encryption key in different locations, and without both 
        /// "halves", it is impossible to decrypt the message.
        /// 
        /// Unfortunately, this method is impractical for most users, 
        /// so the modified method is to use a password as a key. If 
        /// the password is shorter than the message, which is likely, 
        /// the key is repeated cyclically throughout the message. The 
        /// balance for this method is using a sufficiently long 
        /// password key for security, but short enough to be memorable.
        /// 
        /// Your task has been made easy, as the encryption key consists 
        /// of three lower case characters. Using cipher1.txt (right 
        /// click and 'Save Link/Target As...'), a file containing the 
        /// encrypted ASCII codes, and the knowledge that the plain 
        /// text must contain common English words, decrypt the message 
        /// and find the sum of the ASCII values in the original text.
        /// 
        /// Answer: 107359
        /// </summary>
        public Problem059Generator() { }

        protected override string InternalCalculateSolution()
        {
            #region Letter frequency
            // http://www.math.cornell.edu/~mec/2003-2004/cryptography/subs/frequencies.html
            var letterFrequency = new Dictionary<char, double>();
            letterFrequency.Add('e', 12.02);
            letterFrequency.Add('t', 9.10);
            letterFrequency.Add('a', 8.12);
            letterFrequency.Add('o', 7.68);
            letterFrequency.Add('i', 7.31);
            letterFrequency.Add('n', 6.95);
            letterFrequency.Add('s', 6.28);
            letterFrequency.Add('r', 6.02);
            letterFrequency.Add('h', 5.92);
            letterFrequency.Add('d', 4.32);
            letterFrequency.Add('l', 3.98);
            letterFrequency.Add('u', 2.88);
            letterFrequency.Add('c', 2.71);
            letterFrequency.Add('m', 2.61);
            letterFrequency.Add('f', 2.30);
            letterFrequency.Add('y', 2.11);
            letterFrequency.Add('w', 2.09);
            letterFrequency.Add('g', 2.03);
            letterFrequency.Add('p', 1.82);
            letterFrequency.Add('b', 1.49);
            letterFrequency.Add('v', 1.11);
            letterFrequency.Add('k', 0.69);
            letterFrequency.Add('x', 0.17);
            letterFrequency.Add('q', 0.11);
            letterFrequency.Add('j', 0.10);
            letterFrequency.Add('z', 0.07);
            #endregion Letter frequency


            var msg = File.ReadAllText(@"..\Resources\cipher1.txt")
                .SplitFromCSV()
                .Select(s => int.Parse(s))
                .ToList();

            char[] ans = new char[msg.Count];

            foreach (var key in PotentialKeys.ToList())
            {
                double eCnt = 0;
                double tCnt = 0;
                double aCnt = 0;
                double oCnt = 0;
                double iCnt = 0;
                double letterCount = 0;

                for (int i = 0; i < msg.Count; i++)
                {
                    ans[i] = (char)(msg[i] ^ (int)key[i]);
                    switch (ans[i])
                    {
                        case 'E': case 'e': eCnt++; break;
                        case 'T': case 't': tCnt++; break;
                        case 'A': case 'a': aCnt++; break;
                        case 'O': case 'o': oCnt++; break;
                        case 'I': case 'i': iCnt++; break;
                    }
                    if (char.IsLetter(ans[i]))
                        letterCount++;
                }

                eCnt = eCnt / letterCount * 100.0;
                tCnt = tCnt / letterCount * 100.0;
                aCnt = aCnt / letterCount * 100.0;
                oCnt = oCnt / letterCount * 100.0;
                iCnt = iCnt / letterCount * 100.0;


                if (eCnt > letterFrequency['e'] * 1 &&
                    tCnt > letterFrequency['t'] * 1 &&
                    //aCnt > letterFrequency['a'] * 0.8 &&
                    oCnt > letterFrequency['o'] * 0.9 &&
                    iCnt > letterFrequency['i'] * 1)
                {
                    return ans.Select(c => (int)c).Sum().ToString();
                }
                
            }
            return "";
        }

        IEnumerable<string> PotentialKeys
        {
            get
            {
                for (char i = 'a'; i <= 'z'; i++)
                    for (char j = 'a'; j <= 'z'; j++)
                        for (char k = 'a'; k <= 'z'; k++)
                            //yield return new string(new char[] { i, j, k });
                            yield return String.Concat(Enumerable.Repeat(new string(new char[] { i, j, k }), 401));

            }
        }
    }


}
